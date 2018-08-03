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

        protected int GetSignedInt(BitArray bitArr)
        {
            int result = 0;
            if(bitArr[bitArr.Count - 1] == false) // 0, indicates positive
            {
                result = Helpers.GetInt(bitArr);
            }
            else
            {
                bitArr.Not();
                result = Helpers.GetInt(bitArr) + 1;
                result *= -1;
            }
            return result;
        }

        protected int GetUnsignedInt(BitArray bitArr)
        {
            return Helpers.GetInt(bitArr);
        }

        protected BitArray Concatenate(BitArray lowOrder, BitArray highOrder)
        {
            BitArray result = new BitArray(lowOrder.Count + highOrder.Count);

            for(int i = 0; i < lowOrder.Count; i++)
            {
                result[i] = lowOrder[i];
            }
            for(int i = lowOrder.Count; i < result.Count; i++)
            {
                result[i] = highOrder[i - lowOrder.Count];
            }
            return result;
        }

        public abstract string Binary();

        public override abstract string ToString();
    }

}