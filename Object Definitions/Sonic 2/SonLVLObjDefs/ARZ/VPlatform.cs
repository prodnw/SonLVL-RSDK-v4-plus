using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class VPlatform : ObjectDefinition
	{
		private Sprite img;
		private Sprite debug;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(126, 145, 64, 45), -32, -13);
			
			var overlay = new BitmapBits(1, 256);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 1, 256);
			debug = new Sprite(overlay, 0, -64 + 8);

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Reverse", typeof(int), "Extended",
				"Reverses platform movement.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 1 }
				},
				(obj) => ((obj.PropertyValue == 1) ? 1 : 0),
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
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
			var spr = new Sprite(img);
			spr.Offset(0, (obj.PropertyValue == 1) ? -64 : 64);
			return spr;
		}

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}