using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class RotatingLog : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A':
				default:
					img = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(35, 1, 16, 16), -8, -8);
					break;
				case 'B':
					img = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(67, 174, 16, 16), -8, -8);
					break;
				case 'C':
					img = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(83, 174, 16, 16), -8, -8);
					break;
				case 'D':
					img = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(83, 158, 16, 16), -8, -8);
					break;
			}
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Is Leader", typeof(int), "Extended",
				"If this Log is the lead of the set. Only the first one in a set should have this one set.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 1 }
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

		public override string SubtypeName(byte subtype)
		{
			return (subtype) + " logs";
		}

		public override Sprite Image
		{
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}
	}
}