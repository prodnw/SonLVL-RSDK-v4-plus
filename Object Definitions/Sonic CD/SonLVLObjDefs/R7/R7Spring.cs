using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class R7Spring : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[6];

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items.gif");
			sprites[0] = new Sprite(sheet.GetSection(84, 1, 32, 16), -16, -16);
			sprites[1] = new Sprite(sheet.GetSection(117, 1, 16, 32), 0, -16);
			sprites[2] = new Sprite(sheet.GetSection(175, 1, 16, 32), -16, -16);
			sprites[3] = new Sprite(sheet.GetSection(84, 59, 32, 16), -16, 0);
			sprites[4] = new Sprite(sheet.GetSection(84, 117, 32, 32), -8, -24);
			sprites[5] = new Sprite(sheet.GetSection(117, 165, 32, 32), -24, -24);
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
			return "";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			Sprite sprite = new Sprite(sprites[0]);
			
			int index = Math.Max(0, LevelData.Objects.IndexOf(obj) - 1);
			switch (LevelData.Objects[index].Name)
			{
				case "Spring Cage":
				{
					switch (LevelData.Objects[index].PropertyValue)
					{
						case 0:
						default:
							sprite = new Sprite(sprites[0]);
							break;
						case 1:
							sprite = new Sprite(sprites[2]);
							break;
						case 3:
							sprite = new Sprite(sprites[4]);
							break;
						case 2:
						case 4:
							sprite = new Sprite(sprites[0]);
							break;
						case 5:
							sprite = new Sprite(sprites[5]);
							break;
						case 6:
							sprite = new Sprite(sprites[1]);
							break;
					}
					
					goto case "R Spring Cage";
				}
				case "R Spring Cage":
				{
					int xdiff = (LevelData.Objects[index].X - obj.X);
					int ydiff = (LevelData.Objects[index].Y - obj.Y);
					if (Math.Abs(xdiff) < 32 && Math.Abs(ydiff) < 32)
						sprite.Offset(xdiff, ydiff);
					break;
				}
			}
			return sprite;
		}
	}
}