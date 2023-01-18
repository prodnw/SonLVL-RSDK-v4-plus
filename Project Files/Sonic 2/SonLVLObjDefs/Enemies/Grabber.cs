using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Grabber : ObjectDefinition
	{
		private PropertySpec[] properties;

		private readonly Sprite[] sprites = new Sprite[3];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '2')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("CPZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(5, 74, 40, 32), -27, -8);
				sprites[1] = new Sprite(sheet.GetSection(46, 74, 8, 8), -4, -16);
				sprites[2] = new Sprite(sheet.GetSection(5, 140, 23, 16), -6, 8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(131, 280, 40, 32), -27, -8);
				sprites[1] = new Sprite(sheet.GetSection(145, 334, 8, 8), -4, -16);
				sprites[2] = new Sprite(sheet.GetSection(178, 313, 30, 15), -6, 8);
			}

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Spiny will move.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (obj.PropertyValue & 1),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
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
			switch (subtype)
			{
				case 0:
					return "Facing Left";
				case 1:
					return "Facing Right";
				default:
					return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return new Sprite(sprites); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			Sprite sprite = new Sprite(sprites);
			sprite.Flip((subtype & 1) == 1, false);
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}
