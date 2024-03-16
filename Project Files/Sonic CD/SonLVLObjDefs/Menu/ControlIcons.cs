using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace SCDObjectDefinitions.Menu
{
	class ControlIcons : ObjectDefinition
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
				sprites[0] = new Sprite(sheet.GetSection(129, 1, 64, 64), -32, -32);   // D-Pad (Neutral)
				sprites[1] = new Sprite(sheet.GetSection(129, 68, 64, 64), -32, -32);  // D-Pad (Up)
				sprites[2] = new Sprite(sheet.GetSection(129, 135, 64, 64), -32, -32); // D-Pad (Down)
				sprites[3] = new Sprite(sheet.GetSection(129, 202, 48, 48), -24, -24); // Jump Button
				sprites[4] = new Sprite(sheet.GetSection(196, 1, 48, 48), -24, -24);   // Pause Button
			}
			catch
			{
				// The sheet most likely doesn't exist, let's use the in-game versions of those sprites
				// (we *kind of* cheat here? instead of assembling the touch controls piece by piece, we take the unused full sprite and overlay the desired button ontop of it)
				
				BitmapBits sheet = LevelData.GetSpriteSheet("Global/DPad.gif");
				sprites[0] = new Sprite(sheet.GetSection(0, 0, 64, 64), -32, -32); // D-Pad (Neutral)
				sprites[1] = new Sprite(sprites[0], new Sprite(sheet.GetSection(116, 64, 12, 25), -6, -25)); // D-Pad (Up)
				sprites[2] = new Sprite(sprites[0], new Sprite(sheet.GetSection(116, 102, 12, 26), -6, 0)); // D-Pad (Down)
				sprites[3] = new Sprite(sheet.GetSection(65, 0, 48, 48), -24, -24); // Jump Button
				
				// Pause Button
				// For this one, we kind of cheat and enlarge the normal pause button by 3x (since the help menu version of the sprite is an enlarged version, which doesn't exist anywhere else in the game)
				sprites[4] = new Sprite(new Sprite(sheet.GetSection(72, 111, 16, 16), -8, -8).GetBitmap().Scale(3), -24, -24);
			}
			
			properties[0] = new PropertySpec("Frame", typeof(int), "Extended",
				"Which sprite this object should use.", null, new Dictionary<string, int>
				{
					{ "D-Pad Neutral", 0 },
					{ "D-Pad Up", 1 },
					{ "D-Pad Down", 2 },
					{ "Jump Button", 3 },
					{ "Pause Button", 4 }
				},
				(obj) => (int)obj.PropertyValue,
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