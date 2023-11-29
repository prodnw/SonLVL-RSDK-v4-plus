using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Mission
{
	class SignPost2 : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[3];

		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Items2.gif").GetSection(34, 182, 48, 48), -24, -16);
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How the Signpost should act.", null, new Dictionary<string, int>
				{
					{ "Normal", 0 },
					{ "Follow Bounds", 3 }
				},
				(obj) => (obj.PropertyValue < 3) ? 0 : 3,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			properties[1] = new PropertySpec("Lock Range", typeof(int), "Extended",
				"How far to the left, in intervals of 16 pixels, that the screen lock should start to take effect. 0 is default Signpost bounds.", null,
				(obj) => ((V4ObjectEntry)obj).Value0 << 4,
				(obj, value) => ((V4ObjectEntry)obj).Value0 = ((int)value) >> 4);
			
			properties[2] = new PropertySpec("Exit Right", typeof(bool), "Extended",
				"If the Signpost should make the player move right after beating the level.", null,
				(obj) => (((V4ObjectEntry)obj).Value1 != 0),
				(obj, value) => ((V4ObjectEntry)obj).Value1 = ((bool)value ? 1 : 0));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); } // we're not putting Follow Bounds in here because it's not really used anyways
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