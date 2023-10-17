using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class CollapsingFloor : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(126, 112, 64, 32), -32, -16);
			
			// let's combine prop val and dir for this..
			properties[0] = new PropertySpec("Collapse From", typeof(int), "Extended",
				"Which side this platform should collapse from.", null, new Dictionary<string, int>
				{
					{ "Bottom Left", 0x00 },
					{ "Bottom Right", 0x10 },
					{ "Left", 0x01 },
					{ "Right", 0x11 }
				},
				(obj) => {
						int result = obj.PropertyValue & 1;
						if (((V4ObjectEntry)obj).Direction == RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX)
							result |= 0x10;
						return result;
					},
				(obj, value) => {
						obj.PropertyValue = (byte)((int)value & 1);
						if ((int)value > 1)
							((V4ObjectEntry)obj).Direction = RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX;
						else
							((V4ObjectEntry)obj).Direction = RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipNone;
					}
				);
			
			properties[1] = new PropertySpec("Solid", typeof(bool), "Extended",
				"If this object should have solid collision, as opposed to platform collision.", null,
				(obj) => obj.PropertyValue >= 2,
				(obj, value) => obj.PropertyValue = (byte)((bool)value ? 2 : 0));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 2 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype < 2) ? "Platform" : "Solid";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprite;
		}
	}
}