using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class GasPlatform : ObjectDefinition
	{
		private Sprite img;
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("OOZ/Objects.gif").GetSection(84, 108, 48, 12), -24, -8);
			
			BitmapBits overlay = new BitmapBits(2, 208);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 0, 207);
			debug[0] = new Sprite(overlay, 0, -207);
			
			overlay = new BitmapBits(2, 121);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 0, 120);
			debug[1] = new Sprite(overlay, 0, -120);
			
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Platform should pop.", null, new Dictionary<string, int>
				{
					{ "Pop On Interval", 0 },
					{ "Pop On Contact", 1 }
				},
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)(int)value);
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
			switch (subtype)
			{
				case 0:
				default:
					return "Pop On Interval";
				case 1:
					return "Pop On Contact";
			}
		}

		public override Sprite Image
		{
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue == 1 ? 1 : 0];
		}
	}
}
