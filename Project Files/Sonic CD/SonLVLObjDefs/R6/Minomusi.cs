using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System;

namespace SCDObjectDefinitions.R6
{
	class Minomusi : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R6/Objects.gif");
			
			// (the thread kinda looks like a debug vis line lol but ig there's not much we can do about that-)
			Sprite thread = new Sprite(sheet.GetSection(246, 91, 8, 72), -4, 16);
			
			// (for ref - 96 is how many pixels the Minomusi goes down after dropping)
			
			// First, the badnik alone
			// (For icon purposes and etc)
			sprites[2] = new Sprite(sheet.GetSection(1, 151, 32, 32), -16, -8); // Good Minomusi
			sprites[3] = new Sprite(sheet.GetSection(17, 184, 16, 32), -8, -8); // Bad Minomusi
			
			// Now, let's combine it with the thread sprite
			sprites[0] = new Sprite(thread, new Sprite(sprites[2], 0, 96)); // Good, w/ thread
			sprites[1] = new Sprite(thread, new Sprite(sprites[3], 0, 96)); // Bad, w/ thread
			
			properties[0] = new PropertySpec("Condition", typeof(int), "Extended",
				"What condition this Minomusi should be in. Only Good badniks can eject spikes.", null, new Dictionary<string, int>
				{
					{ "Good", 0 },
					{ "Bad", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Good Condition" : "Bad Condition";
		}

		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override Sprite Image
		{
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 0) ? 2 : 3];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 0) ? 0 : 1];
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			Rectangle bounds = sprites[(obj.PropertyValue == 0) ? 2 : 3].Bounds;
			bounds.Offset(obj.X, obj.Y + 96);
			return bounds;
		}
	}
}