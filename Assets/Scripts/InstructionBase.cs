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

        public Instruction(Word word)
        {
            _word = word;
        }

        protected BitArray SignExtend(int numBits, BitArray bits)
        {
            BitArray result = new BitArray(numBits + bits.Count);
            return result;
        }

        public abstract string Binary();

        public override abstract string ToString();
    }

}