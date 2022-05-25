// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------

namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataFloat : TokenData
    {
        /// <summary>
        /// Gets or Sets the value stored.
        /// </summary>
        public float Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataFloat"/> class.
        /// </summary>
        /// <param name="data">The value stored.</param>
        /// <param name="token">The token.</param>
        public TokenDataFloat(float data, Token token) : base(token)
        {
            Value = data;
        }
    }
}
