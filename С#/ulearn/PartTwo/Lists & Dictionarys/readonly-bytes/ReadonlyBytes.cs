using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        public int Length { get => data.Length; }
        private int hash;
        private byte[] data;

        public ReadonlyBytes(params byte[] input)
        {
            if (input is null) throw new ArgumentNullException();
            data = input;
        }

        public override string ToString()
        {
            if (Length == 0) return "[]";
            var str = new StringBuilder();
            str.Append('[');
            for (int i = 0; i < Length; i++)
                str.Append(data[i] + ", ");
            str.Remove(str.Length - 2, 2);
            str.Append(']');
            return str.ToString();
        }

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException();
                return data[index];
            }
        }

        private int GetHash()
        {
            var prime = 1000;
            var hash = 0;
            unchecked
            {
                for (int i = 0; i < Length; i++)
                {
                    hash *= prime;
                    hash ^= data[i];
                }
            }
            return hash;
        }

        public override bool Equals(object obj)
        {
            if (obj is not ReadonlyBytes otherData || otherData.GetType() != typeof(ReadonlyBytes))
                return false;

            if (GetHashCode() == otherData.GetHashCode())
                return Enumerable.SequenceEqual(data, otherData);
            return false;
        }

        public override int GetHashCode() => hash == 0 ? hash = GetHash() : hash;
        public IEnumerator<byte> GetEnumerator() => ((IEnumerable<byte>)data).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}