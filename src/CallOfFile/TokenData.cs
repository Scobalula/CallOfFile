// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Casts this <see cref="TokenData"/> to the provided type with strict checks.
        /// </summary>
        /// <typeparam name="T">The type to cast this to.</typeparam>
        /// <param name="name">The name of the token we expect.</param>
        /// <returns>Casted token data.</returns>
        /// <exception cref="Exception">Thrown if this does not match the type or name.</exception>
        public T Cast<T>(string name) where T : TokenData
        {
            if (Token.Name == name && this is T t)
                return t;

            throw new Exception($"Expected token {name} of type: {typeof(T)} but got {Token.Name} of type {Token.GetType()}");
        }

        /// <summary>
        /// Casts this <see cref="TokenData"/> to the provided type with strict checks.
        /// </summary>
        /// <typeparam name="T">The type to cast this to.</typeparam>
        /// <param name="names">The names of the token we expect.</param>
        /// <returns>Casted token data.</returns>
        /// <exception cref="Exception">Thrown if this does not match the type or name.</exception>
        public T Cast<T>(IEnumerable<string> names) where T : TokenData
        {
            if (names.Contains(Token.Name) && this is T t)
                return t;

            throw new Exception($"Expected token {string.Join(" or ", names)} of type: {typeof(T)} but got {Token.Name} of type {Token.GetType()}");
        }

        /// <inheritdoc/>
        public override string ToString() => Token.Name;
    }
}
