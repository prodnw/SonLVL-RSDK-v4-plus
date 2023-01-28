using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class LeafSpawner : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Size", typeof(byte), "Extended",
				"The size of this Leaf Spawner. Increases in powers of 2, based on this number.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = ((byte)value));
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
			return img;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int width = (32 << (obj.PropertyValue + 1));
			BitmapBits debug = new BitmapBits(width+1, 65);
			debug.DrawRectangle(LevelData.ColorWhite, 0, 0, width, 64);
			return new Sprite(debug, -(width/2), -32);
		}
	}
}