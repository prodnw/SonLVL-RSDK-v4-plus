using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.HTZ
{
	class BreakBlock : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[5];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '5')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("HTZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(69, 1, 32, 80), -16, -40);
				sprites[1] = new Sprite(sheet.GetSection(69, 17, 32, 64), -16, -32);
				sprites[2] = new Sprite(sheet.GetSection(69, 33, 32, 48), -16, -24);
				sprites[3] = new Sprite(sheet.GetSection(69, 49, 32, 32), -16, -16);
				sprites[4] = new Sprite(sheet.GetSection(69, 65, 32, 16), -16, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(105, 687, 32, 80), -16, -40);
				sprites[1] = new Sprite(sheet.GetSection(105, 703, 32, 64), -16, -32);
				sprites[2] = new Sprite(sheet.GetSection(105, 719, 32, 48), -16, -24);
				sprites[3] = new Sprite(sheet.GetSection(105, 735, 32, 32), -16, -16);
				sprites[4] = new Sprite(sheet.GetSection(105, 751, 32, 16), -16, -8);
			}
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How many blocks comprise this Break Block.", null, new Dictionary<string, int>
				{
					{ "5 Blocks", 0 },
					{ "4 Blocks", 1 },
					{ "3 Blocks", 2 },
					{ "2 Blocks", 3 },
					{ "1 Block", 4 }
				},
				(obj) => (obj.PropertyValue & 127),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 128) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Collision Plane", typeof(int), "Extended",
				"Which Collision Plane this Break Block is for.", null, new Dictionary<string, int>
				{
					{ "Plane A", 0 },
					{ "Plane B", 128 }
				},
				(obj) => (obj.PropertyValue & 128),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 127) | (byte)((int)value)));
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
			return sprites[Math.Min(obj.PropertyValue & 127, 4)];
		}
	}
}