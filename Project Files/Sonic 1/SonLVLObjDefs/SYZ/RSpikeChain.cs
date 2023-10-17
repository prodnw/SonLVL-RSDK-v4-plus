using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.SYZ
{
	class RSpikeChain : ObjectDefinition
	{
		private Sprite sprite;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("SYZ/Objects.gif").GetSection(88, 138, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Spike Chain should behave.", null, new Dictionary<string, int>
				{
					{ "Speed: Fast, Length: Long", 0 },
					{ "Speed: Slow, Length: Long", 1 },
					{ "Speed: Slow, Length: Short", 2 },
					{ "Speed: Slow (Reverse), Length: Short", 3 },
				},
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue =  (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3 }); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Long, Fast";
				case 1: return "Long, Slow";
				case 2: return "Short, Slow";
				case 3: return "Short, Slow (Reverse)";
				default: return "Unknown";
			}
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
			int length = 0;
			switch (obj.PropertyValue)
			{
				case 0: 
				case 1: length = 4; break;
				case 2:
				case 3: length = 2; break;
			}
			
			Sprite frame = new Sprite(sprite);
			for (int i = 0; i < length; i++)
				frame = new Sprite(frame, new Sprite(sprite, 0, -((i+1) * 16)));
			
			return frame;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int length = 0;
			switch (obj.PropertyValue)
			{
				case 0: 
				case 1: length = 4; break;
				case 2:
				case 3: length = 2; break;
			}
			
			BitmapBits overlay = new BitmapBits(2 * (length * 16) + 1, 2 * (length * 16) + 1);
			overlay.DrawCircle(6, (length * 16), (length * 16), (length * 16)); // LevelData.ColorWhite
			return new Sprite(overlay, -(length * 16), -(length * 16));
		}
	}
}