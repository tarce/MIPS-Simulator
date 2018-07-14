package mipSim.pipeline;

import mipSim.pipeline.parts.ROB_Entry;
import mipSim.pipeline.parts.RS_Entry;

public class ROB {
	
	private ROB_Entry [] temporary;
	private ROB_Entry [] finalized;
	
	public ROB(int size) {
		temporary = new ROB_Entry [size];
		finalized = new ROB_Entry [size];
		
		int i;
		for (i = 0; i < temporary.length; i++) {
			temporary[i] = new ROB_Entry ();
		}
		
		for (i = 0; i < finalized.length; i++) {
			finalized[i] = new ROB_Entry ();
		}
	}
	
	/**
	 * Used to try to get a slot in the RS.
	 * If no slot available returns -1.
	 * 
	 * @return	Slot number free or -1 if no slot
	 */
	public int getFreeSlot () {
		
		for (int i = 0; i < finalized.length; i++) {
			if (!finalized[i].isBusy()) {
				return i;
			}
		}
		
		return -1;	//indicates no slot is free
	}
	
	public Integer getValue (int pos) {
		return finalized[pos].getValue();
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
	 * Prints the contents of the ROB
	 */
	public void printContents() {
		
//		Collections.sort(finalized);
		
		System.out.println("Reorder Buffer:");
		
		int i = 0;
		for (; i < finalized.length - 1; i++) {
			System.out.println("ROB Entry " + i + ": " + finalized[i].toString());
		}
		System.out.print("ROB Entry " + i + ": " + finalized[finalized.length - 1].toString() + "\n");
	}
	
}
