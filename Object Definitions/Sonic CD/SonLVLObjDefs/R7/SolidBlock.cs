using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class SolidBlock : ObjectDefinition
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
				img = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(117, 219, 32, 28), -16, -16);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(1, 1, 32, 28), -16, -16);
			}
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0:
					return "Single Block";
				case 1:
					return "Two Blocks (Horizontal)";
				case 2:
					return "Three Blocks (Horizontal)";
				case 3:
					return "Four Blocks (Horizontal)";
				case 4:
					return "Two Blocks (Vertical)";
				case 5:
					return "Three Blocks (Vertical)";
				case 6:
					return "Four Blocks (Vertical)";
				case 7:
					return "Invisible Block";
				default:
					return "Unknown";
			}
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
			switch (obj.PropertyValue)
			{
				case 0:
				case 7:
				default:
					block = new Sprite(img);
					blocks.Add(block);
					break;
				case 1:
					block = new Sprite(img);
					block.Offset(-16, 0);
					blocks.Add(block);
					block = new Sprite(img);
					block.Offset(16, 0);
					blocks.Add(block);
					break;
				case 2:
					block = new Sprite(img);
					block.Offset(-32, 0);
					blocks.Add(block);
					block = new Sprite(img);
					blocks.Add(block);
					block = new Sprite(img);
					block.Offset(32, 0);
					blocks.Add(block);
					break;
				case 3:
					block = new Sprite(img);
					block.Offset(-48, 0);
					blocks.Add(block);
					block = new Sprite(img);
					block.Offset(-16, 0);
					blocks.Add(block);
					block = new Sprite(img);
					block.Offset(16, 0);
					blocks.Add(block);
					block = new Sprite(img);
					block.Offset(48, 0);
					blocks.Add(block);
					break;
				case 4:
					block = new Sprite(img);
					block.Offset(0, -16);
					blocks.Add(block);
					block = new Sprite(img);
					block.Offset(0, 16);
					blocks.Add(block);
					break;
				case 5:
					block = new Sprite(img);
					block.Offset(0, -32);
					blocks.Add(block);
					block = new Sprite(img);
					blocks.Add(block);
					block = new Sprite(img);
					block.Offset(0, 32);
					blocks.Add(block);
					break;
				case 6:
					block = new Sprite(img);
					block.Offset(0, -48);
					blocks.Add(block);
					block = new Sprite(img);
					block.Offset(0, -16);
					blocks.Add(block);
					block = new Sprite(img);
					block.Offset(0, 16);
					blocks.Add(block);
					block = new Sprite(img);
					block.Offset(0, 48);
					blocks.Add(block);
					break;
			}
			return new Sprite(blocks.ToArray());
		}
	}
}