// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using System.Numerics;

namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataVector4 : TokenData
    {
        /// <summary>
        /// Gets or Sets the value.
        /// </summary>
        public Vector4 Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataVector4"/> class.
        /// </summary>
        /// <param name="value">The value stored.</param>
        /// <param name="token">The token.</param>
        public TokenDataVector4(Vector4 value, Token token) : base(token)
        {
            Value = value;
        }
    }
}
