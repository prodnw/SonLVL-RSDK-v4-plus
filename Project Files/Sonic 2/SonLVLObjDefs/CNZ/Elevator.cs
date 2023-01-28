using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class Elevator : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(193, 34, 32, 16), -16, -8);
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far the Elevator will go. Direction is determined by the Travel Direction variable.", null,
				(obj) => obj.PropertyValue & 127,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 128) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Travel Direction", typeof(int), "Extended",
				"Which direction the Elevator will travel in.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Downwards", 128 }
				},
				(obj) => obj.PropertyValue & 128,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 127) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 56; }
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
			int dist = (obj.PropertyValue & 127) << 2;
			BitmapBits bitmap = new BitmapBits(2, (2 * dist) + 1);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0, 0, (2 * dist));
			return new Sprite(bitmap, 0, -dist);
		}
	}
}