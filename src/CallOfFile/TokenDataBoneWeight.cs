// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------

namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataBoneWeight : TokenData
    {
        /// <summary>
        /// Gets or Sets the index.
        /// </summary>
        public int BoneIndex { get; set; }

        /// <summary>
        /// Gets or Sets the weight.
        /// </summary>
        public float BoneWeight { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataBoneWeight"/> class
        /// </summary>
        /// <param name="index">The index of the bone within the bone list.</param>
        /// <param name="weight">The weight assigned to the bone.</param>
        public TokenDataBoneWeight(int index, float weight, Token token) : base(token)
        {
            BoneIndex = index;
            BoneWeight = weight;
        }
    }
}
