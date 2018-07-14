package mipSim.instructions;

public class Data {
	
	public String BINARY_STRING;
	public int VALUE;
	
	public Data (int startByte, byte[] binary_data) {
		BINARY_STRING = this.getBinaryString(startByte, binary_data);
	}
		
	/**
	 * Returns a string representing the data.  Mainly used for debug purposes.
	 * 
	 * @return
	 */
	public String toString() {
		return BINARY_STRING + " " + VALUE;
	}
	
	/**
	 * Creates a string representation of the binary form of the data.
	 * The string can be accessed under BINARY_STRING.
	 * 
	 * @param startByte
	 * @param binary_inst
	 * @return
	 */	
	private String getBinaryString(int startByte, byte[] binary_inst) {
		long inst = createData(startByte, binary_inst);
		String result;
		
		if (inst < 0){
			result = String.format("%32s", Long.toBinaryString(inst)).replace(' ', '0').substring(32, 64);
		}
		else{
			result = String.format("%32s", Long.toBinaryString(inst)).replace(' ', '0');
		}	
		return result;
	}
	
	/**
	 * Creates a long representation of 32 bit data from the byte array of
	 * mipSim.instructions provided.  Also sets the numerical value for the data.
	 * 
	 * @param startByte
	 * @param binary_inst
	 * @return
	 */
	private long createData(int startByte, byte[] binary_data) {
		long data = ((binary_data[startByte]&0xFF) << 24) |
				((binary_data[startByte+1]&0xFF) << 16) |
				((binary_data[startByte+2]&0xFF) << 8)  |
				(binary_data[startByte+3]&0xFF); 
		VALUE = (int) data;
		return data;
	}
	
}
