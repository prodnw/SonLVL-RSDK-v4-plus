using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R1
{
	class CLedge1 : R1.CLedge12
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("R1/Objects2.gif").GetSection(1, 1, 80, 80), -40, -40);
		}
	}
	
	class CLedge2 : R1.CLedge12
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("R1/Objects2.gif").GetSection(82, 1, 80, 80), -40, -40);
		}
	}

	abstract class CLedge12 : ObjectDefinition
	{
		private Sprite sprite;
		private Sprite debug;
		
		public abstract Sprite GetFrame();
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			Rectangle bounds = sprite.Bounds;
			BitmapBits overlay = new BitmapBits(bounds.Size);
			overlay.DrawRectangle(6, 0, 0, bounds.Width - 1, bounds.Height - 1); // LevelData.ColorWhite
			debug = new Sprite(overlay, bounds.X, bounds.Y);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return null;
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
			return debug;
		}
	}
}