using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R6
{
	class HangingTrigger : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, 1);
			
			BitmapBits bitmap = new BitmapBits(16, 22);
			bitmap.DrawRectangle(6, 0, 0, 15, 21); // LevelData.ColorWhite
			sprites[1] = new Sprite(sprites[0], new Sprite(bitmap, -8, -2));
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
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[1];
		}
	}
}