// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------

namespace CallOfFile
{
    /// <summary>
    /// A class that holds a token with the provided data types.
    /// </summary>
    public class TokenDataBoneInfo : TokenData
    {
        /// <summary>
        /// Gets or Sets the name of the bone.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets the index.
        /// </summary>
        public int BoneIndex { get; set; }

        /// <summary>
        /// Gets or Sets the parent index.
        /// </summary>
        public int BoneParentIndex { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDataBoneInfo"/> class.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <param name="parentIndex">Parent index.</param>
        /// <param name="name">Bone name.</param>
        /// <param name="token">The token.</param>
        public TokenDataBoneInfo(int index, int parentIndex, string name, Token token) : base(token)
        {
            BoneIndex = index;
            BoneParentIndex = parentIndex;
            Name = name;
        }
    }
}
