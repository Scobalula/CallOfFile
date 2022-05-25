// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------

namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataComment : TokenData
    {
        /// <summary>
        /// Gets or Sets the comment value.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataComment"/> class.
        /// </summary>
        /// <param name="comment">The comment value.</param>
        /// <param name="token">The token.</param>
        public TokenDataComment(string comment, Token token) : base(token)
        {
            Comment = comment;
        }
    }
}
