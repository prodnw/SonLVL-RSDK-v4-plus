using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class SpikeBall : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(1, 199, 32, 32), -16, -16);
			
			// btw even if every time period has its own movement values, they all multiply to be the same thing (2 * 24 and 4 * 12 and 1 * 48 all end up as 48)
			
			// Horizontal
			BitmapBits bitmap = new BitmapBits(97, 2);
			bitmap.DrawLine(6, 0, 0, 96, 0);
			debug[0] = new Sprite(bitmap, -48, 0);
			
			// Vertical
			bitmap = new BitmapBits(2, 65);
			bitmap.DrawLine(6, 0, 0, 0, 64);
			debug[1] = new Sprite(bitmap, 0, -32);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way this Spike Ball should travel.", null, new Dictionary<string, int>
				{
					{ "Horizontal", 0 },
					{ "Vertical", 1 }
				},
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1} ); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 1) ? "Travel Vertically" : "Travel Horizontally";
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
			return debug[(obj.PropertyValue == 1) ? 1 : 0];
		}
	}
}