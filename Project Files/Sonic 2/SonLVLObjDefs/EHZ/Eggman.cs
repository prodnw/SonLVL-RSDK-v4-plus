using SonicRetro.SonLVL.API;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.EHZ
{
	class Eggman : ObjectDefinition
	{
		private Sprite img;

		public override void Init(ObjectData data)
		{
			Sprite[] sprites = new Sprite[2];
			
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '1')
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("EHZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(70, 155, 60, 20), -28, -28);
				sprites[1] = new Sprite(sheet.GetSection(0, 209, 64, 29), -32, -8);
			}
			else
			{
				BitmapBits sheet = LevelData.GetSpriteSheet("MBZ/Objects.gif");
				sprites[0] = new Sprite(sheet.GetSection(1, 1, 60, 20), -28, -28);
				sprites[1] = new Sprite(sheet.GetSection(415, 170, 64, 29), -32, -8);
			}
			
			List<Sprite> sprs = new List<Sprite>();
			for (int i = 0; i < 2; i++)
			{
				Sprite sprite = new Sprite(sprites[i]);
				sprs.Add(sprite);
			}
			img = new Sprite(sprs.ToArray());
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
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
			try
			{
				List<ObjectEntry> objs = LevelData.Objects.Skip(LevelData.Objects.IndexOf(obj) - 2).TakeWhile(a => LevelData.Objects.IndexOf(a) <= (LevelData.Objects.IndexOf(obj) + 4)).ToList();
				
				short xmin = Math.Min(obj.X, objs.Min(a => a.X));
				short ymin = Math.Min(obj.Y, objs.Min(a => a.Y));
				short xmax = Math.Max(obj.X, objs.Max(a => a.X));
				short ymax = Math.Max(obj.Y, objs.Max(a => a.Y));
				BitmapBits bmp = new BitmapBits(xmax - xmin + 1, ymax - ymin + 1);
				
				for (int i = 0; i < objs.Count - 1; i++)
					bmp.DrawLine(LevelData.ColorWhite, obj.X - xmin, obj.Y - ymin, objs[i + 1].X - xmin, objs[i + 1].Y - ymin);
				
				return new Sprite(bmp, xmin - obj.X, ymin - obj.Y);
			}
			catch
			{
			}
			
			return null;
		}
	}
}