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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    public class SW : Instruction_I
    {

        public SW(Word word) :
            base(word)
        {
            _opcode = "sw";
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    public class LW : Instruction_I
    {

        public LW(Word word) :
            base(word)
        {
            _opcode = "lw";
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

    #endregion

}