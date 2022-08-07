using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class CBSwitch : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("B"))
			{
				img = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(1, 225, 32, 28), -4, -16);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(1, 18, 32, 28), -4, -16);
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
			return (subtype) + "";
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