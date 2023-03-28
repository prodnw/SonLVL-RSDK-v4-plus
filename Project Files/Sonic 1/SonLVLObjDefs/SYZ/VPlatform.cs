using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SYZ
{
	class VPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(119, 1, 64, 32), -32, -10);
			
			BitmapBits overlay = new BitmapBits(2, 97);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 0, 96);
			debug = new Sprite(overlay, 0, -38);
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Platform should behave.", null, new Dictionary<string, int>
				{
					{ "Use Stage Oscillation", 0 },
					{ "Use Stage Oscillation (Reversed)", 1 },
					{ "Use Global Oscillation", 2 },
					{ "Use Global Oscillation (Reversed)", 3 },
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)(int)value);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3 }); }
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
				case 0: return "Use Stage Oscillation";
				case 1: return "Use Stage Oscillation (Reversed)";
				case 2: return "Use Global Oscillation";
				case 3: return "Use Global Oscillation (Reversed)";
				
				default: return "Unknown"; // technically "static"
			}
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
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue < 4) ? debug : null;
		}
	}
}