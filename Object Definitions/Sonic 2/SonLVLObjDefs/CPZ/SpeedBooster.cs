using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class SpeedBooster : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '2') // "2" for both CPZ and DEZ (DEZ obj loads CPZ sheet)
			{
				img = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(91, 25, 48, 16), -24, -8);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(474, 321, 48, 16), -24, -8);
			}

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Speed", typeof(int), "Extended",
				"The amount of speed the player gets after being launched.", null, new Dictionary<string, int>
				{
					{ "Fast", 0 },
					{ "Slow", 1 }
				},
				(obj) => (obj.PropertyValue == 0 ? 0 : 1),
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
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
			switch (subtype)
			{
				case 0:
					return "Fast";
				case 1:
					return "Slow";
				default:
					return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}
	}
}