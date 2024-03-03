using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R5
{
	class StaticRock : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("R5/Objects.gif").GetSection(92, 18, 32, 32), -16, -16);
			
			sprites[1] = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			BitmapBits bitmap = new BitmapBits(32, 32);
			bitmap.DrawRectangle(15, 0, 0, 31, 31); // yellow
			sprites[2] = new Sprite(sprites[1], new Sprite(bitmap, -16, -16));
			
			bitmap = new BitmapBits(32, 30);
			bitmap.DrawRectangle(15, 0, 0, 31, 29); // yellow
			sprites[1] = new Sprite(sprites[1], new Sprite(bitmap, -16, -15));
			
			sprites[3] = new Sprite();
			
			properties[0] = new PropertySpec("Mode", typeof(int), "Extended",
				"Which type of block this object is.", null, new Dictionary<string, int>
				{
					{ "Rock", 0 },
					{ "Invisible (Wall)", 1 },
					{ "Invisible (Roof)", 2 }
				},
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1, 2}); }
		}

		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Rock";
				case 1: return "Invisible (Wall)";
				case 2: return "Invisible (Roof)";
				default: return "Unknown";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype > 2) ? 3 : subtype];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue > 0) ? 3 : obj.PropertyValue];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue > 0) ? sprites[Math.Min((int)obj.PropertyValue, 2)] : null;
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			if (obj.PropertyValue == 0)
				return Rectangle.Empty;
			
			int sy = (obj.PropertyValue == 1) ? 30 : 32;
			return new Rectangle(obj.X - 16, obj.Y - (sy / 2), 32, sy);
		}
	}
}