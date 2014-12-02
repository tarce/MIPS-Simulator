package mipSim.pipeline;

public class Registers {
	private int [] temporary;
	private int [] finalized;
	
	public Registers (int size) {
		temporary = new int [size];
		finalized = new int [size];
		
		int i;
		for (i = 0; i < temporary.length; i++) {
			temporary[i] = 0;
		}
		
		for (i = 0; i < finalized.length; i++) {
			finalized[i] = 0;
		}
	}
	
	public int get (int pos) {
		return finalized[pos];
	}
	
}
