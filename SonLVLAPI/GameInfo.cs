using System.Collections.Generic;

namespace SonicRetro.SonLVL.API
{
	public class GameInfo
	{
		public string EXEFile { get; set; }
		public string DataFile { get; set; }
		public OriginsGames OriginsGame { get; set; }
		public EngineVersion RSDKVer { get; set; }
		public bool IsV5U { get; set; }
		[IniCollection(IniCollectionMode.IndexOnly)]
		public Dictionary<string, LevelInfo> Levels { get; set; }

		[IniIgnore]
		public bool IsOrigins { get => OriginsGame != OriginsGames.Invalid; }

		public static GameInfo Load(string filename) => IniSerializer.Deserialize<GameInfo>(filename);

		public void Save(string filename) => IniSerializer.Serialize(this, filename);
	}

	public class LevelInfo
	{
		[IniCollection(IniCollectionMode.NoSquareBrackets, StartIndex = 1)]
		[IniName("ExtraPalette")]
		public List<string> ExtraPalettes { get; set; }
	}
}
