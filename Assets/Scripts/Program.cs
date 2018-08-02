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
                Instruction instruction = Disassembler.Disassemble(word);
                if (instruction != null)
                    Debug.Log(instruction.Binary());
                instructions.Add(instruction);

                
            }
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
                { 36, "lbu"},
                { 37, "lhu"},
                { 40, "sb" },
                { 41, "sh" },
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
            Instruction instruction = null;

            BitArray opcode = word.GetBits(26, 31);
            BitArray funct = word.GetBits(0, 5);
            switch(GetInt(opcode))
            {
                case 0: // R-Instruction
                    switch(GetInt(funct))
                    {
                        case 0:
                            instruction = new SLL(word);
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 6:
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            break;
                        case 12:
                            break;
                        case 16:
                            break;
                        case 17:
                            break;
                        case 18:
                            break;
                        case 19:
                            break;
                        case 24:
                            break;
                        case 25:
                            break;
                        case 26:
                            break;
                        case 27:
                            break;
                        case 32:
                            break;
                        case 33:
                            break;
                        case 34:
                            break;
                        case 35:
                            break;
                        case 36:
                            break;
                        case 37:
                            break;
                        case 38:
                            break;
                        case 39:
                            break;
                        case 42:
                            break;
                        case 43:
                            break;
                        default:
                            throw new KeyNotFoundException("Disassembler: funct not found.");
                    }
                    break;
                case 1:
                    break;
                case 2:
                    instruction = new J(word);
                    break;
                case 3:
                    instruction = new JAL(word);
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    instruction = new ADDI(word);
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 32:
                    break;
                case 33:
                    break;
                case 34:
                    instruction = new LW(word);
                    break;
                case 36:
                    break;
                case 37:
                    break;
                case 40:
                    break;
                case 41:
                    break;
                case 43:
                    instruction = new SW(word);
                    break;
                default:
                    throw new KeyNotFoundException("Disassembler: opcode not found.");
            }

            return instruction;
        }

        private static int GetInt(BitArray bits)
        {
            BitArray mask = new BitArray(bits.Count, true);
            bits = bits.And(mask);
            int[] array = new int[1];
            bits.CopyTo(array, 0);
            return array[0];
        }


    }
}


