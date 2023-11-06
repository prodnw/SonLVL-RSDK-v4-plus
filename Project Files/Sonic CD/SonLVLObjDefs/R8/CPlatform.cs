using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R8
{
	class CPlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("R8/Objects.gif").GetSection(222, 212, 16, 32), -8, -16);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How long the Ledge will be.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 4, 6, 8, 10, 12 }); } // assorted starting values :D
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
			int sx = -(((obj.PropertyValue) * 16) / 2) + 8;
			for (int i = 0; i < Math.Max(1, (int)obj.PropertyValue); i++)
				sprites.Add(new Sprite(sprite, sx + (i * 16), 0));
			return new Sprite(sprites.ToArray());
		}
	}
}