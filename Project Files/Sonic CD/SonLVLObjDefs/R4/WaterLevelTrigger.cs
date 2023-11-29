using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class WaterLevelTrigger : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[10];
		private Sprite debug; // only used on prop val 2
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			
			Sprite[] frames = new Sprite[13];
			
			// this object's kinda weird and i wasn't sure what to do with it.. so i just copied RE2's render for it (thanks Leon :D)
			frames[0] = new Sprite(sheet.GetSection(1, 50, 8, 11)); // 0
			frames[1] = new Sprite(sheet.GetSection(10, 50, 8, 11)); // 1
			frames[2] = new Sprite(sheet.GetSection(19, 50, 8, 11)); // 2
			frames[3] = new Sprite(sheet.GetSection(28, 50, 8, 11)); // 3
			frames[4] = new Sprite(sheet.GetSection(1, 62, 8, 11)); // 4
			frames[5] = new Sprite(sheet.GetSection(10, 62, 8, 11)); // 5
			frames[6] = new Sprite(sheet.GetSection(19, 62, 8, 11)); // 6
			frames[7] = new Sprite(sheet.GetSection(28, 62, 8, 11)); // 7
			frames[8] = new Sprite(sheet.GetSection(1, 74, 8, 11)); // 8
			frames[9] = new Sprite(sheet.GetSection(10, 74, 8, 11)); // 9
			
			frames[10] = new Sprite(sheet.GetSection(10, 98, 8, 8), 0, 2); // Debug Y
			
			frames[11] = new Sprite(sheet.GetSection(173, 67, 16, 16), -8, -8); // "T"
			frames[12] = new Sprite(sheet.GetSection(156, 67, 16, 16), -8, -8); // "L"
			
			// default box
			sprites[9] = new Sprite(new Sprite(frames[11], -8, -8),
			                        new Sprite(frames[12], -8,  8),
			                        new Sprite(frames[12],  8, -8),
			                        new Sprite(frames[11],  8,  8));
			
			// left, right
			// if 0, nothing's drawn
			int[] levels = new int[] {
				0, 1332,
				1332, 1920,
				0, 960,
				0, 1280,
				480, 272,
				272, 576,
				1296, 912,
				912, 1040,
				1040, 1280
			};
			
			for (int i = 0; i < levels.Length; i += 2)
			{
				Sprite left = DrawNumbers(frames, levels[i]);
				left.Offset(-26, -14);
				
				Sprite right = DrawNumbers(frames, levels[i+1]);
				right.Offset(right.Width + 9, 4);
				
				sprites[i / 2] = new Sprite(sprites[9], left, right);
			}
			
			BitmapBits bitmap = new BitmapBits(256, 256);
			bitmap.DrawRectangle(6, 0, 0, 255, 255); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -128, -128);
			
			properties[0] = new PropertySpec("Trigger", typeof(int), "Extended",
				"What Y position this object should bring the water to.", null, new Dictionary<string, int>
				{
					{ "Proximity - 1332", 0 }, // "Proximity" sounds dumb but then again, this script as a whole is kinda dumb too
					{ "Left - 1332 | Right - 1920", 1 },
					{ "Collision - 960", 2 },
					{ "Right - 1280", 3 },
					{ "Left - 480 | Right - 272", 4 },
					{ "Left - 272 | Right - 576", 5 },
					{ "Left - 1296 | Right - 912", 6 },
					{ "Left - 912 | Right - 1040", 7 },
					{ "Left - 1040 | Right - 1280", 8 },
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		private Sprite DrawNumbers(Sprite[] numbers, int value)
		{
			// there's def a better way to do this, but this works fine enough
			// probably overcomplicating it, but oh well
			
			Sprite sprite = new Sprite();
			
			int x = 0;
			while (value > 0)
			{
				int frame = (value % 10);
				sprite = new Sprite(sprite, (new Sprite(numbers[frame], x, 0)));
				
				x -= 8;
				value /= 10;
				
				if (value == 0) // Let's add the Y now
					sprite = new Sprite(sprite, (new Sprite(numbers[10], x, 0)));
			}
			
			return sprite;
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Set water to Y - 1332";
				case 1: return "Left Y - 1332 | Right Y - 1920";
				case 2: return "Collision sets water to Y - 960";
				case 3: return "Right Y - 1280";
				case 4: return "Left Y - 480 | Right Y - 272";
				case 5: return "Left Y - 272 | Right Y - 576";
				case 6: return "Left Y - 1296 | Right Y - 912";
				case 7: return "Left Y - 912 | Right Y - 1040";
				case 8: return "Left Y - 1040 | Right Y - 1280";
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[9]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype > 8) ? 9 : subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue > 8) ? 9 : obj.PropertyValue];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue == 2) ? debug : null;
		}
	}
}