using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R6
{
	class HSpinPlatform : R6.HPlatform
	{
		public override bool lightEnabled { get { return true; } }
	}
	
	class HPlatform : R6.Platform
	{
		public override Point offset { get { return new Point(64, 0); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Right", 0 }, { "Left", 1 }}; } }
	}
	
	class VSpinPlatform : R6.VPlatform
	{
		public override bool lightEnabled { get { return true; } }
	}
	
	class VPlatform : R6.Platform
	{
		public override Point offset { get { return new Point(0, 64); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Bottom", 0 }, { "Top", 1 }}; } }
	}
	
	abstract class Platform : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[3];
		private Sprite debug;
		private PropertySpec[] properties = new PropertySpec[1];
		
		public virtual bool lightEnabled { get { return false; } }
		public abstract Point offset { get; }
		public abstract Dictionary<string, int> names { get; }
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R6/Objects.gif");
			sprites[2] = new Sprite(new Sprite(sheet.GetSection(1, 84, 48, 32), -24, -16), new Sprite(sheet.GetSection(lightEnabled ? 18 : 35, 117, 16, 16), -8, 16));
			sprites[0] = new Sprite(sprites[2],  offset.X,  offset.Y);
			sprites[1] = new Sprite(sprites[2], -offset.X, -offset.Y);
			
			BitmapBits bitmap = new BitmapBits((offset.X * 2) + 1, (offset.Y * 2) + 1);
			bitmap.DrawLine(6, 0, 0, offset.X * 2, offset.Y * 2); // LevelData.ColorWhite
			debug = new Sprite(bitmap, -offset.X, -offset.Y);
			
			properties[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which side this platform should start from.", null, names,
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
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
			return "Start From " + names.GetKey((subtype == 1) ? 1 : 0);
		}

		public override Sprite Image
		{
			get { return sprites[2]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[2];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue == 1) ? 1 : 0];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}