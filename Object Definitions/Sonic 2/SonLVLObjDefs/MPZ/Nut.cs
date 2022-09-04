using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class Nut : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(130, 156, 64, 24), -32, -12);
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Allow Drop", typeof(int), "Extended",
				"If the Nut should drop once it reaches a certain point.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 128 }
				},
				(obj) => (obj.PropertyValue & 128),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 127) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Drop Distance", typeof(int), "Extended",
				"At what point the Nut should start falling. Only applicable if Allow Drop is true.", null,
				(obj) => (obj.PropertyValue & 127),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 128) | (byte)(((int)value) & 127)));
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
			if (obj.PropertyValue > 127)
			{
				int height = (obj.PropertyValue & 127) << 3;
				var bitmap = new BitmapBits(2, height + 1);
				bitmap.DrawLine(LevelData.ColorWhite, 0, 0, 0, height);
				return new Sprite(bitmap);
			}
			
			return null;
		}
	}
}