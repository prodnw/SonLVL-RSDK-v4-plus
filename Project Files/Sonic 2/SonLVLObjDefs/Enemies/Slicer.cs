using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.Enemies
{
	class Slicer : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[4];
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			Sprite img;
			
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '9')
			{
				img = new Sprite(LevelData.GetSpriteSheet("MPZ/Objects.gif").GetSection(29, 1, 47, 32), -16, -16);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(880, 256, 47, 32), -16, -16);
			}
			
			for (int i = 0; i < 4; i++)
			{
				sprites[i] = new Sprite(img);
				sprites[i].Flip((i & 1) == 0, (i & 2) == 2);
			}
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Slicer is facing.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 },
					{ "Left (Roof)", 2 },
					{ "Right  (Roof)", 3 }
				},
				(obj) => (obj.PropertyValue & 3),
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
			switch (subtype & 3)
			{
				default:
				case 0:
					return "Facing Left";
				case 1:
					return "Facing Right";
				case 2:
					return "Facing Left, Roof";
				case 3:
					return "Facing Right, Roof";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[subtype & 3];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 3];
		}
	}
}