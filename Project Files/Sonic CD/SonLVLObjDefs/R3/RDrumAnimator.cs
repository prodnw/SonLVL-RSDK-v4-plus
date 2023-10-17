using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R3
{
	class RDrumAnimator : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			BitmapBits bitmap = new BitmapBits(128, 128);
			bitmap.DrawRectangle(6, 0, 0, 127, 127); // LevelData.ColorWhite
			debug = new Sprite(bitmap);
			
			properties[0] = new PropertySpec("Side", typeof(int), "Extended",
				"Which side of the Drum this object should animate.", null, new Dictionary<string, int>
				{
					{ "Upper Half", 0 },
					{ "Lower Half", 2 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 2 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Upper Half" : "Lower Half";
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
		
		// is this too cluttered? might be..
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			// round to get the chunk's position
			int sx = obj.X & ~0x7f;
			int sy = obj.Y & ~0x7f;
			
			return new Sprite(debug, sx - obj.X, sy - obj.Y);
		}
	}
}