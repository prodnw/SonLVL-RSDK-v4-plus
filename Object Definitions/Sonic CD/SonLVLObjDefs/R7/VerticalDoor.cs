using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class VerticalDoor : ObjectDefinition
	{
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 }); }
		}

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("C"))
			{
				img = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(117, 219, 32, 28), -16, 0);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(1, 1, 32, 28), -16, 0);
			}
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
			List<Sprite> blocks = new List<Sprite>();
			Sprite block = new Sprite(img);
			block.Offset(0, -32);
			blocks.Add(block);
			block = new Sprite(img);
			blocks.Add(block);
			return new Sprite(blocks.ToArray());
		}
	}
}