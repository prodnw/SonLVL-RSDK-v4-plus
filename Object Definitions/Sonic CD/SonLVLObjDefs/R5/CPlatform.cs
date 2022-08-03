using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using SonicRetro.SonLVL.API;

namespace SCDObjectDefinitions.R5
{
	public class CPlatform : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R5/Objects.gif");
			img = new Sprite(sheet.GetSection(108, 51, 16, 32), -8, -16);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 6, 8, 10, 12, 14, 16 }); }
		}

		public override byte DefaultSubtype { get { return 6; } }

		public override string SubtypeName(byte subtype)
		{
			return "length";
		}

		public override Sprite Image
		{
			return img;
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
