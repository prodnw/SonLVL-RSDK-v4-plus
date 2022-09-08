using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.EHZ
{
	class ObjectActivator : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite img;
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}

		public override void Init(ObjectData data)
		{
			img = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(168, 18, 16, 16), -8, -8);
			
			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Activate Offset", typeof(int), "Extended",
				"The Entity Pos offset of the Object to be activated by this Activator.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		public override byte DefaultSubtype
		{
			get { return 1; }
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
			get { return img; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return img;
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return img;
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (obj.PropertyValue == 0)
				return null;
			
			int index = LevelData.Objects.IndexOf(obj) + obj.PropertyValue;
			
			ushort xmin = Math.Min(obj.X, LevelData.Objects[index].X);
			ushort ymin = Math.Min(obj.Y, LevelData.Objects[index].Y);
			ushort xmax = Math.Max(obj.X, LevelData.Objects[index].X);
			ushort ymax = Math.Max(obj.Y, LevelData.Objects[index].Y);
			
			BitmapBits bmp = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			
			bmp.DrawLine(LevelData.ColorWhite, obj.X - xmin, obj.Y - ymin, LevelData.Objects[index].X - xmin, LevelData.Objects[index].Y - ymin);
			
			return new Sprite(bmp, xmin - obj.X, ymin - obj.Y);
		}
	}
}