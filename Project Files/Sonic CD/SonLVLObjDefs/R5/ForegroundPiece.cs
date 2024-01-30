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
			
			// These names are.. kinda bad tbh
			// i think this is the only time i ever refer to the "GF" and "BF" as such?
			// ("Good Future" and "Bad Future" btw, not, like, "girlfriend" and "boyfriend"-)
			// And then each frame is sorted by letters.. this kinda sucks but i'm not really sure what else to do about it?
			// it used to just be "Frame 1"-"Frame 14", honestly i think that might've been better then trying to organise them based on time period?
			// i kind of have an idea to restrict the sprites to the ones only appropriate for the time period and silently leave the rest off the list, but that feels like
			// i might be overstepping the bounds of a level editor, so..
			// i guess we're just stuck with this for now..?
			properties[0] = new PropertySpec("Frame", typeof(int), "Extended",
				"Which sprite this object should display.", null, new Dictionary<string, int>
				{
					{ "Present: H", 0 }, // Horizontal
					{ "Present: V", 1 }, // Vertical
					{ "Past: H", 2 }, // Horizontal
					{ "Past: H (Small)", 3 }, // Horizontal (Small)
					{ "Past: V", 4 }, // Vertical
					{ "Past: V (Small)", 5 }, // Vertical (Small)
					{ "GF: H", 6 }, // Horizontal
					{ "GF: H (Flip)", 7 }, // Horizontal (Flipped)
					{ "GF: V", 8 }, // Vertical
					{ "GF: V (Flip)", 9 }, // Vertical (Flipped)
					{ "BF: H", 10 }, // Horizontal
					{ "BF: H (Flip)", 11 }, // Horizontal (Flipped)
					{ "BF: V", 12 }, // Vertical
					{ "BF: V (Flip)", 13 } // Vertical (Flipped)
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
			return properties[0].Enumeration.GetKey(subtype);
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