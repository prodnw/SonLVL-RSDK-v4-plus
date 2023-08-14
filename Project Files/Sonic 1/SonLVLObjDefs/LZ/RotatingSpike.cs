using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.LZ
{
	class RotatingSpike : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[3];
		private PropertySpec[] properties = new PropertySpec[3];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("LZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(84, 173, 16, 16), -8, -8);
			sprites[1] = new Sprite(sheet.GetSection(101, 173, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(84, 190, 32, 32), -16, -16);
			
			properties[0] = new PropertySpec("Length", typeof(int), "Extended",
				"How many chains this Spike should hang off of.", null,
				(obj) => obj.PropertyValue & 0x0f,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x0f) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Spin Speed", typeof(int), "Extended",
				"How fast the Spikes will spin.", null,
				(obj) => obj.PropertyValue >> 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0xf0) | (byte)((int)value) << 4));
			
			properties[2] = new PropertySpec("Starting Angle", typeof(int), "Extended",
				"Which angle the Spike Ball should start at.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Downwards", 1 },
					{ "Left", 2 },
					{ "Upwards", 3 }
				},
				(obj) => (int)(((V4ObjectEntry)obj).Direction),
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x44; }
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
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			double angle = (double)(((V4ObjectEntry)obj).Direction) * (Math.PI / 2);
			int length = (obj.PropertyValue & 15) - 1;
			List<Sprite> sprs = new List<Sprite>();
			sprs.Add(new Sprite(sprites[0]));
			
			for (int i = 0; i <= length; i++)
			{
				sprs.Add(new Sprite(sprites[(i == length) ? 2 : 1], (int)(Math.Cos(angle) * ((i+1) * 16)), (int)(Math.Sin(angle) * ((i+1) * 16))));
			}
			
			return new Sprite(sprs.ToArray());
		}
	}
}