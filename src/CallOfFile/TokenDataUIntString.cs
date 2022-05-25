// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataUIntString : TokenData
    {
        /// <summary>
        /// Gets or Sets the integer value.
        /// </summary>
        public uint IntegerValue { get; set; }

        /// <summary>
        /// Gets or Sets the string value.
        /// </summary>
        public string StringValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataUIntString"/> class
        /// </summary>
        /// <param name="intVal">The integer value.</param>
        /// <param name="strVal">The string value.</param>
        /// <param name="token">The token.</param>
        public TokenDataUIntString(uint intVal, string strVal, Token token) : base(token)
        {
            IntegerValue = intVal;
            StringValue = strVal;
        }
    }
}
