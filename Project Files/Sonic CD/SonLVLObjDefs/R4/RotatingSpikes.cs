using SonicRetro.SonLVL.API;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SCDObjectDefinitions.R4
{
	class RotatingSpikes : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(163, 52, 16, 16), -8, -8); // post
			sprites[1] = new Sprite(sheet.GetSection(180, 52, 16, 16), -8, -8); // chain
			sprites[2] = new Sprite(sheet.GetSection(221, 53, 32, 32), -16, -16); // spike
			
			properties[0] = new PropertySpec("Length", typeof(int), "Extended",
				"How many chains the Spike Ball should hang off of.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 6; }
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
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			List<Sprite> sprs = new List<Sprite>();
			
			for (int i = 0; i < obj.PropertyValue + 2; i++)
			{
				int frame = (i == 0) ? 0 : (i == obj.PropertyValue) ? 2 : 1;
				sprs.Add(new Sprite(sprites[frame], 0, i * 16));
			}
			
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int length = obj.PropertyValue * 16;
			
			var overlay = new BitmapBits(2 * length + 1, 2 * length + 1);
			overlay.DrawCircle(6, length, length, length); // LevelData.ColorWhite
			return new Sprite(overlay, -length, -length);
		}
	}
}