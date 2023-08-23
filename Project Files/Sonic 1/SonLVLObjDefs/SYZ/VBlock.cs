using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SYZ
{
	class VBlock : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[3];
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			// prop val 0 is 1px lower than prop val 1, it's a tiny diff but we may as well reflect it in editor
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(119, 34, 64, 64), -32, -32+1);
			sprites[1] = new Sprite(sprites[0], 0, -1);
			
			// tagging this area with LevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(65, 128);
			bitmap.DrawRectangle(6, 0, 0, 63, 63); // top box
			bitmap.DrawRectangle(6, 0, 64, 63, 63); // bottom box
			bitmap.DrawLine(6, 32, 32, 32, 96); // movement line
			debug[0] = new Sprite(bitmap, -32, -95); // a 1px difference in ranges between the two... is this really important? prolly not, but hey may as well anyways
			debug[1] = new Sprite(bitmap, -32, -96);
			
			properties[0] = new PropertySpec("Reverse", typeof(bool), "Extended",
				"If this Platform's movement should be inverse of the normal cycle.", null,
				(obj) => (obj.PropertyValue == 1),
				(obj, value) => obj.PropertyValue = (byte)(((bool)value) ? 1 : 0));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 1) ? "Normal Movement" : "Inverse Movement";
		}

		public override Sprite Image
		{
			get { return sprites[1]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 1) ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue == 1) ? 1 : 0];
		}
	}
}