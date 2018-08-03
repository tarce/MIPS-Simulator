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
        public static string GetString(BitArray bitArr)
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
        public static string GetString(byte[] byteArr)
        {
            string bitString = "";
            for (int idx = 0; idx < byteArr.Length; idx++)
            {
                bitString += Convert.ToString(byteArr[idx], 2).PadLeft(8, '0');
            }
            return bitString;
        }

        /// <summary>
        /// Reverses a BitArray in place.
        /// </summary>
        /// <param name="array">The BitArray to be reversed</param>
        public static BitArray Reverse(BitArray bitArr)
        {
            int length = bitArr.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                bool bit = bitArr[i];
                bitArr[i] = bitArr[length - 1 - i];
                bitArr[length - 1 - i] = bit;
            }
            return bitArr;
        }

        /// <summary>
        /// Returns the integer representation of a BitArray
        /// </summary>
        /// <param name="bits">The BitArray</param>
        /// <returns>The integer representation of the BitArray</returns>
        public static int GetInt(BitArray bitArr)
        {
            BitArray mask = new BitArray(bitArr.Count, true);
            bitArr = bitArr.And(mask);
            int[] array = new int[1];
            bitArr.CopyTo(array, 0);
            return array[0];
        }
    }
}
