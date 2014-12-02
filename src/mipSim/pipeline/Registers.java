package mipSim.pipeline;

import mipSim.pipeline.parts.RegStatus_Entry;

public class Registers {
	private int [] temporary;
	private int [] finalized;
	
	public Registers () {
		temporary = new int [32];
		finalized = new int [32];
		
		int i;
		for (i = 0; i < temporary.length; i++) {
			temporary[i] = 0;
		}
		
		for (i = 0; i < finalized.length; i++) {
			finalized[i] = 0;
		}
	}
	
}
