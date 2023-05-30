using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.ARZ
{
	class VPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(126, 145, 64, 45), -32, -13);
			
			BitmapBits overlay = new BitmapBits(2, 129);
			overlay.DrawLine(6, 0, 0, 0, 128); // LevelData.ColorWhite
			debug = new Sprite(overlay, 0, -64);
			
			properties[0] = new PropertySpec("Reverse", typeof(int), "Extended",
				"Reverses platform movement.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 1 }
				},
				(obj) => ((obj.PropertyValue == 1) ? 1 : 0),
				(obj, value) => obj.PropertyValue = (byte)(int)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 1) ? "Start Upwards" : "Start Downwards";
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
			int offset = 64;
			if (obj.PropertyValue == 1)
			{
				offset *= -1;
			}
			return new Sprite(sprite, 0, offset);
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}