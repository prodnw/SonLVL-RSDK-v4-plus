using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class NewtronShoot : ObjectDefinition
	{
		private readonly Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[2];

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '1':
				case 'M': // Origins test mission
				default:
					BitmapBits sheet = LevelData.GetSpriteSheet("GHZ/Objects2.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 1, 39, 39), -20, -20);
					sprites[1] = new Sprite(sheet.GetSection(81, 1, 39, 39), -19, -20);
					break;
				case '7':
					sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
					sprites[0] = new Sprite(sheet.GetSection(1, 164, 39, 39), -20, -20);
					sprites[1] = new Sprite(sheet.GetSection(81, 164, 39, 39), -19, -20);
					break;
			}
			
			// Originally prop val was unused, but Origins uses it (+ val1) for Missions
			// Fire In Dir needs to be true in order for the second one to have any effect
			// However, since they're displayed alphabetically, they're the other way around instead :(
			// At least there's descriptions, though
			
			properties[0] = new PropertySpec("Fire In Dir", typeof(int), "Extended",
				"Mission Mode only. If the Newtroon should only shoot in a specific direction, direction decided by Fire Direction.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 1 }
				},
				(obj) => ((V4ObjectEntry)obj).Value1 & 1,
				(obj, value) => ((V4ObjectEntry)obj).Value1 = (byte)((int)value));
			
			properties[1] = new PropertySpec("Fire Direction", typeof(int), "Extended",
				"Mission Mode only. Which way the Newtron should fire, provided Fire In Dir is active.", null, new Dictionary<string, int>
				{
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => obj.PropertyValue & 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); } // not putting Fire Direction here because its Mission Mode only so it's not usable normally
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 1];
		}
	}
}