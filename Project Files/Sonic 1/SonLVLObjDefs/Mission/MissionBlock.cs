using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Mission
{
	class MissionBlock : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[2];

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Mission/Objects.gif").GetSection(1, 18, 32, 32), -16, -16);
			
			properties[0] = new PropertySpec("Disable Crush", typeof(bool), "Extended",
				"If this Mission Block should not be able to crush the player.", null,
				(obj) => obj.PropertyValue != 0,
				(obj, value) => obj.PropertyValue = (byte)((bool)value ? 1 : 0));
			
			properties[1] = new PropertySpec("Pull Up Flag", typeof(bool), "Extended", // TODO: name's a bit iffy
				"If Knuckles is able to pull himself up on this Mission Block. Only the topmost Block in a pillar should have this set.", null,
				(obj) => ((V4ObjectEntry)obj).Value0 != 0,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((bool)value ? 1 : 0));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
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