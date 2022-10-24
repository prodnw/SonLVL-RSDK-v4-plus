using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.MZ
{
	class ChainedCrusher : ObjectDefinition
	{
		private readonly Sprite[] crusherbase = new Sprite[2];
		private readonly Sprite[] crushers = new Sprite[3];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MZ/Objects.gif");
			crusherbase[0] = new Sprite(sheet.GetSection(308, 69, 8, 32), -4, -21);
			crusherbase[1] = new Sprite(sheet.GetSection(256, 76, 32, 32), -16, -52);

			crushers[0] = new Sprite(new Sprite(sheet.GetSection(143, 180, 112, 32), -56, -20), new Sprite(sheet.GetSection(199, 256, 88, 32), -44, 12));
			crushers[1] = new Sprite(new Sprite(sheet.GetSection(159, 147, 96, 32), -48, -20), new Sprite(sheet.GetSection(199, 256, 88, 32), -44, 12));
			crushers[2] = new Sprite(sheet.GetSection(256, 109, 32, 32), -16, -20);

			properties = new PropertySpec[3];
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"The distance the crusher drops.", null, new Dictionary<string, int>
				{
					{ "112 px", 0 },
					{ "160 px", 1 },
					{ "80 px", 2 },
					{ "120 px", 3 },
					{ "56 px", 4 },
					{ "88 px", 5 },
					{ "184 px", 6 }
				},
				(obj) => ((obj.PropertyValue & 0x0f ) < 7) ? obj.PropertyValue & 0x0f : 0,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 0xf0) | (byte)((int)value)));
			properties[1] = new PropertySpec("Style", typeof(int), "Extended",
				"The style of the crusher.", null, new Dictionary<string, int>
				{
					{ "Large crusher", 0 },
					{ "Medium crusher", 1 },
					{ "Small block", 2 }
				},
				(obj) => ((obj.PropertyValue & 0x70) >> 4) % 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 0x8f) | (byte)((int)value << 4)));
			properties[2] = new PropertySpec("Triggered", typeof(int), "Extended",
				"If the crusher should be activated by a button as opposed to rising automatically.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 1 }
				},
				(obj) => (obj.PropertyValue >> 7),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 0x7f) | (byte)((int)value << 7)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
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
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return crusherbase[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return crusherbase[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int type = (((obj.PropertyValue & 0x70) / 16) % 3);
			var y = ((obj.PropertyValue & 0x80) != 0) ? 20 : 0;	// Small thing to distinguish the triggered ones from the automatic ones..
			var sprites = new Sprite(new Sprite(crusherbase), new Sprite(crushers[type], 0, y));
			return sprites;
		}

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int[] distances = new int[7] {0x70, 0xa0, 0x50, 0x78, 0x38, 0x58, 0xb8};
			int type = (((obj.PropertyValue & 0x70) / 16) % 3);
			var dist = ((obj.PropertyValue & 0x0f) < 7) ? distances[obj.PropertyValue & 0x0f] : distances[0];
			dist = dist * 16 / 0x10;

			var w = 0;
			var h = 0;
			if (type == 0){w = 112; h = 56;}
			else if (type == 1){w = 96; h = 56;}
			else if (type == 2){w = 32; h = 32; dist -= 8;}

			var overlay = new BitmapBits(w, h);
			overlay.DrawRectangle(LevelData.ColorWhite, 0, 0, w - 1, h - 1);
			return new Sprite(overlay, -(w / 2), -12 + dist);
		}

		public override Rectangle GetBounds(ObjectEntry obj)
		{
			int type = (((obj.PropertyValue & 0x70) / 16) % 3);
			var bounds = new Sprite(crushers[type]).Bounds;
			bounds.Offset(obj.X, obj.Y);
			return bounds;
		}
	}
}