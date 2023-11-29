using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R3
{
	class FlipoutBlock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R3/Objects.gif");
			sprites[0] = new Sprite(sheet.GetSection(34, 1, 32, 32), -16, -16);
			sprites[1] = new Sprite(sheet.GetSection(67, 1, 32, 32), -16, -16);
			
			// tagging this area withLevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(33, 49);
			bitmap.DrawRectangle(6, 0, 0, 31, 31);
			bitmap.DrawLine(6, 16, 16, 16, 48);
			debug[0] = new Sprite(bitmap, -16, -16 - 32);
			debug[1] = new Sprite(debug[0], false, true);
			
			properties[0] = new PropertySpec("Carry Object", typeof(bool), "Extended",
				"If this block should carry object[+1] with it.", null,
				(obj) => (obj.PropertyValue & 1) == 0,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | ((bool)value ? 1 : 0)));
			
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this block will move.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Downwards", 2 }
				},
				(obj) => (obj.PropertyValue > 1) ? 2 : 0,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~2) | (int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string name = (subtype > 1) ? "Extend Upwards" : "Extend Downwards";
			if ((subtype & 1) == 0)
				name += " (Carrying Object)";
			return name;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype > 1 ? 1 : 0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue > 1 ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue > 1 ? 1 : 0];
		}
	}
}