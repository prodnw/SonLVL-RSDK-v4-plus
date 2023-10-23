using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class ForegroundPiece : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[14];
		private Sprite[] debug = new Sprite[14];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R5/Objects3.gif");
			sprites[0] = new Sprite(sheet.GetSection(165, 1, 52, 8), -26, -4);
			sprites[1] = new Sprite(sheet.GetSection(218, 1, 8, 52), -4, -26);
			sprites[2] = new Sprite(sheet.GetSection(165, 10, 32, 8), -16, -4);
			sprites[3] = new Sprite(sheet.GetSection(165, 10, 16, 8), -8, -4);
			sprites[4] = new Sprite(sheet.GetSection(227, 1, 8, 32), -4, -16);
			sprites[5] = new Sprite(sheet.GetSection(227, 1, 8, 16), -4, -8);
			sprites[6] = new Sprite(sheet.GetSection(165, 19, 40, 8), -20, -4);
			sprites[7] = new Sprite(sheet.GetSection(165, 28, 40, 8), -20, -4);
			sprites[8] = new Sprite(sheet.GetSection(236, 1, 8, 40), -4, -20);
			sprites[9] = new Sprite(sheet.GetSection(245, 1, 8, 40), -4, -20);
			sprites[10] = new Sprite(sheet.GetSection(165, 37, 40, 8), -20, -4);
			sprites[11] = new Sprite(sheet.GetSection(165, 46, 40, 8), -20, -4);
			sprites[12] = new Sprite(sheet.GetSection(236, 42, 8, 40), -4, -20);
			sprites[13] = new Sprite(sheet.GetSection(245, 42, 8, 40), -4, -20);
			
			for (int i = 0; i < sprites.Length; i++)
			{
				Rectangle bounds = sprites[i].Bounds;
				BitmapBits overlay = new BitmapBits(bounds.Size);
				overlay.DrawRectangle(6, 0, 0, bounds.Width - 1, bounds.Height - 1); // LevelData.ColorWhite
				debug[i] = new Sprite(overlay, bounds.X, bounds.Y);
			}
			
			properties[0] = new PropertySpec("Frame", typeof(int), "Extended",
				"Which sprite this object should display.", null, new Dictionary<string, int>
				{
					{ "Frame 1", 0 },
					{ "Frame 2", 1 },
					{ "Frame 3", 2 },
					{ "Frame 4", 3 },
					{ "Frame 5", 4 },
					{ "Frame 6", 5 },
					{ "Frame 7", 6 },
					{ "Frame 8", 7 },
					{ "Frame 9", 8 },
					{ "Frame 10", 9 },
					{ "Frame 11", 10 },
					{ "Frame 12", 11 },
					{ "Frame 13", 12 },
					{ "Frame 14", 13 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return "Frame " + (subtype + 1);
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue];
		}
	}
}