package mipSim.constructs;

import instructions.Instruction;
import instructions.Instruction.InstException;
import static instructions.Instruction.Type.*;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class Memory {
	
	private int PC;								//the program counter
	private static byte[] BYTE_INPUT;			//byte array of input file
	private Map<Integer, Instruction> CONTENTS; //instruction contents of memory
	private ArrayList<Integer> DATA;
	
	private int INST_START_ADDRS;
	private int DATA_START_ADDRS;
	
	public Memory (int pc) {
		PC= pc;
		INST_START_ADDRS = pc;
		CONTENTS = new HashMap <Integer,Instruction> ();
		DATA = new ArrayList <Integer> ();
	}
	
	public int getPC() {
		return PC;
	}
	
	public void read (String fileName) {
		
		File file = new File(fileName);
		
		createByteArray(file);	
		createContents();
	
	}
	
	public void write (String fileName) {
		
	}
	
	public void print () {
		for (Integer address: CONTENTS.keySet()){
            String instruction = CONTENTS.get(address).toString();
            String binary_inst = CONTENTS.get(address).BINARY_STRING;
            System.out.println(binary_inst + " " + address + " " + instruction); 
		}
		for (Integer i: DATA) {
			System.out.println(i);
		}
	}
	
	private void add (int address, Instruction inst) {
		CONTENTS.put(address, inst);
	}
	
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
			this.add(startAddrs, inst);
			if (inst.TYPE == BREAK) {
				stop = true;
			}
			byteCount += 4;
			startAddrs += 4;
		}

		while (byteCount < BYTE_INPUT.length) {
			Integer data = (int) createData(byteCount, BYTE_INPUT);
			DATA.add(data);
			byteCount += 4;
			startAddrs += 4;
		}
		
	}
	
	private long createData(int startByte, byte[] binary_inst) {
		long instruction = ((binary_inst[startByte]&0xFF) << 24) |
				((binary_inst[startByte+1]&0xFF) << 16) |
				((binary_inst[startByte+2]&0xFF) << 8)  |
				(binary_inst[startByte+3]&0xFF); 
		return instruction;
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
