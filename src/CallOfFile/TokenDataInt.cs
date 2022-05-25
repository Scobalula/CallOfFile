// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------

namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataInt : TokenData
    {
        /// <summary>
        /// Gets or Sets the value.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataInt"/> class.
        /// </summary>
        /// <param name="value">The value this token holds.</param>
        /// <param name="token">The token.</param>
        public TokenDataInt(int value, Token token) : base(token)
        {
            Value = value;
        }
    }
}
