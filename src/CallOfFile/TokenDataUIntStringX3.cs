// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataUIntStringX3 : TokenData
    {
        /// <summary>
        /// Gets or Sets the integer value.
        /// </summary>
        public int IntegerValue { get; set; }

        /// <summary>
        /// Gets or Sets the first string value.
        /// </summary>
        public string StringValue1 { get; set; }

        /// <summary>
        /// Gets or Sets the second string value.
        /// </summary>
        public string StringValue2 { get; set; }

        /// <summary>
        /// Gets or Sets the third string value.
        /// </summary>
        public string StringValue3 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataUIntStringX3"/> class
        /// </summary>
        /// <param name="intVal">The integer value.</param>
        /// <param name="strVal1">The first string value.</param>
        /// <param name="strVal2">The second string value.</param>
        /// <param name="strVal3">The third string value.</param>
        /// <param name="token">The token.</param>
        public TokenDataUIntStringX3(int intVal, string strVal1, string strVal2, string strVal3, Token token) : base(token)
        {
            IntegerValue = intVal;
            StringValue1 = strVal1;
            StringValue2 = strVal2;
            StringValue3 = strVal3;
        }
    }
}
