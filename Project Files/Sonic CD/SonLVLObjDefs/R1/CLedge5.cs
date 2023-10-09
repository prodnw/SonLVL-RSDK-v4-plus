using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class CLedge5 : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[5];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R1/Objects2.gif");
			sprites[0] = new Sprite(sheet.GetSection(163, 1, 16, 48), -8, -40);
			sprites[0] = new Sprite(sprites[0], new Sprite(sheet.GetSection(190, 66, 16, 16), -8, 8)); // combine frames 0/1, since they're always drawn together
			sprites[1] = new Sprite(sheet.GetSection(82, 49, 16, 16), -8, 24);
			sprites[2] = new Sprite(sheet.GetSection(98, 49, 16, 16), -8, 24);
			sprites[3] = new Sprite(sheet.GetSection(130, 49, 16, 16), -8, 24);
			
			sprites[4] = new Sprite(sprites[0], sprites[2]); // object icon
			
			properties[0] = new PropertySpec("Length", typeof(int), "Extended",
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
			get { return sprites[4]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[4];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			List<Sprite> sprs = new List<Sprite>();
			int sx = -(((obj.PropertyValue) * 16) / 2) + 8;
			int length = Math.Max(1, (int)obj.PropertyValue);
			for (int i = 0; i < length; i++)
			{
				sprs.Add(new Sprite(sprites[0], sx + (i * 16), 0));
				
				int frame = (i == 0) ? 1 : (i == (length-1)) ? 3 : 2;
				sprs.Add(new Sprite(sprites[frame], sx + (i * 16), 0));
			}
			return new Sprite(sprs.ToArray());
		}
	}
}