using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Global
{
	class Spikes : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[8];
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Global/Items.gif");
			sprites[0] = new Sprite(sheet.GetSection(84, 133, 40, 32), -20, -16);
			sprites[1] = new Sprite(sheet.GetSection(174, 133, 32, 40), -16, -20);
			sprites[2] = new Sprite(sheet.GetSection(174, 174, 32, 40), -16, -20);
			sprites[3] = new Sprite(sheet.GetSection(125, 133, 40, 32), -20, -16);
			sprites[4] = new Sprite(sheet.GetSection(84, 133, 8, 32), -4, -16);
			sprites[5] = new Sprite(sheet.GetSection(174, 133, 32, 8), -16, -4);
			sprites[6] = new Sprite(sheet.GetSection(174, 174, 32, 8), -16, -4);
			sprites[7] = new Sprite(sheet.GetSection(125, 133, 8, 32), -4, -16);
			
			properties = new PropertySpec[3];
			properties[0] = new PropertySpec("Count", typeof(int), "Extended",
                "How many Spikes there will be.", null, new Dictionary<string, int>
				{
					{ "3 Spikes", 0 },
					{ "1 Spike", 4 },
					{ "3 Spikes - Spaced", 8 },
					{ "6 Spikes - Spaced", 12 }
				},
                (obj) => obj.PropertyValue & 12,
                (obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 243) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("Orientation", typeof(int), "Extended",
                "Which way the Spikes are facing.", null, new Dictionary<string, int>
				{
					{ "Up", 0 },
					{ "Right", 1 },
					{ "Left", 2 },
					{ "Down", 3 }
				},
                (obj) => obj.PropertyValue & 3,
                (obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 252) | (byte)((int)value)));
			
			properties[2] = new PropertySpec("Moving", typeof(int), "Extended",
                "If the Spikes should peek in and out.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 128 }
				},
                (obj) => obj.PropertyValue & 128,
                (obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 127) | (byte)((int)value)));
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
			return null;
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
			int temp = obj.PropertyValue & 15;
			Sprite tmp;
			List<Sprite> sprs = new List<Sprite>();
			switch (temp)
			{
				case 0: // 3 Spikes (Up)
				case 1: // 3 Spikes (Right)
				case 2: // 3 Spikes (Left)
				case 3: // 3 Spikes (Down)
				case 4: // 1 Spike (Up)
				case 5: // 1 Spike (Right)
				case 6: // 1 Spike (Left)
				case 7: // 1 Spike (Down)
					tmp = new Sprite(sprites[temp]);
					sprs.Add(tmp);
					break;
				
				case 8:  // 3 Spikes (Spaced Out) (Up)
				case 11: // 3 Spikes (Spaced Out) (Down)
					temp -= 4;
					tmp = new Sprite(sprites[temp]);
					tmp.Offset(-24, 0);
					sprs.Add(tmp);
					tmp = new Sprite(sprites[temp]);
					sprs.Add(tmp);
					tmp = new Sprite(sprites[temp]);
					tmp.Offset(24, 0);
					sprs.Add(tmp);
					break;
				
				case 9:  // 3 Spikes (Spaced Out) (Right)
				case 10: // 3 Spikes (Spaced Out) (Left)
					temp -= 4;
					tmp = new Sprite(sprites[temp]);
					tmp.Offset(0, -24);
					sprs.Add(tmp);
					tmp = new Sprite(sprites[temp]);
					sprs.Add(tmp);
					tmp = new Sprite(sprites[temp]);
					tmp.Offset(0, 24);
					sprs.Add(tmp);
					break;
					
				case 12: // 6 Spikes (Spaced Out) (Up)
				case 15: // 6 Spikes (Spaced Out) (Down)
					temp -= 8;
					int sx = -60;
					for (int i = 0; i < 6; i++)
					{
						tmp = new Sprite(sprites[temp]);
						tmp.Offset((sx+=24)-24, 0);
						sprs.Add(tmp);
					}
					break;
					
				case 13: // 6 Spikes (Spaced Out) (Right)
				case 14: // 6 Spikes (Spaced Out) (Left)
					temp -= 8;
					int sy = -60;
					for (int i = 0; i < 6; i++)
					{
						tmp = new Sprite(sprites[temp]);
						tmp.Offset(0, (sy+=24)-24);
						sprs.Add(tmp);
					}
					break;
			}
			return new Sprite(sprs.ToArray());
		}
	}
}