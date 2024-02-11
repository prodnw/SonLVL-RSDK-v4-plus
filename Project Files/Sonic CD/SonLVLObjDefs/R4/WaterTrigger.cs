using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class WaterTrigger : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite[] debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			// (left, top, right, bottom)
			int[][] hitboxes = {
				new int[] {-112, -64, 0, 160},
				new int[] {-128, 0, 112, 64},
				new int[] {-128, 0, 112, 64},
				new int[] {-128, 0, 112, 64},
				new int[] {0, -64, 256, 64},
				new int[] {-64, -160, 64, 0}
			};
			
			int[][] arrows = {
				new int[] {80, 220, 80, 64, 80, 54, 10, 54},
				new int[] {10, 10, 230, 54},
				new int[] {230, 10, 10, 54},
				new int[] {230, 10, 10, 54},
				new int[] {10, 64, 246, 64},
				new int[] {10, 32, 54, 32, 64, 32, 64, 150}
			};
			
			debug = new Sprite[hitboxes.Length];
			
			for (int i = 0; i < hitboxes.Length; i++)
			{
				BitmapBits bitmap = new BitmapBits(hitboxes[i][2] - hitboxes[i][0], hitboxes[i][3] - hitboxes[i][1]);
				bitmap.DrawRectangle(6, 0, 0, bitmap.Width - 1, bitmap.Height - 1);
				
				for (int j = 0; j < arrows[i].Length; j += 4)
					bitmap.DrawArrow(6, arrows[i][j], arrows[i][j+1], arrows[i][j+2], arrows[i][j+3]);
				
				debug[i] = new Sprite(bitmap, hitboxes[i][0], hitboxes[i][1]);
			}
			
			properties[0] = new PropertySpec("Trigger", typeof(int), "Extended",
				"Which type of current this object should trigger.", null, new Dictionary<string, int>
				{
					{ "Up -> Left", 0 },
					{ "Down Right", 1 },
					{ "Down Left (Right exit)", 2 },
					{ "Down Left (Bottom exit)", 3 },
					{ "Right", 4 },
					{ "Right -> Down", 5 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5}); }
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