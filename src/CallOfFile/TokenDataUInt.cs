// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataUInt : TokenData
    {
        /// <summary>
        /// Gets or Sets the value.
        /// </summary>
        public uint Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataUInt"/> class
        /// </summary>
        /// <param name="value">The value stored.</param>
        /// <param name="token">The token.</param>
        public TokenDataUInt(uint value, Token token) : base(token)
        {
            Value = value;
        }
    }
}
