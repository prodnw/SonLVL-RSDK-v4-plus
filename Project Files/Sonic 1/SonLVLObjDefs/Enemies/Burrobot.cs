using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Burrobot : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '4':
				case '6': // DLC_Zone06, from Origins
				default:
					sprite = new Sprite(LevelData.GetSpriteSheet("LZ/Objects.gif").GetSection(92, 68, 24, 46), -12, -24);
					break;
				case '7':
					sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(88, 217, 24, 46), -12, -24);
					break;
			}
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
	}
}