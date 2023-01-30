using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Rexon : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[3];
			
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '5')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(143, 123, 23, 16), -19, -6); // head
				sprites[1] = new Sprite(sheet.GetSection(52, 38, 16, 16), -8, -8); // body piece
				sprites[2] = new Sprite(sheet.GetSection(91, 105, 32, 16), -16, -8); // shell
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				
				// using fixed frames
				sprites[0] = new Sprite(sheet.GetSection(469, 289, 23, 16), -19, -6);
				sprites[1] = new Sprite(sheet.GetSection(517, 289, 16, 16), -8, -8);
				sprites[2] = new Sprite(sheet.GetSection(436, 289, 32, 16), -16, -8);
			}
			
			// Assemble the Rexon sprite now
			
			int[] offsets = {
				-31,  4,  // piece 4, spr 1
				-29, -11, // piece 3, spr 1
				-25, -25, // piece 2, spr 1
				-20, -39, // piece 1, spr 1
				-16, -54, // head, spr 0
				 0,   0   // shell, spr 2
			};
			
			Sprite[] sprs = new Sprite[6];
			for (int i = 0; i < 6; i++)
			{
				Sprite spr = new Sprite(sprites[((i == 5) ? 2 : (i == 4) ? 0 : 1)]);
				spr.Offset(offsets[i * 2], offsets[(i * 2) + 1]);
				sprs[i] = spr;
			}
			
			sprite = new Sprite(sprs);
			
			// btw prop val is unused, only ported Rexons have it anyways
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