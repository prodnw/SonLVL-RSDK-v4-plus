using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Antenna : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			// The Antenna normally just loads SCZ sprites regardless of zone, but for consistency with other enemies
			// let's use its MBZ sprites if needed anyways
			
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '1')
			{
				img = new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(195, 159, 8, 30), -4, -16);
			}
			else
			{
				// (SCZ mission ends up here too)
				
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(630, 345, 8, 30), -4, -16);
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