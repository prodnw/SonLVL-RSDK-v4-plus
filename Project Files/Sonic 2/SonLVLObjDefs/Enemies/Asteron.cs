using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Asteron : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '9')
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(223, 1, 32, 28), -16, -14);
			}
			else
			{
				// (SCZ mission ends up here too)
				
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(843, 289, 32, 28), -16, -14);
			}
			
			// even if prop val is set, it isn't used
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