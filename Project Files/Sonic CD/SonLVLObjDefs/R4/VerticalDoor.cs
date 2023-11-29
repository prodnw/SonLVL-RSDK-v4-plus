using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class VerticalDoor : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects.gif").GetSection(1, 69, 16, 128), -8, -64);
			
			BitmapBits bitmap = new BitmapBits(17, 129);
			bitmap.DrawRectangle(6, 0, 0, 15, 127); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -8, -64 - 128);
			debug[1] = new Sprite(bitmap, -8, -64 + 128);
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"What type of button press the Door should require and which way it should open.", null, new Dictionary<string, int>
				{
					{ "Button Hold - Upwards", 0 },
					{ "Button Press - Upwards", 1 },
					{ "Button Press - Downwards", 2 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Button Hold - Upwards";
				case 1: return "Button Press - Upwards";
				case 2: return "Button Press - Downwards";
				default: return "Static"; // "Unknown" works too 
			}
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue == 2) ? 1 : 0];
		}
	}
}