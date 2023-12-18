using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R8
{
	class SpikePuzzle : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R8/Objects2.gif");
			Sprite[] row = { new Sprite(sheet.GetSection(133, 1, 24, 24), -80, -8),
			                    new Sprite(sheet.GetSection(100, 1, 32, 24), -56, -8),
			                    new Sprite(sheet.GetSection(158, 1, 24, 16), -24, -8),
			                    new Sprite(sheet.GetSection(158, 1, 24, 16),   0, -8),
			                    new Sprite(sheet.GetSection(100, 1, 32, 24),  24, -8),
			                    new Sprite(sheet.GetSection(133, 1, 24, 24),  56, -8) };
			
			sprite = new Sprite(row);
			
			Sprite[] outlines = new Sprite[3];
			
			// Gap in the middle
			BitmapBits bitmap = new BitmapBits(161, 25);
			bitmap.DrawRectangle(6, 0, 0, 24 + 32, 24);
			bitmap.DrawRectangle(6, (24 + 32) + (24 + 24), 0, 32 + 24, 24);
			outlines[0] = new Sprite(bitmap, -80, -8);
			
			// Gap to the left
			bitmap.Clear();
			bitmap.DrawRectangle(6, 0, 0, 24, 24);
			bitmap.DrawRectangle(6, (24) + (24 + 24), 0, 32 + 32 + 24, 24);
			outlines[1] = new Sprite(bitmap, -80, -8);
			
			// Gap to the right
			bitmap.Clear();
			bitmap.DrawRectangle(6, 0, 0, 32 + 32 + 24, 24);
			bitmap.DrawRectangle(6, (32 + 32 + 24) + (24 + 24), 0, 24, 24);
			outlines[2] = new Sprite(bitmap, -80, -8);
			
			// Taken from the end of SpikePuzzle_Reset, with each ID subtracted by 1
			int[,] patterns = {{0, 1, 0, 2, 0}, {0, 2, 0, 1, 0}};
			for (int i = 0; i < 2; i++)
			{
				debug[i] = new Sprite();
				int sy = 96;
				for (int j = 0; j < 5; j++)
				{
					debug[i] = new Sprite(debug[i], new Sprite(outlines[patterns[i,j]], 0, sy));
					sy -= 24;
				}
			}
			
			properties[0] = new PropertySpec("Puzzle", typeof(int), "Extended",
				"Which puzzle type this object should be.", null, new Dictionary<string, int>
				{
					{ "Puzzle A", 0 },
					{ "Puzzle B", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Puzzle A" : "Puzzle B";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue == 0) ? 0 : 1];
		}
	}
}