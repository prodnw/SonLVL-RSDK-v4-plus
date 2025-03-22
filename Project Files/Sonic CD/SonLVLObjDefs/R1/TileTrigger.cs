using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class TileTriggerH : R1.TileTriggerV
	{
		public override Sprite DrawSprite(byte length)
		{
			if (length == 0)
				return sprite;
			
			List<Sprite> sprites = new List<Sprite>(length);
			int sx = -(length * 8) + 8;
			for (int i = 0; i < length; i++)
				sprites.Add(new Sprite(sprite, sx + (i * 16), 0));
			
			return new Sprite(sprites.ToArray());
		}
	}
	
	class TileTriggerL : R1.TileTriggerV
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(156, 67, 16, 16), -8, -8);
		}
	}
	
	class TileTriggerV : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		public Sprite sprite;
		private Sprite debug;
		
		public virtual Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
		}
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			BitmapBits bitmap = new BitmapBits(256, 256);
			bitmap.DrawRectangle(6, 0, 0, 255, 255);
			debug = new Sprite(bitmap);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How long the Tile Trigger will be.", null,
				(obj) => (int)obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
			
			// Not only should Object[+1] should be a Blank Object, but its prop val is also (sometimes) used by the blank object too.. now how do we even show that in the editor? i'm not too sure..
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {4, 8, 12, 16}); }
		}
		
		public override bool Debug
		{
			get { return true; }
		}
		
		public override byte DefaultSubtype
		{
			get { return 4; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + " Nodes";
		}

		public override Sprite Image
		{
			get { return sprite; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return DrawSprite(subtype);
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return DrawSprite(obj.PropertyValue);
		}
		
		public virtual Sprite DrawSprite(byte length)
		{
			if (length == 0)
				return sprite;
			
			List<Sprite> sprites = new List<Sprite>(length);
			int sy = -(length * 8) + 8;
			for (int i = 0; i < length; i++)
				sprites.Add(new Sprite(sprite, 0, sy + (i * 16)));
			
			return new Sprite(sprites.ToArray());
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			// Let's draw a line to this object's helper, as well as box around the chunks that this object's gonna change
			
			int index = LevelData.Objects.IndexOf(obj) + 1;
			if (index >= LevelData.Objects.Count)
				return null;
			
			ObjectEntry other = LevelData.Objects[index];
			
			int xmin = Math.Min(obj.X, other.X);
			int ymin = Math.Min(obj.Y, other.Y);
			int xmax = Math.Max(obj.X, other.X);
			int ymax = Math.Max(obj.Y, other.Y);
			
			BitmapBits bitmap = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
			bitmap.DrawLine(6, obj.X - xmin, obj.Y - ymin, other.X - xmin, other.Y - ymin);
			
			return new Sprite(new Sprite(debug, (other.X & ~0x7f) - obj.X, (other.Y & ~0x7f) - obj.Y), new Sprite(bitmap, xmin - obj.X, ymin - obj.Y));
		}
	}
}