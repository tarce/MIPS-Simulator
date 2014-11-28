package mipSim.constructs;

import static mipSim.instructions.Instruction.Type.*;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

import mipSim.instructions.Data;
import mipSim.instructions.Instruction;
import mipSim.instructions.Instruction.InstException;

public class Memory {
	
	public int PC;									//the program counter
	private static byte[] BYTE_INPUT;				//byte array of input file
	private ArrayList <Instruction> INSTRUCTIONS;	//instruction contents of memory
	private ArrayList <Data> DATA;					//data contents of memory
	private Map <Integer, Instruction> INSTRUCTIONS_MAP;
	private Map <Integer, Data> DATA_MAP;
	
	private int INST_START_ADDRS;
	private int DATA_START_ADDRS;
	
	public Memory (int pc) {
		PC= pc;
		INST_START_ADDRS = pc;
		INSTRUCTIONS = new ArrayList <Instruction> ();
		DATA = new ArrayList <Data> ();
		INSTRUCTIONS_MAP = new HashMap <Integer, Instruction> ();
		DATA_MAP = new HashMap <Integer, Data> ();
	}
	
//	/**
//	 * Returns the current program counter.
//	 * 
//	 * @return
//	 */
//	public int getPC() {
//		return PC;
//	}
//	
//	/**
//	 * Sets the program counter.
//	 * 
//	 * @param pc
//	 */
//	public void setPC(int pc) {
//		PC = pc;
//	}
	
	/**
	 * Returns the instruction in memory at the given program counter.
	 * 
	 * @param pc
	 * @return
	 */
	public Instruction fetchInst (int pc) {
		if (pc < DATA_START_ADDRS && pc >= INST_START_ADDRS) {
			return INSTRUCTIONS_MAP.get(pc);
		}
		else {
			Log.add("Instruction memory access out of bounds");
			return null;
		}
	}
	
	/**
	 * Returns the data in memory at the given program counter.
	 * 
	 * @param pc
	 * @return
	 */
	public Data fetchData (int pc) {
		if (pc >= DATA_START_ADDRS) {
			return DATA_MAP.get(pc);
		}
		else {
			Log.add("Data memory access out of bounds");
			return null;
		}
	}
	
	/**
	 * Reads in a file (given by fileName) into memory, creating the instructions and data
	 * in the process.
	 * 
	 * @param fileName
	 */
	public void read (String fileName) {
		
		File file = new File(fileName);
		
		createByteArray(file);	
		createContents();
	
	}
	
	/**
	 * Writes out a file (given by fileName) 
	 * 
	 * @param fileName
	 * @throws FileNotFoundException
	 */
	public void write (String fileName) throws FileNotFoundException {
		PrintWriter out = new PrintWriter(fileName); 

		int j = 0;
        int curAddrs = INST_START_ADDRS;
        
		for (Instruction i: INSTRUCTIONS){
            String instruction = i.toString();
            String binary_inst = i.BINARY_STRING;
            out.write(binary_inst + " " + curAddrs + " " + instruction + "\n");  
            j++;
            curAddrs = INST_START_ADDRS + (j * 4);
		}
		
		for (Data d: DATA) {
			int data_val = d.VALUE;
			String binary_data = d.BINARY_STRING;
			out.write(binary_data + " " + curAddrs + " " + data_val + "\n");
			j++;
			curAddrs = INST_START_ADDRS + (j * 4);
		}

		out.close();
	}

	/**
	 * Prints the contents of memory.
	 */
	public void print () {
		
		int j = 0;
        int curAddrs = INST_START_ADDRS;
        
		for (Instruction i: INSTRUCTIONS){
            String instruction = i.toString();
            String binary_inst = i.BINARY_STRING;
            System.out.println(binary_inst + " " + curAddrs + " " + instruction);            
            j++;
            curAddrs = INST_START_ADDRS + (j * 4);
		}
		
		for (Data d: DATA) {
			int data_val = d.VALUE;
			String binary_data = d.BINARY_STRING;
			System.out.println(binary_data + " " + curAddrs + " " + data_val);
			j++;
			curAddrs = INST_START_ADDRS + (j * 4);
		}
	}
	
	/**
	 * Creates the data and instruction lists.
	 */
	private void createContents() {
		
		int byteCount = 0;
		int startAddrs = INST_START_ADDRS;
		boolean stop = false;

		while (byteCount < BYTE_INPUT.length && !stop) {
			
			Instruction inst = null;
			try {
				inst = new Instruction(byteCount, BYTE_INPUT);
			} catch (InstException e) {
				Log.add(e); //TODO: does this work?
			}
			INSTRUCTIONS.add(inst);
			INSTRUCTIONS_MAP.put(startAddrs, inst);
			if (inst.TYPE == BREAK) {
				stop = true;
			}
			byteCount += 4;
			startAddrs += 4;
		}

		DATA_START_ADDRS = startAddrs;

		while (byteCount < BYTE_INPUT.length) {
			Data data = new Data (byteCount, BYTE_INPUT);
			DATA.add(data);
			DATA_MAP.put(startAddrs, data);
			byteCount += 4;
			startAddrs += 4;
		}
		
	}
	
	private void createByteArray(File file) {
		BYTE_INPUT = new byte[(int) file.length()];
		
		InputStream in = null;
		try {
			in = new BufferedInputStream(new FileInputStream(file));
		} catch (FileNotFoundException e) {
			Log.add("File not found.  Error in Memory.read while trying to create input stream");
		}

		int totalBytesRead = 0;
		
		while(totalBytesRead < BYTE_INPUT.length){
			int bytesRemaining = BYTE_INPUT.length - totalBytesRead;
			int bytesRead = 0;
			try {
				bytesRead = in.read(BYTE_INPUT, totalBytesRead, bytesRemaining);
			} catch (IOException e) {
				Log.add("IO exception.  Error in Memory.read while trying to read in bytes");
			}
			if (bytesRead > 0){totalBytesRead = totalBytesRead + bytesRead;}
		}
		
		try {
			in.close();
		} catch (IOException e) {
			Log.add("IO exception.  Error in Memory.read while trying to close input stream");
		}
	}

}
