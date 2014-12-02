package mipSim.pipeline;

import java.util.LinkedList;

public class ROB {
	
	private LinkedList <Integer> temporary;
	private LinkedList <Integer> finalized;
	
	public ROB() {
		temporary = new LinkedList <Integer> ();
		finalized = new LinkedList <Integer> ();
	}
	
	/**
	 * Sync the two internal buffers.
	 */
	public void sync() {
		
		while(!temporary.isEmpty()) {
			finalized.add(temporary.removeFirst());
		}
		temporary.clear();
	}
	
}
