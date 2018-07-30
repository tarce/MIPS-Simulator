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


    public class Instruction_J : Instruction
    {
        private string opcode;
        private int addresss;

        private string opcodeBits;
        private string addrsBits;

        public Instruction_J(Word word) : 
            base(word)
        {
            _type = Type.jType;

            opcode = Disassembler.opcodes[word.opcode];
            addresss = word.addr;

            opcodeBits = Helpers.toString(word.addrBits);
            addrsBits = Helpers.toString(word.addrBits);
        }

        public string FormattedBits()
        {
            return opcodeBits + " " + addrsBits;
        }

        public override string ToString()
        {
            return opcode + "," + addresss;
        }
    }

    public class Instruciton_R : Instruction
    {
        private string opcode;
        private string rs;
        private string rt;
        private string rd;
        private string shamt;
        private string funct;

        public Instruciton_R(Word word) :
            base(word)
        {
            if (!Disassembler.fcodes.ContainsKey(word.funct))
            {
                Debug.Log("Instruction_R Error: Fcode not found.");
                return;
            }

            _type = Type.rType;

        }

        public override string ToString()
        {
            string instruction = "";
            if  // shift instruction
                (_word.funct == 0 ||
                _word.funct == 2 ||
                _word.funct == 3)
            {
                instruction = funct + " " + rd + "," + rt + "," + shamt;
            }
            else if // jr/jalr
                (_word.funct == 8 ||
                _word.funct == 9)
            {
                instruction = funct + " " + rs;
            }
            else if // syscall or break
                (_word.funct == 12 ||
                _word.funct == 13)
            {
                instruction = funct;
            }
            else if // mfhi, mthi, mflo, mtlo
                (_word.funct == 16 ||
                _word.funct == 17 ||
                _word.funct == 18 ||
                _word.funct == 19)
            {
                instruction = funct + " " + rd;
            }
            else if // mult and div
                (_word.funct == 24 ||
                _word.funct == 26)
            {
                instruction = funct + " " + rs + "," + rt;
            }
            else
            {
                instruction = funct + " " + rd + "," + rs + "," + rt;
            }
            return instruction;
        }
    }

    public class Instruction_I : Instruction
    {
        public Instruction_I(Word word) :
            base(word)
        {
            _type = Type.iType;
        }
    }

    public class Instruction
    {
        public enum Type
        { 
            jType,
            rType,
            iType
        };

        protected Word _word;
        protected Type _type;

        public Instruction(Word word)
        {
            _word = word;
        }

    }

    // TODO: see: https://inst.eecs.berkeley.edu/~cs61c/resources/MIPS_help.html
    public static class Disassembler
    {
        #region opcodes
        public static readonly Dictionary<int, string> opcodes
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
        public static readonly Dictionary<int, string> fcodes
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
        public static readonly Dictionary<int, string> registers
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

        public static Instruction Disassemble(Word word)
        {
            Instruction instr = new Instruction(word); ;

            if (!opcodes.ContainsKey(word.opcode))
            {
                Debug.Log("Opcode not found.");
            }
            else if (word.opcode == 0) // R-Instr
            {
                instr = new Instruciton_R(word);
            }
            else if (word.opcode == 1)
            {
            }
            else if (word.opcode == 2 || word.opcode == 3) // J-Instr
            {
                instr = new Instruction_J(word);
                Debug.Log(instr.ToString());
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

            return instr;
        }


    }
}


