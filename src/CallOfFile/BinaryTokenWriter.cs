// -----------------------------------------------
// Call of File - By Philip Maher
// Refer to LICENSE.md for license information.
// -----------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using K4os.Compression.LZ4;

namespace CallOfFile
{
    /// <summary>
    /// A class that provides methods to write tokens to *_BIN files.
    /// </summary>
    public sealed class BinaryTokenWriter : TokenWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTokenWriter"/> class.
        /// </summary>
        /// <param name="fileName">File to write to.</param>
        public BinaryTokenWriter(string fileName)
        {
            OutputBuffer = new byte[65535];
            Output = File.Create(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTokenWriter"/> class.
        /// </summary>
        /// <param name="stream">Stream to write to.</param>
        public BinaryTokenWriter(Stream stream)
        {
            OutputBuffer = new byte[65535];
            Output = stream;
        }

        /// <inheritdoc/>
        public override void WriteSection(string name, uint hash)
        {
            WriteHash((ushort)hash);
        }

        /// <inheritdoc/>
        public override void WriteComment(string name, uint hash, string value)
        {
            WriteHash((ushort)hash);
            WriteString(value);
        }

        /// <inheritdoc/>
        public override void WriteBoneInfo(string name, uint hash, int boneIndex, int parentIndex, string boneName)
        {
            WriteHash((ushort)hash);
            Align(4);
            Write(boneIndex);
            Write(parentIndex);
            WriteString(boneName);
        }

        /// <inheritdoc/>
        public override void WriteFloat(string name, uint hash, float value)
        {
            WriteHash((ushort)hash);
            Align(4);
            Write(value);
        }

        /// <inheritdoc/>
        public override void WriteInt(string name, uint hash, int value)
        {
            WriteHash((ushort)hash);
            Align(4);
            Write(value);
        }

        /// <inheritdoc/>
        public override void WriteUInt(string name, uint hash, uint value)
        {
            WriteHash((ushort)hash);
            Align(4);
            Write(value);
        }

        /// <inheritdoc/>
        public override void WriteShort(string name, uint hash, short value)
        {
            WriteHash((ushort)hash);
            Align(2);
            Write(value);
        }

        /// <inheritdoc/>
        public override void WriteUShort(string name, uint hash, ushort value)
        {
            WriteHash((ushort)hash);
            Align(2);
            Write(value);
        }

        /// <inheritdoc/>
        public override void WriteVector2(string name, uint hash, Vector2 value)
        {
            WriteHash((ushort)hash);
            Align(4);
            Write(value.X);
            Write(value.Y);
        }

        /// <inheritdoc/>
        public override void WriteVector3(string name, uint hash, Vector3 value)
        {
            WriteHash((ushort)hash);
            Align(4);
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
        }

        /// <inheritdoc/>
        public override void WriteVector316Bit(string name, uint hash, Vector3 value)
        {
            WriteHash((ushort)hash);
            Align(2);
            Write((ushort)(value.X * 32767.0f));
            Write((ushort)(value.Y * 32767.0f));
            Write((ushort)(value.Z * 32767.0f));
        }

        /// <inheritdoc/>
        public override void WriteVector4(string name, uint hash, Vector4 value)
        {
            WriteHash((ushort)hash);
            Align(4);
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
            Write(value.W);
        }

        /// <inheritdoc/>
        public override void WriteVector48Bit(string name, uint hash, Vector4 value)
        {
            WriteHash((ushort)hash);
            Align(4);
            Write((byte)(value.X * 255.0f));
            Write((byte)(value.Y * 255.0f));
            Write((byte)(value.Z * 255.0f));
            Write((byte)(value.W * 255.0f));
        }

        /// <inheritdoc/>
        public override void WriteBoneWeight(string name, uint hash, int boneIndex, float boneWeight)
        {
            WriteHash((ushort)hash);
            Write((ushort)boneIndex);
            Write(boneWeight);
        }

        /// <inheritdoc/>
        public override void WriteTri(string name, uint hash, int objectIndex, int materialIndex)
        {
            WriteHash((ushort)hash);
            Write((byte)objectIndex);
            Write((byte)materialIndex);
        }

        /// <inheritdoc/>
        public override void WriteTri16(string name, uint hash, int objectIndex, int materialIndex)
        {
            WriteHash((ushort)hash);
            Write((ushort)objectIndex);
            Write((ushort)materialIndex);
        }

        /// <inheritdoc/>
        public override void WriteUVSet(string name, uint hash, Vector2 value)
        {
            WriteHash((ushort)hash);
            Write((ushort)1);
            Write(value.X);
            Write(value.Y);
        }

        /// <inheritdoc/>
        public override void WriteUVSet(string name, uint hash, int count, IEnumerable<Vector2> values)
        {
            WriteHash((ushort)hash);
            Write((ushort)count);


            foreach (var value in values)
            {
                Write(value.X);
                Write(value.Y);
            }
        }

        /// <inheritdoc/>
        public override void WriteUShortString(string name, uint hash, ushort intVal, string strVal)
        {
            WriteHash((ushort)hash);
            Write(intVal);
            WriteString(strVal);
        }

        /// <inheritdoc/>
        public override void WriteUShortStringX3(string name, uint hash, ushort intVal, string strVal1, string strVal2, string strVal3)
        {
            WriteHash((ushort)hash);
            Write(intVal);
            WriteString(strVal1);
            WriteString(strVal2);
            WriteString(strVal3);
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    // Dump our final buffer.
                    var result = new byte[LZ4Codec.MaximumOutputSize(CurrentOutputPosition)];
                    var resultSize = LZ4Codec.Encode(OutputBuffer, 0, CurrentOutputPosition, result, 0, result.Length);
                    Output.Write(LZ4Magic, 0, LZ4Magic.Length);
                    Output.Write(BitConverter.GetBytes(CurrentOutputPosition), 0, 4);
                    Output.Write(result, 0, resultSize);
                }
                finally
                {
                    Output.Dispose();
                }
            }
        }

        #region Internal
        /// <summary>
        /// The LZ4 Magic.
        /// </summary>
        internal static readonly byte[] LZ4Magic = { 0x2A, 0x4C, 0x5A, 0x34, 0x2A };

        /// <summary>
        /// Gets or Sets the output buffer.
        /// </summary>
        internal byte[] OutputBuffer { get; set; }

        /// <summary>
        /// Gets or Sets the current output position.
        /// </summary>
        internal int CurrentOutputPosition { get; set; }

        /// <summary>
        /// Gets or Sets the output stream.
        /// </summary>
        internal Stream Output { get; set; }

        /// <summary>
        /// Aligns the buffer.
        /// </summary>
        /// <param name="alignment">The size to align the output to.</param>
        internal void Align(int alignment)
        {
            alignment -= 1;
            CurrentOutputPosition = ~alignment & CurrentOutputPosition + alignment;
        }


        /// <summary>
        /// Writes a hash to the underlying buffer.
        /// </summary>
        /// <param name="hash">Value to write.</param>
        internal void WriteHash(ushort hash)
        {
            Align(4);
            Write(hash);
        }

        /// <summary>
        /// Writes a UTF-8 string to the underlying buffer.
        /// </summary>
        /// <param name="value">Value to write.</param>
        internal void WriteString(string value)
        {
            Align(4);
            foreach (var c in value)
                Write((byte)char.ToLower(c));
            Write((byte)0);
        }

        /// <summary>
        /// Writes data to the underlying buffer.
        /// </summary>
        /// <typeparam name="T">The type to write, this must be a unmanaged type.</typeparam>
        /// <param name="val">The value to write.</param>
        internal void Write<T>(T val) where T : unmanaged
        {
            Span<T> buf = stackalloc T[1]
            {
                val
            };
            var asBytes = MemoryMarshal.Cast<T, byte>(buf);
            var byteCount = asBytes.Length;
            Resize(byteCount);

            if(byteCount < 8)
            {
                for (int i = 0; i < byteCount; i++)
                {
                    OutputBuffer[CurrentOutputPosition++] = asBytes[i];
                }
            }
            else
            {
                var dst = OutputBuffer.AsSpan().Slice(CurrentOutputPosition, byteCount);
                asBytes.CopyTo(dst);
                CurrentOutputPosition += byteCount;
            }
        }


        /// <summary>
        /// Resizes the output buffer if we have more data to write.
        /// </summary>
        /// <param name="sizeOf">The size of the value being read.</param>
        internal void Resize(int sizeOf)
        {
            if (CurrentOutputPosition + sizeOf < OutputBuffer.Length)
                return;
            var newSize = OutputBuffer.Length * 2;
            var newArray = new byte[newSize];
            Buffer.BlockCopy(OutputBuffer, 0, newArray, 0, OutputBuffer.Length);
            OutputBuffer = newArray;
        }
        #endregion
    }
}
