using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;

// this file just hosts basic renders for single-sprite objects that just need zone folder checks

namespace S2ObjectDefinitions.ARZ
{
	class Brick : ARZ.Generic
	{
		public override Sprite GetSprite()
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '3')
			{
				return new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(18, 128, 32, 16), -16, -8);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(436, 306, 32, 16), -16, -8);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanHammer : ARZ.Generic
	{
		public override Sprite GetSprite()
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '3')
			{
				return new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(1, 147, 76, 52), -44, -28);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(222, 5, 76, 52), -44, -28);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanTotem : ARZ.Generic
	{
		public override Sprite GetSprite()
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '3')
			{
				return new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(223, 1, 32, 160), 0, -64);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 95, 32, 160), 0, -64);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
	
	class EggmanArrow : ARZ.Generic
	{
		public override Sprite GetSprite()
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '3')
			{
				return new Sprite(LevelData.GetSpriteSheet("ARZ/Objects.gif").GetSection(194, 1, 29, 6), -16, -3);
			}
			else
			{
				return new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(298, 31, 29, 6), -16, -3);
			}
		}
		
		public override bool Hidden { get { return true; } }
	}
}

namespace S2ObjectDefinitions.ARZ
{
	abstract class Generic : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = GetSprite();
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
			return null;
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
		
		public virtual Sprite GetSprite()
		{
			return (new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(1, 143, 32, 32), -16, -16));
		}
	}
}