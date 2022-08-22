using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class CLedge3 : R1.CLedge34
	{
		public override Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("R1/Objects2.gif").GetSection(163, 1, 16, 48), -8, -32));
		}
	}
	
	class CLedge4 : R1.CLedge34
	{
		public override Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("R1/Objects2.gif").GetSection(163, 1, 16, 64), -8, -40));
		}
	}
}

namespace SCDObjectDefinitions.R1
{
	abstract class CLedge34 : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = GetSprite();
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
                "How long the Ledge will be.", null,
                (obj) => obj.PropertyValue,
                (obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override byte DefaultSubtype
		{
			get { return 8; }
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
			int sx = -(((obj.PropertyValue) * 16) / 2) + 8;
			for (int i = 0; i < Math.Max(1, (int)obj.PropertyValue); i++)
			{
				Sprite sprite = new Sprite(img);
				sprite.Offset(sx + (i * 16), 0);
				sprites.Add(sprite);
			}
			return new Sprite(sprites.ToArray());
		}
		
		public virtual Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8));
		}
	}
}