using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class SwingPlat : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private readonly Sprite[] sprites = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R1/Objects2.gif");
			sprites[0] = new Sprite(sheet.GetSection(173, 230, 16, 16), -8, -8);
			sprites[1] = new Sprite(sheet.GetSection(190, 230, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(207, 230, 48, 16), -24, -8);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
                "How many chains the Platform should hang off of.", null,
                (obj) => obj.PropertyValue,
                (obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {3, 4, 5}); } // it can be anything, but let's give some starting values
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
			return subtype + " chains";
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
			List<Sprite> sprs = new List<Sprite>() { sprites[0] };
			int sy = 16;
			for (int i = 0; i < obj.PropertyValue; i++)
			{
				sprs.Add(new Sprite(sprites[1], 0, sy));
				sy += 16;
			}
			sprs.Add(new Sprite(sprites[2], 0, sy));
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int l = ((obj.PropertyValue + 1) * 16);
			var overlay = new BitmapBits(2 * l + 1, l + 1);
			overlay.DrawCircle(6, l, 0, l); // LevelData.ColorWhite
			return new Sprite(overlay, -l, 0);
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			Rectangle bounds = sprites[2].Bounds;
			bounds.Offset(obj.X, obj.Y + ((obj.PropertyValue + 1) * 16));
			return bounds;
		}
	}
}