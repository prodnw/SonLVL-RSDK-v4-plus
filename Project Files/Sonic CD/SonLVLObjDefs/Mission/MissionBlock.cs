using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Mission
{
	class MissionBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Mission/Objects.gif").GetSection(1, 1, 32, 32), -16, -16);
			
			properties[0] = new PropertySpec("Count", typeof(int), "Extended",
				"How many Mission Blocks there should be.", null,
				(obj) => Math.Max(1, (int)obj.PropertyValue),
				(obj, value) => obj.PropertyValue = (byte)((int)value <= 1 ? 0 : (int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
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
			if (obj.PropertyValue <= 1)
				return sprite;
			
			List<Sprite> sprites = new List<Sprite>();
			for (int i = 0; i < (int)obj.PropertyValue; i++)
				sprites.Add(new Sprite(sprite, i * 32, 0));
			
			return new Sprite(sprites.ToArray());
		}
	}
}