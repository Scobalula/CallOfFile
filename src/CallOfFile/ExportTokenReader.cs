// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;

namespace CallOfFile
{
    /// <summary>
    /// A class that provides methods to consume tokens from *_EXPORT files.
    /// </summary>
    public sealed class ExportTokenReader : TokenReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTokenReader"/> class.
        /// </summary>
        /// <param name="fileName">The path to the file to read from.</param>
        public ExportTokenReader(string fileName)
        {
            TokenBuilder = new StringBuilder(256);
            TokenList = new List<string>(8);
            Reader = new StreamReader(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTokenReader"/> class.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        public ExportTokenReader(Stream stream)
        {
            TokenBuilder = new StringBuilder(256);
            TokenList = new List<string>(8);
            Reader = new StreamReader(stream);
        }


        /// <inheritdoc/>
        public override TokenData RequestNextToken()
        {
            while (true)
            {
                ConsumeTokens();

                if (TokenList.Count == 0)
                    break;

                if (!Token.TryGetToken(TokenList[0], TokenList, out var token))
                    throw new IOException("Unrecognized Token");

                switch (token.DataType)
                {
                    case TokenDataType.Comment:
                        {
                            return new TokenDataComment(TokenList.Count == 1 ? string.Empty : TokenList[1], token);
                        }
                    case TokenDataType.Section:
                        {
                            return new TokenData(token);
                        }
                    case TokenDataType.BoneInfo:
                        {
                            return new TokenDataBoneInfo(
                                int.Parse(TokenList[1]),
                                int.Parse(TokenList[2]),
                                TokenList[3], token);
                        }
                    case TokenDataType.UShortString:
                        {
                            return new TokenDataUIntString(
                                ushort.Parse(TokenList[1]),
                                TokenList[2], token);
                        }
                    case TokenDataType.UShortStringX3:
                        {
                            return new TokenDataUIntStringX3(
                                ushort.Parse(TokenList[1]),
                                TokenList[2],
                                TokenList[3],
                                TokenList[4], token);
                        }
                    case TokenDataType.UShort:
                        {
                            return new TokenDataUInt(
                                ushort.Parse(TokenList[1]), token);
                        }
                    case TokenDataType.Int:
                        {
                            return new TokenDataInt(
                                int.Parse(TokenList[1]), token);
                        }
                    case TokenDataType.UInt:
                        {
                            return new TokenDataUInt(
                                uint.Parse(TokenList[1]), token);
                        }
                    case TokenDataType.Float:
                        {
                            return new TokenDataFloat(
                                float.Parse(TokenList[1]), token);
                        }
                    case TokenDataType.Vector2:
                        {
                            return new TokenDataVector2(new Vector2(
                                float.Parse(TokenList[1]),
                                float.Parse(TokenList[2])), token);
                        }
                    case TokenDataType.Vector3:
                    case TokenDataType.Vector316Bit:
                        {
                            return new TokenDataVector3(new Vector3(
                                float.Parse(TokenList[1]),
                                float.Parse(TokenList[2]),
                                float.Parse(TokenList[3])), token);
                        }
                    case TokenDataType.Vector4:
                    case TokenDataType.Vector48Bit:
                        {
                            return new TokenDataVector4(new Vector4(
                                float.Parse(TokenList[1]),
                                float.Parse(TokenList[2]),
                                float.Parse(TokenList[3]),
                                float.Parse(TokenList[4])), token);
                        }
                    case TokenDataType.BoneWeight:
                        {
                            return new TokenDataBoneWeight(
                                ushort.Parse(TokenList[1]),
                                float.Parse(TokenList[1]), token);
                        }
                    case TokenDataType.Tri:
                    case TokenDataType.Tri16:
                        {
                            return new TokenDataTri(
                                int.Parse(TokenList[1]),
                                int.Parse(TokenList[2]), token);
                        }
                    case TokenDataType.UVSet:
                        {
                            var result = new TokenDataUVSet(token);
                            var uvSets = ushort.Parse(TokenList[1]);
                            // exportx patch, 0 UV sets reported.....
                            if (uvSets == 0)
                                uvSets = 1;
                            for (int i = 0; i < uvSets; i++)
                                result.UVs.Add(new Vector2(
                                    float.Parse(TokenList[2 + (i * 2) + 0]),
                                    float.Parse(TokenList[2 + (i * 2) + 1])));
                            return result;
                        }
                }

                throw new IOException($"Token Data Type: {token.DataType} for Token: {token.Name} @ {Reader.BaseStream.Position}");
            }

            return null;
        }
    
        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TokenList.Clear();
                TokenBuilder.Clear();
                Reader.Dispose();
            }
        }

        #region Internal
        /// <summary>
        /// Gets the Reader
        /// </summary>
        internal StreamReader Reader { get; set; }

        /// <summary>
        /// Gets or Sets the token builder
        /// </summary>
        internal StringBuilder TokenBuilder { get; set; }

        /// <summary>
        /// Gets or Sets the current token list
        /// </summary>
        internal List<string> TokenList { get; set; }

        /// <summary>
        /// Consumes tokens from the underlying reader.
        /// </summary>
        /// <exception cref="IOException">Thrown if EOF or EOL is hit while reading a string literal.</exception>
        internal void ConsumeTokens()
        {
            TokenList.Clear();
            int c;

            while (true)
            {
                c = Reader.Read();

                while (true)
                {
                    if (c == -1 || c != ',' && !char.IsWhiteSpace((char)c) && c != '\n' && c != '\r')
                        break;

                    c = Reader.Read();
                }

                TokenBuilder.Clear();

                if (c == -1)
                    break;

                if (c == '"')
                {
                    while (true)
                    {
                        c = Reader.Read();

                        if (c == '"')
                            break;
                        if (c == -1)
                            throw new IOException("Unexpected EOF while parsing string literal in export file.");
                        if (c == '\n' || c == '\r')
                            throw new IOException("Unexpected EOL while parsing string literal in export file.");

                        TokenBuilder.Append((char)c);
                    }

                    c = Reader.Peek();
                }
                // For comments we must read until the end of the line.
                else if (c == '/' && Reader.Peek() == '/')
                {
                    TokenBuilder.Append((char)c);
                    TokenBuilder.Append((char)Reader.Read());
                    TokenList.Add(TokenBuilder.ToString());
                    TokenBuilder.Clear();

                    while (true)
                    {
                        if (c == '\n' || c == '\r' || c == -1)
                            break;

                        TokenBuilder.Append((char)c);
                        c = Reader.Read();
                    }
                }
                else
                {
                    while (true)
                    {
                        if (c == -1 || c == ',' || char.IsWhiteSpace((char)c) || c == '\n' || c == '\r')
                            break;

                        TokenBuilder.Append((char)c);
                        c = Reader.Read();
                    }
                }


                // TODO: It's technically acceptable to the Mod Tools
                // to accept files with tokens on mutliple lines, so I 
                // reckon me might move this to a stack and pop off while
                // we have data.
                // However I have yet to see files like that, so this works for
                // now.
                TokenList.Add(TokenBuilder.ToString());


                if (c == '\n' || c == '\r' || c == -1)
                    break;
            }
        }
        #endregion
    }
}
