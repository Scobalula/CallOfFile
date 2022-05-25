// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using K4os.Compression.LZ4;

namespace CallOfFile
{
    /// <summary>
    /// A class that provides methods to consume tokens from from *_BIN files.
    /// </summary>
    public sealed class BinaryTokenReader : TokenReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTokenReader"/> class.
        /// </summary>
        /// <param name="fileName">The path to the file to read from.</param>
        public BinaryTokenReader(string fileName)
        {
            using var temp = File.OpenRead(fileName);
            InputBuffer = Decompress(temp);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTokenReader"/> class.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        public BinaryTokenReader(Stream stream)
        {
            InputBuffer = Decompress(stream);
        }

        /// <inheritdoc/>
        public override TokenData? RequestNextToken()
        {
            // No tokens left
            if (InputPosition >= InputBuffer.Length)
                return null;

            Align(4);

            if (!Token.TryGetToken(Read<ushort>(), out var token))
                throw new IOException("Unrecognized Token");

            switch(token.DataType)
            {
                case TokenDataType.Comment:
                    {
                        Align(4);
                        return new TokenDataComment(ReadUTF8String(), token);
                    }
                case TokenDataType.Section:
                    {
                        return new TokenData(token);
                    }
                case TokenDataType.BoneInfo:
                    {
                        Align(4);
                        return new TokenDataBoneInfo(
                            Read<int>(),
                            Read<int>(),
                            ReadUTF8String(), token);
                    }
                case TokenDataType.Short:
                    {
                        Align(2);
                        return new TokenDataInt(Read<short>(), token);
                    }
                case TokenDataType.UShortString:
                    {
                        Align(2);
                        return new TokenDataUIntString(
                            Read<ushort>(),
                            ReadUTF8String(), token);
                    }
                case TokenDataType.UShortStringX3:
                    {
                        Align(2);
                        return new TokenDataUIntStringX3(
                            Read<ushort>(),
                            ReadUTF8String(),
                            ReadUTF8String(),
                            ReadUTF8String(), token);
                    }
                case TokenDataType.UShort:
                    {
                        Align(2);
                        return new TokenDataUInt(Read<ushort>(), token);
                    }
                case TokenDataType.Int:
                    {
                        Align(4);
                        return new TokenDataInt(Read<int>(), token);
                    }
                case TokenDataType.UInt:
                    {
                        Align(4);
                        return new TokenDataUInt(Read<uint>(), token);
                    }
                case TokenDataType.Float:
                    {
                        Align(4);
                        return new TokenDataFloat(Read<float>(), token);
                    }
                case TokenDataType.Vector2:
                    {
                        Align(4);
                        return new TokenDataVector2(new(
                            Read<float>(),
                            Read<float>()), token);
                    }
                case TokenDataType.Vector3:
                    {
                        Align(4);
                        return new TokenDataVector3(new(
                            Read<float>(),
                            Read<float>(),
                            Read<float>()), token);
                    }
                case TokenDataType.Vector316Bit:
                    {
                        Align(2);
                        return new TokenDataVector3(new(
                            Read<short>() * (1 / 32767.0f),
                            Read<short>() * (1 / 32767.0f),
                            Read<short>() * (1 / 32767.0f)), token);
                    }
                case TokenDataType.Vector4:
                    {
                        Align(4);
                        return new TokenDataVector4(new(
                            Read<float>(),
                            Read<float>(),
                            Read<float>(),
                            Read<float>()), token);
                    }
                case TokenDataType.Vector48Bit:
                    {
                        Align(4);
                        return new TokenDataVector4(new(
                            Read<byte>() * (1 / 255.0f),
                            Read<byte>() * (1 / 255.0f),
                            Read<byte>() * (1 / 255.0f),
                            Read<byte>() * (1 / 255.0f)), token);
                    }
                case TokenDataType.BoneWeight:
                    {
                        Align(2);
                        return new TokenDataBoneWeight(
                            Read<ushort>(),
                            Read<float>(), token);
                    }
                case TokenDataType.Tri:
                    {
                        return new TokenDataTri(
                            Read<byte>(),
                            Read<byte>(), token);
                    }
                case TokenDataType.UVSet:
                    {
                        var result = new TokenDataUVSet(token);
                        var uvSets = Read<ushort>();
                        for (int i = 0; i < uvSets; i++)
                            result.UVs.Add(new(
                                Read<float>(),
                                Read<float>()));
                        return result;
                    }
            }

            throw new IOException($"Token Data Type: {token.DataType} for Token: {token.Name} @ {InputPosition}");
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing) { }

        #region Internal
        /// <summary>
        /// Gets or Sets the input buffer.
        /// </summary>
        internal byte[] InputBuffer { get; set; }

        /// <summary>
        /// Gets or Sets the current position in the input buffer.
        /// </summary>
        internal int InputPosition { get; set; }

        /// <summary>
        /// Reads data from the underlying buffer.
        /// </summary>
        /// <typeparam name="T">The type to read, this must be a unmanaged type.</typeparam>
        /// <returns>The resulting data read.</returns>
        internal T Read<T>() where T : unmanaged
        {
            Span<byte> buf = stackalloc byte[Unsafe.SizeOf<T>()];
            InputBuffer.AsSpan().Slice(InputPosition, buf.Length).CopyTo(buf);
            InputPosition += buf.Length;
            return MemoryMarshal.Cast<byte, T>(buf)[0];
        }

        /// <summary>
        /// Reads a null terminated UTF-8 string from the underlying buffer.
        /// </summary>
        /// <returns>The resulting string read.</returns>
        internal string ReadUTF8String()
        {
            var output = new StringBuilder(32);

            while (true)
            {
                var c = Read<byte>();
                if (c == 0)
                    break;
                output.Append(Convert.ToChar(c));
            }

            Align(4);

            return output.ToString();
        }

        /// <summary>
        /// Decompresses the binary file into a byte array.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="IOException"></exception>
        internal static byte[] Decompress(Stream stream)
        {
            Span<byte> magicBuf = stackalloc byte[5];
            Span<byte> sizeBuf = stackalloc byte[4];

            if (stream.Read(magicBuf) < magicBuf.Length)
                throw new IOException("Unexpected end of file while reading XBin Magic");
            if (stream.Read(sizeBuf) < sizeBuf.Length)
                throw new IOException("Unexpected end of file while reading XBin Size");

            var compressedSize = stream.Length - stream.Position;
            var size = BitConverter.ToInt32(sizeBuf);

            var compressedBuffer = new byte[compressedSize];
            var decompressedBuffer = new byte[size];

            if (stream.Read(compressedBuffer) < compressedBuffer.Length)
                throw new IOException("Unexpected end of file while reading XBin Data");
            if (LZ4Codec.Decode(compressedBuffer, decompressedBuffer) != size)
                throw new IOException("No more kitten, daddy is not ready.");

            return decompressedBuffer;
        }

        /// <summary>
        /// Aligns the buffer.
        /// </summary>
        /// <param name="alignment">The size to align the reader to.</param>
        internal void Align(int alignment)
        {
            alignment -= 1;
            InputPosition = ~alignment & InputPosition + alignment;
        }
        #endregion
    }
}
