namespace MIPS
{
    using System;
    using System.Collections;

    public static class Helpers
    {
        /// <summary>
        /// Converts a BitArray to its bit string representation.
        /// </summary>
        /// <param name="bitArr">The BitArray to be converted</param>
        /// <returns>Returns a string representing the BitArray.</returns>
        public static string toString(BitArray bitArr)
        {
            string bitString = "";
            foreach (bool bit in bitArr)
            {
                bitString += (bit) ? "1" : "0";
            }
            return bitString;
        }

        /// <summary>
        /// Converts a byte array to its bit string representation.
        /// </summary>
        /// <param name="byteArr">The byte array to be converted</param>
        /// <returns>Returns a string representing the byte array.</returns>
        public static string toString(byte[] byteArr)
        {
            string bitString = "";
            for (int idx = 0; idx < byteArr.Length; idx++)
            {
                bitString += Convert.ToString(byteArr[idx], 2).PadLeft(8, '0');
            }
            return bitString;
        }

        public static BitArray reverse(BitArray bits)
        {
            BitArray temp = new BitArray(bits.Count);
            for (int i = 0; i < bits.Count; i++)
            {
                temp[(bits.Count - 1) - i] = bits[i];
            }
            return temp;
        }
    }
}
