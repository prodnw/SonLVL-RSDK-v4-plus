using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SLZ
{
	class RotatePlatform : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("SLZ/Objects.gif").GetSection(1, 196, 48, 16), -24, -8);
			
			int radius = 80;
			var overlay = new BitmapBits(radius * 2 + 1, radius * 2 + 1);
			overlay.DrawCircle(6, radius, radius, radius); // LevelData.ColorWhite
			debug = new Sprite(overlay, -radius, -radius);
			
			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("Starting position", typeof(int), "Extended",
				"The position (or angle) from where the Platform will start.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 },
					{ "Down", 2 },
					{ "Up", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~3) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"The direction in which the Platform moves.", null, new Dictionary<string, int>
				{
					{ "Counter-clockwise", 0 },
					{ "Clockwise", 4 }
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
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			Sprite spr = new Sprite(sprite);
			int radius = ((obj.PropertyValue & 4) != 0) ? -80 : 80;
			
			if ((obj.PropertyValue & 3) == 0)
			{
				spr.Offset(-radius, 0);
			}
			else if ((obj.PropertyValue & 3) == 1)
			{
				spr.Offset(radius, 0);
			}
			else if ((obj.PropertyValue & 3) == 2)
			{
				spr.Offset(0, radius);
			}
			else if ((obj.PropertyValue & 3) == 3)
			{
				spr.Offset(0, -radius);
			}
			return new Sprite(spr);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}