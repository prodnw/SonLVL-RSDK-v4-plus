using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class TagaTaga : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			// (btw good and bad versions use the same sprite for this frame)
			sprite = new Sprite(LevelData.GetSpriteSheet("R1/Objects.gif").GetSection(223, 154, 32, 44), -16, -22);
			
			// it's kind of hard to guess how high the Taga Taga will jump, so let's draw a debug vis for it
			BitmapBits bitmap = new BitmapBits(2, 127);
			bitmap.DrawLine(6, 0, 0, 0, 126);
			debug[1] = new Sprite(bitmap.GetSection(0, 0, 1, 77), 0, -77);
			debug[0] = new Sprite(bitmap, 0, -126);
			
			properties[0] = new PropertySpec("Condition", typeof(int), "Extended",
				"What condition this TagaTaga should be in. Affects jump speed and height.", null, new Dictionary<string, int>
				{
					{ "Good", 0 },
					{ "Bad", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Good Condition" : "Bad Condition";
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
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
			return debug[(obj.PropertyValue == 0) ? 0 : 1];
		}
	}
}