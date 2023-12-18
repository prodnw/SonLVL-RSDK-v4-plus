using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R6
{
	class MovingBlocks2 : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("R6/Objects.gif").GetSection(173, 1, 32, 32), -16, -16);
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(6, 0, 0, 31, 31); // LevelData.ColorWhite
			
			Sprite block = new Sprite(bitmap, -16, -16);
			
			debug = new Sprite(
			                new Sprite(block, 0, -64),
			                new Sprite(block, 0, -32),
			                new Sprite(block, 0,  32),
			                new Sprite(block, 0,  64));
			
			bitmap = new BitmapBits(2, (4 * 32) + 1);
			bitmap.DrawLine(6, 0, 0, 0, 4 * 32);
			
			debug = new Sprite(debug, new Sprite(bitmap, 0, -(2 * 32)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
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
			return debug;
		}
	}
}