MIPSimulator
============

This is a MIPS simulator that was started as a class project.  From time to time I will add more functionality to it.

The purpose is to simulate Tomasulo's Algorithm.

For the first part of this project, the MIPS-Simulator accepts a binary input file and after clicking the disassemble button, outputs to the screen the MIPS disassembly of the input file.

The disassembler output contains 4 columns of data with each column separated by one tab character:

* The binary (e.g., 0's and 1's) string representing the 32-bit data word at that location. For instructions, it is split this into six groups of digits to represent different parts of the MIPS instruction word: a group of 6 bits, 4 groups of 5 bits, and a final group of 6 bits.
* The address (in decimal) of that location.
* The disassembled instruction opcode, or signed decimal integer value, depending on whether the current location is after the BREAK instruction.
* If you are displaying an instruction, the fourth column should contain the remaining part of the instruction, with each argument separated by a comma and then a space. (�, �)
 
The instructions and instruction arguments should be in capital letters. Display all integer values in decimal. Immediate values should be proceeded by a �#� symbol. Be careful � some instructions take signed immediate values while others take unsigned immediate values. You will have to make sure you properly display a signed or unsigned value depending on the context.

Because we will be using �diff� to initially check your output versus ours, try to mimic the sample output format as closely as possible.

Sample Data

 

Here is a sample program to test your disassembler with.

fibonacci_c : This contains the C source code for the test program. This is for your reference only.
fibonacci_mips : This is the compiled version of the C code in MIPS assembly. This is for your reference only.
fibonacci_bin : This is the assembled version of the above assembly code. It is the input to your program.
fibonacci_out : This is what your program should output given the above binary input file.
fibonacci_bin_txt : This is the txt version of the input binary file . You can use txt2bin program to convert it to binary or use the binary file provided above.
 

Remember that we will also test your program with other data that you will not know of in advance. It is recommended that you construct your own sample input files with which to further test your disassembler. For your convenience we have provided a program to convert a text file of binary words into a binary file with which to test your disassembler.

txt2bin. (click right to save)This program will convert a text file of binary words into a binary file. It will run in the CISE Solaris environment.
fibonacci_bin_txt:� Running txt2bin on this file should produce the Assembler binary file for the sample data above.
Note, you may use beav �(click right to save) to read the binary file.
���� For example:� �beav fibonacci_bin.bin�
