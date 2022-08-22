using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class SolidBlock : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("C"))
			{
				img = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(117, 219, 32, 32), -16, -16);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(1, 1, 32, 32), -16, -16);
			}
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Formation", typeof(int), "Extended",
				"The formation of the blocks.", null, new Dictionary<string, int>
				{
					{ "Single Block", 0},
					{ "Two Blocks (Horizontal)", 1},
					{ "Three Blocks (Horizontal)", 2},
					{ "Four Blocks (Horizontal)", 3},
					{ "Two Blocks (Vertical)", 4},
					{ "Three Blocks (Vertical)", 5},
					{ "Four Blocks (Vertical)", 6},
					{ "Invisible Block", 7}
				},
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
			return null;
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
			Sprite block;
			switch (obj.PropertyValue)
			{
				case 0:
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
				case 7:
					break;
			}
			return new Sprite(blocks.ToArray());
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			if (obj.PropertyValue == 7)
				return new Rectangle(obj.X - 16, obj.Y - 16, 32, 32);
				
			return Rectangle.Empty;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (obj.PropertyValue == 7)
			{
				var bitmap = new BitmapBits(33, 33);
				bitmap.DrawRectangle(LevelData.ColorWhite, 0, 0, 32, 32);
				return new Sprite(bitmap, -16, -16);
			}
			
			return null;
		}
	}
}