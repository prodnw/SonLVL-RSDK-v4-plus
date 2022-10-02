using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.MPZ
{
	class WallBumper : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(93, 113, 16, 16), -8, -8);
			
			// First bit is unused, it seems
			// The official definition for it probably has something, 
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How large the Wall Bumper will be.", null, new Dictionary<string, int>
				{
					{ "4 Bumpers", 0 },
					{ "8 Bumpers", 1 }
				},
				(obj) => (obj.PropertyValue & 0x70) >> 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 0x8f) | (byte)((((int)value) & 7) << 4)));
			
			properties[1] = new PropertySpec("Bounce Direction", typeof(int), "Extended",
				"Which way Sonic will be bounced by this wall.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)) ? 1 : 0,
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
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
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			List<Sprite> sprs = new List<Sprite>();
			
			int count = (((obj.PropertyValue & 0x10) >> 4) + 1) * 8;
			int sy    = (((obj.PropertyValue & 0x10) >> 4) + 1) * (-64);
			
			for (int i = 0; i < count; i++)
			{
				Sprite tmp = new Sprite(img);
				tmp.Offset(0, sy + (i * 16) + 8);
				sprs.Add(tmp);
			}
			
			return new Sprite(sprs.ToArray());
		}
	}
}