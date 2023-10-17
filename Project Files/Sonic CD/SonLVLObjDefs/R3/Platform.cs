using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R3
{
	class HPlatform : R3.Platform
	{
		public override Point offset { get { return new Point(78, 0); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Right", 0 }, { "Left", 1 }}; } }
	}
	
	class VPlatform : R3.Platform
	{
		// well technically the object uses Sin instead of Cos so it starts from origin on both types and this isn't *really* true, but
		// let's just keep it this way since it's cleaner
		public override Point offset { get { return new Point(0, 78); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Bottom", 0 }, { "Top", 1 }}; } }
	}
	
	class RPlatform : R3.Platform
	{
		// not exactly sure how to distinguish this from moving platforms..
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits bitmap = new BitmapBits(2, 409);
			bitmap.DrawLine(6, 0, 0, 0, 408); // LevelData.ColorWhite
			return new Sprite(bitmap, 0, -408);
		}
		
		public override PropertySpec[] SetupProperties()
		{
			return null;
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
		}
	}
	
	abstract class Platform : ObjectDefinition
	{
		private PropertySpec[] properties;
		private Sprite[] sprites = new Sprite[3];
		private Sprite debug;
		
		public virtual Point offset { get { return new Point(0, 0); } }
		public virtual Dictionary<string, int> names { get { return new Dictionary<string, int>{}; } }
		
		public virtual Sprite SetupDebugOverlay()
		{
			if (offset.IsEmpty)
				return null;
			
			BitmapBits bitmap = new BitmapBits((offset.X * 2) + 1, (offset.Y * 2) + 1);
			bitmap.DrawLine(6, 0, 0, offset.X * 2, offset.Y * 2); // LevelData.ColorWhite
			return new Sprite(bitmap, -offset.X, -offset.Y);
		}
		
		public virtual PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[1];
			props[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which side this platform should start from.", null, names,
				(obj) => (obj.PropertyValue == 1) ? 1 : 0,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			return props;
		}
		
		public override void Init(ObjectData data)
		{
			sprites[2] = new Sprite(LevelData.GetSpriteSheet("R3/Objects.gif").GetSection(100, 1, 64, 32), -32, -16);
			sprites[0] = new Sprite(sprites[2],  offset.X,  offset.Y);
			sprites[1] = new Sprite(sprites[2], -offset.X, -offset.Y);
			
			debug = SetupDebugOverlay();
			properties = SetupProperties();
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