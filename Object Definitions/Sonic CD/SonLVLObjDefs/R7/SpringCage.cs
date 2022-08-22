using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class SpringCage : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[6];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 3, 4, 5, 6 }); }
		}

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R7/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(90, 52, 16, 16), -8, -8);
			sprites[1] = new Sprite(sheet.GetSection(34, 96, 64, 24), -8, -28);
			sprites[2] = new Sprite(sheet.GetSection(59, 121, 56, 56), -24, -56);
			sprites[3] = new Sprite(sheet.GetSection(59, 121, 56, 56), -56, -24);
			sprites[4] = new Sprite(sheet.GetSection(34, 121, 24, 64), -28, -56);
			sprites[5] = new Sprite(sheet.GetSection(34, 96, 64, 24), -56, -28);
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0:
				default:
					return "Rotating";
				case 1:
					return "Pointing Left";
				case 3:
					return "Pointing Up-Right";
				case 2:
				case 4:
					return "Pointing Up";
				case 5:
					return "Pointing Up-Left";
				case 6:
					return "Pointing Right";
			}
		}

		public override Sprite Image
		{
			get { return SubtypeImage(0); }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			List<Sprite> cageSprites = new List<Sprite>();
			Sprite sprite;
			switch (subtype)
			{
				case 0:
				case 2:
				case 4:
				default:
					sprite = new Sprite(sprites[4]);
					cageSprites.Add(sprite);
					sprite = new Sprite(sprites[4]);
					sprite.Flip(true, false);
					cageSprites.Add(sprite);
					break;
				case 1:
					sprite = new Sprite(sprites[5]);
					cageSprites.Add(sprite);
					sprite = new Sprite(sprites[5]);
					sprite.Flip(false, true);
					cageSprites.Add(sprite);
					break;
				case 3:
					sprite = new Sprite(sprites[2]);
					cageSprites.Add(sprite);
					sprite = new Sprite(sprites[3]);
					sprite.Flip(true, true);
					cageSprites.Add(sprite);
					break;
				case 5:
					sprite = new Sprite(sprites[2]);
					sprite.Flip(true, false);
					cageSprites.Add(sprite);
					sprite = new Sprite(sprites[3]);
					sprite.Flip(false, true);
					cageSprites.Add(sprite);
					break;
				case 6:
					sprite = new Sprite(sprites[1]);
					cageSprites.Add(sprite);
					sprite = new Sprite(sprites[1]);
					sprite.Flip(false, true);
					cageSprites.Add(sprite);
					break;
			}
			sprite = new Sprite(sprites[0]);
			cageSprites.Add(sprite);
			return new Sprite(cageSprites.ToArray());
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}