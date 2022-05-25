// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using System.Numerics;

namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with a <see cref="Vector2"/> value.
    /// </summary>
    public class TokenDataVector2 : TokenData
    {
        /// <summary>
        /// Gets or Sets the value.
        /// </summary>
        public Vector2 Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataVector2"/> class.
        /// </summary>
        /// <param name="value">The value this token holds.</param>
        /// <param name="token">The token.</param>
        public TokenDataVector2(Vector2 value, Token token) : base(token)
        {
            Value = value;
        }
    }
}
