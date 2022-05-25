// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------

namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataTri : TokenData
    {
        /// <summary>
        /// Gets or Sets the the index of the object this tri belongs to.
        /// </summary>
        public int ObjectIndex { get; set; }

        /// <summary>
        /// Gets or Sets the index of the material assigned to this tri.
        /// </summary>
        public int MaterialIndex { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataTri"/> class
        /// </summary>
        /// <param name="objIndex">The index of the object this tri belongs to.</param>
        /// <param name="matIndex">The index of the material assigned to this tri.</param>
        public TokenDataTri(int objIndex, int matIndex, Token token) : base(token)
        {
            ObjectIndex = objIndex;
            MaterialIndex = matIndex;
        }
    }
}
