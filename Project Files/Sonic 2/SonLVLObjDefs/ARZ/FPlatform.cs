using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class FPlatform : ObjectDefinition
	{
		private Sprite img;
		private Sprite debug;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(126, 145, 64, 45), -32, -13);
			
			var bitmap = new BitmapBits(1, 0x1C);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0x00, 0, 0x03);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0x08, 0, 0x0B);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0x10, 0, 0x13);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0x18, 0, 0x1B);
			debug = new Sprite(bitmap, 0, 33);

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Falls", typeof(int), "Extended",
				"If the platform should fall or not.", null, new Dictionary<string, int>
				{
					{ "True", 0 },
					{ "False", 1 }
				},
				(obj) => ((obj.PropertyValue == 0) ? 0 : 1),
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
			return img;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (obj.PropertyValue == 0)
			{
				return debug;
			}
			return new Sprite();
		}
	}
}