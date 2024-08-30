using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R8
{
	class TubePath : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug = new Sprite[13];
		
		public override void Init(ObjectData data)
		{
			// half sure that one of the sprites in R8/Objects2 is supposed to be for this object, but let's keep the T icon for consistency between all types (and imo i think the R8 icon is kind of weird-)
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			// Tube Switch icon
			sprites[1] = new Sprite(
			                new Sprite(sprite, -8, -8),
			                new Sprite(sprite,  8, -8),
			                new Sprite(sprite, -8,  8),
			                new Sprite(sprite,  8,  8));
			
			// Which angles each subtype should its lines drawn towards
			double[][] angles = {
				new double[] {0, 0.5, 1, 1.5}, // four ways
				new double[] {0.5, 1.5}, // up/down
				new double[] {0.5, 0}, // up/right
				new double[] {1.5, 1}, // down/left
				new double[] {0.5, 1}, // up/left
				new double[] {1.5, 0}, // down/right
				new double[] {1.75, 1}, // down right/left
				new double[] {0.75, 0}, // up left/right
				new double[] {0.25, 1}, // up right/left
				new double[] {1.25, 0}, // down left/right
				new double[0], // tunnel (it's p much just a tubeswitch and not an actual tunnel path)
				new double[] {0.25, 0.75}, // up right/up left
				new double[] {1.75, 1.25} // down right/down left
			};
			
			BitmapBits bitmap;
			
			for (int i = 0; i < angles.Length; i++)
			{
				bitmap = new BitmapBits(96, 96);
				
				for (int j = 0; j < angles[i].Length; j++)
				{
					int x =  Math.Min(Math.Max((int)(Math.Cos(angles[i][j] * Math.PI) * 100), -48), 48);
					int y = -Math.Min(Math.Max((int)(Math.Sin(angles[i][j] * Math.PI) * 100), -48), 48);
					bitmap.DrawArrow(24, 48, 48, x + 48, y + 48);
				}
				
				debug[i] = new Sprite(bitmap, -48, -48);
				
				debug[i] = new Sprite(
					new Sprite(debug[i], -1, -1),
					new Sprite(debug[i],  0, -1),
					new Sprite(debug[i],  1, -1),
					new Sprite(debug[i], -1,  0),
					new Sprite(debug[i],  1,  0),
					new Sprite(debug[i], -1,  1),
					new Sprite(debug[i],  0,  1),
					new Sprite(debug[i],  1,  1));
			}
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this object should send the player.", null, new Dictionary<string, int>
				{
					{ "Junction", 0 },
					{ "Entrance", 1 }, // Entrance/Exit
					{ "Tube Switch", 10 },
					{ "Up <-> Right", 2 },
					{ "Down <-> Left", 3 },
					{ "Up <-> Left", 4 },
					{ "Down <-> Right", 5 },
					{ "Down Right <-> Left", 6 },
					{ "Up Left <-> Right", 7 },
					{ "Up Right <-> Left", 8 },
					{ "Down Left <-> Right", 9 },
					{ "Up Right <-> Up Left", 11 },
					{ "Down Right <-> Down Left", 12 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 10, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12}); }
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
			return sprites[(obj.PropertyValue == 10) ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if ((obj.PropertyValue > 12) && (obj.PropertyValue != 10))
				return null;
			
			return debug[obj.PropertyValue];
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