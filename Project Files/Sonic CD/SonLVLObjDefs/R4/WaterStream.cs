using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R4
{
	class WaterStreamD : R4.WaterStream
	{
		public override double angle { get { return 0.5; } }
		public override bool vertical { get { return true; } }
	}
	
	class WaterStreamL : R4.WaterStream
	{
		public override double angle { get { return 1.0; } }
	}
	
	class WaterStreamR : R4.WaterStream
	{
		public override double angle { get { return 0.0; } }
	}
	
	class WaterStreamR2 : R4.WaterStream
	{
		public override double angle { get { return 0.0; } }
	}
	
	class WaterStreamU : R4.WaterStream
	{
		public override double angle { get { return 1.5; } }
		public override bool vertical { get { return true; } }
	}
	
	abstract class WaterStream : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public abstract double angle { get; }
		public virtual bool vertical { get { return false; } }
		
		public override void Init(ObjectData data)
		{
			sprite = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			
			properties[0] = new PropertySpec("Size", typeof(int), "Extended",
				"How large this current's hitbox should be.", null,
				(obj) => obj.PropertyValue,
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		
		private BitmapBits DrawArrow(BitmapBits bitmap, byte index, int x1, int y1, int x2, int y2)
		{
			bitmap.DrawLine(index, x1, y1, x2, y2);
			double angle = Math.Atan2(y1 - y2, x1 - x2);
			
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle + 0.40) * 10), y2 + (int)(Math.Sin(angle + 0.40) * 10));
			bitmap.DrawLine(index, x2, y2, x2 + (int)(Math.Cos(angle - 0.40) * 10), y2 + (int)(Math.Sin(angle - 0.40) * 10));
			return bitmap;
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x10, 0x11, 0x12, 0x13, 0x14}); } // this is kinda iffy..
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
			return (subtype << 4) + " Pixels Wide";
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
			Sprite debug;
			
			int length = (obj.PropertyValue << 4);
			BitmapBits bitmap;
			
			// original plan was to use the Rotate method to make a v stream from an h stream but it turns out that function's just broken (i think) so we gotta do it this way instead
			if (vertical)
			{
				bitmap = new BitmapBits(64, length);
				bitmap.DrawRectangle(6, 0, 0, 63, length-1);
				
				if (length > 16)
				{
					DrawArrow(bitmap, 6, 32, length / 2, 32, (length / 2) + (int)(Math.Sin(angle * Math.PI) * (length / 3)));
				}
				
				debug = new Sprite(bitmap, -32, -length / 2);
			}
			else
			{
				bitmap = new BitmapBits(length, 64);
				bitmap.DrawRectangle(6, 0, 0, length-1, 63);
				
				if (length > 16)
				{
					DrawArrow(bitmap, 6, (length / 2), 32, (length / 2) + (int)(Math.Cos(angle * Math.PI) * (length / 3)), 32);
				}
				
				debug = new Sprite(bitmap, -length / 2, -32);
			}
			
			return debug;
		}
	}
}