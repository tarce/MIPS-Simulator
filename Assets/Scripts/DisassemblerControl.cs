
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
}

