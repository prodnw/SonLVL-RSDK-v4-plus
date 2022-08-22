using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class VPlatform : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("R1/Objects.gif").GetSection(101, 109, 64, 32), -32, -16);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Starting Direction", typeof(int), "Extended",
                "Which way the platform will go initially.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Downwards", 1 }
				},
                (obj) => obj.PropertyValue & 1,
                (obj, value) => obj.PropertyValue = (byte)(((int)value) & 1));
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
			return (subtype) + "";
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
			var bitmap = new BitmapBits(1, 98);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0, 0, 97);
			return new Sprite(bitmap, 0, -49);
		}
	}
}