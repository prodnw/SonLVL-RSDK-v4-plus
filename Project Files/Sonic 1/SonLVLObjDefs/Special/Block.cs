using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

// This file is for the flashing, coloured blocks in the Special Stage
// See the Block class for more info about how this script works

namespace S1ObjectDefinitions.Special
{
	class BlueBlock : Block
	{
		public override Sprite[] GetFrames(BitmapBits sheet)
		{
			return new Sprite[] {
				// Blue Block
				new Sprite(
					new Sprite(sheet.GetSection(126, 76, 24, 24), -12, -12),
					new Sprite(sheet.GetSection(151, 76, 20, 16), -10, -8),
					new Sprite(sheet.GetSection(1, 1, 24, 24), -12, -12)
				),
				
				// Yellow Block
				new Sprite(
					new Sprite(sheet.GetSection(126, 176, 24, 24), -12, -12),
					new Sprite(sheet.GetSection(151, 176, 20, 16), -10, -8),
					new Sprite(sheet.GetSection(1, 101, 24, 24), -12, -12)
				)
			};
		}
	}
	
	class YellowBlock : Block
	{
		public override Sprite[] GetFrames(BitmapBits sheet)
		{
			return new Sprite[] {
				// Yellow Block
				new Sprite(
					new Sprite(sheet.GetSection(126, 176, 24, 24), -12, -12),
					new Sprite(sheet.GetSection(151, 176, 20, 16), -10, -8),
					new Sprite(sheet.GetSection(1, 101, 24, 24), -12, -12)
				),
				
				// Blue Block
				new Sprite(
					new Sprite(sheet.GetSection(126, 76, 24, 24), -12, -12),
					new Sprite(sheet.GetSection(151, 76, 20, 16), -10, -8),
					new Sprite(sheet.GetSection(1, 1, 24, 24), -12, -12)
				)
			};
		}
	}
	
	class PinkBlock : Block
	{
		public override Sprite[] GetFrames(BitmapBits sheet)
		{
			return new Sprite[] {
				// Pink Block
				new Sprite(
					new Sprite(sheet.GetSection(126, 276, 24, 24), -12, -12),
					new Sprite(sheet.GetSection(151, 276, 20, 16), -10, -8),
					new Sprite(sheet.GetSection(1, 201, 24, 24), -12, -12)
				),
				
				// Yellow Block
				new Sprite(
					new Sprite(sheet.GetSection(126, 176, 24, 24), -12, -12),
					new Sprite(sheet.GetSection(151, 176, 20, 16), -10, -8),
					new Sprite(sheet.GetSection(1, 101, 24, 24), -12, -12)
				)
			};
		}
	}
	
	class GreenBlock : Block
	{
		public override Sprite[] GetFrames(BitmapBits sheet)
		{
			return new Sprite[] {
				// Green Block
				new Sprite(
					new Sprite(sheet.GetSection(126, 376, 24, 24), -12, -12),
					new Sprite(sheet.GetSection(151, 376, 20, 16), -10, -8),
					new Sprite(sheet.GetSection(1, 301, 24, 24), -12, -12)
				),
				
				// Pink Block
				new Sprite(
					new Sprite(sheet.GetSection(126, 276, 24, 24), -12, -12),
					new Sprite(sheet.GetSection(151, 276, 20, 16), -10, -8),
					new Sprite(sheet.GetSection(1, 201, 24, 24), -12, -12)
				)
			};
		}
	}
	
	abstract class Block : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[9];
		
		// Okay, so
		// The way blocks in the S1SS are, they flash between their main colour and a secondary colour
		// To show that in the editor, let's have a sliver of the flashing colour overlayed on top of the block, with the sliver's location being related to the block's interval value
		
		// (note added in post - okay so ngl after doing all this i realised that it doesn't even look that well, so.. let's just not do that--)
		// (all the code for it's still in here, just dummied out by SubtypeImage/GetSprite only return sprites[0]--)
		
		// Should return an array of two Sprites, the first is the primary sprite and the second is the secondary one
		public abstract Sprite[] GetFrames(BitmapBits sheet);
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Special/Objects.gif");
			
			Sprite[] temp = GetFrames(sheet);
			
			sprites[0] = temp[0];
			BitmapBits block = temp[1].GetBitmap();
			
			// See above, we're taking a vertical slice from `block` and placing it onto `sprites[0]`
			for (int i = 0; i < 8; i++)
			{
				sprites[i+1] = new Sprite(
					sprites[0],
					new Sprite(block.GetSection(i * 3, 0, 3, 24), -12 + (i * 3), -12)
				);
			}
			
			properties[0] = new PropertySpec("Flash Interval", typeof(int), "Extended",
				"Which frame this object's flash should be.", null, new Dictionary<string, int>
				{
					{ "Don't Flash", 0 },
					{ "Frame 1", 1 },
					{ "Frame 2", 2 },
					{ "Frame 3", 3 },
					{ "Frame 4", 4 },
					{ "Frame 5", 5 },
					{ "Frame 6", 6 },
					{ "Frame 7", 7 },
					{ "Frame 8", 8 }
				},
				(obj) => (obj.PropertyValue < 9) ? (int)obj.PropertyValue : 0, // Past 8, the object doesn't flash properly anymore
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8}); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Don't Flash" : ("Flash Interval Frame " + subtype);
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			//return sprites[(subtype < 9) ? subtype : 0];
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			//return sprites[(obj.PropertyValue < 9) ? obj.PropertyValue : 0];
			return sprites[0];
		}
	}
}