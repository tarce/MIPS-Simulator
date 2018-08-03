using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIPS
{

    #region R-Instructions

    public class BREAK : Instruciton_R
    {
        public BREAK(Word word) :
            base(word)
        {
            _funct = "break";
        }

        public override string ToString()
        {
            return _funct;
        }
    }

    public class SLL : Instruciton_R
    {
        public SLL(Word word) :
            base(word)
        {
            _funct = "sll";
            if(_rd == 0)
            {
                _funct = "nop";
            }
        }

        public override string ToString()
        {
            string instruction;
            if(_rd == 0)
            {
                instruction = _funct;
            }
            else
            {
                instruction = _funct + " R" + _rd + ", R" + _rt + ", #" + _shamt;
            }
            return instruction;
        }
    }

    public class ADD : Instruciton_R
    {
        public ADD(Word word) :
            base(word)
        {
            _funct = "add";
        }

        public override string ToString()
        {
            return _funct + " R" + _rd + ", R" + _rs + ", R" + _rt;
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

    public class BEQ : Instruction_I
    {
        public BEQ(Word word) :
            base(word)
        {
            _opcode = "beq";
            BitArray lowOrder = new BitArray(2);
            _imm = GetSignedInt(SignExtend32(
                    Concatenate(lowOrder, _immBits)));
        }

        public override string ToString()
        {
            return _opcode + " R" + _rs + ", R" + _rt + ", #" + _imm;
        }
    }

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