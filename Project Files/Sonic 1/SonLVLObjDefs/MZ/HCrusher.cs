using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.MZ
{
	class HCrusher : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[2];
		private Sprite[,] debug = new Sprite[2,4];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MZ/Objects.gif");
			
			// this is kinda iffy
			// if i go `[2][4]` then the compiler gets mad
			// and i don't want to go `[2,4]` because i need to get the entire table which (i think) i can only do if i declare it like this
			// i dunno as long as it works it's fine ig
			Sprite[][] frames = new Sprite[2][];
			frames[0] = new Sprite[4];
			frames[1] = new Sprite[4];
			
			frames[0][0] = new Sprite(sheet.GetSection(295, 388, 21, 16),  16, -8);
			frames[1][0] = new Sprite(sheet.GetSection(295, 388, 21, 16), -38, -8);
			
			frames[0][1] = new Sprite(sheet.GetSection(289, 158, 8, 32), 36, -16);
			frames[0][2] = new Sprite(sheet.GetSection(256, 142, 32, 64), -12, -32);
			frames[0][3] = new Sprite(sheet.GetSection(256, 207, 31, 48), -43, -24);
			
			for (int i = 1; i < 4; i++) frames[1][i] = new Sprite(frames[0][i], true, false);
			
			sprites[0] = new Sprite(frames[0]);
			sprites[1] = new Sprite(frames[1]);
			
			BitmapBits bitmap = new BitmapBits(55, 65);
			bitmap.DrawRectangle(6, 0, 0, 53, 63); // LevelData.ColorWhite
			debug[0,0] = new Sprite(bitmap, -42 - 56, -32);
			debug[0,1] = new Sprite(bitmap, -42 - 159, -32);
			debug[0,2] = new Sprite(bitmap, -42 - 80, -32);
			debug[0,3] = debug[0,0];
			
			debug[1,0] = new Sprite(bitmap, -12 + 56, -32);
			debug[1,1] = new Sprite(bitmap, -12 + 159, -32);
			debug[1,2] = new Sprite(bitmap, -12 + 80, -32);
			debug[1,3] = debug[1,0];
			
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"The distance the crusher extends.", null, new Dictionary<string, int>
				{
					{ "56 px", 0 },
					{ "159 px", 1 },
					{ "80 px", 2 }
					// { "56 px (Use Button)", 3 } // RE2 has this but i don't think this is actually any different from the normal 56px?
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~3) | ((int)value)));
			
			/*
			// Well.. it *should* work like this, but right-facing crushers aren't programmed correctly in the actual game, so...
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this crusher should be facing in.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 0x40 }
				},
				(obj) => obj.PropertyValue & 0x40,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x40) | ((int)value)));
			*/
			
			properties[1] = new PropertySpec("Triggered", typeof(bool), "Extended",
				"If the crusher should be activated by a button, as opposed to extending automatically.", null,
				(obj) => (obj.PropertyValue & 0x80) == 0x80,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | ((bool)value ? 0x80 : 0x00)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
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
			// Still respecting direction here, even if it's not really supported (cause it don't work)
			return sprites[(obj.PropertyValue & 0x40) >> 6];
		}

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			// Same here
			return debug[(obj.PropertyValue & 0x40) >> 6, obj.PropertyValue & 3];
		}
	}
}