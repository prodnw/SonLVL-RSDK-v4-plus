using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R6
{
	class TubeSwitch : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			sprite = new Sprite(
			                new Sprite(sprite, -8, -8),
			                new Sprite(sprite,  8, -8),
			                new Sprite(sprite, -8,  8),
			                new Sprite(sprite,  8,  8));
			
			BitmapBits bitmap = new BitmapBits(96, 96);
			bitmap.DrawRectangle(6, 0, 0, 95, 95); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -48, -48);
			
			properties[0] = new PropertySpec("Trigger", typeof(int), "Extended",
				"Which effect this Tube Switch should apply.", null, new Dictionary<string, int>
				{
					{ "H Entry", 0 },
					{ "H Exit", 1 },
					{ "Unstick", 2 },
					{ "Upwards Entry", 3 },
					{ "Upwards Exit", 4 },
					{ "Downwards Entry", 9 },
					{ "Downwards Exit", 10 },
					{ "Wheel Entry", 11 },
					{ "Wheel - TL Exit", 5 },
					{ "Wheel - TR Exit", 6 },
					{ "Wheel - BL Exit", 8 },
					{ "Wheel - Overlap", 7 },
					{ "Curve", 12 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2, 3, 4, 9, 10, 11, 5, 6, 8, 7, 12}); }
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
			return properties[0].Enumeration.GetKey(subtype);
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
		
		// not sure if i should add a "and object class name == "Tube SW"" check here or something?
		// in all versions of act 1, the obj has the wrong name in the stageconfig, so all hitboxes are 0x0
		// in act 2, however, the name is correct, so hitboxes are fine there
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if ((obj.PropertyValue == 5) || (obj.PropertyValue == 6) || (obj.PropertyValue == 8))
				return debug;
			
			return null;
		}
	}
}