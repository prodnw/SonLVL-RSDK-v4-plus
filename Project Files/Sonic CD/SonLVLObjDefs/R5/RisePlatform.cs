using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class RisePlatform2 : R5.RisePlatform
	{
		public override int distance { get { return 320; } }
		
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(1, 84, 96, 32), -48, -16);
		}
	}
	
	class RisePlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public virtual int distance { get { return 810; } }
		
		public virtual Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(1, 51, 32, 32), -16, -16);
		}
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			BitmapBits bitmap = new BitmapBits(2, distance + 1);
			bitmap.DrawLine(6, 0, 0, 0, distance);
			debug = new Sprite(bitmap, 0, -distance);
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