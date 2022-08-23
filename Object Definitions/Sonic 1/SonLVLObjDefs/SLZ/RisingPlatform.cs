using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SLZ
{
	class RisingPlatform : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("SLZ/Objects.gif").GetSection(84, 188, 80, 32), -40, -8);

			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Movement", typeof(int), "Extended",
                "The pattern the Rising Platform will follow for moving.", null, new Dictionary<string, int>
				{
					{ "Up - Slow", 0 },
					{ "Up - Medium", 1 },
					{ "Up - Fast", 2 },
					{ "Down - Slow", 3 },
					{ "Down - Medium", 4 },
					{ "Down - Fast", 5 },
					{ "Up - Slow, Faster", 6 },
					{ "Up - Medium, Faster", 7 },
					{ "Up - Fast, Faster", 8 },
					{ "Down - Slow, Faster", 9 },
					{ "Down - Medium, Faster", 10 },
					{ "Down - Fast, Faster", 11 },
					{ "Up-Right", 12 },
					{ "Down-Left", 13 }
				},
                (obj) => ((obj.PropertyValue & 128) != 0) ? (obj.PropertyValue & 15) : 0,
                (obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 240) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Interval", typeof(int), "Extended",
                "The timings of the Platform. Based on 6 (ie 6, 12, 18, 24...).", null,
                (obj) => obj.PropertyValue & 127,
                (obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 128) | (byte)(System.Math.Max((int)value, 1))));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
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
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}
	}
}