using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class FPlatform : R1.Platform
	{
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits overlay = new BitmapBits(2, 62);
			for (int i = 0; i < 62; i += 12)
				overlay.DrawLine(6, 0, i, 0, i + 6); // LevelData.ColorWhite
			return new Sprite(overlay, 0, 0);
		}
		
		public override PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[1];
			props[0] = new PropertySpec("Behaviour", typeof(int), "Extended",
				"How this Platform should act upon player contact.", null, new Dictionary<string, int>
				{
					{ "Fall", 0 },
					{ "Hover", 1 }
				},
				(obj) => (obj.PropertyValue == 0) ? 0 : 1,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			return props;
		}
		
		public override byte DefaultSubtype
		{
			get { return 1; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return (subtype == 0) ? "Fall" : "Hover";
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return (obj.PropertyValue > 0) ? null : base.GetDebugOverlay(obj);
		}
	}
	
	class HPlatform : R1.Platform
	{
		public override Point offset { get { return new Point(48, 0); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Right", 0 }, { "Left", 1 }}; } }
	}
	
	class VPlatform : R1.Platform
	{
		// well technically the object uses Sin instead of Cos so it starts from origin on both types and this isn't *really* true, but
		// let's just keep it this way since it's cleaner
		public override Point offset { get { return new Point(0, 48); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Bottom", 0 }, { "Top", 1 }}; } }
	}
	
	class DPlatform : R1.Platform
	{
		private Sprite[] sprites = new Sprite[5];
		private Sprite[] debug = new Sprite[2];
		
		public override Point offset { get { return new Point(48, -48); } }
		public override Dictionary<string, int> names { get { return new Dictionary<string, int>{{ "Top Right", 0 }, { "Top Left", 3 }, { "Bottom Right", 2 }, { "Bottom Left", 1 }}; } }
		
		// offset has a negative value, so let's just empty this out
		public override Sprite SetupDebugOverlay() { return null; }
		
		public override PropertySpec[] SetupProperties()
		{
			PropertySpec[] props = new PropertySpec[1];
			props[0] = new PropertySpec("Start From", typeof(int), "Extended",
				"Which side this platform should start from.", null, names,
				(obj) => obj.PropertyValue & 3,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			return props;
		}
		
		public override void Init(ObjectData data)
		{
			base.Init(data);
			
			sprites[4] = new Sprite(LevelData.GetSpriteSheet("R1/Objects.gif").GetSection(101, 109, 64, 32), -32, -16);
			sprites[0] = new Sprite(sprites[4],  offset.X,  offset.Y);
			sprites[1] = new Sprite(sprites[4], -offset.X, -offset.Y);
			sprites[2] = new Sprite(sprites[4],  offset.X, -offset.Y);
			sprites[3] = new Sprite(sprites[4], -offset.X,  offset.Y);
			
			BitmapBits bitmap = new BitmapBits(98, 98);
			bitmap.DrawLine(6, 97, 0, 0, 97); // LevelData.ColorWhite
			debug[0] = new Sprite(bitmap, -48, -48);
			debug[1] = new Sprite(debug[0], true, false);
		}
		
		public override string SubtypeName(byte subtype)
		{
			return "Start From " + names.GetKey(subtype & 3);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 3, 2, 1}); }
		}
		
		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[obj.PropertyValue & 3]; // normally i try to replicate how the game would react to invalid subtypes but idc here
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug[(obj.PropertyValue > 1) ? 1 : 0];
		}
	}
	
	class RPlatform : R1.Platform
	{
		// not exactly sure how to distinguish this from moving platforms..
		
		public override Sprite SetupDebugOverlay()
		{
			BitmapBits bitmap = new BitmapBits(2, 99);
			bitmap.DrawLine(6, 0, 0, 0, 98); // LevelData.ColorWhite
			return new Sprite(bitmap, 0, -98);
		}
		
		public override PropertySpec[] SetupProperties()
		{
			return null;
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[0]); }
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
			sprites[2] = new Sprite(LevelData.GetSpriteSheet("R1/Objects.gif").GetSection(101, 109, 64, 32), -32, -16);
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