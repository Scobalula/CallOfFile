// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------

namespace CallOfFile
{
    /// <summary>
    /// A class that holds a blank token.
    /// </summary>
    public class TokenData
    {
        /// <summary>
        /// Gets the token.
        /// </summary>
        public Token Token { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenData"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public TokenData(Token token)
        {
            Token = token;
        }

        /// <inheritdoc/>
        public override string ToString() => Token.Name;
    }
}
