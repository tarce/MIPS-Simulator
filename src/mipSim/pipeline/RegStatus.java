package mipSim.pipeline;

import mipSim.pipeline.parts.RegStatus_Entry;

public class RegStatus {
	private RegStatus_Entry [] temporary;
	private RegStatus_Entry [] finalized;
	
	/**
	 * Create Registers Status table.
	 */
	public RegStatus (int size) {
		temporary = new RegStatus_Entry [size];
		finalized = new RegStatus_Entry [size];
		
		int i;
		for (i = 0; i < temporary.length; i++) {
			temporary[i] = new RegStatus_Entry ();
		}
		
		for (i = 0; i < finalized.length; i++) {
			finalized[i] = new RegStatus_Entry ();
		}
	}

	/**
	 * Given a register number, returns whether or not the 
	 * register status table is busy at that register.
	 * 
	 * @param reg	The register
	 * @return		boolean true if it is busy, false otherwise
	 */
	public boolean isBusy (int reg) {
		return finalized[reg].isBusy();
	}
	
	public int getROB_ID (int reg) {
		return finalized[reg].getROB_ID();
	}
	
	/**
	 * Sets the Register Status table at a given register
	 * to busy.  Must include the ROB ID of where the result
	 * will be available.
	 * 
	 * @param reg		The register
	 * @param rob_id	The ROB ID
	 * @return			true if it sets the register, false otherwise
	 */
	public boolean setBusy (int reg, int rob_id) {
		if (!isBusy(reg)) {
			temporary[reg].setBusy(rob_id);
			return true;
		}
		return false;
	}
	
	/**
	 * Sets the register status table entry free.
	 * 
	 * @param reg	The register to be freed
	 */
	public void setFree (int reg) {
		temporary[reg].clear();
	}
	
	/**
	 * Sync the two internal buffers.
	 */
	public void sync() {
		
		for (int i = 0; i <= temporary.length - 1; i++) {
			finalized[i] = temporary[i];
		}

//TODO:		temporary.clear();
	}
	
	/**
	 * Prints the contents of the RegisterFile
	 */
	public void printContents() {
		
//		Collections.sort(finalized);
		
		System.out.print("Register Status:");
		
		int i = 0;
		for (; i < finalized.length - 1; i++) {
			System.out.println("Reg " + i + ": " + finalized[i].toString());
		}
		System.out.print("Reg " + i + ": " + finalized[finalized.length - 1].toString() + "\n");
	}

}
