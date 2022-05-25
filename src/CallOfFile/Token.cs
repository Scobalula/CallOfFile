// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace CallOfFile
{
    /// <summary>
    /// A class to hold a token
    /// </summary>
    public record Token
    {
        /// <summary>
        /// Gets the name of the block
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the data type
        /// </summary>
        public TokenDataType DataType { get; private set; }

        /// <summary>
        /// Gets the block hash (CRC16 with data type as init)
        /// </summary>
        public uint Hash { get; private set; }

        #region Internal
        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="name">The name of the token.</param>
        /// <param name="dataType">The data type for this token.</param>
        /// <param name="hash">The CRC/Hash.</param>
        internal Token(string name, TokenDataType dataType, uint hash)
        {
            Name     = name;
            DataType = dataType;
            Hash     = hash;
        }

        /// <summary>
        /// Gets supported tokens
        /// </summary>
        internal static readonly Token[] Tokens =
        {
            new(";",                TokenDataType.Comment,          0x8738),
            new("//",               TokenDataType.Comment,          0xC355),
            new("AMBIENTCOLOR",     TokenDataType.Vector4,          0x37FF),
            new("ANIMATION",        TokenDataType.Section,          0x7AAC),
            new("BLINN",            TokenDataType.Vector2,          0x83C7),
            new("BONE",             TokenDataType.UShort,           0xDD9A),
            new("BONE",             TokenDataType.BoneWeight,       0xF1AB),
            new("BONE",             TokenDataType.BoneInfo,         0xF099),
            new("BONES",            TokenDataType.UShort,           0xEA46),
            new("COEFFS",           TokenDataType.Vector2,          0xC835),
            new("COLOR",            TokenDataType.Vector48Bit,      0x6DD8),
            new("FIRSTFRAME",       TokenDataType.UShort,           0xBCD4),
            new("FRAME",            TokenDataType.UInt,             0xC723),
            new("FRAME",            TokenDataType.Unk4,             0x1675),
            new("FRAMERATE",        TokenDataType.UShort,           0x92D3),
            new("GLOW",             TokenDataType.Vector2,          0xFE0C),
            new("INCANDESCENCE",    TokenDataType.Vector4,          0x4265),
            new("MATERIAL",         TokenDataType.UShortStringX3,   0xA700),
            new("MODEL",            TokenDataType.Section,          0x46C8),
            new("NORMAL",           TokenDataType.Vector316Bit,     0x89EC),
            new("NOTETRACK",        TokenDataType.UShort,           0x4643),
            new("NOTETRACKS",       TokenDataType.Section,          0xC7F3),
            new("NUMBONES",         TokenDataType.UShort,           0x76BA),
            new("NUMFACES",         TokenDataType.UInt,             0xBE92),
            new("NUMFRAMES",        TokenDataType.UInt,             0xB917),
            new("NUMKEYS",          TokenDataType.UShort,           0x7A6C),
            new("NUMMATERIALS",     TokenDataType.UShort,           0xA1B2),
            new("NUMOBJECTS",       TokenDataType.UShort,           0x62AF),
            new("NUMPARTS",         TokenDataType.UShort,           0x9279),
            new("NUMTRACKS",        TokenDataType.UShort,           0x9016),
            new("NUMVERTS",         TokenDataType.UShort,           0x950D),
            new("NUMVERTS32",       TokenDataType.UInt,             0x2AEC),
            new("OBJECT",           TokenDataType.UShortString,     0x87D4),
            new("OFFSET",           TokenDataType.Vector3,          0x9383),
            new("PART",             TokenDataType.UShort,           0x745A),
            new("PART",             TokenDataType.UShortString,     0x360B),
            new("PHONG",            TokenDataType.Float,            0x5CD2),
            new("REFLECTIVE",       TokenDataType.Vector2,          0x7D76),
            new("REFLECTIVECOLOR",  TokenDataType.Vector4,          0xE593),
            new("REFRACTIVE",       TokenDataType.Vector2,          0x7E24),
            new("SCALE",            TokenDataType.Vector3,          0x1C56),
            new("SPECULARCOLOR",    TokenDataType.Vector4,          0x317C),
            new("TRANSPARENCY",     TokenDataType.Vector4,          0x6DAB),
            new("TRI",              TokenDataType.Tri,              0x562F),
            new("TRI16",            TokenDataType.Tri16,            0x6711),
            new("UV",               TokenDataType.UVSet,            0x1AD4),
            new("VERSION",          TokenDataType.UShort,           0x24D1),
            new("VERT",             TokenDataType.UShort,           0x8F03),
            new("VERT32",           TokenDataType.UInt,             0xB097),
            new("X",                TokenDataType.Vector316Bit,     0xDCFD),
            new("Y",                TokenDataType.Vector316Bit,     0xCCDC),
            new("Z",                TokenDataType.Vector316Bit,     0xFCBF),
            new("NUMSBONES",        TokenDataType.Int,              0x1FC2),
            new("NUMSWEIGHTS",      TokenDataType.Int,              0xB35E),
            new("QUATERNION",       TokenDataType.Vector4,          0xEF69),
            new("NUMIKPITCHLAYERS", TokenDataType.Int,              0xA65B),
            new("IKPITCHLAYER",     TokenDataType.UInt,             0x1D7D),
            new("ROTATION",         TokenDataType.Vector3,          0xA58B),
            new("NUMCOSMETICBONES", TokenDataType.Int,              0x7836),
            new("EXTRA",            TokenDataType.Vector4,          0x6EEE),
        };

        /// <summary>
        /// Calculates a CRC-16 value for the binary token hash.
        /// </summary>
        /// <param name="name">Token name.</param>
        /// <param name="dataType">Token data type.</param>
        /// <returns>Resulting CRC-16.</returns>
        internal static ushort CalculateTokenHash(string name, int dataType)
        {
            var result = dataType;

            foreach (var c in name)
            {
                result = (c << 8) ^ result;

                for (int i = 0; i < 8; i++)
                {
                    if ((result & 0x8000) != 0)
                        result = result << 1 ^ 0x1021;
                    else
                        result <<= 1;
                }
            }

            return (ushort)result;
        }

        /// <summary>
        /// Attempts to get a token matching the provided input from the list.
        /// </summary>
        /// <param name="hash">The token hash.</param>
        /// <param name="result">The resulting token if found.</param>
        /// <returns>True if a matching token is found, otherwise false.</returns>
        internal static bool TryGetToken(uint hash,[NotNullWhen(true)] out Token? result)
        {
            // Could move to hash table, but testing showed very little
            // benefit in speed due to the how small our token table is.
            foreach (var token in Tokens)
            {
                if (token.Hash == hash)
                {
                    result = token;
                    return true;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Attempts to get a token matching the provided input from the list.
        /// </summary>
        /// <param name="name">The token name.</param>
        /// <param name="tokens">The token values.</param>
        /// <param name="result">The resulting token if found.</param>
        /// <returns>True if a matching token is found, otherwise false.</returns>
        internal static bool TryGetToken(string name, List<string> tokens, [NotNullWhen(true)] out Token? result)
        {
            // Could move to hash table, but testing showed very little
            // benefit in speed due to the how small our token table is.
            foreach (var token in Token.Tokens)
            {
                if (token.Name == name)
                {
                    // This is sort of how export2bin handles it from what I can see
                    // only I think it uses index of token/type and keeps a sorted list
                    // of tokens indexing to the next, but this does the job too
                    switch (token.Hash)
                    {
                        // Bone Info
                        case 0xF099:
                            if (tokens.Count != 4)
                                continue;
                            break;
                        // Bone Weights
                        case 0xF1AB:
                            if (tokens.Count != 3)
                                continue;
                            break;
                        // Bone Index
                        case 0xDD9A:
                            if (tokens.Count != 2)
                                continue;
                            break;
                    }

                    result = token;
                    return true;
                }
            }

            result = default;
            return false;
        }
        #endregion
    }
}
