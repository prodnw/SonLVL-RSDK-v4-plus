using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.MZ
{
	class GlassPillar : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[2];
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MZ/Objects.gif");
			Sprite shine = new Sprite(sheet.GetSection(159, 114, 31, 32), -16, -16);
			
			sprites[0] = new Sprite(new Sprite(sheet.GetSection(191, 1, 64, 144), -32, -72), shine);
			sprites[1] = new Sprite(new Sprite(sheet.GetSection(126, 1, 64, 112), -32, -56), shine);
			
			// tagging this area with LevelData.ColorWhite
			BitmapBits bitmap = new BitmapBits(65, 145);
			bitmap.DrawRectangle(6, 0, 0, 63, 143); // top box
			bitmap.DrawLine(6, 32, 72, 32, 136); // movement line
			debug[0] = new Sprite(bitmap, -32, -136);
			
			bitmap = new BitmapBits(65, 113);
			bitmap.DrawRectangle(6, 0, 0, 63, 111); // top box
			bitmap.DrawLine(6, 32, 56, 32, 120); // movement line
			debug[1] = new Sprite(bitmap, -32, -120);
			
			properties[0] = new PropertySpec("Frame", typeof(int), "Extended",
				"What sprite this Pillar should display.", null, new Dictionary<string, int>
				{
					{ "Long", 0 },
					{ "Medium", 0x10 }
				},
				(obj) => obj.PropertyValue & 0x10,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x10) | ((int)value)));
			
			properties[1] = new PropertySpec("Movement", typeof(bool), "Extended",
				"What pattern this Pillar's movement should follow.", null, new Dictionary<string, int>
				{
					{ "Static", 0 },
					{ "Vertical", 1 },
					{ "Vertical (Reverse)", 2 }
				},
				(obj) => obj.PropertyValue & 7,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~7) | ((int)value)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return null;
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype & 0x10) >> 4];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue & 0x10) >> 4];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue & 0x10) >> 4];
		}
	}
}