using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class BreakablePillar : ObjectDefinition
	{

		private readonly Sprite[] sprites = new Sprite[2];

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("ARZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(59, 42, 56, 90), -28, -72);
			sprites[1] = new Sprite(sheet.GetSection(223, 137, 32, 24), -16, 8);
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
			get { return new Sprite(sprites); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return new Sprite(sprites);
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return new Sprite(sprites);
		}
	}
}
