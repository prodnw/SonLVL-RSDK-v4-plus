using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class BoundaryAdjust : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Display.gif");
			
			Sprite[] frames = new Sprite[13];
			
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
			
			frames[11] = new Sprite(sheet.GetSection(173, 67, 16, 16), -8, -8); // "T"
			frames[12] = new Sprite(sheet.GetSection(156, 67, 16, 16), -8, -8); // "L"
			
			// default box
			sprites[2] = new Sprite(new Sprite(frames[11], -8, -8),
			                        new Sprite(frames[12], -8,  8),
			                        new Sprite(frames[12],  8, -8),
			                        new Sprite(frames[11],  8,  8));
			
			// left, right
			int[] levels = new int[] {
				768, 520,
				520, 768
			};
			
			for (int i = 0; i < levels.Length; i += 2)
			{
				Sprite left = DrawNumbers(frames, levels[i]);
				left.Offset(-26, -14);
				
				Sprite right = DrawNumbers(frames, levels[i+1]);
				right.Offset(right.Width + 9, 4);
				
				sprites[i / 2] = new Sprite(sprites[2], left, right);
			}
			
			properties[0] = new PropertySpec("Trigger", typeof(int), "Extended",
				"What Y position this object should bring lock camera bounds to.", null, new Dictionary<string, int>
				{
					{ "Left - 768 | Right - 520", 0 },
					{ "Left - 520 | Right - 768", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
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
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Left - 768 | Right - 520" : "Left - 520 | Right - 768";
		}

		public override Sprite Image
		{
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 0) ? 0 : 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 0) ? 0 : 1];
		}
	}
}