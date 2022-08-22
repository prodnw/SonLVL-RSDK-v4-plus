using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Mission
{
	class MissionBlock : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Mission/Objects.gif").GetSection(1, 1, 32, 32), -16, -16);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Count", typeof(int), "Extended",
                "How many Mission Blocks there should be.", null,
                (obj) => Math.Max(1, (int)obj.PropertyValue),
                (obj, value) => obj.PropertyValue = (byte)(((int)value) <= 1 ? 0 : (int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
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
			return (subtype) + " blocks";
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
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < Math.Max(1, (int)obj.PropertyValue); i++)
			{
				Sprite tmp = new Sprite(img);
				tmp.Offset(i * 32, 0);
				sprs.Add(tmp);
			}
			return new Sprite(sprs.ToArray());
		}
	}
}