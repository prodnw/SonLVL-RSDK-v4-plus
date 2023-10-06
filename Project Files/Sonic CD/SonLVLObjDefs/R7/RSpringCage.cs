using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class RSpringCage : ObjectDefinition
	{
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R7/Objects.gif");
			
			Sprite[] frames = new Sprite[3];
			frames[0] = new Sprite(sheet.GetSection(34, 121, 24, 64), -28, -56);
			frames[1] = new Sprite(frames[0], 0, 48, true, false);
			frames[2] = new Sprite(sheet.GetSection(90, 52, 16, 16), -8, -8);
			
			sprite = new Sprite(frames);
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
			// if obj[+1] is an R7 Spring, then let's make sure we update its sprite too, since it normally depends on this object
			if ((LevelData.Objects.IndexOf(obj) + 1) < LevelData.Objects.Count)
				if (LevelData.Objects[LevelData.Objects.IndexOf(obj) + 1].Name == "R7 Spring")
					LevelData.Objects[LevelData.Objects.IndexOf(obj) + 1].UpdateSprite();
			
			return sprite;
		}
	}
}