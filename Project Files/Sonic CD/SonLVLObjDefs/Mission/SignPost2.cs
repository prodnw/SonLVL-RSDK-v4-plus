using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Mission
{
	class SignPost2 : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items2.gif");
			sprite = new Sprite(new Sprite(sheet.GetSection(0, 150, 24, 24), -12, -12), new Sprite(sheet.GetSection(34, 99, 48, 32), -24, -44));
			
			// this obj kind of sucks because of how they decided to do the screen bounds stuff (it would've been so much easier if they just used the lower 7 bits for it instead of.. whatever they did)
			// because of that, this def itself is kind of sub-par too, but hopefully it's still fine enough..
			
			properties[0] = new PropertySpec("Lock Range", typeof(int), "Extended",
				"How far to the left, in intervals of 16 pixels, that the screen lock should start to take effect. 0 is default Signpost bounds.", null,
				(obj) => ((obj.PropertyValue < 0x80) ? obj.PropertyValue : (256 - obj.PropertyValue)) << 4,
				(obj, value) => {
						int val = (((int)value >> 4) & 0x7f);
						if (obj.PropertyValue < 0x80)
							obj.PropertyValue = (byte)val;
						else
							obj.PropertyValue = (byte)(0x100 - Math.Max(val, 1)); // yeah you can't use default bounds on exit right types..
					}
				);
			
			properties[1] = new PropertySpec("Exit Right", typeof(bool), "Extended",
				"If the Signpost should make the player move right after beating the level.", null,
				(obj) => (obj.PropertyValue > 0x7f),
				(obj, value) => {
						// kinda hacky, but honestly it's the best thing we can do without being too weird about it
						int bounds = (int)properties[0].GetValue(obj);
						obj.PropertyValue = (byte)((bool)value ? 0x80 : 0x00);
						properties[0].SetValue(obj, bounds);
					}
				);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0x00, 0x01, 0x0a, 0xff, 0xf6}); } // using values that (for the most part) the base game uses already
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			string name = (subtype < 0x80) ? "End Still" : "End Stage Right";
			name += " (" + ((subtype == 0) ? "Default Bounds" : ((((subtype < 0x80) ? subtype : (256 - subtype)) << 4) + " Pixel Bounds")) + ")";
			return name;
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
	}
}