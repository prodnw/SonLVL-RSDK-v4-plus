using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Spiker : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[4];
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '5')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(66, 206, 23, 24), -12, -8);
				sprites[1] = new Sprite(sheet.GetSection(66, 173, 24, 32), -12, -32);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(67, 781, 23, 24), -12, -8);
				sprites[1] = new Sprite(sheet.GetSection(67, 748, 24, 32), -12, -32);
			}

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("PDir", typeof(int), "Extended",
				"Which way the Spiker is facing (not to be confused with object.direction).", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 },
					{ "Left (Roof)", 2 },
					{ "Right (Roof)", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = ((byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3 }); }
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
				case 2:
					return "Facing Left, Roof";
				case 3:
					return "Facing Right, Roof";
				default:
					return "Unknown";
			}
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
				sprite.Flip((subtype & 1) == 1, (subtype & 2) == 2);
				sprite.Offset(0, (subtype & 2) == 0 ? 8 : -8);
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