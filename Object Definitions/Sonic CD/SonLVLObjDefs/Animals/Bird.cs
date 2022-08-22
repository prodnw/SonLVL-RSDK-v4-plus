using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Animals
{
	class BlueBird : Animals.Bird
	{
		public override Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("Global/Items3.gif").GetSection(240, 199, 16, 16), -8, -8));
		}
	}
	
	class R3_Bird : Animals.Bird
	{
		public override Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("R3/Objects3.gif").GetSection(132, 1, 16, 16), -8, -8));
		}
	}
	
	class R6_Bird : Animals.Bird
	{
		public override Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("R6/Objects.gif").GetSection(197, 34, 24, 16), -12, -8));
		}
	}
	
	class R7_Bird : Animals.Bird
	{
		public override Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(34, 186, 16, 16), -8, -8));
		}
	}
	
	class R8_Bird : Animals.Bird
	{
		public override Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("R8/Objects2.gif").GetSection(143, 110, 16, 16), -8, -8));
		}
	}
}

namespace SCDObjectDefinitions.Animals
{
	abstract class Bird : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			img = GetSprite();
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype) + "";
		}

		public override Sprite Image
		{
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			var overlay = new BitmapBits(257, 130);
			overlay.DrawCircle(LevelData.ColorWhite, 128, 0, 128);
			return new Sprite(overlay, -128, 0);
		}
		
		public virtual Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("Global/Items3.gif").GetSection(240, 199, 16, 16), -8, -8));
		}
	}
}