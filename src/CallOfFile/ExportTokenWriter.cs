// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace CallOfFile
{
    /// <summary>
    /// A class that provides methods to write tokens to *_EXPORT files.
    /// </summary>
    public class ExportTokenWriter : TokenWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTokenWriter"/> class.
        /// </summary>
        /// <param name="fileName">File to write to.</param>
        public ExportTokenWriter(string fileName)
        {
            Writer = new StreamWriter(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExportTokenWriter"/> class.
        /// </summary>
        /// <param name="stream">Stream to write to.</param>
        public ExportTokenWriter(Stream stream)
        {
            Writer = new StreamWriter(stream);
        }

        /// <inheritdoc/>
        public override void WriteSection(string name, uint hash)
        {
            Writer.WriteLine($"{name}");
        }

        /// <inheritdoc/>
        public override void WriteComment(string name, uint hash, string value)
        {
            Writer.WriteLine($"{name} {value}");
        }

        /// <inheritdoc/>
        public override void WriteBoneInfo(string name, uint hash, int boneIndex, int parentIndex, string boneName)
        {
            Writer.WriteLine($"{name} {boneIndex} {parentIndex} \"{boneName}\"");
        }

        /// <inheritdoc/>
        public override void WriteFloat(string name, uint hash, float value)
        {
            Writer.WriteLine($"{name} {value}");
        }

        /// <inheritdoc/>
        public override void WriteInt(string name, uint hash, int value)
        {
            Writer.WriteLine($"{name} {value}");
        }

        /// <inheritdoc/>
        public override void WriteUInt(string name, uint hash, uint value)
        {
            Writer.WriteLine($"{name} {value}");
        }

        /// <inheritdoc/>
        public override void WriteShort(string name, uint hash, short value)
        {
            Writer.WriteLine($"{name} {value}");
        }

        /// <inheritdoc/>
        public override void WriteUShort(string name, uint hash, ushort value)
        {
            Writer.WriteLine($"{name} {value}");
        }

        /// <inheritdoc/>
        public override void WriteVector2(string name, uint hash, Vector2 value)
        {
            Writer.WriteLine($"{name} {value.X} {value.Y}");
        }

        /// <inheritdoc/>
        public override void WriteVector3(string name, uint hash, Vector3 value)
        {
            Writer.WriteLine($"{name} {value.X} {value.Y} {value.Z}");
        }

        /// <inheritdoc/>
        public override void WriteVector316Bit(string name, uint hash, Vector3 value)
        {
            Writer.WriteLine($"{name} {value.X} {value.Y} {value.Z}");
        }

        /// <inheritdoc/>
        public override void WriteVector4(string name, uint hash, Vector4 value)
        {
            Writer.WriteLine($"{name} {value.X} {value.Y} {value.Z} {value.W}");
        }

        /// <inheritdoc/>
        public override void WriteVector48Bit(string name, uint hash, Vector4 value)
        {
            Writer.WriteLine($"{name} {value.X} {value.Y} {value.Z} {value.W}");
        }

        /// <inheritdoc/>
        public override void WriteBoneWeight(string name, uint hash, int boneIndex, float boneWeight)
        {
            Writer.WriteLine($"{name} {boneIndex} {boneWeight}");
        }

        /// <inheritdoc/>
        public override void WriteTri(string name, uint hash, int objectIndex, int materialIndex)
        {
            Writer.WriteLine($"{name} {objectIndex} {materialIndex} 0 0");
        }

        /// <inheritdoc/>
        public override void WriteTri16(string name, uint hash, int objectIndex, int materialIndex)
        {
            Writer.WriteLine($"{name} {objectIndex} {materialIndex} 0 0");
        }

        /// <inheritdoc/>
        public override void WriteUVSet(string name, uint hash, Vector2 value)
        {
            Writer.WriteLine($"{name} 1 {value.X} {value.Y}");
        }

        /// <inheritdoc/>
        public override void WriteUVSet(string name, uint hash, int count, IEnumerable<Vector2> values)
        {
            Writer.Write($"{name} {count}");

            foreach (var value in values)
            {
                Writer.Write($" {value.X} {value.Y}");
            }

            Writer.WriteLine();
        }

        /// <inheritdoc/>
        public override void WriteUShortString(string name, uint hash, ushort intVal, string strVal)
        {
            Writer.WriteLine($"{name} {intVal} \"{strVal}\"");
        }

        /// <inheritdoc/>
        public override void WriteUShortStringX3(string name, uint hash, ushort intVal, string strVal1, string strVal2, string strVal3)
        {
            Writer.WriteLine($"{name} {intVal} \"{strVal1}\" \"{strVal2}\" \"{strVal3}\"");
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                Writer.Dispose();
            }
        }

        #region Internal
        /// <summary>
        /// Gets or Sets the Writer
        /// </summary>
        internal StreamWriter Writer { get; set; }
        #endregion
    }
}
