using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class SpearBlock : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[5];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '9')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MPZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(34, 102, 8, 32), -4, -16);
				sprites[1] = new Sprite(sheet.GetSection(34, 84, 32, 8), -16, -4);
				sprites[2] = new Sprite(sheet.GetSection(43, 102, 8, 32), -4, -16);
				sprites[3] = new Sprite(sheet.GetSection(34, 93, 32, 8), -16, -4);
				sprites[4] = new Sprite(sheet.GetSection(52, 102, 32, 32), -16, -16);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(859, 615, 8, 32), -4, -16);
				sprites[1] = new Sprite(sheet.GetSection(893, 648, 32, 8), -16, -4);
				sprites[2] = new Sprite(sheet.GetSection(868, 615, 8, 32), -4, -16);
				sprites[3] = new Sprite(sheet.GetSection(893, 657, 32, 8), -16, -4);
				sprites[4] = new Sprite(sheet.GetSection(926, 541, 32, 32), -16, -16);
			}
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Spear Direction", typeof(int), "Extended",
				"Where the Spear will point initially.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Right", 1 },
					{ "Downwards", 2 },
					{ "Left", 3 }
				},
				(obj) => (obj.PropertyValue & 3),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 252) | (byte)((int)value)));
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
			return null;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			// A bit odd, could probably be optimised a bit
			// This is mostly just a direct translation of what the base game does
			
			List<Sprite> sprs = new List<Sprite>();
			
			Sprite tmp = new Sprite(sprites[(obj.PropertyValue & 3)]);
			switch (obj.PropertyValue & 3)
			{
				case 0:
					tmp.Offset(0, -32);
					break;
				case 1:
					tmp.Offset(32, 0);
					break;
				case 2:
					tmp.Offset(0, 32);
					break;
				case 3:
					tmp.Offset(-32, 0);
					break;
			}
			
			sprs.Add(tmp);
			sprs.Add(sprites[4]);
			
			return new Sprite(sprs.ToArray());
		}
	}
}