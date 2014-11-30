package mipSim.pipeline.units;

public class RegFile {
	private Integer [] temporary;
	private Integer [] finalized;
	
	/**
	 * Create Registers initialized to 0.
	 */
	public RegFile() {
		temporary = new Integer [32];
		finalized = new Integer [32];
		
		int i;
		for (i = 0; i < temporary.length; i++) {
			temporary[i] = 0;
		}
		
		for (i = 0; i < finalized.length; i++) {
			finalized[i] = 0;
		}
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
		
		System.out.print("RF:");
		
		for (int i = 0; i < finalized.length - 1; i++) {
			System.out.print(finalized[i]+ ",");
		}
		System.out.print(finalized[finalized.length - 1] + "\n");
	}

}
