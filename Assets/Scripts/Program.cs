namespace MIPS
{
    using System;
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

            opcodeBits = Helpers.toString(word.opcodeBits);
            addrsBits = Helpers.toString(word.addrBits);
        }

        public override string FormattedBits()
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
        private int rs;
        private int rt;
        private int rd;
        private int shamt;
        private string funct;

        private string opcodeBits;
        private string rsBits;
        private string rtBits;
        private string rdBits;
        private string shamtBits;
        private string functBits;

        public Instruciton_R(Word word) :
            base(word)
        {
            if (!Disassembler.fcodes.ContainsKey(word.funct))
            {
                Debug.Log("Instruction_R Error: Fcode not found.");
                return;
            }

            _type = Type.rType;

            opcode = Disassembler.opcodes[word.opcode];
            rs = word.rs;
            rt = word.rt;
            rd = word.rd;
            shamt = word.shamt;
            funct = Disassembler.fcodes[word.funct];

            opcodeBits = Helpers.toString(word.opcodeBits);
            rsBits = Helpers.toString(word.rsBits);
            rtBits = Helpers.toString(word.rtBits);
            rdBits = Helpers.toString(word.rdBits);
            shamtBits = Helpers.toString(word.shamtBits);
            functBits = Helpers.toString(word.functBits);
        }

        public override string FormattedBits()
        {
            return opcodeBits + " " + rsBits + " " + rtBits + " " + rdBits + 
                " " + shamtBits + " " + functBits;
        }

        public override string ToString()
        {
            string instruction = "";
            if  // shift by amount
                (_word.funct == 0 ||
                _word.funct == 2 ||
                _word.funct == 3)
            {
                instruction = funct + " " + rd + "," + rt + "," + shamt;
            }
            else if // shift by value
                (_word.funct == 4 ||
                _word.funct == 6 ||
                _word.funct == 7)
            {
                instruction = funct + " " + rd + "," + rt + "," + rs;
            }
            else if // jr
                (_word.funct == 8)
            {
                instruction = funct + " " + rs;
            }
            else if // jalr TODO: check implicit form
                (_word.funct == 9)
            {
                instruction = funct + " " + rd + "," + rs;
            }
            else if // syscall or break
                (_word.funct == 12 ||
                _word.funct == 13)
            {
                instruction = funct;
            }
            else if // mfhi, mflo 
                (_word.funct == 16 ||
                _word.funct == 18)
            {
                instruction = funct + " " + rd;
            }
            else if // mthi, mtlo
                (_word.funct == 17 || 
                _word.funct == 19)
            {
                instruction = funct + " " + rs;
            }
            else if // mult and div
                (_word.funct == 24 ||
                _word.funct == 25 ||
                _word.funct == 26 ||
                _word.funct == 27)
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
        private string opcode;
        private int rs;
        private int rt;
        private int imm;

        private string opcodeBits;
        private string rsBits;
        private string rtBits;
        private string immBits;

        public Instruction_I(Word word) :
            base(word)
        {
            _type = Type.iType;
        }

        public override string FormattedBits()
        {
            return opcodeBits + " " + rsBits + " " + rtBits + " " + immBits;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Instruction
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

        public abstract string FormattedBits();

        public override abstract string ToString();

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
                { 9, "addiu" },
                { 10, "slti" },
                { 11, "sltiu" },
                { 12, "andi" },
                { 13, "ori" },
                { 14, "xori" },
                { 15, "lui" },
                { 32, "lb"},
                { 33, "lh"},
                { 34, "lw"},
                { 35, "lw" }, // TODO: Check lw
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
                { 13, "break" },  // TODO: check this code
                { 16, "mfhi" },
                { 17, "mthi" },  
                { 18, "mflo" },
                { 19, "mtlo" }, 
                { 24, "mult" },
                { 25, "multu" },
                { 26, "div" },
                { 27, "divu" },
                { 32, "add" },
                { 33, "addu" },
                { 34, "sub" },
                { 35, "subu" },
                { 36, "and" },
                { 37, "or" },
                { 38, "xor" },
                { 39, "nor" },
                { 42, "slt" },
                { 43, "sltu" }
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
            Instruction instr = null;

            if (!opcodes.ContainsKey(word.opcode))
            {
                Debug.Log("Opcode not found.");
            }
            else if (word.opcode == 0) // R-Instr
            {
                instr = new Instruciton_R(word);
                Debug.Log(instr.ToString());
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


