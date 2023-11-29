using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Animals
{
	class BlueBird : Animals.Bird
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Global/Items3.gif").GetSection(240, 199, 16, 16), -8, -8);
		}
	}
	
	class R3_Bird : Animals.Bird
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("R3/Objects3.gif").GetSection(132, 1, 16, 16), -8, -8);
		}
	}
	
	class R6_Bird : Animals.Bird
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("R6/Objects.gif").GetSection(197, 34, 24, 16), -12, -8);
		}
	}
	
	class R7_Bird : Animals.Bird
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(34, 186, 16, 16), -8, -8);
		}
	}
	
	class R8_Bird : Animals.Bird
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("R8/Objects2.gif").GetSection(143, 110, 16, 16), -8, -8);
		}
	}
	
	abstract class Bird : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public abstract Sprite GetFrame()
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			BitmapBits bitmap = new BitmapBits(257, 130);
			bitmap.DrawCircle(6, 128, 0, 128); // LevelData.ColorWhite
			debug = new Sprite(overlay, -128, 0);
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