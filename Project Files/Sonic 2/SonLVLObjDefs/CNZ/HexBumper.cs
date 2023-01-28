using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CNZ
{
	class HexBumper : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '4')
			{
				img = new Sprite(LevelData.GetSpriteSheet("CNZ/Objects.gif").GetSection(99, 99, 48, 32), -24, -16);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(581, 343, 48, 32), -24, -16);
			}
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Moving", typeof(int), "Extended",
				"If the Bumper will be Moving or not.", null, new Dictionary<string, int>
				{
					{ "Static", 0 },
					{ "Moving", 1 }
				},
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
				(obj, value) => obj.PropertyValue = ((byte)((int)value)));
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
				default:
					return "Static";
				case 1:
					return "Moving";
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