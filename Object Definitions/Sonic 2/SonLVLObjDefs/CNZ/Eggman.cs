using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class Eggman : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '4')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("CNZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(76, 140, 44, 16), -28, -16);
				sprites[1] = new Sprite(sheet.GetSection(175, 183, 80, 72), -40, -40);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 5, 44, 16), -28, -16);
				sprites[1] = new Sprite(sheet.GetSection(232, 112, 80, 72), -40, -40);
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
			// All point here for simplicity's sake
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
			return SubtypeImage(0);
		}
	}
}