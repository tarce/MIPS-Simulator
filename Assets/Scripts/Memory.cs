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

        public Word(byte[] bytes)
        {
            if (!(bytes.Length == 4))
            {
                Debug.Log("Word Error: byte[] must be length 4");
                return;
            }
            _bytes = bytes;
           Array.Reverse(_bytes);
            _bits = new BitArray(_bytes);
        }

        public override string ToString()
        {
            return Helpers.GetString(_bits);
        }

        public BitArray GetBits(int startIdx, int endIdx)
        {
            if (endIdx >= _bits.Count)
            {
                throw new IndexOutOfRangeException("GetBits index out of range.");
            }

            int size = endIdx - startIdx + 1;
            BitArray bits = new BitArray(size);

            for (int idx = 0; idx < size; idx++, startIdx++)
            {
                bits[idx] = _bits[startIdx];
            }
            return bits;
        }



        //private void getShamt()
        //{
        //    BitArray shamtMask = new BitArray(5, true);
        //    shamtBits = new BitArray(5);
        //    for (int idx = 21; idx <= 25; idx++)
        //    {
        //        shamtBits[25 - idx] = _bits[idx];
        //    }
        //    shamt = getIntVal(shamtBits, shamtMask);
        //    shamtBits = Helpers.reverse(shamtBits);
        //}

        //private void getFunct()
        //{
        //    BitArray functMask = new BitArray(6, true);
        //    functBits = new BitArray(6);
        //    for (int idx = 26; idx <= 31; idx++)
        //    {
        //        functBits[31 - idx] = _bits[idx];
        //    }
        //    funct = getIntVal(functBits, functMask);
        //    functBits = Helpers.reverse(functBits);
        //}

    }
}