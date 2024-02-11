using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R6
{
	class PoleTrigger : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How tall, in tiles, this object should be.", null,
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x18, 0x19, 0x1a, 0x1b, 0x1c}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0x1a; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return subtype + " Tiles Tall";
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
			// with how colourful all the poles are, it's kind of hard to find a good color combination to use in order to actually make the vis, well, visible..
			int size = ((obj.PropertyValue << 3) + 8) * 2;
			BitmapBits bitmap = new BitmapBits(8, size + 1);
			bitmap.DrawRectangle(6, 0, 0, 7, size); // black
			bitmap.DrawRectangle(1, 1, 1, 5, size-2); // white
			return new Sprite(bitmap, -4, -(size >> 1));
		}
	}
}