using SonicRetro.SonLVL.API;
using System;

namespace S2ObjectDefinitions.CNZ
{
	class EggmanBomb : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '4')
			{
				img = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(145, 172, 8, 8), -4, -4);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(224, 130, 8, 8), -4, -4);
			}
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
			return subtype + "";
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