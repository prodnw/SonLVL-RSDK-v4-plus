using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Bomb : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder)
			{
				case "Zone05":
				default:
					img = new Sprite(LevelData.GetSpriteSheet("SLZ/Objects.gif").GetSection(53, 131, 20, 37), -10, -21);
					break;
				case "Zone06":
					img = new Sprite(LevelData.GetSpriteSheet("SBZ/Objects.gif").GetSection(52, 40, 20, 37), -10, -21);
					break;
				case "Zone07":
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(53, 328, 20, 37), -10, -21);
					break;
			}

			properties = new PropertySpec[1];
			
			// The ideal name would be "Direction" but that's already in use...
			properties[0] = new PropertySpec("Orientation", typeof(int), "Extended",
                "Where the Bomb is facing (not to be confused with object.direction).", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 },
					{ "Left - Roof", 2 },
					{ "Right - Roof", 3 }
				},
                (obj) => obj.PropertyValue & 3,
                (obj, value) => obj.PropertyValue = ((byte)((int)value)));
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
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			Sprite sprite = new Sprite(img);
			sprite.Flip((subtype & 1) != 0, (subtype & 2) != 0); // Do pardon the odd syntax...
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}