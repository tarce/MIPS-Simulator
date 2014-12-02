package mipSim.pipeline.parts;

public class RS_Entry {
	
	private boolean BUSY;
	private String OP;
	private Integer Vj;
	private Integer Vk;
	private Integer Qj;
	private Integer Qk;
	private int ROB_ID;		// the ROB entry that will produce the data (0 indicates no ROB)
	private Integer A;
	
	public RS_Entry () {
		BUSY = false;
		OP = null;
		Vj = null;
		Vk = null;
		Qj = null;
		Qk = null;
		ROB_ID = 0;
		A = null;
	}
	
	public void clear () {
		BUSY = false;
		OP = null;
		Vj = null;
		Vk = null;
		Qj = null;
		Qk = null;
		ROB_ID = 0;
		A = null;
	}
	
	public boolean isBusy () {
		return BUSY;
	}
	
	public void setOP (String op) {
		OP = op;
	}
	
	public String getOP () {
		return OP;
	}
	
	public String toString () {
		return "Busy: " + BUSY + " OP: " + OP + 
				" Vj: " + Vj + " Vk: " + Vk + 
				" Qj: " + Qj + " Qk: " + Qk + 
				" ROB ID: " + ROB_ID + " A: " + A;
	}

}
