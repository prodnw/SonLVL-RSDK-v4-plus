using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class VPlatformLarge : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];

		public override void Init(ObjectData data)
		{
			BitmapBits sheet;
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A':
				default:
					sheet = LevelData.GetSpriteSheet("R5/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 84, 96, 32), -48, -16);
					sprites[1] = new Sprite(sheet.GetSection(1, 191, 96, 16), -48, -16);
					break;
				case 'B':
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[1] = new Sprite(sheet.GetSection(159, 131, 96, 16), -48, -16);
					break;
				case 'C':
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[1] = new Sprite(sheet.GetSection(159, 165, 96, 16), -48, -16);
					break;
				case 'D':
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[1] = new Sprite(sheet.GetSection(159, 199, 96, 16), -48, -16);
					break;
			}
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 2, 3, 5 }); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0:
					return "Downwards";
				case 2:
					return "Downwards (Conveyor)";
				case 3:
					return "Upwards";
				case 5:
					return "Upwards (Conveyor)";
				case 1:
				case 4:
				default:
					return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			List<Sprite> sprs = new List<Sprite>();
			Sprite tmp = new Sprite(sprites[0]);
			sprs.Add(tmp);
			if (subtype == 2 || subtype == 5)
			{
				tmp = new Sprite(sprites[1]);
				sprs.Add(tmp);
			}
			return new Sprite(sprs.ToArray());
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}