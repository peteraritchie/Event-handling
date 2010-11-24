namespace pvc.Projections
{
	public interface IChecksumStorage
	{
		long GetChecksumByName(string checksum);
		void SaveChecksum(string name, long checksum);
	}
}