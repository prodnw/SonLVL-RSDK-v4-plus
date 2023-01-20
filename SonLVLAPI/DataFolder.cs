using System.IO;

namespace SonicRetro.SonLVL.API
{
	class DataFolder : RSDKv3_4.IDataPack
	{
		string basefolder;

		public DataFolder(string folder)
		{
			basefolder = folder;
		}

		public bool FileExists(string fileName) => File.Exists(Path.Combine(basefolder, fileName));

		public byte[] GetFileData(string fileName) => File.ReadAllBytes(Path.Combine(basefolder, fileName));

		public bool TryGetFileData(string fileName, out byte[] fileData)
		{
			string fullpath = Path.Combine(basefolder, fileName);
			if (File.Exists(fullpath))
			{
				fileData = File.ReadAllBytes(fullpath);
				return true;
			}
			fileData = null;
			return false;
		}
	}
}
