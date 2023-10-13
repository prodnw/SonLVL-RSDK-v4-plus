using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R8
{
	class RPlaneShifter : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[4];
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R8/Objects2.gif");
			sprites[0] = new Sprite(new Sprite(sheet.GetSection(1, 200, 80, 16), -40, -8), new Sprite(sheet.GetSection(1, 217, 80, 16), -40, -184));
			
			sprites[1] = new Sprite(new Sprite[] {
				new Sprite(sheet.GetSection(100, 35, 8, 32), -4, -40),
				new Sprite(sheet.GetSection(100, 35, 8, 32), -4, -72),
				new Sprite(sheet.GetSection(100, 35, 8, 32), -4, -104),
				new Sprite(sheet.GetSection(100, 35, 8, 32), -4, -136),
				new Sprite(sheet.GetSection(100, 35, 8, 32), -4, -168)});
			
			sprites[2] = new Sprite(sprites[1],  20, 0);
			sprites[3] = new Sprite(sprites[1], -20, 0);
			
			// TODO: better name
			properties[0] = new PropertySpec("Mode", typeof(int), "Extended",
				"What type of object this Plane Shifter is. Parents should be followed by three child objects.", null, new Dictionary<string, int>
				{
					{ "Parent", 0 },
					{ "Child", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 1}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Parent Object" : "Child Object";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 0) ? 0 : 1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			if (obj.PropertyValue == 0)
				return sprites[0];
			
			int index = LevelData.Objects.IndexOf(obj) - 1;
			int offset = 0;
			while (index > 0)
			{
				if ((LevelData.Objects[index].Type == obj.Type) && (LevelData.Objects[index].PropertyValue == 0))
					break;
				
				index--;
				offset++;
				
				if (offset == 3)
				{
					return sprites[1];
				}
			}
			
			return sprites[offset+1];
		}
		
		public override Rectangle GetBounds(ObjectEntry obj)
		{
			if (obj.PropertyValue > 0)
				return base.GetBounds(obj);
			
			// the sprite includes both the top and bottom, so let's make the sel box only be the bottom
			return new Rectangle(-40 + obj.X, -8 + obj.Y, 80, 16);
		}
	}
}