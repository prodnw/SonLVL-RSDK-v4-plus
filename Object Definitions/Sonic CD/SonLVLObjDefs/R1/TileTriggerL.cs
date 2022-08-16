using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class TileTriggerL : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(156, 67, 16, 16), -8, -8);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
                "How large the Tile Trigger will be.", null,
                (obj) => obj.PropertyValue,
                (obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override byte DefaultSubtype
		{
			get { return 4; }
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
			List<Sprite> sprites = new List<Sprite>();
			Sprite sprite;
			if (obj.PropertyValue == 0)
			{
				sprite = new Sprite(img);
				sprites.Add(sprite);
			}
			else
			{
				int sy = -(((obj.PropertyValue) * 16) / 2) + 8;
				for (int i = 0; i < (obj.PropertyValue); i++)
				{
					sprite = new Sprite(img);
					sprite.Offset(0, sy + (i * 16));
					sprites.Add(sprite);
				}
			}
			return new Sprite(sprites.ToArray());
		}
	}
}