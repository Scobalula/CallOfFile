// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CallOfFile
{
    /// <summary>
    /// A class that defines methods to handle consuming tokens from Call of Duty *_EXPORT/*_BIN files.
    /// </summary>
    public abstract class TokenReader : IDisposable
    {
        /// <summary>
        /// Requests the next token from the stream
        /// </summary>
        /// <returns></returns>
        public abstract TokenData RequestNextToken();

        /// <summary>
        /// Requests the next token from the stream
        /// </summary>
        /// <returns></returns>
        public T RequestNextTokenOfType<T>(string name) where T : TokenData
        {
            while (true)
            {
                var nextToken = RequestNextToken();

                if (nextToken == null)
                    throw new EndOfStreamException();

                if (nextToken.Token.Name == name && nextToken is T expectedToken)
                    return expectedToken;
                if (nextToken.Token.Name == "//")
                    continue;

                throw new IOException($"Expected token {name} of type: {typeof(T)} but got {nextToken.Token.Name} of type {nextToken.GetType()}");
            }
        }

        /// <summary>
        /// Requests the next token from the stream
        /// </summary>
        /// <returns></returns>
        public T RequestNextTokenOfType<T>(IEnumerable<string> names) where T : TokenData
        {
            while (true)
            {
                var nextToken = RequestNextToken();

                if (nextToken == null)
                    throw new EndOfStreamException();
                if (nextToken.Token.Name == "//")
                    continue;
                if (names.Contains(nextToken.Token.Name) && nextToken is T expectedToken)
                    return expectedToken;

                throw new IOException($"Expected token {names} of type: {typeof(T)} but got {nextToken?.Token.Name} of type {nextToken?.GetType()}");
            }
        }

        /// <summary>
        /// Requests the next token from the stream
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TokenData> ReadTokens()
        {
            TokenData token;

            while((token = RequestNextToken()) != null)
            {
                yield return token;
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Handles disposing of the <see cref="TokenReader"/> and finalizes its reading.
        /// </summary>
        /// <param name="disposing">Whether this is called from a Dispose method.</param>
        protected abstract void Dispose(bool disposing);

        /// <summary>
        /// Creates a matching <see cref="TokenReader"/> for the provided file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Resulting token reader, if the provided file isn't supported, null is returned.</returns>
        public static TokenReader CreateReader(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;
            if (!File.Exists(fileName))
                return null;

            var ext = Path.GetExtension(fileName);

            if (ext.Equals(".xmodel_bin", StringComparison.CurrentCultureIgnoreCase) ||
                ext.Equals(".xanim_bin", StringComparison.CurrentCultureIgnoreCase))
            {
                return new BinaryTokenReader(fileName);
            }
            else if (ext.Equals(".xmodel_export", StringComparison.CurrentCultureIgnoreCase) ||
                     ext.Equals(".xanim_export", StringComparison.CurrentCultureIgnoreCase))
            {
                return new ExportTokenReader(fileName);
            }

            return null;
        }

        /// <summary>
        /// Creates a matching <see cref="TokenReader"/> for the provided file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="reader">Resulting reader.</param>
        /// <returns>True if a reader was successfully created, otherwise false.</returns>
        public static bool TryCreateReader(string fileName, out TokenReader reader) => (reader = CreateReader(fileName)) != null;
    }
}
