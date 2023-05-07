using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class VBlock : ObjectDefinition
	{
		private Sprite img;
		private Sprite debug;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(82, 34, 64, 64), -32, -32);
			
			BitmapBits bitmap = new BitmapBits(2, 193);
			bitmap.DrawLine(6, 0, 0, 0, 192); // LevelData.ColorWhite
			debug = new Sprite(bitmap, 0, -96);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Starting Direction", typeof(int), "Extended",
				"Which direction the Vertical Block will travel in.", null, new Dictionary<string, int>
				{
					{ "Downwards", 0 },
					{ "Upwards", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
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
			switch (subtype)
			{
				case 0:
					return "Initially Downwards";
				case 1:
				default:
					return "Initially Upwards";
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
			return debug;
		}
	}
}