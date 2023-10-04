using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R6
{
	class TubeTrapDoor : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("R6/Objects.gif").GetSection(191, 175, 64, 80), -32, -40);
			
			BitmapBits bitmap = new BitmapBits(64, 80);
			bitmap.DrawRectangle(6, 0, 0, 63, 79); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -32, -40 - 80);
			
			bitmap = new BitmapBits(2, 80);
			bitmap.DrawLine(6, 0, 0, 0, 80); // LevelData.ColorWhite
			debug = new Sprite(debug, new Sprite(bitmap, 0, -80));
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