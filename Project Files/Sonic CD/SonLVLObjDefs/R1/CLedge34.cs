using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class CLedge3 : R1.CLedge34
	{
		public override Sprite GetFrame()
		{
			return (new Sprite(LevelData.GetSpriteSheet("R1/Objects2.gif").GetSection(163, 1, 16, 48), -8, -32));
		}
	}
	
	class CLedge4 : R1.CLedge34
	{
		public override Sprite GetFrame()
		{
			return (new Sprite(LevelData.GetSpriteSheet("R1/Objects2.gif").GetSection(163, 1, 16, 64), -8, -40));
		}
	}
	
	abstract class CLedge34 : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public abstract Sprite GetFrame();
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
                "How long the Ledge will be.", null,
                (obj) => obj.PropertyValue,
                (obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {6, 8, 10}); } // can be any value, but let's give some starting ones
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
			return subtype + " Blocks";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			List<Sprite> sprites = new List<Sprite>();
			int sx = -((obj.PropertyValue * 16) / 2) + 8;
			for (int i = 0; i < Math.Max(1, (int)obj.PropertyValue); i++)
				sprites.Add(new Sprite(sprite, sx + (i * 16), 0));
			return new Sprite(sprites.ToArray());
		}
	}
}