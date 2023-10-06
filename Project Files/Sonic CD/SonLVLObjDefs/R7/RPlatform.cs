using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class RPlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[6];
		private Sprite[] debug = new Sprite[6];
		private Rectangle[] bounds = new Rectangle[6];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[3];
			
			BitmapBits sheet = LevelData.GetSpriteSheet("R7/Objects.gif");
			frames[0] = new Sprite(sheet.GetSection(34, 1, 16, 16), -8, -8);
			frames[1] = new Sprite(sheet.GetSection(51, 1, 16, 16), -8, -8);
			frames[2] = new Sprite(sheet.GetSection(68, 1, 64, 16), -32, -8);
			
			int[] lengths = {2, 2, 5, 2, 2, 2};
			double[] angles = {0.5, 1.5, 0.5, 0.0, 1.5, 1.5};
			int[] debugInfo = {
				// width of bitmap, height of bitmap, x pos of circle, y pos of circle
				2, 1, 1, 0,
				2, 1, 1, 1,
				2, 2, 1, 1,
				1, 2, 0, 1,
				2, 2, 1, 1,
				2, 2, 1, 1
			};
			
			for (int i = 0; i < 6; i++)
			{
				double angle = angles[i] * Math.PI;
				
				List<Sprite> sprs = new List<Sprite>() { frames[0] };
				for (int j = 0; j < lengths[i] + 1; j++)
				{
					sprs.Add(new Sprite(frames[(j < lengths[i]) ? 1 : 2], (int)(Math.Cos(angle) * ((j+1) * 16)), (int)(Math.Sin(angle) * ((j+1) * 16))));
					if (j == lengths[i])
					{
						bounds[i] = frames[2].Bounds;
						bounds[i].Offset((int)(Math.Cos(angle) * ((j+1) * 16)), (int)(Math.Sin(angle) * ((j+1) * 16)));
					}
				}
				
				sprites[i] = new Sprite(sprs.ToArray());
				
				int l = (lengths[i] + 1) * 16;
				BitmapBits bitmap = new BitmapBits(debugInfo[i * 4] * l + 1, debugInfo[(i * 4) + 1] * l + 1);
				bitmap.DrawCircle(6, debugInfo[(i * 4) + 2] * l, debugInfo[(i * 4) + 3] * l, l); // LevelData.ColorWhite
				
				if (i == 2) // are we on the 240 degree one?
				{
					// wipe out everything between pi/6 and 5pi/6
					for (int x = -(int)(Math.Cos(Math.PI/6) * l); x < (int)(Math.Cos((Math.PI)/6) * l); x++)
					{
						for (int y = 0; y < debugInfo[(i * 4) + 3] * l; y++)
							bitmap.SafeSetPixel(0, debugInfo[(i * 4) + 2] * l + x, y);
					}
				}
				
				debug[i] = new Sprite(bitmap, -(debugInfo[(i * 4) + 2] * l), -(debugInfo[(i * 4) + 3] * l));
			}
			
			properties[0] = new PropertySpec("Range", typeof(int), "Extended",
				"What this platform's rotation range should be.", null, new Dictionary<string, int>
				{
					{ "180 Degrees (Downwards)", 19 },
					{ "180 Degrees (Upwards)", 35 },
					{ "180 Degrees (Right)", 67 },
					{ "240 Degrees", 54 },
					{ "360 Degrees (Clockwise)", 99 },
					{ "360 Degrees (Counter-Clockwise)", 115 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {19, 35, 67, 54, 99, 115}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 19; }
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
			Dictionary<int, int> indexes = new Dictionary<int, int>
			{
				{ 19, 0 },
				{ 35, 1 },
				{ 54, 2 },
				{ 67, 3 },
				{ 99, 4 },
				{ 115, 5 }
			};
			
			if (indexes.ContainsKey((int)obj.PropertyValue))
				return sprites[indexes[(int)obj.PropertyValue]];
			else
				return sprites[0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			Dictionary<int, int> indexes = new Dictionary<int, int>
			{
				{ 19, 0 },
				{ 35, 1 },
				{ 54, 2 },
				{ 67, 3 },
				{ 99, 4 },
				{ 115, 5 }
			};
			
			if (indexes.ContainsKey((int)obj.PropertyValue))
				return debug[indexes[(int)obj.PropertyValue]];
			else
				return null;
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			Dictionary<int, int> indexes = new Dictionary<int, int>
			{
				{ 19, 0 },
				{ 35, 1 },
				{ 54, 2 },
				{ 67, 3 },
				{ 99, 4 },
				{ 115, 5 }
			};
			
			if (!indexes.ContainsKey((int)obj.PropertyValue))
				return Rectangle.Empty;
			
			Rectangle bound = bounds[indexes[(int)obj.PropertyValue]];
			bound.Offset(obj.X, obj.Y);
			return bound;
		}
	}
}