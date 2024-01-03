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
			
			properties[0] = new PropertySpec("Can Crush", typeof(bool), "Extended",
				"If this Mission Block is be able to crush the player.", null,
				(obj) => obj.PropertyValue == 0,
				(obj, value) => obj.PropertyValue = (byte)((bool)value ? 0 : 1));
			
			properties[1] = new PropertySpec("Pull Up", typeof(bool), "Extended", // name's weird but it's the best i can think of, so..
				"If Knuckles is able to pull himself up on this Mission Block.", null,
				(obj) => ((V4ObjectEntry)obj).Value0 != 0,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((bool)value ? 1 : 0));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Can Crush" : "Disable Crush";
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