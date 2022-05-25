// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------

namespace CallOfFile
{
    /// <summary>
    /// An enum that defines supported token data types.
    /// </summary>
    public enum TokenDataType
    {
        /// <summary>
        /// A comment token.
        /// </summary>
        Comment           = 00,

        /// <summary>
        /// A token that defines a section of the file.
        /// </summary>
        Section           = 01,

        /// <summary>
        /// A token that holds a signed short.
        /// </summary>
        Short             = 02,

        /// <summary>
        /// A token that holds an unsigned short.
        /// </summary>
        UShort            = 03,

        /// <summary>
        /// A token that holds an unsigned integer.
        /// </summary>
        UInt              = 04,

        /// <summary>
        /// A token that holds a unigned integer.
        /// </summary>
        Int               = 05,

        /// <summary>
        /// A token that holds an 8Bit 4D vector.
        /// </summary>
        Vector48Bit       = 06,

        /// <summary>
        /// A token that holds an 16Bit Normalized 3D vector.
        /// </summary>
        Vector316Bit      = 07,

        /// <summary>
        /// A token that holds a single precision float.
        /// </summary>
        Float             = 08,

        /// <summary>
        /// A token that holds an 2D Vector.
        /// </summary>
        Vector2           = 09,

        /// <summary>
        /// A token that holds an 3D Vector.
        /// </summary>
        Vector3           = 10,

        /// <summary>
        /// A token that holds an 4D Vector.
        /// </summary>
        Vector4           = 11,

        /// <summary>
        /// A token that holds a bone weight.
        /// </summary>
        BoneWeight        = 12,

        /// <summary>
        /// A token that holds a UV set.
        /// </summary>
        UVSet             = 13,

        /// <summary>
        /// A token that holds an unsigned short and string.
        /// </summary>
        UShortString      = 14,

        /// <summary>
        /// A token that holds an unsigned short and 3 strings.
        /// </summary>
        UShortStringX3    = 15,

        /// <summary>
        /// Unknown.
        /// </summary>
        Unk4              = 16,

        /// <summary>
        /// A token that holds a bone info definition.
        /// </summary>
        BoneInfo          = 17,

        /// <summary>
        /// A token that holds triangle information.
        /// </summary>
        Tri               = 18,

        /// <summary>
        /// A token that holds triangle information.
        /// </summary>
        Tri16             = 19,

        /// <summary>
        /// Unknown.
        /// </summary>
        Unk9              = 20,
    }
}
