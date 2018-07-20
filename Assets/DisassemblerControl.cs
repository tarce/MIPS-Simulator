
namespace MIPS
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.IO;
    using System;

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
        private static IDictionary<int, string> opcodes;
        


        static Disassembler()
        {
            _binary = new List<string>();
            _instructions = new List<string>();
            initOpcodes();
        }

        private static initOpcodes()
        {
            opcodes = new Dictionary<int, string>();
            opcodes.Add(0, "rtyp");
            opcodes.Add(1, "branch");
            opcodes.Add(2, "j");
            opcodes.Add(3, "jal");
            opcodes.Add(4, "beq");
            opcodes.Add(5, "bne");
            opcodes.Add(6, "blez");
            opcodes.Add(7, "bgtz");
            opcodes.Add(8, "addi");
            opcodes.Add(10, "slti");
            opcodes.Add(12, "andi");
            opcodes.Add(13, "ori");
            opcodes.Add(14, "xori");
            opcodes.Add(15, "lui");
            opcodes.Add(32, "lb");
            opcodes.Add(35, "lw");
            opcodes.Add(40, "sb");
            opcodes.Add(43, "sw");
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
                bool readError = false;
                while (br.BaseStream.Position != br.BaseStream.Length &&
                    !readError)
                {
                    // Read the source file into a byte array, 
                    // 32 bits at a time (4 bytes = 1 word)
                    byte[] word = br.ReadBytes(4);

                    if (word.Length < 4)
                    {
                        Debug.Log("Error: byte[].length < 32 bits");
                        readError = true;
                    }
                    else
                    {
                        disassemble(word);
                    }
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
            BitArray _word = createBitArr(word);
            _binary.Add(toString(_word));
            parse(_word);
        }



        private static void parse(BitArray word)
        {
            int opcode = getOpcode(word);

            if (!opcodes.ContainsKey())


    if opcode not in opcodes:
            return "error"

    elif(opcode == 0): # Rtype instruction

            Debug.Log(getIntFromBitArray(opcode.And(opcodeMask)));
        }

        private static int getOpcode(BitArray word)
        {
            int opcode = -1;

            BitArray opcodeMask = new BitArray(6, true);
            BitArray opcodeBits = new BitArray(6);
            for (int idx = 0; idx <= 5; idx++)
            {
                opcodeBits[5 - idx] = word[idx];
            }

            opcode = getIntFromBitArray(opcodeBits.And(opcodeMask));

            return opcode;
        }

        /// <summary>
        /// Creates and returns a BitArray with the correct Endianess
        /// </summary>
        /// <param name="word">The byte array to be made into a BitArray</param>
        /// <returns>A bit array with proper endianess</returns>
        /// <remarks>This is a file specific call.</remarks>
        private static BitArray createBitArr(byte[] word)
        {
            BitArray _tempBitArr = new BitArray(word);
            bool[] _tempBool = new bool[_tempBitArr.Count];

            for (int byteNum = 1; byteNum <= word.Length * 8; byteNum += 8)
                for (int idx = 0; idx <= 7; idx++)
                    _tempBool[idx + byteNum - 1] = _tempBitArr[(7 - idx) + byteNum - 1];

            return new BitArray(_tempBool);
        }


        /// <summary>
        /// Converts a BitArray to its bit string representation.
        /// </summary>
        /// <param name="bitArr">The BitArray to be converted</param>
        /// <returns>Returns a string representing the BitArray.</returns>
        private static string toString(BitArray bitArr)
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
        private static string toString(byte[] byteArr)
        {
            string bitString = "";
            for (int idx = 0; idx < byteArr.Length; idx++)
            {
                bitString += Convert.ToString(byteArr[idx], 2).PadLeft(8, '0');
            }
            return bitString;
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

