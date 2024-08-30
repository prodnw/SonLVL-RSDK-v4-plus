using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Menu
{
	class TrophiesHeading : ObjectDefinition
	{
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("Menu/MenuGfx1_EN.gif");
			// btw yeah the font doesn't have parenthesis, but let's just ignore that
			sprite = new Sprite(new Sprite(TrophieList.DrawText(sheet, "Achievements (0/12)"), 20, 2), new Sprite(TrophieList.DrawText(sheet, "Gamerscore   (0/200)"), 20, 16));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
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
	
	class TrophieList : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[13];
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprites[12] = new Sprite(LevelData.GetSpriteSheet("Menu/MenuGfx2_EN.gif").GetSection(260, 1, 32, 32), -16, -16); // Sonic gear icon (the actual achievement icons use Hi-Res mode, so..)
			
			string[,] text = new string[,]
			{
				{"88 Miles Per Hour", "Travel through time."},
				{"Just One Hug is Enough", "Get a hug from Amy."},
				{"Paradise Found", "Complete a Zone in the Good Future."},
				{"Take the High Road", "Pass the upper signpost in Collision Chaos Zone 2."},
				{"King of the Rings", "Collect 200 Rings."},
				{"Statue Saviour", "Find the angel statue in Wacky Workbench."},
				{"Heavy Metal", "Defeat Metal Sonic without getting hurt."},
				{"All Stages Clear!", "Finish the game."},
				{"Treasure Hunter", "Collect all the Time Stones."},
				{"Dr. Eggman Got Served", "Destroy Dr. Eggman's final machine."},
				{"Just In Time!", "Complete the Time Attack mode in under 25 minutes."},
				{"Saviour of the Planet", "Destroy all the robot transporters and Metal Sonic holograms in the past."}
			};
			
			BitmapBits sheet = LevelData.GetSpriteSheet("Menu/MenuGfx1_EN.gif");
			for (int i = 0; i < text.Length / 2; i++)
			{
				sprites[i] = new Sprite(sprites[12], new Sprite(DrawText(sheet, text[i, 0]), 20, -13));
				sprites[i] = new Sprite(sprites[i], new Sprite(DrawText(sheet, text[i, 1]), 20, 2));
			}
			
			properties[0] = new PropertySpec("Trophy", typeof(int), "Extended",
				"Which Trophy this object is.", null, new Dictionary<string, int>
				{
					{ "88 Miles Per House", 0 },
					{ "Just One Hug is Enough", 1 },
					{ "Paradise Found", 2 },
					{ "Take the High Road", 3 },
					{ "King of the Rings", 4 },
					{ "Statue Saviour", 5 },
					{ "Heavy Metal", 6 },
					{ "All Stages Clear!", 7 },
					{ "Treasure Hunter", 8 },
					{ "Dr. Eggman Got Served", 9 },
					{ "Just In Time!", 10 },
					{ "Saviour of the Planet", 11 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public static Sprite DrawText(BitmapBits fontSheet, string text)
		{
			Sprite sprite = new Sprite();
			for (int i = 0; i < text.Length; i++)
			{
				int x = 0, y = 0;
				int character = text[i];
				if (character >= 'a' && character <= 'z')
					character -= 0x20;
				
				if (character >= 'A' && character <= 'Z')
				{
					x = 154 + (((character - 'A') % 14) * 9);
					y = 366 + (((character - 'A') / 14) * 17);
				}
				else if (character >= '0' && character <= '9')
				{
					x = 154 + ((character - '0') * 9);
					y = 400;
				}
				else if (character == '.')
				{
					x = 262;
					y = 383;
				}
				else if (character == ',')
				{
					x = 271;
					y = 383;
				}
				/*
				else if (character == '\'') // used in one of the achievemnt descriptions, let's pull from the time tick marks
				{
					x = 168;
					y = 434;
				}
				*/
				
				if ((x + y) > 0)
					sprite = new Sprite(sprite, new Sprite(fontSheet.GetSection(x, y, 8, 16), i * 9, 0));
			}
			
			return sprite;
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11}); }
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
			get { return sprites[12]; }
		}
		
		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[12];
		}
		
		public override Sprite GetSprite(ObjectEntry obj)
		{
			return (obj.PropertyValue < 12) ? sprites[obj.PropertyValue] : new Sprite(LevelData.UnknownSprite);
		}
	}
}