using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Balkiry : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '0':
					img = new Sprite(LevelData.GetSpriteSheet("SCZ/Objects.gif").GetSection(1, 34, 64, 34), -36, -20);
					break;
				case 'M':
				default:
					img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(588, 306, 64, 34), -36, -20);
					break;
			}

			properties = new PropertySpec[1];
			
			properties[0] = new PropertySpec("Speed", typeof(int), "Extended",
				"How fast the Balkiry will be.", null, new Dictionary<string, int>
				{
					{ "Slow", 0 },
					{ "Fast", 1 }
				},
				(obj) => obj.PropertyValue & 1,
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
					return "Slow";
				case 1:
					return "Fast";
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