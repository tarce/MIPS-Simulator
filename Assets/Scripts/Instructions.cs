using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIPS
{

    #region R-Instructions

    public class SLL : Instruciton_R
    {

        public SLL(Word word) :
            base(word)
        {
            _opcode = "sll";
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region J-Instructions

    public class J : Instruction_J
    {
        public J(Word word) : base(word)
        {
            _opcode = "j";
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class JAL : Instruction_J
    {
        public JAL(Word word) : base(word)
        {
            _opcode = "jal";
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
    #endregion

    #region I-Instructions

    public class ADDI : Instruction_I
    {
        public ADDI(Word word) :
            base(word)
        {
            _opcode = "addi";
            _imm = GetSignedInt(SignExtend32(_immBits));
        }

        public override string ToString()
        {
            return _opcode + " R" + _rt + ", R" + _rs + ", #" + _imm;
        }
    }

    public class ADDIU : Instruction_I
    {
        public ADDIU(Word word) :
            base(word)
        {
            _opcode = "addiu";
            _imm = GetUnsignedInt(SignExtend32(_immBits));
        }

        public override string ToString()
        {
            return _opcode + " R" + _rt + ", R" + _rs + ", #" + _imm;
        }
    }

    public class SB : Instruction_I
    {

        public SB(Word word) :
            base(word)
        {
            _opcode = "sb";
            _imm = GetUnsignedInt(SignExtend32(_immBits));
        }

        public override string ToString()
        {
            return _opcode + " R" + _rt + ", " + _imm + "(" + _rs + ")";
        }
    }

    public class SH : Instruction_I
    {

        public SH(Word word) :
            base(word)
        {
            _opcode = "sh";
            _imm = GetUnsignedInt(SignExtend32(_immBits));
        }

        public override string ToString()
        {
            return _opcode + " R" + _rt + ", " + _imm + "(R" + _rs + ")";
        }
    }

    public class SW : Instruction_I
    {

        public SW(Word word) :
            base(word)
        {
            _opcode = "sw";
            _imm = GetUnsignedInt(SignExtend32(_immBits));
        }

        public override string ToString()
        {
            return _opcode + " R" + _rt + ", " + _imm + "(R" + _rs + ")";
        }
    }

    public class LW : Instruction_I
    {

        public LW(Word word) :
            base(word)
        {
            _opcode = "lw";
            _imm = GetUnsignedInt(SignExtend32(_immBits));
        }

        public override string ToString()
        {
            return _opcode + " R" + _rt + ", " + _imm + "(" + _rs + ")";
        }
    }

    #endregion

}