using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace SCDObjectDefinitions.Menu
{
	class HelpItems : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[9];
		
		public override void Init(ObjectData data)
		{
			// they gutted this sheet in origins for some reason so we gotta do this
			// if the actual sheet doesn't exist, let's use the in-game versions of these sprites
			// (is there really a point to doing this? it's not like anyone cares about this scene anyways, it's just some out of the way menu..)
			try
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("Menu/MetalSonic.gif");
				sprites[0] = new Sprite(sheet.GetSection(204, 184, 16, 16), -8, -8);   // Ring
				sprites[1] = new Sprite(sheet.GetSection(223, 183, 32, 30), -16, -15); // Ring Monitor
				sprites[2] = new Sprite(sheet.GetSection(223, 216, 32, 30), -16, -15); // Shield Monitor
				sprites[3] = new Sprite(sheet.GetSection(223, 249, 32, 30), -16, -15); // Invincibility Monitor
				sprites[4] = new Sprite(sheet.GetSection(223, 282, 32, 30), -16, -15); // Speed Shoes Monitor
				sprites[5] = new Sprite(sheet.GetSection(223, 315, 32, 30), -16, -15); // Sonic 1UP Monitor
				sprites[6] = new Sprite(sheet.GetSection(204, 201, 16, 64), -8, -32);  // Lamp Post
				sprites[7] = new Sprite(sheet.GetSection(196, 52, 32, 64), -16, -32);  // Past Post
				sprites[8] = new Sprite(sheet.GetSection(196, 117, 32, 64), -16, -32); // Future Post
			}
			catch
			{
				// The sheet most likely doesn't exist, let's use the in-game versions of those sprites
				// (addition/subtraction is used to mark difference between original frame and frame used here)
				
				BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 1, 16, 16), -8, -8); // Ring
				sprites[1] = new Sprite(sheet.GetSection(18, 1 + 1, 32, 32 - 2), -16, -16 + 1); // Ring Monitor
				sprites[2] = new Sprite(sheet.GetSection(18, 34 + 1, 32, 32 - 2), -16, -16 + 1); // Shield Monitor
				sprites[3] = new Sprite(sheet.GetSection(18, 67 + 1, 32, 32 - 2), -16, -16 + 1); // Invincibility Monitor
				sprites[4] = new Sprite(sheet.GetSection(18, 100 + 1, 32, 32 - 2), -16, -16 + 1); // Speed Shoes Monitor
				sprites[5] = new Sprite(sheet.GetSection(18, 133 + 1, 32, 32 - 2), -16, -16 + 1); // Sonic 1UP Monitor
				sprites[6] = new Sprite(sheet.GetSection(1, 137, 16, 64), -8, -32);  // Lamp Post
				sprites[7] = new Sprite(new Sprite(sheet.GetSection(35, 204, 16, 48), -8, -24), new Sprite(sheet.GetSection(118, 239, 32, 16), -16, -40)); // Past Post
				sprites[8] = new Sprite(new Sprite(sheet.GetSection(35, 204, 16, 48), -8, -24), new Sprite(sheet.GetSection(52, 239, 32, 16), -16, -40)); // Future Post
			}
			
			properties[0] = new PropertySpec("Frame", typeof(int), "Extended",
				"Which sprite this object should use.", null, new Dictionary<string, int>
				{
					{ "Ring", 0 },
					{ "Ring Monitor", 1 },
					{ "Shield Monitor", 2 },
					{ "Invincibility Monitor", 3 },
					{ "Speed Shoes Monitor", 4 },
					{ "Sonic Monitor", 5 },
					{ "Lamp Post", 6 },
					{ "Past Post", 7 },
					{ "Future Post", 8 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return properties[0].Enumeration.GetKey(subtype);
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue];
		}
	}
}