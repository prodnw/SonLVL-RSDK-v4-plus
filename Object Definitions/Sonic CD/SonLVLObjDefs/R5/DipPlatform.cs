using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class DipPlatform : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[6];
		private ReadOnlyCollection<byte> subtypes = new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3, 4, 5 });

		public override void Init(ObjectData data)
		{
			BitmapBits sheet;
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A':
				default:
					sheet = LevelData.GetSpriteSheet("R5/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 51, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(34, 51, 64, 32), -32, -16);
					sprites[2] = new Sprite(sheet.GetSection(1, 84, 96, 32), -48, -16);
					sprites[3] = new Sprite(sheet.GetSection(65, 208, 32, 16), -16, -16);
					sprites[4] = new Sprite(sheet.GetSection(1, 208, 64, 16), -32, -16);
					sprites[5] = new Sprite(sheet.GetSection(1, 191, 96, 16), -48, -16);
					break;
				case 'B':
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 170, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(34, 170, 64, 32), -32, -16);
					sprites[2] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[3] = new Sprite(sheet.GetSection(223, 148, 32, 16), -16, -16);
					sprites[4] = new Sprite(sheet.GetSection(159, 148, 64, 16), -32, -16);
					sprites[5] = new Sprite(sheet.GetSection(159, 131, 96, 16), -48, -16);
					break;
				case 'D':
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 170, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(34, 170, 64, 32), -32, -16);
					sprites[2] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[3] = new Sprite(sheet.GetSection(223, 182, 32, 16), -16, -16);
					sprites[4] = new Sprite(sheet.GetSection(159, 182, 64, 16), -32, -16);
					sprites[5] = new Sprite(sheet.GetSection(159, 165, 96, 16), -48, -16);
					break;
				case 'C':
					sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 170, 32, 32), -16, -16);
					sprites[1] = new Sprite(sheet.GetSection(34, 170, 64, 32), -32, -16);
					sprites[2] = new Sprite(sheet.GetSection(1, 203, 96, 32), -48, -16);
					sprites[3] = new Sprite(sheet.GetSection(223, 216, 32, 16), -16, -16);
					sprites[4] = new Sprite(sheet.GetSection(159, 216, 64, 16), -32, -16);
					sprites[5] = new Sprite(sheet.GetSection(159, 199, 96, 16), -48, -16);
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
			switch (subtype)
			{
				case 0:
					return "Small";
				case 1:
					return "Medium";
				case 2:
					return "Large";
				case 3:
					return "Small Conveyor";
				case 4:
					return "Medium Conveyor";
				case 5:
					return "Large Conveyor";
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
			Sprite tmp;
			if (subtype > 3)
			{
				tmp = new Sprite(sprites[subtype-3]);
				sprs.Add(tmp);
			}
			tmp = new Sprite(sprites[subtype]);
			sprs.Add(tmp);
			return new Sprite(sprs.ToArray());
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}