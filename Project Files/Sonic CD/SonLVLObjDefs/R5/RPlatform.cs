using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class RPlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[3];
		private Sprite[] debug = new Sprite[3];
		private Rectangle[] bounds = new Rectangle[3];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[3];
			BitmapBits sheet = LevelData.GetSpriteSheet("R5/Objects.gif");
			frames[0] = new Sprite(sheet.GetSection(1, 1, 16, 16), -8, -8);
			frames[1] = new Sprite(sheet.GetSection(18, 1, 16, 16), -8, -8);
			frames[2] = new Sprite(sheet.GetSection(1, 208, 64, 16), -32, -8);
			
			int[] signs = {1, 0, 0, 1, 0, -1};
			for (int i = 0; i < 3; i++)
			{
				List<Sprite> sprs = new List<Sprite>() { frames[0] };
				for (int j = 0; j <= 5; j++)
				{
					int index = (j == 5) ? 2 : 1;
					sprs.Add(new Sprite(frames[index], j * 16 * signs[i * 2], j * 16 * signs[i * 2 + 1]));
					
					if (j == 5)
					{
						bounds[i] = frames[2].Bounds;
						bounds[i].Offset(j * 16 * signs[i * 2], j * 16 * signs[i * 2 + 1]);
					}
				}
				
				sprites[i] = new Sprite(sprs.ToArray());
			}
			
			int l = 5 * 16;
			BitmapBits bitmap = new BitmapBits(2 * l + 1, 2 * l + 1);
			bitmap.DrawCircle(6, l, l, l); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -l, -l);
			
			bitmap = bitmap.GetSection(0, l, 2 * l + 1, l);
			debug[1] = new Sprite(bitmap, -l, 0);
			
			debug[2] = new Sprite(debug[1], false, true);
			
			properties[0] = new PropertySpec("Range", typeof(int), "Extended",
                "The range the platform will swing in.", null, new Dictionary<string, int>
				{
					{ "180 Degrees (Downwards)", 2 },
					{ "180 Degrees (Upwards)", 3 },
					{ "360 Degrees", 1 },
					{ "Static", 0 } // doesn't really exist, but functions as such so may as well add it here ig?
				},
                (obj) => (obj.PropertyValue > 3) ? 0 : (int)obj.PropertyValue,
                (obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {2, 3, 1, 0}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 2; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				default:
				case 0: return "Static";
				case 1: return "360 Degrees";
				case 2: return "180 Degrees (Downwards)";
				case 3: return "180 Degrees (Upwards)";
			}
		}

		public override Sprite Image
		{
			get { return sprites[1]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			int index = 0;
			switch (subtype)
			{
				case 1:
				case 2: index = 1; break;
				case 3: index = 2; break;
			}
			
			return sprites[index];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int index = 0;
			switch (obj.PropertyValue)
			{
				case 1:
				case 2: index = 1; break;
				case 3: index = 2; break;
			}
			
			return sprites[index];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue > 0 && obj.PropertyValue < 4) ? debug[obj.PropertyValue - 1] : null;
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			int index = 0;
			switch (obj.PropertyValue)
			{
				case 1:
				case 2: index = 1; break;
				case 3: index = 2; break;
			}
			
			Rectangle bound = bounds[index];
			bound.Offset(obj.X, obj.Y);
			return bound;
		}
	}
}