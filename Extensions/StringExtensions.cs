using System;
using System.Linq;
using System.Text;

namespace Lab17.Extensions
{
    public static class StringExtensions
    {
        private const int BITS_PER_CHAR = 8;

        public static string ToASCIIBinaryString(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte character in str)
            {
                string bitsOfChar = Convert.ToString(character, 2).PadLeft(BITS_PER_CHAR, '0');
                sb.Append(bitsOfChar);
            }
            return sb.ToString();
        }

        public static string ToStringASCIIFromBinary(this string binary)
        {
            var byteArr = Enumerable.Range(0, binary.Length / BITS_PER_CHAR).
                Select(pos => Convert.ToByte(
                    binary.Substring( pos * BITS_PER_CHAR, BITS_PER_CHAR), 2 )
                ).ToArray();
            return Encoding.ASCII.GetString(byteArr);
        }

        

    }
}