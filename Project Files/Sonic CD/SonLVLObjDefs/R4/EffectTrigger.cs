using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class EffectTrigger : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[3];
		private Sprite[] debug = new Sprite[14];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			sprites[0] = new Sprite(sheet.GetSection(173, 67, 16, 16), -8, -8);
			
			Sprite[] frames = new Sprite[13];
			
			frames[0] = new Sprite(sheet.GetSection(1, 50, 8, 11), 0, 0); // 0
			frames[1] = new Sprite(sheet.GetSection(10, 50, 8, 11), 0, 0); // 1
			frames[2] = new Sprite(sheet.GetSection(19, 50, 8, 11), 0, 0); // 2
			frames[3] = new Sprite(sheet.GetSection(28, 50, 8, 11), 0, 0); // 3
			frames[4] = new Sprite(sheet.GetSection(1, 62, 8, 11), 0, 0); // 4
			frames[5] = new Sprite(sheet.GetSection(10, 62, 8, 11), 0, 0); // 5
			frames[6] = new Sprite(sheet.GetSection(19, 62, 8, 11), 0, 0); // 6
			frames[7] = new Sprite(sheet.GetSection(28, 62, 8, 11), 0, 0); // 7
			frames[8] = new Sprite(sheet.GetSection(1, 74, 8, 11), 0, 0); // 8
			frames[9] = new Sprite(sheet.GetSection(10, 74, 8, 11), 0, 0); // 9
			
			frames[10] = new Sprite(sheet.GetSection(10, 98, 8, 8), 0, 2); // Debug Y
			
			frames[11] = new Sprite(sheet.GetSection(173, 67, 16, 16), -8, -8); // "T"
			frames[12] = new Sprite(sheet.GetSection(156, 67, 16, 16), -8, -8); // "L"
			
			Sprite box = new Sprite(new Sprite(frames[11], -8, -8),
			                        new Sprite(frames[12], -8,  8),
			                        new Sprite(frames[12],  8, -8),
			                        new Sprite(frames[11],  8,  8));
			
			int[] levels = {
				644, 612,
				1332, 1920
			};
			
			for (int i = 0; i < levels.Length; i += 2)
			{
				Sprite left = DrawNumbers(frames, levels[i]);
				left.Offset(-26, -14);
				
				Sprite right = DrawNumbers(frames, levels[i+1]);
				right.Offset(right.Width + 9, 4);
				
				sprites[(i / 2) + 1] = new Sprite(box, left, right);
			}
			
			for (int i = 0; i < 14; i++)
				debug[i] = new Sprite();
			
			double[] angles = {
				1.5, // bottom entry
				0.5, // top entry
				1.0, // left turn
				0.0, // right turn
				1.5, // upwards turn
				0.5  // downwards turn
			};
			
			BitmapBits bitmap;
			
			// just like every other time a tube thing is used it's kinda hard to see the arrow this time too, but oh well
			for (int i = 0; i < angles.Length; i++)
			{
				bitmap = new BitmapBits(64, 64);
				bitmap.DrawArrow(24, 32, 32, 32 + (int)(Math.Cos(angles[i] * Math.PI) * 32), 32 + (int)(Math.Sin(angles[i] * Math.PI) * 32));
				debug[i + 4] = new Sprite(bitmap, -32, -32);
			}
			
			// make the downwards exit one the same as the normal downwards one
			debug[12] = debug[9];
			
			bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(6, 0, 0, 31, 31);
			debug[10] = debug[11] = new Sprite(bitmap, -16, -16); // set velocity boxes
			
			properties[0] = new PropertySpec("Effect", typeof(int), "Extended",
				"Which effect this object should trigger.", null, new Dictionary<string, int>
				{
					{ "Exterior BG - < X 640", 0 },
					{ "Exterior BG Left | Interior BG Right ", 1 },
					{ "Water: 644 Left | 612 Right", 2 },
					{ "Water: 1332 Left | 1920 Right", 13 },
					// (3 doesn't exist)
					{ "Pipe - Bottom Entry", 4 }, // done
					{ "Pipe - Top Entry", 5 }, // done
					{ "Pipe - Left", 6 }, // done
					{ "Pipe - Right", 7 }, // done
					{ "Pipe - Upwards", 8 }, // done
					{ "Pipe - Downwards", 9 }, // done
					{ "Pipe - Reset XVel", 10 }, // done
					{ "Pipe - Reset YVel", 11 }, // done
					{ "Pipe - Downwards Exit", 12 } // done
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		private Sprite DrawNumbers(Sprite[] numbers, int value)
		{
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
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 13, 4, 5, 6, 7, 8, 9, 10, 11, 12}); }
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
			return sprites[(subtype == 2) ? 1 : (subtype == 13) ? 2 : 0];
		}
		
		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 2) ? 1 : (obj.PropertyValue == 13) ? 2 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (obj.PropertyValue > 13)
				return null;
			
			return debug[obj.PropertyValue];
		}
	}
	
	public static class BitmapBitsExtensions
	{
		public static void DrawArrow(this BitmapBits bitmap, byte index, int x1, int y1, int x2, int y2)
		{
			bitmap.DrawLine(index, x1, y1, x2, y2);
			
			double angle = Math.Atan2(y1 - y2, x1 - x2);
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle + 0.40) * 10), y2 + (int)(Math.Sin(angle + 0.40) * 10));
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle - 0.40) * 10), y2 + (int)(Math.Sin(angle - 0.40) * 10));
		}
	}
}