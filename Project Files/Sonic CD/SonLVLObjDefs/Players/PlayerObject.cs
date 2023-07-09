using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

namespace SCDObjectDefinitions.Players
{
	class PlayerObject : ObjectDefinition
	{
		private PropertySpec[] properties;
		private ReadOnlyCollection<byte> subtypes;
		private Sprite[] sprites = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			string[] anis = new string[]{ "Sonic.ani", "Tails.ani", "Knuckles.ani", "Amy.ani" };
			
			bool plus = false;
			
			try
			{
				for (int i = 0; i < anis.Length; i++)
				{
					RSDKv3_4.Animation anim = LevelData.ReadFile<RSDKv3_4.Animation>("Data/Animations/" + anis[i]);
					RSDKv3_4.Animation.AnimationEntry.Frame frame = anim.animations[0].frames[0];
					sprites[i] = new Sprite(LevelData.GetSpriteSheet(anim.spriteSheets[frame.sheet]).GetSection(frame.sprX, frame.sprY, frame.width, frame.height), frame.pivotX, frame.pivotY);
				}
				
				plus = true;
			}
			catch
			{
				// one or more of the ani files doesn't exist, so we're likely on a pre-plus file that doesn't have knux or amy
				// (don't really need to reset it again but may as well)
				plus = false;
			}
			
			if (plus)
			{
				// Since we're on a Plus data file, let's allow character-specific spanws
				
				properties = new PropertySpec[1];
				properties[0] = new PropertySpec("Character", typeof(int), "Extended",
					"Which characters should start here. Only has effect in Origins.", null, new Dictionary<string, int>
					{
						{ "Default", 0 },
						{ "Tails", 1 },
						{ "Knuckles", 2 },
						{ "Amy", 5 }
					},
					(obj) => (int)obj.PropertyValue,
					(obj, value) => obj.PropertyValue = (byte)((int)value));
				
				subtypes = new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 5 });
			}
			else
			{
				// We're on a pre-Plus data file, so let's not use any of Plus's features
				
				properties = new PropertySpec[0];
				subtypes = new ReadOnlyCollection<byte>(new byte[] {});
			}
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return subtypes; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			if (properties.Length > 0)
			{
				// We're on a Plus file
				switch (subtype)
				{
					case 0: return "Default Start";
					case 1: return "Tails Start";
					case 2: return "Knuckles Start";
					case 5: return "Amy Start";
					default: return "Unknown";
				}
			}
			
			return "";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			int index = 0;
			if (properties.Length > 0) index = (subtype > 2) ? 3 : subtype;
			return sprites[index];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int index = 0;
			if (properties.Length > 0) index = (obj.PropertyValue > 2) ? 3 : obj.PropertyValue;
			return sprites[index];
		}
	}
}