package mipSim;

import java.io.FileNotFoundException;

import mipSim.constructs.Log;
import mipSim.instructions.*;
import mipSim.pipeline.*;

public class MIPSimulator {
	
	private static int RS_SIZE = 8;
	private static int REG_SIZE = 32;
	private static int ROB_SIZE = 6;
	private static int PC_START = 584;

	
	private static int START;
	private static int END;
	private static String INPUT_FILE;
	private static String OUTPUT_FILE;
	private static String MODE;
	
	private static Memory MAIN_MEMORY;
	private static InstQueue IQ;
	private static RS RS;
	private static RegStatus REG_STAT;
	private static Registers REG;
	private static ROB ROB;
	
	/**
	 * Entry point of simulator.
	 * 
	 * Format: 
	 * MIPSimulator inputfilename outputfilename operation [-Tm:n] 
	 * 
	 * inputfilename - The file name of the binary input file.
	 * outputfilename - The file name to which to print the output.
	 * operation - Either"dis" or"sim" to specify disassembly or simulation.
	 * -Tm:n - Optional argument to specify the start (m) and end (n) cycles of simulation tracing output. Tracing should be done in a single-step fashion with the contents of registers and memory shown after every processor cycle. -T0:0 indicates that no tracing is to be performed; eliminating the argument specifies that every cycle is to be traced.
	 * 
	 * @param args	- The list of above arguments provided
	 */
	
	public static void main(String[] args) {
		
		switch (args.length) {
		case 3:
			INPUT_FILE = args[0];
			OUTPUT_FILE = args[1];
			MODE = args[2];
			break;
		case 4:
			INPUT_FILE = args[0];
			OUTPUT_FILE = args[1];	
			MODE = args[2];
			//TODO: add intervals
			break;
		default:
			System.out.println("Usage:  java MIPSimulator <inputfilename> <outputfilename> <operation> [-Tm:n]");
			System.exit(1);
		}
		
		MAIN_MEMORY = new Memory(PC_START);
		MAIN_MEMORY.read(INPUT_FILE);			
		
		switch (MODE) {
		case "dis": 
			MIPSimulator.disassemble();
			break;
		case "sim": 
			MIPSimulator.simulate();
			break;
		default:
			Log.add("Problem in mode switching in main program.");
		}
	}

	private static void simulate() {
		
		System.out.println("Starting simulator...");
		
		IQ = new InstQueue();
		RS = new RS(RS_SIZE);
		REG_STAT = new RegStatus(REG_SIZE);
		REG = new Registers(REG_SIZE);
		ROB = new ROB(ROB_SIZE);
		
		fetch();
		issue();
		IQ.sync();
		
		IQ.printContents();
		
		MAIN_MEMORY.PC = 640;
				
		fetch();
		issue();
		IQ.sync();
		
		IQ.printContents();
		
		System.out.println("Simulation complete.");
	}

	private static void disassemble() {
		System.out.println("Starting disassembler...");
		try {
			MAIN_MEMORY.write(OUTPUT_FILE);
		} catch (FileNotFoundException e) {
			Log.add(e);
		}
		System.out.println("Disassembly complete.");
	}
	
	/**
	 * Represents instruction fetch stage.
	 */
	public static void fetch() {
		Instruction i = MAIN_MEMORY.fetchInst(MAIN_MEMORY.PC);	
		if (i == null) { return; }	
		IQ.putToken(i);
	}
	
	public static void issue() {
		Instruction i = IQ.getTop();
		
		int rs_pos = RS.getFreeSlot();
		int rob_pos = ROB.getFreeSlot();
		
		if (rs_pos == -1 || rob_pos == -1) {
			return;
		}
		
		Integer qj;		//associated w/ RS
		Integer qk;		//assciated w/ RT
		Integer vj;
		Integer vk;
		Integer a;
		
		switch (i.TYPE) {
		
		case SUB:
		case SUBU:
		case ADDU:
		case AND:
		case OR:
		case NOR:
		case XOR:
		case SLT:
		case SLTU:
		case ADD:
	
			if (REG_STAT.isBusy(i.RS)){
				int rob_id = REG_STAT.getROB_ID(i.RS);
				Integer rob_answer = ROB.getValue(rob_id);
				if (rob_answer == null) {
					qj = rob_id;
					vj = null;
				}
				else {
					qj = null;
					vj = rob_answer;
				}
			}
			else {
				qj = null;
				vj = REG.get(i.RS);
			}
			if (REG_STAT.isBusy(i.RT)){
				int rob_id = REG_STAT.getROB_ID(i.RT);
				Integer rob_answer = ROB.getValue(rob_id);
				if (rob_answer == null) {
					qk = rob_id;
					vk = null;
				}
				else {
					qk = null;
					vk = rob_answer;
				}
			}
			else {
				qk = null;
				vk = REG.get(i.RT);
			}
			
			a = null;
			
			RS.update(rs_pos, i.TYPE, vj, vk, qj, qk, rob_pos, a);
		
		default:
			//TODO:
		}
		
		IQ.removeTop();
		
	}
	
}
