using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIPS
{

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
        private bool validOpcode;

        public Instruction(Word word)
        {
            _word = word;
        }

        protected BitArray SignExtend(int numBits, BitArray bits)
        {
            BitArray result = new BitArray(numBits + bits.Count);



            return result;
        }

        public abstract string FormattedBits();

        public override abstract string ToString();
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

            opcode = Disassembler.opcodes[word.opcode];
            rs = word.rs;
            rt = word.rt;
            imm = word.imm;

            //opcodeBits = Helpers.toString(word.opcodeBits);
            //rsBits = Helpers.toString(word.rsBits);
            //rtBits = Helpers.toString(word.rtBits);
            //immBits = Helpers.toString(word.immBits);
        }

        //public int getImmediate()
        //{

        //}

        public override string FormattedBits()
        {
            return opcodeBits + " " + rsBits + " " + rtBits + " " + immBits;
        }

        public override string ToString()
        {
            string instruction = "";
            if // beq or bne
                (_word.opcode == 4 ||
                _word.opcode == 5)
            {
                instruction = opcode + " " + rs + ", " + rt + ", " + imm;
            }
            else if //branch (bgez, bltz), blez, bgtz
                (_word.opcode == 1 ||
                _word.opcode == 6 ||
                _word.opcode == 7)
            {
                instruction = opcode + " " + rs + ", " + imm;
            }
            else if // addi, addiu, slti, sltiu, andi, ori, xori
                (_word.opcode >= 8 &&
                _word.opcode <= 14
                )
            {
                instruction = opcode + " " + rt + ", " + rs + ", " + imm;
            }
            else if // lui
                (_word.opcode == 15)
            {
                instruction = opcode + " " + rt + ", " + imm;
            }
            else
            {
                instruction = opcode + " " + rt + ", " + imm + "(" + rs + ")";
            }
            return instruction;
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
            _type = Type.rType;

            opcode = Disassembler.opcodes[word.opcode];
            rs = word.rs;
            rt = word.rt;
            rd = word.rd;
            shamt = word.shamt;
            funct = Disassembler.fcodes[word.funct];

            //opcodeBits = Helpers.toString(word.opcodeBits);
            //rsBits = Helpers.toString(word.rsBits);
            //rtBits = Helpers.toString(word.rtBits);
            //rdBits = Helpers.toString(word.rdBits);
            //shamtBits = Helpers.toString(word.shamtBits);
            //functBits = Helpers.toString(word.functBits);
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
                instruction = funct + " " + rd + ", " + rt + ", " + shamt;
            }
            else if // shift by value
                (_word.funct == 4 ||
                _word.funct == 6 ||
                _word.funct == 7)
            {
                instruction = funct + " " + rd + ", " + rt + ", " + rs;
            }
            else if // jr
                (_word.funct == 8)
            {
                instruction = funct + " " + rs;
            }
            else if // jalr TODO: check implicit form
                (_word.funct == 9)
            {
                instruction = funct + " " + rd + ", " + rs;
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
                instruction = funct + " " + rs + ", " + rt;
            }
            else
            {
                instruction = funct + " " + rd + ", " + rs + ", " + rt;
            }
            return instruction;
        }
    }

}