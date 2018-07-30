namespace MIPS
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    public class Memory : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _binaryFile;

        [SerializeField]
        private GameObject _LogTemplate;

        private List<Word> _words; // A word is 32 bits (4 bytes)

        private void Awake()
        {
            _words = new List<Word>();
            ReadFromDisk(_binaryFile.bytes);
        }

        public void Load()
        {
            foreach (Word word in _words)
            {
                GameObject newWord = Instantiate<GameObject>(_LogTemplate) as GameObject;
                newWord.SetActive(true);
                newWord.GetComponent<MemoryLogItem>().SetText(word.ToString());
                newWord.transform.SetParent(_LogTemplate.transform.parent, false);
            }
        }

        public List<Word> getWords()
        {
            return _words;
        }

        /// <summary>
        /// Reads bytes into memory storing them as words.
        /// </summary>
        /// <param name="bytes"></param>
        public void ReadFromDisk(byte[] bytes)
        {
            Stream ms = new MemoryStream(bytes);
            using (BinaryReader br = new BinaryReader(ms))
            {
                bool readError = false;
                while (br.BaseStream.Position != br.BaseStream.Length &&
                    !readError)
                {
                    byte[] word = br.ReadBytes(4);
                    if (word.Length < 4)
                    {
                        Debug.Log("Error: byte[].length < 32 bits");
                        readError = true;
                        break;
                    }
                    _words.Add(new MIPS.Word(word));
                }
                br.Close();
            }
        }
    }

    public class Word
    {
        private byte[] _bytes;
        private BitArray _bits;

        #region Type Parts Values
        public int opcode;
        public int rs;
        public int rt;
        public int rd;
        public int shamt;   // R-type
        public int funct;   // R-type
        public int imm;     // I-type
        public int addr;    // J-type
        #endregion

        #region Type Parts Bits
        public BitArray opcodeBits;
        public BitArray rsBits;
        public BitArray rtBits;
        public BitArray rdBits;
        public BitArray shamtBits;   // R-type
        public BitArray functBits;   // R-type
        public BitArray immBits;     // I-type
        public BitArray addrBits;    // J-type
        #endregion

        public Word(byte[] bytes)
        {
            if (!(bytes.Length == 4))
            {
                Debug.Log("Word Error: byte[] must be length 4");
                return;
            }

            _bytes = bytes;
            _bits = store(bytes);
            getOpcode();
            getRs();
            getRt();
            getRd();
            getShamt();
            getAddr();
        }

        /// <summary>
        /// Converts a byte array word to a BitArray with the correct Endianess
        /// </summary>
        /// <param name="word">The byte array to be made into a BitArray</param>
        /// <returns>A bit array with proper endianess</returns>
        private BitArray store(byte[] word)
        {
            BitArray _tempBitArr = new BitArray(word);
            bool[] _tempBool = new bool[_tempBitArr.Count];

            for (int byteNum = 1; byteNum <= word.Length * 8; byteNum += 8)
                for (int idx = 0; idx <= 7; idx++)
                    _tempBool[idx + byteNum - 1] = _tempBitArr[(7 - idx) + byteNum - 1];

            return new BitArray(_tempBool);
        }

        public override string ToString()
        {
            return Helpers.toString(_bits);
        }

        private void getOpcode()
        {
            BitArray opcodeMask = new BitArray(6, true);
            opcodeBits = new BitArray(6);
            for (int idx = 0; idx <= 5; idx++)
            {
                opcodeBits[5 - idx] = _bits[idx];
            }
            opcode = getIntVal(opcodeBits,opcodeMask);
            opcodeBits = Helpers.reverse(opcodeBits);
        }

        private void getRs()
        {
            BitArray rsMask = new BitArray(5, true);
            rsBits = new BitArray(5);
            for (int idx = 6; idx <= 10; idx++)
            {
                rsBits[10 - idx] = _bits[idx];
            }
            rs = getIntVal(rsBits, rsMask);
            rsBits = Helpers.reverse(rsBits);
        }

        private void getRt()
        {
            BitArray rtMask = new BitArray(5, true);
            rtBits = new BitArray(5);
            for (int idx = 11; idx <= 15; idx++)
            {
                rtBits[15 - idx] = _bits[idx];
            }
            rt = getIntVal(rtBits, rtMask);
            rtBits = Helpers.reverse(rtBits);
        }

        private void getRd()
        {
            BitArray rdMask = new BitArray(5, true);
            rdBits = new BitArray(5);
            for (int idx = 16; idx <= 20; idx++)
            {
                rdBits[20 - idx] = _bits[idx];
            }
            rd = getIntVal(rdBits, rdMask);
            rdBits = Helpers.reverse(rdBits);
        }

        private void getShamt()
        {
            BitArray shamtMask = new BitArray(5, true);
            shamtBits = new BitArray(5);
            for (int idx = 21; idx <= 25; idx++)
            {
                shamtBits[25 - idx] = _bits[idx];
            }
            shamt = getIntVal(shamtBits, shamtMask);
            shamtBits = Helpers.reverse(shamtBits);
        }

        private void getFunct()
        {
            BitArray functMask = new BitArray(6, true);
            functBits = new BitArray(6);
            for (int idx = 26; idx <= 31; idx++)
            {
                functBits[31 - idx] = _bits[idx];
            }
            funct = getIntVal(functBits, functMask);
            functBits = Helpers.reverse(functBits);
        }

        private void getImm()
        {
            BitArray immMask = new BitArray(16, true);
            immBits = new BitArray(16);
            for (int idx = 16; idx <= 31; idx++)
            {
                immBits[31 - idx] = _bits[idx];
            }
            imm = getIntVal(immBits, immMask);
            immBits = Helpers.reverse(immBits);
        }

        private void getAddr()
        {
            BitArray addrMask = new BitArray(28, true);
            addrBits = new BitArray(28);

            for (int idx = 6; idx <= 31; idx++)
            {
                addrBits[33 - idx] = _bits[idx];
            }
            addr = getIntVal(addrBits,addrMask);
            addrBits = Helpers.reverse(addrBits);
        }

        private int getIntVal(BitArray bits, BitArray mask)
        {
            bits = bits.And(mask);
            int[] array = new int[1];
            bits.CopyTo(array, 0);
            return array[0];
        }

    }
}