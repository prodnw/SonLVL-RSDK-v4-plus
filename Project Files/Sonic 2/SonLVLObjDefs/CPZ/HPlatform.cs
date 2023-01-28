using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class HPlatform : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[2];
		private Sprite debug;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '2')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("CPZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(136, 155, 64, 27), -32, -16);
				sprites[1] = new Sprite(sheet.GetSection(136, 183, 48, 26), -24, -16);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(464, 971, 64, 27), -32, -16);
				sprites[1] = new Sprite(sheet.GetSection(529, 971, 48, 26), -24, -16);
			}
			
			BitmapBits overlay = new BitmapBits(192, 1);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 192, 1);
			debug = new Sprite(overlay, -96, 0);
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"The size of the platform.", null, new Dictionary<string, int>
				{
					{ "Large", 0 },
					{ "Small", 1 }
				},
				(obj) => (obj.PropertyValue > 0) : 1 : 0, // in-game behaviour: 0 is large, all other values are small
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			// maybe "invert movement" would be simpler, but this one works well enough hopefully
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Platform will go.", null, new Dictionary<string, int>
				{
					{ "Downwards", 0 },
					{ "Upwards", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)) ? 1 : 0,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
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
			switch (subtype)
			{
				case 0:
					return "Large";
				case 1:
				default:
					return "Small";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[Math.Min(subtype, (byte)1)];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			Sprite sprite = new Sprite(SubtypeImage(obj.PropertyValue));
			// Flip XY doesn't flip the platform..
			if (((V4ObjectEntry)obj).Direction == (RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX))
			{
				sprite.Offset(-96, 0);
			}
			return sprite;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}