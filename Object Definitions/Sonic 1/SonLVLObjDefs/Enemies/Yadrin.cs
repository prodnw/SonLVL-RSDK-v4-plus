using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Yadrin : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '1':
				default:
					img = new Sprite(LevelData.GetSpriteSheet("GHZ/Objects.gif").GetSection(1, 2, 40, 38), -20, -20);
					break;
				case '3':
					img = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(1, 2, 40, 38), -20, -20);
					break;
				case '7':
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(138, 2, 40, 38), -20, -20);
					break;
			}

			properties = new PropertySpec[1];
			
			properties[0] = new PropertySpec("PDir", typeof(int), "Extended",
				"Where the Yadrin is facing (not to be confused with object.direction).", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => obj.PropertyValue & 1,
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
			sprite.Flip((subtype & 1) != 0, false);
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}