using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Sol : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
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

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("PDir", typeof(int), "Extended",
				"The direction the Orbinaut is facing (not to be confused with object.direction).", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
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
			get { return SubtypeImage(0); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			// Copied from S1's Orbinaut, just like the original script itself
			
			List<Sprite> sprs = new List<Sprite>();
			
			Sprite sprite = new Sprite(sprites[0]);
			sprite.Flip((subtype & 1) != 0, false);
			sprs.Add(sprite);

			int[] posoffsets = {-18, 0, 0, -18, 18, 0, 0, 18 };
			
			for (int i = 0; i < 8; i += 2)
			{
				Sprite tmp = new Sprite(sprites[1]);
				tmp.Offset(posoffsets[i], posoffsets[i+1]);
				sprs.Add(tmp);
			}
			
			return new Sprite(sprs.ToArray());
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}