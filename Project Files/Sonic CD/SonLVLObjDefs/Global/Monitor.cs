using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace SCDObjectDefinitions.Global
{
	class Monitor : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private ReadOnlyCollection<byte> subtypes;
		private Sprite[] sprites = new Sprite[13];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items.gif");
			
			sprites[0] = new Sprite(sheet.GetSection(51, 67, 32, 32), -16, -16); // empty
			sprites[1] = new Sprite(sheet.GetSection(18, 1, 32, 32), -16, -16); // rings
			sprites[2] = new Sprite(sheet.GetSection(18, 34, 32, 32), -16, -16); // shield
			sprites[3] = new Sprite(sheet.GetSection(18, 67, 32, 32), -16, -16); // invincibility
			sprites[4] = new Sprite(sheet.GetSection(18, 100, 32, 32), -16, -16); // speed shoes
			sprites[5] = new Sprite(sheet.GetSection(18, 133, 32, 32), -16, -16); // sonic
			sprites[6] = new Sprite(sheet.GetSection(18, 166, 32, 32), -16, -16); // clock
			sprites[7] = new Sprite(sheet.GetSection(51, 100, 32, 32), -16, -16); // tails
			sprites[8] = new Sprite(sheet.GetSection(51, 133, 32, 32), -16, -16); // super
			// (9 and 10 are origins frames, let's hold off on those for now) 
			sprites[11] = new Sprite(sheet.GetSection(51, 1, 32, 32), -16, -16); // static 1
			sprites[12] = new Sprite(sheet.GetSection(51, 34, 32, 32), -16, -16); // static 2
			
			bool plus = false;
			
			try
			{
				// the pre-plus sheet isn't large enough to hold these, let's use that to see which datafile type we're on
				// (there are probably mods which expand the sheet too, but i can't really think of any better way to do this, sorry..)
				
				sprites[9] = new Sprite(sheet.GetSection(34, 256, 32, 32), -16, -16); // knux (origins)
				sprites[10] = new Sprite(sheet.GetSection(1, 256, 32, 32), -16, -16); // amy (origins)
				
				plus = true;
			}
			catch
			{
				// the sheet isn't large enough for the new frames, let's assume this is a pre-plus datafile
				
				sprites[9] = sprites[11];
				sprites[10] = sprites[12];
				
				plus = false;
			}
			
			// (let's replace the blank icon with a static one, looks better that way i'd say)
			sprites[0] = sprites[11];
			
			Dictionary<string, int> names;
			
			if (plus)
			{
				names = new Dictionary<string, int> {
					{ "Static", 0 },
					{ "Rings", 1 },
					{ "Shield", 2 },
					{ "Invincibility", 3 },
					{ "Speed Shoes", 4 },
					{ "Sonic 1UP", 5 },
					{ "Tails 1UP", 7 },
					{ "Knuckles 1UP", 9 },
					{ "Amy 1UP", 10 },
					{ "Clock", 6 },
					{ "Super", 8 }
				};
				
				subtypes = new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 7, 9, 10, 6, 8, 11, 12, 13, 14, 15, 16, 18, 20, 21, 17, 19});
			}
			else
			{
				names = new Dictionary<string, int> {
					{ "Static", 0 },
					{ "Rings", 1 },
					{ "Shield", 2 },
					{ "Invincibility", 3 },
					{ "Speed Shoes", 4 },
					{ "Sonic 1UP", 5 },
					{ "Tails 1UP", 7 },
					{ "Clock", 6 },
					{ "Super", 8 }
				};
				
				subtypes = new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 7, 6, 8, 9, 10, 11, 12, 13, 14, 16, 15, 17});
			}
			
			// this part is kind of scuffed.. it sure would've been easier it plane was just the top 4 bits instead of whatever this is
			
			int n = (subtypes.Count / 2);
			
			properties[0] = new PropertySpec("Contents", typeof(int), "Extended",
				"The Contents of this Monitor.", null, names,
				(obj) => (obj.PropertyValue % n),
				(obj, value) => obj.PropertyValue = (byte)(obj.PropertyValue - (obj.PropertyValue % n) + (int)value));
			
			properties[1] = new PropertySpec("Plane", typeof(int), "Extended",
				"Which Plane this Monitor should be on.", null, new Dictionary<string, int>
				{
					{ "High Plane", 0 },
					{ "Low Plane", n },
				},
				(obj) => (obj.PropertyValue > n) ? n : 0,
				(obj, value) => obj.PropertyValue = (byte)(obj.PropertyValue - ((obj.PropertyValue > n) ? n : 0) + (int)value));
			
			// maybe there could be like a `Mode` thing here or something? not a real object property, but a way for the user to switch between Standalone and Plus monitor configs
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return subtypes; }
		}
		
		public override byte DefaultSubtype
		{
			get { return 1; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			string name = properties[0].Enumeration.GetKey(subtype % (subtypes.Count / 2));
			if (subtype >= (subtypes.Count / 2)) name += " (Low Plane)";
			return name;
		}

		public override Sprite Image
		{
			get { return sprites[1]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype % (subtypes.Count / 2)];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue % (subtypes.Count / 2)];
		}
	}
}