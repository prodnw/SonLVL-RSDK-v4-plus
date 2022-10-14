using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class RotatePlatform : ObjectDefinition
	{
		private PropertySpec[] properties;

		private readonly Sprite[] sprites = new Sprite[2];

		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(136, 155, 64, 27), -32, -16);
			sprites[1] = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(136, 183, 48, 26), -24, -16);

			properties = new PropertySpec[3];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of the platform.", null, new Dictionary<string, int>
				{
					{ "Large", 0 },
					{ "Small", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 240) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Starting position", typeof(int), "Extended",
				"The position (or angle) from where the Platform will start.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Down", 0x10 },
					{ "Right", 0x20 },
					{ "Up", 0x30 }
				},
				(obj) => (obj.PropertyValue % 0x40) & 0x30,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~(0x30)) | (byte)((int)value)));
			
			properties[2] = new PropertySpec("Direction", typeof(int), "Extended",
				"The direction in which the Platform moves.", null, new Dictionary<string, int>
				{
					{ "Counter-clockwise", 0 },
					{ "Clockwise", 0x40 }
				},
				(obj) => obj.PropertyValue & 0x40,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 0xbf) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (((subtype % 0x10) == 0) ? 0 : 1)
			{
				case 0:
					return "Large";
				case 1:
					return "Small";
				default:
					return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[((subtype % 0x10) == 0) ? 0 : 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			var sprite = new Sprite(SubtypeImage(obj.PropertyValue));
			int radius = ((obj.PropertyValue & 0x40) != 0) ? -64 : 64;
			
			if ((obj.PropertyValue & 0x30) == 0x30)
			{
				sprite.Offset(0, -radius);
			}
			else if ((obj.PropertyValue & 0x20) == 0x20)
			{
				sprite.Offset(radius, 0);
			}
			else if ((obj.PropertyValue & 0x10) == 0x10)
			{
				sprite.Offset(0, radius);
			}
			else
			{
				sprite.Offset(-radius, 0);
			}
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int radius = 64;
			var overlay = new BitmapBits(radius * 2 + 1, radius * 2 + 1);
			overlay.DrawCircle(LevelData.ColorWhite, radius, radius, radius);
			return new Sprite(overlay, -radius, -radius - 4);
		}
	}
}