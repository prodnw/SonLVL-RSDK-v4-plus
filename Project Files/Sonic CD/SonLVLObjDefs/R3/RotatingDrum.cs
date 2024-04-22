using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R3
{
	class RotatingDrum : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[11];
		private Sprite[] debug;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R3/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(67, 34, 48, 16), -24, -16);
			sprites[1] = new Sprite(sheet.GetSection(116, 34, 48, 16), -24, -12);
			sprites[2] = new Sprite(sheet.GetSection(165, 34, 48, 16), -24, -8);
			sprites[3] = new Sprite(sheet.GetSection(67, 51, 48, 16), -24, -4);
			sprites[4] = new Sprite(sheet.GetSection(116, 51, 48, 16), -24, -4);
			sprites[5] = new Sprite(sheet.GetSection(67, 51, 48, 16), -24, -12);
			sprites[6] = new Sprite(sheet.GetSection(165, 34, 48, 16), -24, -12);
			sprites[7] = new Sprite(sheet.GetSection(116, 34, 48, 16), -24, -12);
			sprites[8] = new Sprite(sheet.GetSection(67, 34, 48, 16), -24, -16);
			sprites[9] = new Sprite(sheet.GetSection(165, 18, 48, 8), -24, -8);
			sprites[10] = new Sprite(sheet.GetSection(165, 18, 48, 8), -24, -8);
			
			for (int i = 5; i < 10; i++)
			{
				// (the Sprite constructor doesn't have a variant with both BitmapBits and flipping so we gotta flip 'em manually)
				sprites[i].Flip(false, true);
			}
			
			BitmapBits bitmap = new BitmapBits(2, 193);
			bitmap.DrawLine(6, 0, 0, 0, 192);
			
			int[] offsets = {0, -4, -24, -48, -80, -112, -144, -168, -188, -192, -188, -168, -144, -112, -80, -48, -24, -4};
			debug = new Sprite[offsets.Length];
			for (int i = 0; i < offsets.Length; i++)
			{
				debug[i] = new Sprite(bitmap, 0, offsets[i]);
			}
			
			properties[0] = new PropertySpec("Offset", typeof(int), "Extended",
				"How offset this object should be from its base interval. Offsets past 55% are in the background.", null, new Dictionary<string, int>
				{
					{ "0%", 0 },
					{ "5%", 1 },
					{ "11%", 2 },
					{ "16%", 3 },
					{ "22%", 4 },
					{ "27%", 5 },
					{ "33%", 6 },
					{ "38%", 7 },
					{ "44%", 8 },
					{ "50%", 9 },
					{ "55%", 10 },
					{ "61%", 11 },
					{ "66%", 12 },
					{ "72%", 13 },
					{ "77%", 14 },
					{ "83%", 15 },
					{ "88%", 16 },
					{ "94%", 17 }
				},
				(obj) => (obj.PropertyValue % 18),
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return properties[0].Enumeration.GetKey(subtype) + " Through Interval";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype > 13) ? 10 : (subtype > 9) ? 9 : subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue > 13) ? 10 : (obj.PropertyValue > 9) ? 9 : obj.PropertyValue];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue % 18];
		}
	}
}