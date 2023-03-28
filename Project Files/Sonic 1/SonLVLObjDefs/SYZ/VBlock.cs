using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SYZ
{
	class VBlock : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(119, 34, 64, 64), -32, -32);
			
			BitmapBits overlay = new BitmapBits(2, 65);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 0, 64);
			debug[0] = new Sprite(overlay, 0, -63); // a 1px difference in ranges between the two... is this really important? prolly not, but hey may as well anyways
			debug[1] = new Sprite(overlay, 0, -64);
			
			properties[0] = new PropertySpec("Starting Direction", typeof(int), "Extended",
				"How this Block should behave.", null, new Dictionary<string, int>
				{
					{ "Upwards", 0 },
					{ "Downwards", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)(int)value);
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
			return (subtype == 1) ? "Initially Upwards" : "Initially Downwards";
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
			return debug[obj.PropertyValue == 1 ? 1 : 0];
		}
	}
}