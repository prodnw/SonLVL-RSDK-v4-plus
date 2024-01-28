using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class PushButton : ObjectDefinition
	{
		private Sprite sprite;

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects.gif").GetSection(130, 35, 32, 16), -16, -8);
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
			// If obj[-1] is a Button Blocks 1, then let's offset ourselves to match its position
			// (normally the button is supposed to be on top of the pillar, but in the scene, its position is *in* the pillar)
			// (in-game, the object gets moved to its correct position, so let's show that position in the editor too)
			ObjectEntry other = LevelData.Objects[Math.Max(0, LevelData.Objects.IndexOf(obj) - 1)];
			if (other.Name == "Button Blocks 1")
			{
				int xdiff = (other.X + 16) - obj.X;
				int ydiff = (other.Y - 72) - obj.Y;
				if (Math.Abs(xdiff) < 64 && Math.Abs(ydiff) < 64) // in-game it doesn't matter how far it is, but let's have a limit here
					return new Sprite(sprite, xdiff, ydiff);
			}
			
			return sprite;
		}
	}
}