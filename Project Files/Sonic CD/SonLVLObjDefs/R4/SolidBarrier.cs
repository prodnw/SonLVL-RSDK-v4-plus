using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class SolidBarrier : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			Sprite block;
			
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case 'A':
				default:
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects.gif").GetSection(163, 1, 32, 32));
					break;
				case 'B':
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 157, 32, 32));
					break;
				case 'C':
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 190, 32, 32));
					break;
				case 'D':
					block = new Sprite(LevelData.GetSpriteSheet("R4/Objects2.gif").GetSection(1, 223, 32, 32));
					break;
			}
			
			List<Sprite> sprites = new List<Sprite>();
			
			for (int offy = -64; offy < 64; offy += 32)
			{
				for (int offx = -64; offx < 64; offx += 32) sprites.Add(new Sprite(block, offx, offy));
			}
			
			sprite = new Sprite(sprites.ToArray());
			
			BitmapBits bitmap = new BitmapBits(224, 129);
			bitmap.DrawRectangle(6, 160, 0, 63, 127); // LevelData.ColorWhite
			bitmap.DrawLine(6, 0, 64, 192, 64); // LevelData.ColorWhite
			debug = new Sprite(bitmap, 0, -64);
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
		
		/*
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
		*/
	}
}