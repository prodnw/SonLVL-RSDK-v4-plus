using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Mission
{
	class SignPost2 : ObjectDefinition
	{
		private Sprite img;
		private PropertySpec[] properties;

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Global/Items2.gif").GetSection(34, 182, 48, 48), -24, -16);

			properties = new PropertySpec[3];
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How the Signpost should act.", null, new Dictionary<string, int>
				{
					{ "Normal", 0 },
					{ "Follow Bounds", 3 }
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			properties[1] = new PropertySpec("Center Offset", typeof(int), "Extended",
				"How offset the Signpost's camera will be. X-wise. Measured in pixels, to every 16th pixel", null,
				(obj) => ((V4ObjectEntry)obj).Value0 << 4,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((int)value) >> 4);
			
			properties[2] = new PropertySpec("Enable Move Right", typeof(bool), "Extended",
				"If the Signpost should make the player move right afterwards.", null,
				(obj) => (((V4ObjectEntry)obj).Value1 == 1),
				(obj, value) => ((V4ObjectEntry)obj).Value1 = ((bool)value == false ? 0 : 1));
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