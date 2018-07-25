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
        private BitArray _bits2;

        public Word(byte[] bytes)
        {
            if (!(bytes.Length == 4))
            {
                Debug.Log("Word Error: byte[] must be length 4");
                return;
            }

            _bytes = bytes;
            _bits = store(bytes);
            _bits2 = new BitArray(bytes);
        }

        /// <summary>
        /// Converts a byte array word to a BitArray with the correct Endianess
        /// </summary>
        /// <param name="word">The byte array to be made into a BitArray</param>
        /// <returns>A bit array with proper endianess</returns>
        private static BitArray store(byte[] word)
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
    }
}