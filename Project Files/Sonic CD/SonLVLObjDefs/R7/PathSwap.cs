using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class PathSwap : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("R7/Objects.gif").GetSection(109, 18, 48, 40), -24, -15);
			
			// outline of the chunks this obj will change
			BitmapBits bitmap = new BitmapBits(256, 256);
			bitmap.DrawRectangle(6, 0, 0, 255, 255); // LevelData.ColorWhite
			debug = new Sprite(bitmap);
			
			properties[0] = new PropertySpec("Trigger", typeof(int), "Extended",
				"What type of Path Swap this object is.", null, new Dictionary<string, int>
				{
					{ "Normal", 0 },
					{ "Decoration", 1 },
					{ "Invisible", 2 }
				},
				(obj) => (obj.PropertyValue > 1) ? 2 : obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			string[] names = {"Normal", "Decoration", "Invisible"};
			return names[(subtype > 1) ? 2 : subtype];
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
		
		/*
		// imo this makes things too cluttered so let's just hide it for now, then..
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (obj.PropertyValue == 1)
				return null;
			
			// offset the sprite to the nearest chunk tl
			int sx = obj.X & ~0x7f;
			int sy = obj.Y & ~0x7f;
			
			return new Sprite(debug, sx - obj.X, sy - obj.Y);
		}
		*/
	}
}