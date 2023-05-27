using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SYZ
{
	class VPlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(119, 1, 64, 32), -32, -10);
			
			// tagging this area with LevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(65, 129);
			bitmap.DrawRectangle(6, 0, 0, 63, 32); // top box
			bitmap.DrawRectangle(6, 0, 96, 63, 32); // bottom box
			bitmap.DrawLine(6, 32, 10, 32, 106); // movement line
			debug = new Sprite(bitmap, -32, -58);
			
			properties[0] = new PropertySpec("Cycle", typeof(int), "Extended",
				"Which Oscillation this Platform should follow.", null, new Dictionary<string, int>
				{
					{ "Use Stage Oscillation", 0 },
					{ "Use Global Oscillation", 2 }
				},
				(obj) => obj.PropertyValue & 2,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~2) | ((int)value)));
			
			properties[1] = new PropertySpec("Reverse", typeof(bool), "Extended",
				"If this Platform's movement should be inverse of the normal cycle.", null,
				(obj) => (obj.PropertyValue & 1) == 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (((bool)value) ? 1 : 0)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3 }); }
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