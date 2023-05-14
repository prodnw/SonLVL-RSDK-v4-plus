using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace S1ObjectDefinitions.SYZ
{
	class HVSpikeBall : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[5];
		private Sprite[] debug = new Sprite[4];
		private PropertySpec[] properties = new PropertySpec[2];
		
		public override void Init(ObjectData data)
		{
			sprites[4] = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(61, 178, 48, 48), -24, -24);
			sprites[0] = new Sprite(sprites[4], -48, 0);
			sprites[1] = new Sprite(sprites[4], -48, 0);
			sprites[2] = new Sprite(sprites[4], 0, -48);
			sprites[3] = new Sprite(sprites[4], 0, -80);
			
			BitmapBits bitmap = new BitmapBits(97, 2);
			bitmap.DrawLine(6, 0, 0, 96, 0); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -96, 0);
			
			debug[1] = new Sprite(debug[0]);
			
			bitmap = new BitmapBits(2, 97);
			bitmap.DrawLine(6, 0, 0, 0, 96); // LevelData.ColorWhite
			debug[2] = new Sprite(bitmap, 0, -96);
			
			debug[3] = new Sprite(debug[2], 0, -32);
			
			properties[0] = new PropertySpec("Movement", typeof(int), "Extended",
				"Which way this Spike Ball should move.", null, new Dictionary<string, int>
				{
					{ "Horizontal", 0 },
					{ "Vertical", 2 }
				},
				(obj) => obj.PropertyValue & 2,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~2) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Reverse", typeof(bool), "Extended",
				"If this Spike Ball's movement should be inverse of the normal cycle.", null,
				(obj) => (obj.PropertyValue & 1) == 1,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~1) | (((bool)value) ? 1 : 0)));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3 }); }
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
				case 0: return "Right, Then Left";
				case 1: return "Left, Then Right";
				case 2: return "Down, Then Up";
				case 3: return "Up, Then Down";
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[4]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[4];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[Math.Min(obj.PropertyValue, (byte)5)];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue < 5) ? debug[obj.PropertyValue] : null;
		}
	}
}