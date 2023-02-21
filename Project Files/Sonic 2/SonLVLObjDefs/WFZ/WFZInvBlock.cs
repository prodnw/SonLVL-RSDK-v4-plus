using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.WFZ
{
	class WFZInvBlock : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(1, 176, 16, 14), -8, -7);
			
			properties = new PropertySpec[3];
			properties[0] = new PropertySpec("Width", typeof(int), "Extended",
				"How wide the Invisible Block will be.", null,
				(obj) => (obj.PropertyValue >> 4) + 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 15) | (byte)(Math.Max((((((int)value) & 15) << 4) - 1), 0))));
			
			properties[1] = new PropertySpec("Height", typeof(int), "Extended",
				"How tall the Invisible Block will be.", null,
				(obj) => (obj.PropertyValue & 15) + 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 240) | (byte)(Math.Max(((((int)value) & 15) - 1), 0))));
			
			properties[2] = new PropertySpec("Time Attack Only", typeof(bool), "Extended",
				"If this Block should only be in Time Attack.", null,
				(obj) => ((V4ObjectEntry)obj).Value0 == 1,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((bool)value ? 1 : 0));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override byte DefaultSubtype
		{
			get { return 0x11; }
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
			int width = obj.PropertyValue >> 4;
			int height = obj.PropertyValue & 15;
			width += 1; height += 1;
			
			int sx = (obj.PropertyValue & 240) << 15;
			int sy = (obj.PropertyValue & 15) << 19;
			sx >>= 16; sy >>= 16;
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					Sprite tmp = new Sprite(img);
					tmp.Offset(-sx + (j * 16), -sy + (i * 16));
					sprs.Add(tmp);
				}
			}
			return new Sprite(sprs.ToArray());
		}
	}
}