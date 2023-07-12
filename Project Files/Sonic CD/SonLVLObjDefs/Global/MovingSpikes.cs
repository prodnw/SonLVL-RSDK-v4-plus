using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Global
{
	class MovingSpikes : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[4];
		private Sprite[] debug   = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items3.gif");
			sprites[0] = new Sprite(sheet.GetSection(50, 1, 32, 32), -16, -16);
			sprites[1] = new Sprite(sheet.GetSection(50, 34, 32, 32), -16, -16);
			sprites[2] = new Sprite(sheet.GetSection(50, 67, 32, 32), -16, -16);
			sprites[3] = new Sprite(sheet.GetSection(50, 100, 32, 32), -16, -16);
			
			properties[0] = new PropertySpec("Orientation", typeof(int), "Extended",
				"Which way the Spikes are facing.", null, new Dictionary<string, int>
				{
					{ "Up", 0 },
					{ "Right", 1 },
					{ "Left", 2 },
					{ "Down", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(6, 0, 0, 31, 31); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -16,  16);
			debug[1] = new Sprite(bitmap, -48, -16);
			debug[2] = new Sprite(bitmap,  16, -16);
			debug[3] = new Sprite(bitmap, -16, -48);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3 }); }
		}

		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Facing Up";
				case 1: return "Facing Right";
				case 2: return "Facing Left";
				case 3: return "Facing Down";
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 3];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 3];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue & 3];
		}
	}
}