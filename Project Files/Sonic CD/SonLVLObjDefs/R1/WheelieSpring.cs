using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class WheelieSpring : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items.gif");
			sprite = new Sprite(new Sprite(sheet.GetSection(84, 1, 32, 16), -16, -16), new Sprite(sheet.GetSection(233, 0, 16, 16), -8, 0));
			
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far the spring should travel, in pixels.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 32, 48, 64, 80, 96 }); } // assorted starting values :D
		}
		
		public override byte DefaultSubtype
		{
			get { return 48; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return "Travel " + subtype + " px";
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
			BitmapBits bitmap = new BitmapBits((obj.PropertyValue << 1) + 1, 2);
			bitmap.DrawLine(6, 0, 0, (obj.PropertyValue << 1), 0); // LevelData.ColorWhite
			return new Sprite(bitmap, -obj.PropertyValue, 0);
		}
	}
}