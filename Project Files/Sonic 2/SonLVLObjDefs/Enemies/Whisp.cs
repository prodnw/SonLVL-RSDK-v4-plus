using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Whisp : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[2];
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '3')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(34, 42, 24, 15), -12, -7);
				sprites[1] = new Sprite(sheet.GetSection(34, 58, 21, 6), -9, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(111, 317, 24, 15), -12, -7);
				sprites[1] = new Sprite(sheet.GetSection(110, 302, 21, 6), -9, -8);
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