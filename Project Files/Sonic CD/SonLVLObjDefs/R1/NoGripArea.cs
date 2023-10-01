using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class NoGripArea : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(69, 0, 0, 31, 31); // green, based on the object's in-game debug vis
			sprite = new Sprite(new Sprite(LevelData.GetSpriteSheet("Global/Display_k.gif").GetSection(187, 189, 16, 16), -8, -8), new Sprite(bitmap, -16, -16)); // using Knux's life icon, not sure if a generic "T" would be better instead?
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override bool Debug
		{
			get { return true; }
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
	}
}