using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

// Not sure how I want to handle this object yet.. do I want to have 5 copies of essentially the same def, since they're all separate objects in the scripts?
// Or do I want to just go the actually sensible route and maybe just make a "Common" folder or something...

namespace SCDObjectDefinitions.R1
{
	class InvisibleBlock : ObjectDefinition // yeah the object is called "InvisivleBlock" but let's just use the script name and not object name for this
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public override void Init(ObjectData data)
		{
			Sprite frame = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			// Maybe climbables could be yellow while non-climbables could be red? red boxes are kinda scary though, not sure if they'd fit well in the editor...
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(15, 0, 0, 31, 31); // yellow
			sprite = new Sprite(frame, new Sprite(bitmap, -16, -16));
			
			properties[0] = new PropertySpec("Climbable", typeof(bool), "Extended",
				"If Knuckles should be able to climb this object. Only has effect in Origins.", null,
				(obj) => obj.PropertyValue == 0,
				(obj, value) => obj.PropertyValue = (byte)((bool)value ? 0 : 1));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1 }); }
		}

		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Climbable";
				case 1: return "Not Climbable";
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
			return sprite;
		}
	}
}