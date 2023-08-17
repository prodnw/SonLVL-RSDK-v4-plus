using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

// Not sure how I want to handle this object yet.. do I want to have 4 copies of the exact same def, since they're all separate objects in the scripts?
// Or do I want to just go the actually sensible route and maybe just make a "Common" folder or something...

namespace SCDObjectDefinitions.R3
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
			get { return new ReadOnlyCollection<byte>(new byte[] {}); }
		}

		public override string SubtypeName(byte subtype)
		{
			return "";
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