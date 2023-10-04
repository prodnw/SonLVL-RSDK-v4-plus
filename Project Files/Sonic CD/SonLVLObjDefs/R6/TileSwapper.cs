using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R6
{
	class TileSwapper : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite[,] debug = new Sprite[2,4]; // [trigger box/chunk box], [prop val]
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			// trigger hitboxes.. maybe they should be an outline of which chunks will get changed instead?
			int[][] hitboxes = {new int[] {0, -64, 64, 64}, new int[] {-48, -64, -8, 64}, new int[] {-48, -64, -8, 64}, new int[] {0, -64, 64, 64}};
			int[] chunkcounts = {6, 3, 5, 2};
			for (int i = 0; i < hitboxes.Length; i++)
			{
				BitmapBits bitmap = new BitmapBits(hitboxes[i][2] - hitboxes[i][0], hitboxes[i][3] - hitboxes[i][1]);
				bitmap.DrawRectangle(6, 0, 0, bitmap.Width-1, bitmap.Height-1); // LevelData.ColorWhite
				debug[0,i] = new Sprite(bitmap, hitboxes[i][0], hitboxes[i][1]);
				
				bitmap = new BitmapBits(chunkcounts[i] * 128 + 1, 128);
				bitmap.DrawRectangle(6, 0, 0, bitmap.Width-1, bitmap.Height-1); // LevelData.ColorWhite
				debug[1,i] = new Sprite(bitmap);
			}
			
			properties[0] = new PropertySpec("Trigger", typeof(int), "Extended",
				"Which direction this object should push the player.", null, new Dictionary<string, int>
				{
					{ "Catwalk - 6 Chunks", 0 },
					{ "Catwalk - 3 Chunks", 1 },
					{ "Catwalk - 5 Chunks", 2 },
					{ "Tunnel", 3 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			string[] names = {"Type A", "Catwalk - 3 Chunks", "Catwalk - 5 Chunks", "Tunnel", "Unknown"};
			return names[(subtype > 3) ? 4 : subtype];
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
		
		// this might be a bit too cluttered? not sure..
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (obj.PropertyValue > 3)
				return null;
			
			int sx = obj.X & ~0x7f;
			int sy = obj.Y & ~0x7f;
			
			return new Sprite(debug[0, obj.PropertyValue], new Sprite(debug[1, obj.PropertyValue], sx - obj.X, sy - obj.Y));
		}
	}
}