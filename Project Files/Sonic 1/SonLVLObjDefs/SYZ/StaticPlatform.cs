using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SYZ
{
	class StaticPlatform : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(119, 1, 64, 32), -32, -10);
			
			BitmapBits overlay = new BitmapBits(2, 512);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 0, 511);
			debug = new Sprite(overlay, 0, -511);
			
			properties[0] = new PropertySpec("Triggered By Button", typeof(bool), "Extended",
				"If this Platform should rise vertically if button[-1] is pressed.", null,
				(obj) => (obj.PropertyValue == 1),
				(obj, value) => obj.PropertyValue = (byte)(((bool)value == false) ? 0 : 1));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
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
			return (subtype == 1) ? "Can Move" : "Static";
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
			return (obj.PropertyValue == 1) ? debug : null;
		}
	}
}