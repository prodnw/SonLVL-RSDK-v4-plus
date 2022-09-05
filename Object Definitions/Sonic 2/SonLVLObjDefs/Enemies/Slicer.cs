using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Slicer : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '9')
			{
				img = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(29, 1, 47, 32), -16, -16);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(880, 256, 47, 32), -16, -16);
			}

			properties = new PropertySpec[2];
			properties[0] = new PropertySpec("PDir", typeof(int), "Extended",
				"Where the Slicer is facing (not to be confused with object.direction).", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (obj.PropertyValue & 1),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 254) | (byte)((int)value)));
			
			properties[1] = new PropertySpec("On Roof", typeof(int), "Extended",
				"If the Slicer is on a roof or not.", null, new Dictionary<string, int>
				{
					{ "False", 0 },
					{ "True", 2 }
				},
				(obj) => (obj.PropertyValue & 2),
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & 253) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3 }); }
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
					return "Facing Left";
				case 1:
					return "Facing Right";
				case 2:
					return "Facing Left, Roof";
				case 3:
					return "Facing Right, Roof";
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
			Sprite sprite = new Sprite(img);
			sprite.Flip((subtype & 1) == 0, (subtype & 2) == 2);
			return sprite;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return SubtypeImage(obj.PropertyValue);
		}
	}
}