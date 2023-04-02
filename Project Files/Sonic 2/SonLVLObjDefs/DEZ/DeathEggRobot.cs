using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.DEZ
{
	class DeathEggRobot : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties = new PropertySpec[1];

		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '2')
			{
				img = new Sprite(LevelData.GetSpriteSheet("DEZ/Objects.gif").GetSection(399, 183, 112, 72), -44, -36);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(911, 183, 112, 72), -44, -36);
			}
			
			properties[0] = new PropertySpec("Skip Cutscene", typeof(int), "Extended",
				"If the Death Egg Robot should skip the ending cutscene after it is defeated.", null,
				(obj) => obj.PropertyValue != 0,
				(obj, value) => obj.PropertyValue = ((byte)(((bool)value == true) ? 1 : 0)));
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
					return "Trigger Cutscene";
				default:
				case 1:
					return "Skip Cutscene";
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