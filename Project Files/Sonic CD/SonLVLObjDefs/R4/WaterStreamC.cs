using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class WaterStreamC : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite[] debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			// 0 - counter clockwise
			// 1 - clockwise
			// 2 - counter clockwise
			// 3 - clockwise
			// 4 - clockwise
			// 5 - counter-clockwise
			// 6 - clockwise
			// 7 - clockwise
			// 8 - counter-clockwise
			// 9 - clockwise
			// 10 - counter-clockwise
			
			// (left, top, right, bottom)
			int[][] hitboxes = {
				new int[] {-96, -96, 96, 96},
				new int[] {-96, -96, 96, 96},
				new int[] {-128, -128, 128, 128},
				new int[] {-128, -128, 128, 128},
				new int[] {-160, -96, 0, 64},
				new int[] {0, -96, 96, 0},
				new int[] {-96, 0, 0, 96},
				new int[] {-96, -96, 0, 0},
				new int[] {0, -128, 128, 0}, // C (right, top half)
				new int[] {-128, -128, 0, 128}, // C (left)
				new int[] {0, -128, 128, 128} // C (right)
			};
			
			int[] radius = {64, 64, 80, 80, 80, 52, 52, 52, 96, 96, 96};
			bool[] flip = {true, false, true, false, false, true, false, false, true, false, true};
			
			debug = new Sprite[hitboxes.Length];
			
			for (int i = 0; i < hitboxes.Length; i++)
			{
				BitmapBits bitmap = new BitmapBits(hitboxes[i][2] - hitboxes[i][0], hitboxes[i][3] - hitboxes[i][1]);
				bitmap.DrawRectangle(6, 0, 0, bitmap.Width - 1, bitmap.Height - 1);
				
				bitmap.DrawCircle(6, -hitboxes[i][0], -hitboxes[i][1], radius[i]);
				
				for (double angle = Math.PI/4; angle < (2 * Math.PI); angle += (Math.PI / 2))
				{
					if (flip[i])
						bitmap.DrawArrow(6, (int)(Math.Cos(angle + 0.1) * radius[i]) - hitboxes[i][0], (int)(Math.Sin(angle + 0.1) * radius[i]) - hitboxes[i][1], (int)(Math.Cos(angle - 0.1) * radius[i]) - hitboxes[i][0], (int)(Math.Sin(angle - 0.1) * radius[i]) - hitboxes[i][1]);
					else
						bitmap.DrawArrow(6, (int)(Math.Cos(angle - 0.1) * radius[i]) - hitboxes[i][0], (int)(Math.Sin(angle - 0.1) * radius[i]) - hitboxes[i][1], (int)(Math.Cos(angle + 0.1) * radius[i]) - hitboxes[i][0], (int)(Math.Sin(angle + 0.1) * radius[i]) - hitboxes[i][1]);
				}
				
				debug[i] = new Sprite(bitmap, hitboxes[i][0], hitboxes[i][1]);
			}
			
			// yeah these names are.. really bad
			// i don't know what else to call 'em, though
			// the debug vis should hopefully explain it better
			properties[0] = new PropertySpec("Current", typeof(int), "Extended",
				"Which size and direction this current should be.", null, new Dictionary<string, int>
				{
					{ "Small Circle (Counter-Clockwise)", 0 },
					{ "Small Circle (Clockwise)", 1 },
					{ "Large Circle (Counter-Clockwise)", 2 },
					{ "Large Circle (Clockwise)", 3 },
					{ "Partial Circle (Clockwise)", 4 },
					{ "Top Right (Counter-Clockwise)", 5 },
					{ "Bottom Left (Clockwise)", 6 },
					{ "Top Left (Clockwise)", 7 },
					{ "Large Top Right (Counter-Clockwise)", 8 },
					{ "Semi Circle (Clockwise)", 9 },
					{ "Semi Square (Counter-Clockwise)", 10 },
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return properties[0].Enumeration.GetKey(subtype);
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
			return (obj.PropertyValue < debug.Length) ? debug[obj.PropertyValue] : null;
		}
	}
	
	public static class BitmapBitsExtensions
	{
		public static void DrawArrow(this BitmapBits bitmap, byte index, int x1, int y1, int x2, int y2)
		{
			bitmap.DrawLine(index, x1, y1, x2, y2);
			
			double angle = Math.Atan2(y1 - y2, x1 - x2);
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle + 0.40) * 10), y2 + (int)(Math.Sin(angle + 0.40) * 10));
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle - 0.40) * 10), y2 + (int)(Math.Sin(angle - 0.40) * 10));
		}
	}
}