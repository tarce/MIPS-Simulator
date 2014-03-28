package instructions;

public class Instruction {

	public String BINARY_STRING;
	public Type TYPE;
	
	private int IMMEDIATE;
	private int RT;
	private int RS;
	private int RD;
	private int SA;
	private int TARGET;
	
	/**
	 * Types of various instructions covered in the MIPSimulator
	 */
	public static enum Type {
		ADD, 
		ADDI, 
		ADDIU, 
		ADDU, 
		AND, 
		BEQ, 
		BNE, 
		BGEZ,
		BGTZ, 
		BLEZ, 
		BLTZ, 
		BREAK, 
		J, 
		LW, 
		NOP, 
		NOR, 
		OR, 
		SLL, 
		SLT, 
		SLTI, 
		SLTU, 
		SRA, 
		SRL, 
		SUB, 
		SUBU, 
		SW, 
		XOR
	}
	
	public Instruction(int startByte, byte[] binary_inst) throws InstException {
		
		BINARY_STRING = this.getBinaryString(startByte, binary_inst);
		TYPE = this.getType();
		
		//from here on, set various fields according to the type of instruction
		
		switch (TYPE) {
		case ADDI:
		case SW:
		case LW:
		case ADDIU:
		case SLTI:
			IMMEDIATE = this.getImmediate1();
			break;
		case BEQ:
		case BGEZ:
		case BGTZ:
		case BNE:
		case BLEZ:
		case BLTZ:
			IMMEDIATE = this.getImmediate2();
			break;
		default :
			IMMEDIATE = 0;	//TODO: could this be a problem? although it shouldn't be...
			break;
		}
		
		//TODO: set various fields of the instruction, regardless of type (lazy way of doing things)
		RS = Integer.parseInt(BINARY_STRING.substring(6, 11), 2);
		RT = Integer.parseInt(BINARY_STRING.substring(11, 16), 2);
		RD = Integer.parseInt(BINARY_STRING.substring(16, 21), 2);
		SA = Integer.parseInt(BINARY_STRING.substring(21, 26), 2);
		TARGET = getTarget();
		
	}
	
	/**
	 * Returns a string representing the instruction in assembler.
	 * 
	 * @return
	 */
	public String toString() {
		String result = "";
		
		switch (TYPE) {
		
		case ADDI:
			result = TYPE.toString() + " R" + RT + ", R" + RS + ", #" + IMMEDIATE; break;
		case SW:
			result = TYPE.toString() + " R" + RT + ", " + IMMEDIATE + "(R" + RS + ")"; break;					
		case LW:
			result = TYPE.toString() + " R" + RT + ", " + IMMEDIATE + "(R" + RS + ")"; break;
		case BEQ:								
			result = TYPE.toString() + " R" + RS + ", R" + RT + ", #" + IMMEDIATE; break;	
		case BREAK:					
			result = "BREAK"; break;
		case J:							
			result = TYPE.toString() + " #" + TARGET; break;
		case ADD:										
			result = TYPE.toString() + " R" + RD + ", R" + RS + ", R" + RT; break;
		case BGEZ:		
			result = TYPE.toString() + " R" + RS + ", #" + IMMEDIATE; break;
		case BGTZ:										
			result = TYPE.toString() + " R" + RS + ", #" + IMMEDIATE; break;								
		case BNE:
			result = TYPE.toString() + " R" + RS + ", R" + RT + ", #" + IMMEDIATE; break;
		case BLEZ:
			result = TYPE.toString() + " R" + RS + ", #" + IMMEDIATE; break;	
		case BLTZ:
			result = TYPE.toString() + " R" + RS + ", #" + IMMEDIATE; break;					
		case ADDIU:
			result = TYPE.toString() + " R" + RT + ", R" + RS + ", #" + IMMEDIATE; break;								
		case SLTI:
			result = TYPE.toString() + " R" + RT + ", R" + RS + ", #" + IMMEDIATE; break;
		case SLT:
			result = TYPE.toString() + " R" + RD + ", R" + RS + ", R" + RT; break;
		case SLTU:
			result = TYPE.toString() + " R" + RD + ", R" + RS + ", R" + RT; break;
		case SLL:									
			result = TYPE.toString() + " R" + RD + ", R" + RT + ", #" + SA; break;
		case SRL:
			result = TYPE.toString() + " R" + RD + ", R" + RT + ", #" + SA; break;
		case SRA:
			result = TYPE.toString() + " R" + RD + ", R" + RT + ", #" + SA; break;
		case SUB:
			result = TYPE.toString() + " R" + RD + ", R" + RS + ", R" + RT; break;
		case SUBU:
			result = TYPE.toString() + " R" + RD + ", R" + RS + ", R" + RT; break;
		case ADDU:
			result = TYPE.toString() + " R" + RD + ", R" + RS + ", R" + RT; break;
		case AND:
			result = TYPE.toString() + " R" + RD + ", R" + RS + ", R" + RT; break;
		case OR:
			result = TYPE.toString() + " R" + RD + ", R" + RS + ", R" + RT; break;
		case NOR:
			result = TYPE.toString() + " R" + RD + ", R" + RS + ", R" + RT; break;
		case XOR:
			result = TYPE.toString() + " R" + RD + ", R" + RS + ", R" + RT; break;
		case NOP:
			result = TYPE.toString(); break;
		default:
			result = "Instruction format not found.";
		}	
		return result;
	}
	
	/**
	 * Sets the immediate field if needed.
	 * 
	 * @return
	 */
	private int getImmediate2() {
		String sign = BINARY_STRING.substring(16, 17);	
		String imm = BINARY_STRING.substring(16,32);
		int result;
		
		imm += "00"; //shift left 2 bits
		if (sign.equals("0")){
			int immediate_pos = Integer.parseInt(imm, 2);
			result = immediate_pos;
		}
		else{
			imm = "11111111111111" + imm; //if negative sign extend to 32 bits
			int immediate_neg = Long.valueOf(imm, 2).intValue();
			result = immediate_neg;
		}
		return result;
	}
	

	/**
	 * Sets the immediate field if needed.
	 * 
	 * @return
	 */
	private int getImmediate1() {
		String sign = BINARY_STRING.substring(16, 17);	
		String imm = BINARY_STRING.substring(16,32);
		int result;
		
		if (sign.equals("0")){
			int immediate_pos = Integer.parseInt(imm, 2);
			result = immediate_pos;
		}
		else{
			short immediate_neg = Integer.valueOf(imm, 2).shortValue(); 
			result = immediate_neg;
		}	
		
		return result;
	}
	
	/**
	 * Sets the target field if needed.
	 * 
	 * @return
	 */
	private int getTarget() {
		String newTARGET = BINARY_STRING.substring(6, 32);;
		newTARGET += "00"; //shift left 2 bits
		int target_pos = Integer.parseInt(newTARGET, 2);
		return target_pos;
	}

	
	/**
	 * Sets the type of the instruction according to the opcode.
	 * 
	 * @throws InstException
	 * @return
	 */
	private Type getType() throws InstException {

		String opcode = BINARY_STRING.substring(0, 6);
		
		Type result = null;
		
		switch (opcode) {
		case "000000":		
			String function = BINARY_STRING.substring(26, 32);
	
			switch (function) {			
			case "100000": 	result = Type.ADD;	break;
			case "100001": 	result = Type.ADDU;	break;
			case "100100": 	result = Type.AND;	break;
			case "001101": 	result = Type.BREAK;break;
			case "100111": 	result = Type.NOR; 	break;
			case "100101": 	result = Type.OR;	break;
			case "000000": 	result = Type.SLL;	break;
			case "101010": 	result = Type.SLT;	break;
			case "101011": 	result = Type.SLTU;	break;
			case "000011": 	result = Type.SRA;	break;
			case "000010": 	result = Type.SRL;	break;
			case "100010": 	result = Type.SUB;	break;
			case "100011": 	result = Type.SUBU;	break;
			case "100110": 	result = Type.XOR;	break;
			}

			break;
			
		case "000010":		result = Type.J;	break;		
		case "000001":
			String rt = BINARY_STRING.substring(11, 16);
			
			switch (rt) {
			case "00001":	result = Type.BGEZ;	break;
			case "00000":	result = Type.BLTZ;	break;
			}
			
			break;
			
		case "000110":
			if (BINARY_STRING.substring(11, 16).equals("00000")) {
							result = Type.BLEZ;
			}
			else {throw new InstException(opcode);}
			
			break;
			
		case "000111":	
			if (BINARY_STRING.substring(11, 16).equals("00000")) {
							result = Type.BGTZ;
			}
			else {throw new InstException(opcode);}
			
			break;
		
		case "001000":		result = Type.ADDI;	break;
		case "001001":		result = Type.ADDIU;break;
		case "000100":		result = Type.BEQ;	break;
		case "000101":		result = Type.BNE;	break;
		case "100011":		result = Type.LW;	break;
		case "001010":		result = Type.SLTI;	break;
		case "101011":		result = Type.SW;	break;
		default:
			if (BINARY_STRING.equals("00000000000000000000000000000000")) {
							result = Type.NOP;
			}
			else { throw new InstException(opcode); }
			
			break;
		}
		return result;
	}

	/**
	 * Creates a string representation of the binary form of the instruction.
	 * The string can be accessed under BINARY_STRING.
	 * 
	 * @param startByte
	 * @param binary_inst
	 * @return
	 */
	private String getBinaryString(int startByte, byte[] binary_inst) {
		long inst = createInst(startByte, binary_inst);
		String result;
		
		if (inst < 0){
			result = String.format("%32s", Long.toBinaryString(inst)).replace(' ', '0').substring(32, 64);
		}
		else{
			result = String.format("%32s", Long.toBinaryString(inst)).replace(' ', '0');
		}	
		return result;
	}

	/**
	 * Creates a long representation of 32 bit instruction from the byte array of
	 * instructions provided.
	 * 
	 * @param startByte
	 * @param binary_inst
	 * @return
	 */
	private long createInst(int startByte, byte[] binary_inst) {
		long instruction = ((binary_inst[startByte]&0xFF) << 24) |
				((binary_inst[startByte+1]&0xFF) << 16) |
				((binary_inst[startByte+2]&0xFF) << 8)  |
				(binary_inst[startByte+3]&0xFF); 
		return instruction;
	}
	
	//TODO: fix the exception to print.
	@SuppressWarnings("serial")
	public class InstException extends Exception {

		public InstException (String msg) { super(msg); }
		public String toString() { return super.toString();}
		
	}
	
}
