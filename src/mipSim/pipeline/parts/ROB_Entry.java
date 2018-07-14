package mipSim.pipeline.parts;

import mipSim.instructions.Instruction;

public class ROB_Entry {
	private boolean BUSY;
	private Instruction INSTR;
	private State STATE;
	private int DEST;
	private Integer VALUE;
	
	public static enum State {
		ISSUE, 
		EXECUTE, 
		WRITE_RESULT,
		COMMIT
	}
	
	public ROB_Entry () {
		BUSY = false;
		INSTR = null;
		STATE = null;
		DEST = -1;
		VALUE = null;
	}
	
	public boolean isBusy () {
		return BUSY;
	}
	
	public Integer getValue () {
		return VALUE;
	}
	
}
