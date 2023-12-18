using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R3
{
	class Bumper : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[7];
		private Sprite[] debug = new Sprite[3];
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("R3/Objects.gif").GetSection(67, 167, 32, 32), -16, -16);
			
			// 48 (*2 = 96)  - global
			// 56 (*2 = 112) - local
			
			sprites[1] = new Sprite(sprites[0], -48, 0);
			sprites[2] = new Sprite(sprites[0],  48, 0);
			
			sprites[3] = new Sprite(sprites[0], 0,  56);
			sprites[4] = new Sprite(sprites[0], 0, -56);
			
			sprites[5] = new Sprite(sprites[0], 0,  48);
			sprites[6] = new Sprite(sprites[0], 0, -48);
			
			BitmapBits bitmap = new BitmapBits(97, 2);
			bitmap.DrawLine(6, 0, 0, 96, 0);
			debug[0] = new Sprite(bitmap, -48, 0);
			
			bitmap = new BitmapBits(2, 113);
			bitmap.DrawLine(6, 0, 0, 0, 112);
			debug[1] = new Sprite(bitmap, 0, -56);
			
			bitmap = new BitmapBits(2, 97);
			bitmap.DrawLine(6, 0, 0, 0, 96);
			debug[2] = new Sprite(bitmap, 0, -48);
			
			// this is kinda weird, i dunno how else to label it without being too verbose though
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which direction this Bumper should start from, given that it moves at all.", null, new Dictionary<string, int>
				{
					{ "Static", 0 },
					{ "Left", 1 },
					{ "Right", 2 },
					{ "Bottom", 5 },
					{ "Top", 6 },
					{ "Bottom (Farther)", 3 }, // maybe it's worth noting the bumper's movement sync too, but dist is more important imo
					{ "Top (Farther)", 4 }
				},
				(obj) => (obj.PropertyValue < 7) ? (int)obj.PropertyValue : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 5, 6, 3, 4}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			if ((subtype == 0) || (subtype > 6))
				return "Static";
			
			return "Start From " + properties[0].Enumeration.GetKey(subtype);
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue < 7) ? (int)obj.PropertyValue : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if ((obj.PropertyValue == 0) || (obj.PropertyValue > 6))
				return null;
			
			return debug[(obj.PropertyValue - 1) >> 1];
		}
	}
}