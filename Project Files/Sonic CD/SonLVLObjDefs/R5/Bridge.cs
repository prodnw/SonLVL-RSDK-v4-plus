using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class Bridge : ObjectDefinition
	{
		private Sprite img;

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
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 8; }
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
			int st = -(((obj.PropertyValue) * 16) / 2) + 8;
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < System.Math.Max((int)obj.PropertyValue, 1); i++)
			{
				Sprite tmp = new Sprite(img);
				tmp.Offset(st + (i * 16), 0);
				sprs.Add(tmp);
			}
			return new Sprite(sprs.ToArray());
		}
	}
}