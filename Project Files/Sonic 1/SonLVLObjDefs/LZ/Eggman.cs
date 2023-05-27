using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S1ObjectDefinitions.LZ
{
	class Eggman : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Eggman.gif").GetSection(1, 1, 64, 56), -28, -32);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
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
			// Copied from the original script
			int[] nodePositionTable = new int[] { 
				 0x0000, 0x0000,
				-0x700000, 0x0000,
				 0x380000, -0xC00000,
				 0x600000, -0x1000000,
				 0x0000, -0x4C00000,
				 0x13C0000, -0x5000000,
				 0xB80000, -0x4D00000
			};
			
			for (int i = 0; i < nodePositionTable.Length; i += 2)
			{
				nodePositionTable[i] += (obj.X << 16);
				nodePositionTable[i+1] += (obj.Y << 16);
			}
			
			int xmin = obj.X;
			int ymin = obj.Y;
			int xmax = obj.X;
			int ymax = obj.Y;
			
			for (int i = 0; i < nodePositionTable.Length; i += 2)
			{
				xmin = Math.Min(xmin, nodePositionTable[i] >> 16);
				ymin = Math.Min(ymin, nodePositionTable[i+1] >> 16);
				xmax = Math.Max(xmax, nodePositionTable[i] >> 16);
				ymax = Math.Max(ymax, nodePositionTable[i+1] >> 16);
			}
			
			BitmapBits bmp = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			
			for (int i = 2; i < nodePositionTable.Length; i += 2)
				bmp.DrawLine(6, (nodePositionTable[i-2] >> 16) - xmin, (nodePositionTable[i-1] >> 16) - ymin, (nodePositionTable[i] >> 16) - xmin, (nodePositionTable[i+1] >> 16) - ymin); // LevelData.ColorWhite
			
			return new Sprite(bmp, xmin - obj.X, ymin - obj.Y);
		}
	}
}