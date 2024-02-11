using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class TileTriggerH : R1.TileTriggerV
	{
		public override Sprite DrawSprite(byte length)
		{
			if (length == 0)
				return sprite;
			
			List<Sprite> sprites = new List<Sprite>();
			int sx = -((length * 16) / 2) + 8;
			for (int i = 0; i < length; i++)
				sprites.Add(new Sprite(sprite, sx + (i * 16), 0));
			
			return new Sprite(sprites.ToArray());
		}
	}
	
	class TileTriggerL : R1.TileTriggerV
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(156, 67, 16, 16), -8, -8);
		}
	}
	
	class TileTriggerV : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		public Sprite sprite;
		
		public virtual Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
		}
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How long the Tile Trigger will be.", null,
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {4, 8, 12, 16}); }
		}
		
		public override bool Debug
		{
			get { return true; }
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
			return subtype + " Nodes";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return DrawSprite(subtype);
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return DrawSprite(obj.PropertyValue);
		}
		
		public virtual Sprite DrawSprite(byte length)
		{
			if (length == 0)
				return sprite;
			
			List<Sprite> sprites = new List<Sprite>();
			int sy = -((length * 16) / 2) + 8;
			for (int i = 0; i < length; i++)
				sprites.Add(new Sprite(sprite, 0, sy + (i * 16)));
			
			return new Sprite(sprites.ToArray());
		}
	}
}