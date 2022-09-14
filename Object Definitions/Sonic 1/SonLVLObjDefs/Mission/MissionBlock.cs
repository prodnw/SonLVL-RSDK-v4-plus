using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Mission
{
	class MissionBlock : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Mission/Objects.gif").GetSection(1, 18, 32, 32), -16, -16);

			properties = new PropertySpec[2];
			
			// Flipped because this sounds cleaner
			properties[0] = new PropertySpec("Can Crush", typeof(bool), "Extended",
				"If this Mission Block can crush the player.", null,
				(obj) => obj.PropertyValue == 0,
				(obj, value) => obj.PropertyValue = (byte)((bool)value == false ? 1 : 0));
			
			properties[1] = new PropertySpec("Climbable", typeof(bool), "Extended", // TODO: prolly could name this better, Knuckles can climb all blocks but this value is if he can pull himself up or not
				"If Knuckles is able to pull himself up on this Mission Block. Only the topmost Block in a pillar should have this set.", null,
				(obj) => (((V4ObjectEntry)obj).Value0 == 1),
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((bool)value == false ? 0 : 1));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
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
			return subtype + "";
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