package mipSim.pipeline;

import mipSim.instructions.*;

import java.util.LinkedList;


public class InstQueue {
	public LinkedList<Instruction> temporary;
	public LinkedList<Instruction> finalized;
	
	/**
	 * Constructor.
	 */
	public InstQueue() {
		temporary = new LinkedList<Instruction>();
		finalized = new LinkedList<Instruction>();
	}
	
	/**
	 * Returns whether or not the buffer is empty.
	 * 
	 * @return true if buffer is empty, false otherwise
	 */
	public boolean isEmpty() {
		return finalized.isEmpty();
	}

	/**
	 * Returns the instruction to be used in the next stage.
	 * 
	 * @return Instruction
	 */
	public Instruction getTop() {
		if (!isEmpty()) {
			return finalized.getFirst();
		}
		return null;
	}
	
	/**
	 * Removes the instruction that is to be put in the next stage.
	 * Note: call getTop() first.
	 * 
	 * @return true if instruction on top was removed, false otherwise
	 */
	public boolean removeTop() {
		if (!isEmpty()) {
			finalized.removeFirst();
			return true;
		}
		return false;
	}
	
	/**
	 * Synchronizes the two internal buffers for keeping time step.
	 * 
	 */
	public void sync() {

		while(!temporary.isEmpty()) {
			finalized.add(temporary.removeFirst());
		}
		temporary.clear();
	}
	
	/**
	 * Puts an instruction in the instruction queue.
	 * 
	 * @param i - the instruction
	 */
	public void putToken(Instruction i) {
		temporary.add(i);
	}
	
	/**
	 * Prints the contents of the buffer.
	 */
	public void printContents() {
		
//		Collections.sort(finalized);
		
		System.out.print("IB:");
		
		if (finalized.isEmpty()) {
			System.out.print("\n");
			return;
		}
		
		for (int i = 0; i < finalized.size() - 1; i++) {
			System.out.print(finalized.get(i).toString() + ",");
		}
		System.out.print(finalized.get(finalized.size()-1).toString() + "\n");
	}
	
}
