using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

// this obj is the same in every zone it's used, so all of them pull from this r1 file
// (yeah it looks weird to have R6 stuff in the R1 folder, but it beats having the same script copied like 6 times)

namespace SCDObjectDefinitions.R6
{
	class InvisibleBlockNK : R1.InvisibleBlock
	{
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return null; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
		}
	}
}

namespace SCDObjectDefinitions.R1
{
	class InvisibleBlock : ObjectDefinition // yeah the object is called "InvisivleBlock" in R1 but let's just use the standard name instead
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
				"Origins only. If Knuckles should be able to climb this object.", null,
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