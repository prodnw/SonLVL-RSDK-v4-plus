using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Clucker : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '1')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("SCZ/Objects.gif");
				sprites[1] = new Sprite(sheet.GetSection(9, 223, 32, 32), -16, -16);
				sprites[0] = new Sprite(sheet.GetSection(1, 246, 8, 7), -24, 7);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[1] = new Sprite(sheet.GetSection(845, 256, 32, 32), -16, -16);
				sprites[0] = new Sprite(sheet.GetSection(837, 279, 8, 7), -24, 7);
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
			get { return SubtypeImage(0); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 2; i++)
			{
				Sprite sprite = new Sprite(sprites[i]);
				sprs.Add(sprite);
			}
			return new Sprite(sprs.ToArray());
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}