using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Coconuts : ObjectDefinition
	{
		private Sprite img;

		// Coconuts' subtype is normally 30, but this value isn't used in any proper way
		// (property value is supposed to be direction, but it gets reset to face the player in-game and a subtype of 30 doesn't exactly sound like a direction either)
		// MBZ Coconuts don't have anything in their prop val either anyways, so let's just gloss over it here

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '1')
			{
				img = new Sprite(LevelData.GetSpriteSheet("EHZ/Objects.gif").GetSection(1, 63, 26, 45), -8, -14);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(50, 256, 26, 45), -8, -14);
			}
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
			return subtype + "";
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
	}
}