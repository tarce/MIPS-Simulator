package mipSim.pipeline;

import mipSim.instructions.Instruction;
import mipSim.pipeline.parts.RS_Entry;

public class RS {
	private RS_Entry [] temporary;
	private RS_Entry [] finalized;
	
	public RS (int size) {
		temporary = new RS_Entry [size];
		finalized = new RS_Entry [size];
		
		int i;
		for (i = 0; i < temporary.length; i++) {
			temporary[i] = new RS_Entry ();
		}
		
		for (i = 0; i < finalized.length; i++) {
			finalized[i] = new RS_Entry ();
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
	
	/**
	 * Tries to put an instruction in the reservation station.
	 * If successful it will return true, otherwise false.
	 * @param i		The instruction
	 * @return		True if successfully put
	 */
	public void update (int pos, Instruction.Type op, Integer vj, Integer vk, Integer qj, Integer qk, int rob_id, Integer a) {

		temporary[pos].clear();
		temporary[pos].updateEntry(op, vj, vk, qj, qk, rob_id, a);
	
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
	 * Prints the contents of the Reservation Station
	 */
	public void printContents() {
		
//		Collections.sort(finalized);
		
		System.out.println("Resevation Station:");
		
		int i = 0;
		for (; i < finalized.length - 1; i++) {
			System.out.println("RS Entry " + i + ": " + finalized[i].toString());
		}
		System.out.print("RS Entry " + i + ": " + finalized[finalized.length - 1].toString() + "\n");
	}
}
