using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class VPlatform2 : ObjectDefinition
	{
		private Sprite img;
		private Sprite debug;
		private PropertySpec[] properties;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(191, 170, 64, 85), -32, -45);
			
			BitmapBits overlay = new BitmapBits(2, 65);
			overlay.DrawLine(6, 0, 0, 0, 64); // LevelData.ColorWhite
			debug = new Sprite(overlay, 0, -32);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Start Direction", typeof(int), "Extended",
				"The starting direction of this Platform.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Downwards", 1 }
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
			return null;
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
			return debug;
		}
	}
}