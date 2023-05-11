using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SBZ
{
	class SwingingSpike : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private readonly Sprite[] sprites = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("SBZ/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(92, 31, 30, 30), -15, -23);
			sprites[1] = new Sprite(sheet.GetSection(65, 106, 16, 16), -8, -8);
			sprites[2] = new Sprite(sheet.GetSection(397, 182, 48, 48), -24, -24);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How many chains the Spike should hang off of.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			properties[1] = new PropertySpec("Inverted", typeof(int), "Extended",
				"If the Spike's movement should be inverse the global cycle.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Direction.HasFlag(RSDKv3_4.Tiles128x128.Block.Tile.Directions.FlipX) ? 1 : 0),
				(obj, value) => ((V4ObjectEntry)obj).Direction = (RSDKv3_4.Tiles128x128.Block.Tile.Directions)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 6; }
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
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i <= (obj.PropertyValue + 1); i++)
			{
				int frame = (i == 0) ? 0 : (i == (obj.PropertyValue + 1)) ? 2 : 1;
				Sprite sprite = new Sprite(sprites[frame]);
				sprite.Offset(0, (i * 16));
				sprs.Add(sprite);
			}
			return new Sprite(sprs.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			var overlay = new BitmapBits(2 * ((obj.PropertyValue * 16) + 24) + 1, (obj.PropertyValue * 16) + 25);
			overlay.DrawCircle(6, ((obj.PropertyValue * 16) + 24), 0, (obj.PropertyValue * 16) + 24); // LevelData.ColorWhite
			return new Sprite(overlay, -((obj.PropertyValue * 16) + 24), -8);
		}
	}
}