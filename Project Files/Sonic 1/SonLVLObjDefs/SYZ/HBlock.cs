using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SYZ
{
	class HBlock : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(119, 34, 32, 32), -16, -16);
			
			// tagging this area withLevelData.ColorWhite
			
			BitmapBits bitmap = new BitmapBits(161, 33);
			bitmap.DrawRectangle(6, 0, 0, 31, 31); // left box
			bitmap.DrawRectangle(6, 128, 0, 31, 31); // left box
			bitmap.DrawLine(6, 16, 16, 144, 16);
			debug[0] = new Sprite(bitmap, -80, -16);
			
			bitmap = new BitmapBits(97, 33);
			bitmap.DrawRectangle(6, 0, 0, 31, 31); // left box
			bitmap.DrawRectangle(6, 64, 0, 31, 31); // left box
			bitmap.DrawLine(6, 16, 16, 80, 16);
			debug[1] = new Sprite(bitmap, -48, -16);
			
			properties[0] = new PropertySpec("Range", typeof(int), "Extended",
				"The distance that this Block should travel.", null, new Dictionary<string, int>
				{
					{ "Far", 0 },
					{ "Close", 1 },
					{ "Close (Reverse)", 2 },
					{ "Far (Reverse)", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
			// names sound weird but i can't think of much better
			switch (subtype)
			{
				case 0: return "Far Movement";
				case 2: return "Close Movement";
				case 1: return "Far Movement, Reverse";
				case 3: return "Close Movement, Reverse";
				default: return "Unknown";
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
			switch (obj.PropertyValue)
			{
				case 0:
				case 3: return debug[0];
				case 1:
				case 2: return debug[1];
				default: return null;
			}
		}
	}
}