using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SLZ
{
	class Fan : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[3];
		private readonly Sprite[,] sprites = new Sprite[2,4];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("SLZ/Objects.gif");
			sprites[0,0] = new Sprite(sheet.GetSection(6, 223, 27, 32), -11, -16);
			sprites[1,0] = new Sprite(sheet.GetSection(72, 225, 27, 30), -11, -14);
			
			for (int i = 1; i < 4; i++) // set up flipped sprites
			{
				// it would be cool if we could cast to RSDKv3_4.Tiles128x128.Block.Tile.Directions for flip dir but it turns out only BitmapBits has that, not Sprite
				sprites[0,i] = new Sprite(sprites[0,0], (i & 1) == 1, (i & 2) == 2);
				sprites[1,i] = new Sprite(sprites[1,0], (i & 1) == 1, (i & 2) == 2);
			}
			
			properties[0] = new PropertySpec("Wind Direction", typeof(int), "Extended",
				"Where the wind blows.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Always Powered", typeof(int), "Extended",
				"If the Fan should always be active.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 2 }
				},
				(obj) => obj.PropertyValue & 2,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~2) | (byte)((int)value)));
			
			properties[2] = new PropertySpec("Range", typeof(int), "Extended",
				"The range of the Fan's push.", null, new Dictionary<string, int>
				{
					{ "Short", 0 },
					{ "Long", 4 }
				},
				(obj) => obj.PropertyValue & 4,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~4) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
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
			get { return sprites[0,0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0,0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 1, (int)(((V4ObjectEntry)obj).Direction)];
		}
		
		// TODO: add hitbox for the fan, that would be cool
		// its method of hitbox checking is rather odd, though...
	}
}