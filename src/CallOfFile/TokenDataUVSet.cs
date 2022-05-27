// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using System.Collections.Generic;
using System.Numerics;

namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataUVSet : TokenData
    {
        /// <summary>
        /// Gets or Sets the UV Layers.
        /// </summary>
        public List<Vector2> UVs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataUVSet"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        public TokenDataUVSet(Token token) : base(token)
        {
            UVs = new List<Vector2>();
        }
    }
}
