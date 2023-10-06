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
		
		public override string SubtypeName(byte subtype)
		{
			return null;
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
			int index = 0;
			
			// If obj[-1] is a spring cage, then let's offset ourselves a little to match its position and match its rotation
			ObjectEntry other = LevelData.Objects[Math.Max(0, LevelData.Objects.IndexOf(obj) - 1)];
			switch (other.Name)
			{
				case "Spring Cage":
				{
					switch (other.PropertyValue)
					{
						case 0:
							index = 0; break;
						case 1:
							index = 2; break;
						case 3:
							index = 4; break;
						case 4:
							index = 0; break;
						case 5:
							index = 5; break;
						case 2:
						case 6:
						default:
							index = 1; break;
					}
					
					goto case "R Spring Cage";
				}
				case "R Spring Cage":
				{
					int xdiff = (other.X - obj.X);
					int ydiff = (other.Y - obj.Y);
					if (Math.Abs(xdiff) < 32 && Math.Abs(ydiff) < 32)
						return new Sprite(sprites[index], xdiff, ydiff);
					break;
				}
			}
			
			return sprites[0];
		}
	}
}