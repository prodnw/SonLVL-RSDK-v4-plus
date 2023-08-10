using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class HorizontalDoor : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		private Sprite[] debug = new Sprite[2];
		
		public override void Init(ObjectData data)
		{
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] <= 'B')
				sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects.gif").GetSection(18, 69, 128, 16), -64, -8); // past/present frame
			else
				sprite = new Sprite(LevelData.GetSpriteSheet("R4/Objects3.gif").GetSection(62, 211, 128, 16), -64, -8); // past/present frame
			
			BitmapBits bitmap = new BitmapBits(129, 17);
			bitmap.DrawRectangle(6, 0, 0, 127, 15); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -64 - 128, -8);
			debug[1] = new Sprite(bitmap, -64 + 128, -8);
			
			properties[0] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way this Door should open.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 1 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
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
				case 0: return "Open Left";
				case 1: return "Open Right";
				default: return "Static"; // "Unknown" works too 
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
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[obj.PropertyValue & 1];
		}
	}
}