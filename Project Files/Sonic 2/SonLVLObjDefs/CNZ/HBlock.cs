using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class HBlock : ObjectDefinition
	{
		private Sprite img;
		private Sprite debug;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(82, 34, 64, 64), -32, -32);
			
			BitmapBits bitmap = new BitmapBits(193, 2);
			bitmap.DrawLine(LevelData.ColorWhite, 0, 0, 192, 0);
			debug = new Sprite(bitmap, -96, 0);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Starting Direction", typeof(int), "Extended",
				"Which direction the Horizontal Block will initially travel in. Overall range is the same between both directions.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
			switch (subtype)
			{
				case 0:
					return "Initially Right";
				case 1:
				default:
					return "Initially Left";
			}
		}

		public override Sprite Image
		{
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}