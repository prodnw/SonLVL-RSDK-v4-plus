using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HTZ
{
	class HPlatform : ObjectDefinition
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
			img = new Sprite(LevelData.GetSpriteSheet("HTZ/Objects.gif").GetSection(191, 223, 64, 32), -32, -12);
			
			BitmapBits overlay = new BitmapBits(129, 2);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 128, 0);
			debug = new Sprite(overlay, -64, 0);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Start Direction", typeof(int), "Extended",
				"The starting direction of this Platform.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => ((obj.PropertyValue == 1) ? 1 : 0),
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