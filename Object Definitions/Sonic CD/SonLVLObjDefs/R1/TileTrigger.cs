using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class TileTriggerH : R1.TileTrigger
	{
		public override Sprite DrawSprite(ObjectEntry obj, Sprite spr)
		{
			List<Sprite> sprites = new List<Sprite>();
			int sx = -(((obj.PropertyValue) * 16) / 2) + 8;
			for (int i = 0; i < Math.Max(1, (int)obj.PropertyValue); i++)
			{
				Sprite sprite = new Sprite(spr);
				sprite.Offset(sx + (i * 16), 0);
				sprites.Add(sprite);
			}
			return new Sprite(sprites.ToArray());
		}
	}
	
	class TileTriggerL : R1.TileTrigger
	{
		public override Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(156, 67, 16, 16), -8, -8));
		}
	}
	
	class TileTriggerV : R1.TileTrigger
	{
	}
}

namespace SCDObjectDefinitions.R1
{
	abstract class TileTrigger : ObjectDefinition
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
			return DrawSprite(obj, img);
		}
		
		public virtual Sprite DrawSprite(ObjectEntry obj, Sprite spr)
		{
			List<Sprite> sprites = new List<Sprite>();
			int sy = -(((obj.PropertyValue) * 16) / 2) + 8;
			for (int i = 0; i < Math.Max(1, (int)obj.PropertyValue); i++)
			{
				Sprite sprite = new Sprite(img);
				sprite.Offset(0, sy + (i * 16));
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