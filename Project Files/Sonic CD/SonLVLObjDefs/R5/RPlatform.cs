using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class RPlatform : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R5/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(1, 1, 16, 16), -8, -8);
			sprites[1] = new Sprite(sheet.GetSection(18, 1, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(1, 208, 64, 16), -32, -8);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Range", typeof(int), "Extended",
                "The range the platform will swing in.", null, new Dictionary<string, int>
				{
					{ "Static", 0 }, // doesn't really exist, but functions as such so may as well add it here
					{ "Full Rotation", 1 },
					{ "Bottom Half", 2 },
					{ "Top Half", 3 }
				},
                (obj) => obj.PropertyValue & 3,
                (obj, value) => obj.PropertyValue = ((byte)((int)value)));
		}
		
		public override byte DefaultSubtype
		{
			get { return 1; }
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
			return new Sprite();
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			List<Sprite> spritesR = new List<Sprite>();
			for (int i = 0; i <= 6; i++)
			{
				int frame = (i == 0) ? 0 : (i == 6) ? 2 : 1;
				Sprite sprite = new Sprite(sprites[frame]);
				sprite.Offset(0, (i * ((obj.PropertyValue == 3) ? -16 : 16)));
				spritesR.Add(sprite);
			}
			return new Sprite(spritesR.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			var overlay = new BitmapBits(209, 209);
			switch (obj.PropertyValue)
			{
				case 0:
				default:
					return null;
				case 1:
					overlay.DrawCircle(LevelData.ColorWhite, 104, 104, 104);
					return new Sprite(overlay, -104, -104);
				case 2:
					overlay.DrawCircle(LevelData.ColorWhite, 104, 0, 104);
					return new Sprite(overlay, -104, 0);
				case 3:
					overlay.DrawCircle(LevelData.ColorWhite, 104, 0, 104);
					Sprite rtr = new Sprite(overlay, -104, 0);
					rtr.Flip(false, true);
					return rtr;
			}
		}
	}
}