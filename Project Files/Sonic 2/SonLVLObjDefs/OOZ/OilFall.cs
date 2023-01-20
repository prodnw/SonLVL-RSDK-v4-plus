using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.OOZ
{
	class OilFall : ObjectDefinition
	{
		private PropertySpec[] properties;

		private readonly Sprite[] spritesSmall = new Sprite[8];
		private readonly Sprite[] spritesSmallFlip = new Sprite[8];
		private readonly Sprite[] spritesLarge = new Sprite[8];
		private readonly Sprite[] spritesLargeDouble = new Sprite[8];

		public override void Init(ObjectData data)
		{
			// this does not perfectly mimic the rendering logic, but it does use the same spritesheet data and stuff

			BitmapBits sheet = LevelData.GetSpriteSheet("OOZ/Objects.gif");
			Sprite[] sprs;

			spritesSmall[0] = new Sprite(sheet.GetSection(182, 51, 6, 32), -2, -16);
			spritesSmall[1] = new Sprite(sheet.GetSection(182, 59, 6, 32), -2, -16);

			for (int i = 2; i <= 7; i++)
			{
				sprs = new Sprite[i];
				for (int j = 0; j < i; j++)
				{
					sprs[j] = new Sprite(spritesSmall[1], 0, 32 * (j));
				}
				spritesSmall[i] = new Sprite(sprs);
				spritesSmall[i].Offset(0, (i - 1) * -16);
			}

			spritesSmallFlip[0] = new Sprite(sheet.GetSection(182, 156, 6, 32), -2, -16);
			spritesSmallFlip[1] = new Sprite(sheet.GetSection(182, 156, 6, 32), -2, -16);

			for (int i = 2; i <= 7; i++)
			{
				sprs = new Sprite[i];
				for (int j = 0; j < i; j++)
				{
					sprs[j] = new Sprite(spritesSmallFlip[1], 0, 32 * (j));
				}
				spritesSmallFlip[i] = new Sprite(sprs);
				spritesSmallFlip[i].Offset(0, (i - 1) * -16);
			}

			spritesLarge[0] = new Sprite(sheet.GetSection(189, 75, 16, 32), -8, -16);
			spritesLarge[1] = new Sprite(sheet.GetSection(189, 107, 16, 32), -8, -16);

			for (int i = 2; i <= 7; i++)
			{
				sprs = new Sprite[i];
				for (int j = 0; j < i; j++)
				{
					sprs[j] = new Sprite(spritesLarge[1], 0, 32 * (j));
				}
				spritesLarge[i] = new Sprite(sprs);
				spritesLarge[i].Offset(0, (i - 1) * -16);
			}

			spritesLargeDouble[0] = new Sprite(sheet.GetSection(189, 107, 48, 32), -24, -16);
			spritesLargeDouble[1] = new Sprite(sheet.GetSection(189, 107, 48, 32), -24, -16);

			for (int i = 2; i <= 7; i++)
			{
				sprs = new Sprite[i];
				for (int j = 0; j < i; j++)
				{
					sprs[j] = new Sprite(spritesLargeDouble[1], 0, 32 * (j));
				}
				spritesLargeDouble[i] = new Sprite(sprs);
				spritesLargeDouble[i].Offset(0, (i - 1) * -16);
			}	
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {  }); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return spritesSmall[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return spritesSmall[2];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			if ((obj.PropertyValue & 0x10) == 0x10)
			{
				if ((obj.PropertyValue & 0x08) == 0x08)
				{
					return spritesLargeDouble[obj.PropertyValue % 8];
				}
				return spritesLarge[obj.PropertyValue % 8];
			}
			if ((obj.PropertyValue & 0x08) == 0x08)
			{
				return spritesSmallFlip[obj.PropertyValue % 8];
			}
			return spritesSmall[obj.PropertyValue % 8];
		}
	}
}