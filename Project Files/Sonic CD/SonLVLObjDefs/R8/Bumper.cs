using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R8
{
	class Bumper : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[5];
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("R8/Objects3.gif").GetSection(100, 100, 32, 32), -16, -16);
			
			sprites[1] = new Sprite(sprites[0], -48, 0);
			sprites[2] = new Sprite(sprites[0],  48, 0);
			
			sprites[3] = new Sprite(sprites[0], 0, -48);
			sprites[4] = new Sprite(sprites[0], 0,  48);
			
			BitmapBits bitmap = new BitmapBits(97, 2);
			bitmap.DrawLine(6, 0, 0, 96, 0);
			debug[0] = new Sprite(bitmap, -48, 0);
			
			bitmap = new BitmapBits(2, 97);
			bitmap.DrawLine(6, 0, 0, 0, 96);
			debug[1] = new Sprite(bitmap, 0, -48);
			
			// this is kinda weird, i dunno how else to label it without being too verbose though
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which direction this Bumper should start from, given that it moves at all.", null, new Dictionary<string, int>
				{
					{ "Static", 0 },
					{ "Left", 1 },
					{ "Right", 2 },
					{ "Bottom", 3 },
					{ "Top", 4 }
				},
				(obj) => (obj.PropertyValue < 5) ? (int)obj.PropertyValue : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				default:
				case 0: return "Static";
				case 1: return "Start From Left";
				case 2: return "Start From Right";
				case 3: return "Start From Bottom";
				case 4: return "Start From Top";
			}
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
			return sprites[(obj.PropertyValue < 5) ? (int)obj.PropertyValue : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if ((obj.PropertyValue == 0) || (obj.PropertyValue > 4))
				return null;
			
			return debug[((obj.PropertyValue == 1) || (obj.PropertyValue == 2)) ? 0 : 1];
		}
	}
}