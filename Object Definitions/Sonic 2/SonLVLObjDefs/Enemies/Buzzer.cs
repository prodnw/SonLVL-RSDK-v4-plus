using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Buzzer : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '1')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("EHZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 1, 48, 16), -24, -8);
				sprites[1] = new Sprite(sheet.GetSection(19, 50, 6, 5), 5, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 256, 48, 16), -24, -8);
				sprites[1] = new Sprite(sheet.GetSection(137, 331, 6, 5), 5, -8);
			}

			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("PDir", typeof(int), "Extended",
				"The direction the Buzzer will be facing initially (not to be confused with object.direction).", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Static", typeof(int), "Extended",
				"If the Buzzer should stay in one place, rather than hover around a spot. Only has effect in Origins' Mission Mode.",
				null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 2 }
				},
				(obj) => obj.PropertyValue & 2,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 253) | (byte)((int)value)));
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
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 2; i++)
			{
				Sprite sprite = new Sprite(sprites[i]);
				sprite.Flip((subtype & 1) == 1, false);
				sprs.Add(sprite);
			}
			return new Sprite(sprs.ToArray());
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}