using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace S1ObjectDefinitions.SLZ
{
	class RisingPlatform : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite sprites;
		private Sprite[] debug = new Sprite[15];
		
		public override void Init(ObjectData data)
		{
			sprites = new Sprite(LevelData.GetSpriteSheet("SLZ/Objects.gif").GetSection(84, 188, 80, 32), -40, -8);
			
			// The default values from RisingPlatform_distanceTable ("table28") in the object's script
			// If you've modified those, you can simply copy them over.
			int[] RisingPlatform_distanceTable = new int[15] {0x400000, 0x800000, 0xD00000, 0x400000, 0x800000, 0xD00000, 0x500000, 0x900000, 0xB00000, 0x500000, 0x900000, 0xB00000, 0x800000, 0x800000, 0xC00000};
			
			for (int i = 0; i < 14; i++)
			{
				BitmapBits overlay = new BitmapBits(80, 32);
				overlay.DrawRectangle(6, 0, 0, 80 - 1, 32 - 1); // LevelData.ColorWhite
				if (i < 14)
				{
					if (i < 12)
					{
						int sign = (i % 6 < 3) ? -1 : 1;
						debug[i] = new Sprite(overlay, -40, sign * (RisingPlatform_distanceTable[i] / 0x10000 * 2) - 8);
					}
					else
					{
						int sign = (i == 13) ? -1 : 1;
						debug[i] = new Sprite(overlay, sign * (RisingPlatform_distanceTable[i] / 0x10000 * 2) - 40, -sign * (RisingPlatform_distanceTable[i] / 0x10000) - 8);
					}
				}
				else
					debug[i] = new Sprite(overlay, -40, -8);
			}
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Movement", typeof(int), "Extended",
				"The Platform's movement.", null, new Dictionary<string, int>
				{
					{ "Up (128px)", 0 },
					{ "Up (256px)", 1 },
					{ "Up (416px)", 2 },
					
					{ "Down (128px)", 3 },
					{ "Down (256px)", 4 },
					{ "Down (416px)", 5 },
					
					{ "Up (160px)", 6 },
					{ "Up (288px)", 7 },
					{ "Up (352px)", 8 },
					
					{ "Down (160px)", 9 },
					{ "Down (288px)", 10 },
					{ "Down (352px)", 11 },
					
					{ "Up-Right", 12 },
					{ "Down-Left", 13 }
				},
				(obj) => obj.PropertyValue & 15,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~15) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Up (128px)";
				case 1: return "Up (256px)";
				case 2: return "Up (416px)";
				case 3: return "Down (128px)";
				case 4: return "Down (256px)";
				case 5: return "Down (416px)";
				case 6: return "Up (160px)";
				case 7: return "Up (288px)";
				case 8: return "Up (352px)";
				case 9: return "Down (160px)";
				case 10: return "Down (288px)";
				case 11: return "Down (352px)";
				case 12: return "Up-Right";
				case 13: return "Down-Left";
				
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[Math.Min((int)obj.PropertyValue, 14)];
		}
	}
}