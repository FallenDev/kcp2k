// byte[] buffer with Position, resizes automatically.
// There is no size limit because we will only use it with ~MTU sized arrays.
using System;
using System.Runtime.CompilerServices;

namespace kcp2k
{
    public class ByteBuffer
    {
        public int Position;
        internal const int InitialCapacity = 1200;
        public byte[] RawBuffer = new byte[InitialCapacity];

        // resize to 'value' capacity if needed
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void EnsureCapacity(int value)
        {
            if (RawBuffer.Length < value)
            {
                int capacity = Math.Max(value, RawBuffer.Length * 2);
                Array.Resize(ref RawBuffer, capacity);
            }
        }

        // Write bytes at offset
        public void WriteBytes(byte[] bytes, int startIndex, int length)
        {
            if (length <= 0 || startIndex < 0) return;

            int total = length + Position;
            EnsureCapacity(total);
            Array.Copy(bytes, startIndex, RawBuffer, Position, length);
            Position = total;
        }
    }
}
