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
		private PropertySpec[] properties = new PropertySpec[1];
		private ReadOnlyCollection<byte> subtypes;
		private Sprite[] sprites = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			try
			{
				string[] anis = new string[]{ "Sonic.ani", "Tails.ani", "Knuckles.ani", "Amy.ani" };
				
				for (int i = 0; i < anis.Length; i++)
				{
					RSDKv3_4.Animation anim = LevelData.ReadFile<RSDKv3_4.Animation>("Data/Animations/" + anis[i]);
					RSDKv3_4.Animation.AnimationEntry.Frame frame = anim.animations[0].frames[0];
					sprites[i] = new Sprite(LevelData.GetSpriteSheet(anim.spriteSheets[frame.sheet]).GetSection(frame.sprX, frame.sprY, frame.width, frame.height), frame.pivotX, frame.pivotY);
				}
				
				// let's give tails his, well, tails
				sprites[1] = new Sprite(new Sprite(LevelData.GetSpriteSheet("Players/Tails1.gif").GetSection(82, 199, 16, 24), -22, -8), sprites[1]);
			}
			catch
			{
				// one or more of the ani files doesn't exist, we're likely on a pre-plus file that doesn't have knux or amy
				
				// let's clear all sprites after Sonic
				for (int i = 1; i < 4; i++)
				{
					sprites[i] = null;
				}
			}
			
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
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 5 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Default Start";
				case 1: return "Tails Start";
				case 2: return "Knuckles Start";
				case 5: return "Amy Start";
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			// looks really goofy lol
			int index = (subtype > 2) ? 3 : subtype;
			return sprites[(sprites[index] != null) ? index : 0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int index = (obj.PropertyValue > 2) ? 3 : obj.PropertyValue;
			return sprites[(sprites[index] != null) ? index : 0];
		}
	}
}