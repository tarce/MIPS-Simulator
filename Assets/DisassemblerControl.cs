
namespace MIPS
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO;
    using System;
    using System.Text;

    public class DisassemblerControl : MonoBehaviour
    {
        public TextAsset binaryFile;

        public void Disassemble()
        {
            Disassembler.readBinaryFile(binaryFile.bytes);
        }
    }

    static class Disassembler
    {
        private static List<string> _binary;
        private static List<string> _instructions;

        static Disassembler()
        {
            _binary = new List<string>();
            _instructions = new List<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void readBinaryFile(byte[] binaryFile)
        {
            Stream ms = new MemoryStream(binaryFile);
            using (BinaryReader br = new BinaryReader(ms))
            {
                while (br.BaseStream.Position != br.BaseStream.Length)
                {
                    // Read the source file into a byte array, 
                    // 32 bits at a time (4 bytes = 1 word)
                    byte[] word = br.ReadBytes(4);
                    disassemble(word);
                }
                br.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        private static void disassemble(byte[] word)
        {
            if (word.Length < 4)
            {
                Debug.Log("Error: byte[].length < 32 bits");
                return;
            }
            _binary.Add(wordToStr(word));
            parse(word);
        }

        /// <summary>
        /// The word2Str method converts a byte array to its string representation.
        /// </summary>
        /// <param name="word">A byte[] of lenth 4 (4 bytes = word)</param>
        /// <returns>Returns a string representing the binary of the 32 bit word.</returns>
        private static string wordToStr(byte[] word)
        {
            if (word.Length < 4)
            {
                Debug.Log("Error: byte[].length < 32 bits");
                return "";
            }
            string bitString = Convert.ToString(word[0], 2).PadLeft(8, '0') +
                Convert.ToString(word[1], 2).PadLeft(8, '0') +
                Convert.ToString(word[2], 2).PadLeft(8, '0') +
                Convert.ToString(word[3], 2).PadLeft(8, '0') ;
            return bitString;
        }

        private static void parse(byte[] word)
        {
            BitArray bitArrWord = new BitArray(word);
            BitArray opcodeMask = new BitArray(32);
            for (int bit = 0; bit < 6; bit++)
            {
                opcodeMask[bit] = true;
            }

            Debug.Log(toString(opcodeMask));
        }

        public static string toString(BitArray bitArray)
        {
            string values = "";
            foreach (bool bit in bitArray)
            {
                values += (bit == true) ? "1" : "0";
            }
            return values;
        }

        private static int getIntFromBitArray(BitArray bitArray)
        {

            if (bitArray.Length > 32)
                throw new ArgumentException("Argument length shall be at most 32 bits.");

            int[] array = new int[1];
            bitArray.CopyTo(array, 0);
            return array[0];

        }
    }
}

