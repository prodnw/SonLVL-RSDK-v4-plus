using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Sol : ObjectDefinition
	{
		private Sprite[] frames = new Sprite[2];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[2];
			
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '5')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(91, 222, 16, 16), -8, -8);
				sprites[1] = new Sprite(sheet.GetSection(1, 33, 16, 16), -8, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				
				// using fixed frames
				sprites[0] = new Sprite(sheet.GetSection(625, 289, 16, 16), -8, -8);
				sprites[1] = new Sprite(sheet.GetSection(835, 71, 16, 16), -8, -8);
			}
			
			Sprite[] sprs = new Sprite[5];
			
			Sprite sprite = new Sprite(sprites[0]);
			sprs[0] = sprite;

			int[] posoffsets = {
				-18,   0,
				  0, -18,
				 18,   0,
				  0,  18
			};
			
			for (int i = 0; i < 4; i++)
			{
				Sprite tmp = new Sprite(sprites[1]);
				tmp.Offset(posoffsets[i * 2], posoffsets[(i * 2) + 1]);
				sprs[1+i] = tmp;
			}
			
			frames[0] = new Sprite(sprs);
			
			sprite.Flip(true, false);
			frames[1] = new Sprite(sprs);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Sol is facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return frames[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return frames[(subtype == 0) ? 0 : 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return frames[(obj.PropertyValue == 0) ? 0 : 1];
		}
	}
}