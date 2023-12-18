using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace SCDObjectDefinitions.R6
{
	class FreezeJet : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R6/Objects.gif");
			
			sprites[0] = new Sprite(sheet.GetSection(1, 1, 64, 16), -32, -8); // jet, bounds box is this alone and doesn't include the beam
			sprites[1] = new Sprite(new Sprite(sheet.GetSection(1, 18, 32, 64), -16, 4), sprites[0]); // w/ beam, beam goes behind the jet
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
			get { return sprites[1]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[1];
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			Rectangle bounds = sprites[0].Bounds;
			bounds.Offset(obj.X, obj.Y);
			return bounds;
		}
	}
}