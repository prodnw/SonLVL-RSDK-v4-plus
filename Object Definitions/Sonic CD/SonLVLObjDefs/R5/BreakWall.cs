using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class BreakWall : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A':
				default:
					img = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(223, 141, 32, 96), -16, -48);
					break;
				case 'B':
					img = new Sprite(LevelData.GetSpriteSheet("R5/Objects3.gif").GetSection(132, 1, 32, 96), -16, -48);
					break;
				case 'C':
					img = new Sprite(LevelData.GetSpriteSheet("R5/Objects3.gif").GetSection(66, 1, 32, 96), -16, -48);
					break;
				case 'D':
					img = new Sprite(LevelData.GetSpriteSheet("R5/Objects3.gif").GetSection(99, 1, 32, 96), -16, -48);
					break;
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