using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R6
{
	class MovingBlocks : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite[] debug = new Sprite[5];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("R6/Objects.gif").GetSection(173, 1, 32, 32), -16, -16);
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(6, 0, 0, 31, 31); // LevelData.ColorWhite
			Sprite block = new Sprite(bitmap, -16, -16);
			
			// yeah this kinda looks p messy, but i don't have any better ideas, so..
			
			// block positions
			int[][] offsets = {
				new int[] {
					0, -32,
					0, -64,
					0, -96,
					32, 0,
					64, 0,
					96, 0,
					0, 32,
					0, 64,
					0, 96,
					-32, 0,
					-64, 0,
					-96, 0},
				new int[] {
					32, 0,
					64, 0,
					64, -32,
					0, 32,
					-32, 32,
					-64, 32,
					-32, 0,
					-32, -32,
					-64, -32,
					0, -32,
					32, -32,
					32, -64},
				new int[] {},
				new int[] {0, -32,
					0, -64,
					0, -96,
					32, 0,
					64, 0,
					96, 0,
					0, 32,
					0, 64,
					0, 96,
					-32, 0,
					-64, 0,
					-96, 0},
				new int[] {
					0, -32,
					0, -64,
					32, -64,
					32, 0,
					32, -32,
					64, -32,
					0, 32,
					0, 64,
					32, 64,
					-32, 0,
					-64, 0,
					-96, 0}
			};
			
			// line points
			int[][] points = {
				new int[] {
					0, 0,
					0, -96,
					
					0, 0,
					96, 0,
					
					0, 0,
					0, 96,
					
					0, 0,
					-96, 0},
				new int[] {
					0, 0,
					64, 0,
					
					64, 0,
					64, -32,
					
					0, 0,
					0, 32,
					
					0, 32,
					-64, 32,
					
					0, 0,
					-32, 0,
					
					-32, 0,
					-32, -32,
					
					-32, -32,
					-64, -32,
					
					0, 0,
					0, -32,
					
					0, -32,
					32, -32,
					
					32, -32,
					32, -64},
				new int[0],
				new int[] {
					0, 0,
					0, -96,
					
					0, 0,
					96, 0,
					
					0, 0,
					0, 96,
					
					0, 0,
					-96, 0},
				new int[] {
					0, 0,
					0, -64,
					
					0, -64,
					32, -64,
					
					0, 0,
					32, 0,
					
					32, 0,
					32, -32,
					
					32, -32,
					64, -32,
					
					0, 0,
					0, 64,
					
					0, 64,
					32, 64,
					
					0, 0,
					-96, 0}
			};
			
			for (int i = 0; i < offsets.Length; i++)
			{
				debug[i] = new Sprite();
				bitmap = new BitmapBits(225, 225); // (7 * 32) + 1
				
				for (int j = 0; j < offsets[i].Length; j += 2)
					debug[i] = new Sprite(debug[i], new Sprite(block, offsets[i][j], offsets[i][j+1]));
				
				for (int j = 0; j < points[i].Length; j += 4)
					bitmap.DrawLine(6, points[i][j] + 112, points[i][j+1] + 112, points[i][j+2] + 112, points[i][j+3] + 112);
				
				debug[i] = new Sprite(debug[i], new Sprite(bitmap, -112, -112));
			}
			
			properties[0] = new PropertySpec("Pattern", typeof(int), "Extended",
				"Which movement pattern this object should have.", null, new Dictionary<string, int>
				{
					{ "Long (Clockwise)", 0 },
					{ "Tetrominoes A", 1 },
					{ "Long (Counter-Clockwise)", 3 },
					{ "Tetrominoes B", 4 },
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 3, 4} ); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Long (Clockwise)";
				case 1: return "Tetrominoes A";
				case 3: return "Long (Counter-Clockwise)";
				case 4: return "Tetrominoes B";
				
				default: return "Unknown";
			}
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
			return (obj.PropertyValue < 5) ? debug[obj.PropertyValue] : null;
		}
	}
}