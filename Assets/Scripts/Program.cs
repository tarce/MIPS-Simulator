namespace MIPS
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Program : MonoBehaviour
    {
        [SerializeField]
        public Memory _memory;

        private List<Instruction> instructions;

        private void Awake()
        {
            instructions = new List<Instruction>();
        }

        public void Disassemble()
        {
            foreach (Word word in _memory.getWords())
            {
                Disassembler.Disassemble(word);
            }
        }
    }

    public class Instruction
    {
        Word _word;

        public Instruction()
        {

        }
    }

    public static class Disassembler
    {
        private static List<string> _binary;
        private static List<string> _instructions;

        #region opcodes
        private static readonly Dictionary<int, string> opcodes
            = new Dictionary<int, string>
            {
                { 0, "rtyp" },
                { 1, "branch" },
                { 2, "j" },
                { 3, "jal" },
                { 4, "beq" },
                { 5, "bne" },
                { 6, "blez" },
                { 7, "bgtz" },
                { 8, "addi" },
                { 10, "slti" },
                { 12, "andi" },
                { 13, "ori" },
                { 14, "xori" },
                { 15, "lui" },
                { 32, "lb"},
                { 35, "lw" },
                { 40, "sb" },
                { 43, "sw" }
            };
        #endregion

        #region fcodes
        private static readonly Dictionary<int, string> fcodes
            = new Dictionary<int, string>
            {
                { 0, "sll" },
                { 2, "slr" },
                { 3, "sra" },
                { 4, "sllv" },
                { 6, "srlv" },
                { 7, "srav" },
                { 8, "jr" },
                { 9, "jalr" },
                { 12, "syscall" },
                { 13, "break" },
                { 16, "mfhi" },
                { 17, "mtlo" },  // TODO: check this code
                { 18, "mflo" },
                { 19, "mtlo" },  // TODO: check this code
                { 24, "mult" },
                { 26, "div" },
                { 32, "add" },
                { 34, "sub" },
                { 36, "and" },
                { 37, "or" },
                { 38, "xor" },
                { 39, "nor" },
                { 42, "slt" }
            };
        #endregion

        #region registers
        private static readonly Dictionary<int, string> registers
            = new Dictionary<int, string>
            {
                { 0, "$zero" },
                { 1, "$at" },
                { 2, "$v0" },
                { 3, "$v1" },
                { 4, "$a0"},
                { 5, "$a1" },
                { 6, "$a2" },
                { 7, "$a3" },
                { 8, "$t0" },
                { 9, "$t1" },
                { 10, "$t2"},
                { 11, "$t3" },
                { 12, "$t4" },
                { 13, "$t5" },
                { 14, "$t6" },
                { 15, "$t7" },
                { 16, "$s0" },
                { 17, "$s1" },
                { 18, "$s2" },
                { 19, "$s3" },
                { 20, "$s4" },
                { 21, "$s5" },
                { 22, "$s6" },
                { 23, "$s7" },
                { 24, "$t8" },
                { 25, "$t9" },
                { 26, "$k0" },
                { 27, "$k1" },
                { 28, "$gp" },
                { 29, "$sp" },
                { 30, "$fp" },
                { 31, "$ra" }
            };
        #endregion

        static Disassembler()
        {
            _binary = new List<string>();
            _instructions = new List<string>();
        }

        public static void Disassemble(Word word)
        {
            if (!opcodes.ContainsKey(word.opcode))
            {
                Debug.Log("Opcode not found.");
            }
            else if (word.opcode == 0) // R-Instr
            {
            }
            else if (word.opcode == 1)
            {
            }
            else if (word.opcode == 2 || word.opcode == 3) // J-Instr
            {
                Debug.Log(Helpers.toString(word.addrBits));
                Debug.Log(opcodes[word.opcode] + " " + word.addr);
            }
            else if (word.opcode == 15)
            {

            }
            else if (word.opcode == 32 || word.opcode == 35 || word.opcode == 40 || word.opcode == 43)
            {
            }
            else
            {

            }
        }


    }
}


