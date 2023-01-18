using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class Stalactite : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			int sprX = 0;

			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'B':
					sprX = 189;
					break;
				case 'D':
					sprX = 155;
					break;
				case 'A':
				case 'C':
				default:
					sprX = 172;
					break;
			}
			
			img = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(sprX, 207, 16, 48), -8, -24);
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
			return (subtype) + "";
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