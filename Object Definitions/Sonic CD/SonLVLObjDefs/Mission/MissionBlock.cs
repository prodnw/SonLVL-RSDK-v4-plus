using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Mission
{
	class MissionBlock : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Mission/Objects.gif").GetSection(1, 1, 32, 32), -16, -16);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 8; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype) + " blocks";
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
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < Math.Min(1, obj.PropertyValue); i++)
			{
				Sprite tmp = new Sprite(img);
				tmp.Offset(i * 32, 0);
				sprs.Add(tmp);
			}
			return new Sprite(sprs.ToArray());
		}
	}
}