using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class SpikeBallLauncher : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[4];
		private Sprite[] debug = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R7/Objects.gif");
			Sprite spike = new Sprite(sheet.GetSection(1, 199, 32, 32), -16, -16);
			sprites[0] =  new Sprite(new Sprite(spike, 0, -16), new Sprite(sheet.GetSection(133, 1, 32, 16), -16, -16));
			sprites[1] =  new Sprite(new Sprite(spike, 0,  16), new Sprite(sheet.GetSection(166, 1, 32, 16), -16, 0));
			sprites[2] =  new Sprite(new Sprite(spike, -16, 0), new Sprite(sheet.GetSection(1, 166, 16, 32), -16, -16));
			sprites[3] =  new Sprite(new Sprite(spike,  16, 0), new Sprite(sheet.GetSection(17, 166, 16, 32), 0, -16));
			
			int[] points = {
				85, 0,
				85, 170,
				0, 85,
				170, 85
			};
			
			for (int i = 0; i < points.Length; i += 2)
			{
				BitmapBits bitmap = new BitmapBits(170, 170);
				bitmap.DrawLine(6, 85, 85, points[i], points[i+1]);
				debug[i / 2] = new Sprite(bitmap, -85, -85);
			}
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way this launcher should fire its spike.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Down", 1 },
					{ "Left", 2 },
					{ "Right", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3} ); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Facing Upwards";
				case 1: return "Facing Downwards";
				case 2: return "Facing Left";
				case 3: return "Facing Right";
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 3];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 3];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue & 3];
		}
	}
}