using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R7
{
	class SpringCage : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[7];
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[6];
			
			BitmapBits sheet = LevelData.GetSpriteSheet("R7/Objects.gif");
			frames[0] = new Sprite(sheet.GetSection(90, 52, 16, 16), -8, -8);
			frames[1] = new Sprite(sheet.GetSection(34, 96, 64, 24), -8, -28);
			frames[2] = new Sprite(sheet.GetSection(59, 121, 56, 56), -24, -56);
			frames[3] = new Sprite(sheet.GetSection(59, 121, 56, 56), -56, -24);
			frames[4] = new Sprite(sheet.GetSection(34, 121, 24, 64), -28, -56);
			frames[5] = new Sprite(sheet.GetSection(34, 96, 64, 24), -56, -28);
			
			sprites[4] = sprites[2] = sprites[0] = new Sprite(frames[4], new Sprite(frames[4], true, false), frames[0]);
			sprites[1] = new Sprite(frames[5], new Sprite(frames[5], false, true), frames[0]);
			sprites[3] = new Sprite(frames[2], new Sprite(frames[3], true, true), frames[0]);
			sprites[5] = new Sprite(new Sprite(frames[2], true, false), new Sprite(frames[3], false, true), frames[0]);
			sprites[6] = new Sprite(frames[1], new Sprite(frames[1], false, true), frames[0]);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Spring Cage is facing.", null, new Dictionary<string, int>
				{
					{ "Rotating", 0 },
					{ "Left", 1 },
					{ "Up-Left", 5 },
					{ "Upwards", 4 },
					{ "Up-Right", 3 },
					{ "Right", 6 }
				},
				(obj) => (obj.PropertyValue == 2 || obj.PropertyValue > 6) ? 4 : obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 5, 4, 3, 6 }); }
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
					return "Rotating";
				case 1:
					return "Pointing Left";
				case 3:
					return "Pointing Up-Right";
				case 4:
					return "Pointing Up";
				case 5:
					return "Pointing Up-Left";
				case 6:
					return "Pointing Right";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 2 || subtype > 6) ? 6 : subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			// if obj[+1] is an R7 Spring, then let's make sure we update its sprite too, since it normally depends on this object
			if ((LevelData.Objects.IndexOf(obj) + 1) < LevelData.Objects.Count)
				if (LevelData.Objects[LevelData.Objects.IndexOf(obj) + 1].Name == "R7 Spring")
					LevelData.Objects[LevelData.Objects.IndexOf(obj) + 1].UpdateSprite();
			
			return sprites[(obj.PropertyValue == 2 || obj.PropertyValue > 6) ? 6 : obj.PropertyValue];
		}
	}
}