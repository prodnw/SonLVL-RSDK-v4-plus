using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.TAttack
{
	class RoundIcon : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[8];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[14];
			
			BitmapBits sheet = LevelData.GetSpriteSheet("TAttack/TimeAttack.gif");
			frames[0] = new Sprite(sheet.GetSection(1, 609, 68, 90), -34, -45);
			frames[1] = new Sprite(sheet.GetSection(70, 577, 64, 16), -32, 15);
			frames[2] = new Sprite(sheet.GetSection(135, 577, 64, 16), -32, 15);
			frames[3] = new Sprite(sheet.GetSection(200, 577, 64, 16), -32, 15);
			frames[4] = new Sprite(sheet.GetSection(265, 577, 64, 16), -32, 15);
			frames[5] = new Sprite(sheet.GetSection(70, 593, 64, 16), -32, 15);
			frames[6] = new Sprite(sheet.GetSection(135, 593, 64, 16), -32, 15);
			frames[7] = new Sprite(sheet.GetSection(200, 593, 64, 16), -32, 15);
			frames[8] = new Sprite(sheet.GetSection(265, 593, 64, 16), -32, 15);
			frames[9] = new Sprite(sheet.GetSection(70, 638, 64, 13), -32, 31);
			frames[10] = new Sprite(sheet.GetSection(70, 652, 64, 13), -32, 31);
			frames[11] = new Sprite(sheet.GetSection(441, 860, 70, 92), -35, -46);
			frames[12] = new Sprite(sheet.GetSection(223, 679, 56, 21), -29, -22);
			frames[13] = new Sprite(sheet.GetSection(330, 604, 8, 13), 20, 16);
			
			for (int i = 0; i < 7; i++)
			{
				sprites[i] = new Sprite(frames[0], frames[10], frames[i + 1]);
			}
			
			sprites[7] = new Sprite(frames[0], frames[11], frames[13]);
			
			properties[0] = new PropertySpec("Round", typeof(int), "Extended",
                "Which Round this Icon is for.", null, new Dictionary<string, int>
				{
					{ "Palmtree Panic", 0 },
					{ "Collision Chaos", 1 },
					{ "Tidal Tempest", 2 },
					{ "Quartz Quadrant", 3 },
					{ "Wacky Workbench", 4 },
					{ "Stardust Speedway", 5 },
					{ "Metallic Madness", 6 },
					{ "Total Time", 7 }
				},
                (obj) => obj.PropertyValue,
                (obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0:
					return "Palmtree Panic";
				case 1:
					return "Collision Chaos";
				case 2:
					return "Tidal Tempest";
				case 3:	
					return "Quartz Quadrant";
				case 4:
					return "Wacky Workbench";
				case 5:
					return "Stardust Speedway";
				case 6:
					return "Metallic Madness";
				case 7:
					return "Total Time";
				default:
					return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype > 7) ? 7 : subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue > 7) ? 7 : obj.PropertyValue];
		}
	}
}