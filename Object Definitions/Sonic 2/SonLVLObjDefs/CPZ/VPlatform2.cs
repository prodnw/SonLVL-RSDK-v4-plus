using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class VPlatform2 : ObjectDefinition
	{
		private PropertySpec[] properties;

		private readonly Sprite[] sprites = new Sprite[2];

		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(136, 155, 64, 27), -32, -16);
			sprites[1] = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(136, 183, 48, 26), -24, -16);

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of the platform.", null, new Dictionary<string, int>
				{
					{ "Large", 0 },
					{ "Small", 1 }
				},
				(obj) => (obj.PropertyValue & 1),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
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
			switch (subtype % 0x10)
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
			return sprites[subtype % 0x10];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			var sprite = new Sprite(SubtypeImage(obj.PropertyValue));
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			var overlay = new BitmapBits(1, 192);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0x00, 0, 192);
			return new Sprite(overlay, 0, -192);
		}
	}
}