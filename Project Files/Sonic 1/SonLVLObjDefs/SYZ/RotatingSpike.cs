using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SYZ
{
	class RotatingSpike : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[3];
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[2];
		
		public override void Init(ObjectData data)
		{
			sprites[2] = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(61, 178, 48, 48), -24, -24);
			sprites[0] = new Sprite(sprites[2], 80, 0);
			sprites[1] = new Sprite(sprites[2], -80, 0);
			
			BitmapBits bitmap = new BitmapBits(161, 161);
			bitmap.DrawCircle(6, 80, 80, 80); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -80, -80);
			
			properties[0] = new PropertySpec("Speed", typeof(int), "Extended",
				"How fast this Spike should spin.", null, new Dictionary<string, int>
				{
					{ "Fast", 0 },
					{ "Slow", 2 },
					{ "Medium", 4 }
				},
				(obj) => obj.PropertyValue & 6,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~6) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Starting Side", typeof(int), "Extended",
				"Which side this Spike should start at.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (byte)((int)value)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3, 4, 5 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Speed: Fast, Starting Side: Right";
				case 1: return "Speed: Fast, Starting Side: Left";
				case 2: return "Speed: Slow, Starting Side: Right";
				case 3: return "Speed: Slow, Starting Side: Left";
				case 4: return "Speed: Medium, Starting Side: Right";
				case 5: return "Speed: Medium, Starting Side: Left";
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[2];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}