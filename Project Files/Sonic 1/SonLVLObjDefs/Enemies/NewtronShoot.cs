using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class NewtronShoot : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder.EndsWith("Zone07"))
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 164, 39, 39), -20, -20);
			}
			else
			{
				sprite = new Sprite(LevelData.GetSpriteSheet("GHZ/Objects2.gif").GetSection(1, 1, 39, 39), -20, -20);
			}
			
			// Originally prop val was unused (but still set sometimes), but Origins uses it (+ val1) for Missions
			
			properties[0] = new PropertySpec("Fire Direction", typeof(int), "Extended",
				"Mission Mode only. Which way the Newtron should be able to fire.", null, new Dictionary<string, int>
				{
					{ "Normal", -1 },
					{ "Right", 0 },
					{ "Left", 1 }
				},
				(obj) => (((V4ObjectEntry)obj).Value1 == 0) ? -1 : (int)obj.PropertyValue,
				(obj, value) => {
						int val = (int)value;
						((V4ObjectEntry)obj).Value1 = (val < 0) ? 0 : 1;
						obj.PropertyValue = (byte)((val < 0) ? 0 : val);
					}
				);
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); } // not putting Fire Direction here because it needs val1 to be set
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
}