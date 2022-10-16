using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class TippingFloor : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '2')
			{
				img = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(1, 222, 32, 32), -16, -16);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(34, 692, 32, 32), -16, -16);
			}

			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Offset", typeof(int), "Extended",
				"The offset (i.e. an offset of 1 will flip before an offset of 2).", null,
				(obj) => (obj.PropertyValue & 0x0f),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 0xf0) | (byte)((int)value)));
			properties[1] = new PropertySpec("Duration", typeof(int), "Extended",
				"How long the platform will face up/down each cycle.", null,
				(obj) => (obj.PropertyValue & 0xf0) >> 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 0x0f) | (byte)((int)value << 4)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
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
	}
}
