using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class BreakWall : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[8];
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R1/Objects2.gif");
			sprites[0] = new Sprite(sheet.GetSection(190, 1, 32, 48), -16, -24);
			sprites[1] = new Sprite(sheet.GetSection(223, 1, 32, 48), -16, -24);
			sprites[2] = new Sprite(sheet.GetSection(190, 50, 32, 48), -16, -24);
			sprites[3] = new Sprite(sheet.GetSection(223, 50, 32, 48), -16, -24);
			sprites[4] = new Sprite(sheet.GetSection(190, 99, 32, 48), -16, -24);
			sprites[5] = new Sprite(sheet.GetSection(223, 99, 32, 48), -16, -24);
			sprites[6] = new Sprite(sheet.GetSection(190, 148, 32, 48), -16, -24);
			sprites[7] = new Sprite(sheet.GetSection(223, 148, 32, 48), -16, -24);
			
			BitmapBits bitmap = new BitmapBits(32, 48);
			bitmap.DrawRectangle(6, 0, 0, 31, 47); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -16, -24);
			
			properties[0] = new PropertySpec("Side", typeof(int), "Extended",
				"Which side this Breakable Wall is facing.", null, new Dictionary<string, int>
				{
					{ "Single", 0 },
					{ "Left", 1 },
					{ "Middle", 2 },
					{ "Right", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~3) | (int)value));
			
			properties[1] = new PropertySpec("Pattern", typeof(int), "Extended",
				"Which checkerboard pattern should be on this Wall.", null, new Dictionary<string, int>
				{
					{ "Pattern A", 0 },
					{ "Pattern B", 4 }
				},
				(obj) => obj.PropertyValue & 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~4) | (int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7}); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return properties[0].Enumeration.GetKey(subtype & 3) + ", " + properties[1].Enumeration.GetKey(subtype & 4);
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 7];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 7];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}