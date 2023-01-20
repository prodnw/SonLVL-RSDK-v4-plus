using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Rexon : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '5')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(143, 123, 23, 16), -19, -6);
				sprites[1] = new Sprite(sheet.GetSection(52, 38, 16, 16), -8, -8);
				sprites[2] = new Sprite(sheet.GetSection(91, 105, 32, 16), -16, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				
				// using fixed frames
				sprites[0] = new Sprite(sheet.GetSection(469, 289, 23, 16), -19, -6);
				sprites[1] = new Sprite(sheet.GetSection(517, 289, 16, 16), -8, -8);
				sprites[2] = new Sprite(sheet.GetSection(436, 289, 32, 16), -16, -8);
			}
			
			// prop val is unused, only ported Rexons have it anyways
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
			get { return SubtypeImage(0); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			// TODO: temp, probably should fix it but i think this looks funny
			// (in other words, idk how to do it the right way)
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 6; i++)
			{
				Sprite sprite = new Sprite(sprites[(i == 5 ? 0 : 1)]);
				sprite.Offset(0, -(i * 17));
				sprs.Add(sprite);
			}
			
			Sprite tmp = new Sprite(sprites[2]);
			sprs.Add(tmp);
			
			return new Sprite(sprs.ToArray());
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}