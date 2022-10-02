using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HTZ
{
	class Eggman : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '5')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(66, 108, 17, 7), -15, -10);
				sprites[1] = new Sprite(sheet.GetSection(1, 210, 64, 45), -32, -12);
				sprites[2] = new Sprite(sheet.GetSection(9, 186, 40, 24), -24, -36);
			}
			else
			{
				// he looks weird in the level, but at least he's using the right sheet
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1007, 34, 17, 7), -15, -10);
				sprites[1] = new Sprite(sheet.GetSection(415, 154, 64, 45), -32, -12);
				sprites[2] = new Sprite(sheet.GetSection(423, 130, 40, 24), -24, -36);
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
			for (int i = 0; i < 3; i++)
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