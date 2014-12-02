package mipSim.pipeline.parts;

public class RegStatus_Entry {
	private boolean BUSY;	// register is in use by an instruction that will be writing it
	private int ROB_ID;		// the ROB entry that will produce the data (0 indicates no ROB)

	public RegStatus_Entry () {
		BUSY = false;
		ROB_ID = 0;
	}
	
	public void clear () {
		BUSY = false;
		ROB_ID = 0;
	}
	
	public void setBusy (int rob_id) {
		ROB_ID = rob_id + 1;	// this assumes rob_ids incoming can be 0, but are actually the first
	}
	
	public boolean isBusy () {
		return BUSY;
	}
	
	public int getROB_ID () {
		return ROB_ID;
	}
	
	public String toString () {
		return "Busy: " + BUSY + " ROB_ID: " + ROB_ID;
		
	}
}
