using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class BoundsTriggerV : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(84, 0, 0, 31, 31); // its in-game colour is green but that's the same as NoGripArea, so let's make it blue instead
			sprite = new Sprite(sprite, new Sprite(bitmap, -16, -16));
			
			// yeah i literally have no idea why it's like this..
			// if you don't want the object to do anything, then maybe just don't place it at all??? what's the point of this????
			properties[0] = new PropertySpec("Enabled", typeof(bool), "Extended",
				"If this object should have any effect or not.", null,
				(obj) => obj.PropertyValue == 0,
				(obj, value) => obj.PropertyValue = (byte)((bool)value ? 0 : 1));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Enabled" : "Disabled";
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