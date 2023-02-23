using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Crawlton : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[2];
			
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '6')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MCZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(152, 114, 24, 16), -16, -8);
				sprites[1] = new Sprite(sheet.GetSection(135, 114, 16, 15), -8, -8);
			}
			else
			{
				// broken frames btw
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(152, 114, 24, 16), -16, -8);
				sprites[1] = new Sprite(sheet.GetSection(135, 114, 16, 15), -8, -8);
			}
			
			sprite = new Sprite(sprites);
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