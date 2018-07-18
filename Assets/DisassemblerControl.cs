
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
            Debug.Log("Dissasembling");
            if (word.Length < 4)
            {
                Debug.Log("Error: byte[].length < 32 bits");
                return;
            }
            BitArray bitArrWord = new BitArray(word);
            Debug.Log(wordToStr(word));
        }

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
    }
}

