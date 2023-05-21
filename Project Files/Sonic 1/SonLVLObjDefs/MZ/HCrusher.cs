using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.MZ
{
	class HCrusher : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[3];
		private Sprite[] sprites = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("MZ/Objects.gif");
			
			// this is kinda iffy
			// if i go `[2][4]` then the compilter gets mad
			// and i don't want to go `[2,4]` because i need to get the entire table which (i think) i can only do if i declare it like this
			// i dunno as long as it works it's fine ig
			Sprite[][] frames = new Sprite[2][];
			frames[0] = new Sprite[4];
			frames[1] = new Sprite[4];
			
			frames[0][0] = new Sprite(sheet.GetSection(295, 388, 21, 16),  16, -8);
			frames[1][0] = new Sprite(sheet.GetSection(295, 388, 21, 16), -38, -8);
			
			frames[0][1] = new Sprite(sheet.GetSection(289, 158, 8, 32), 36, -16);
			frames[0][2] = new Sprite(sheet.GetSection(256, 142, 32, 64), -12, -32);
			frames[0][3] = new Sprite(sheet.GetSection(256, 207, 31, 48), -43, -24);
			
			for (int i = 1; i < 4; i++) frames[1][i] = new Sprite(frames[0][i], true, false);
			
			sprites[0] = new Sprite(frames[0]);
			sprites[1] = new Sprite(frames[1]);
			
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"The distance the crusher drops.", null, new Dictionary<string, int>
				{
					{ "56 px", 0 },
					{ "159 px", 1 },
					{ "80 px", 2 },
					{ "56 px (Use Button)", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~3) | ((int)value)));
			
			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which direction this Crusher should be facing in.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 0x40 }
				},
				(obj) => obj.PropertyValue & 0x40,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x40) | ((int)value)));
			
			properties[2] = new PropertySpec("Triggered", typeof(bool), "Extended",
				"If the crusher should be activated by a button, as opposed to rising automatically.", null,
				(obj) => (obj.PropertyValue & 0x80) == 0x80,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | ((bool)value ? 0x80 : 0x00)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
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
			return subtype + "";
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
			return sprites[(obj.PropertyValue & 0x40) >> 6];
		}

		// TODO: debug visualisation
	}
}