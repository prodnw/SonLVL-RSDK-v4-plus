using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class Ramp3D : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[5];
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			
			Sprite[] frames = new Sprite[11];
			
			// this obj uses hardcoded positions to set y bounds, let's just give up and draw those numbers
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
			
			sprites[0] = new Sprite(sheet.GetSection(173, 67, 16, 16), -8, -8); // "T"
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(6, 0, 0, 31, 31);
			sprites[1] = new Sprite(sprites[0], new Sprite(bitmap, -16, -16));
			
			bitmap = new BitmapBits(32, 144);
			bitmap.DrawRectangle(6, 0, 0, 31, 143);
			debug = new Sprite(bitmap, -16, -128);
			
			// frame id, left, right
			int[] levels = new int[] {
				2, 1280, 1024,
				3, 1024, 792,
				4, 792, 1024
			};
			
			for (int i = 0; i < levels.Length; i += 3)
			{
				Sprite left = DrawNumbers(frames, levels[i+1]);
				left.Offset(-26, -14);
				
				Sprite right = DrawNumbers(frames, levels[i+2]);
				right.Offset(right.Width + 9, 4);
				
				sprites[levels[i]] = new Sprite(sprites[0], left, right);
			}
			
			// these names suck tbh... (but at the same time these objects are also so hardcoded that there's no reason to make them descriptive-)
			properties[0] = new PropertySpec("Trigger", typeof(int), "Extended",
				"What effect this object should trigger.", null, new Dictionary<string, int>
				{
					{ "Run", 0 },
					// 1 is unused
					{ "Exit", 2 },
					{ "Enterance", 4 },
					{ "Drop", 5 },
					{ "Exit (Boost)", 8 },
					{ "Y Bounds - 1024/792", 6 },
					{ "Y Bounds - 792/1024", 7 },
					{ "Y Bounds - 1280/1024", 3 }
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
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 2, 4, 5, 8, 6, 7, 3}); }
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
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int[] indexes = {1, 1, 1, 2, 0, 0, 3, 4, 1};
			if (obj.PropertyValue > (indexes.Length-1))
				return sprites[0];
			
			return sprites[indexes[obj.PropertyValue]];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue == 5) ? debug : null;
		}
	}
}