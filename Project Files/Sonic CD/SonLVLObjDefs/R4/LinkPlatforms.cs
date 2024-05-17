using SonicRetro.SonLVL.API;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SCDObjectDefinitions.R4
{
	class LinkPlatforms : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[11];
		private Sprite debug;
		
		public override void Init(ObjectData data)
		{
			BitmapBits sheet = LevelData.GetSpriteSheet("R4/Objects.gif");
			
			Sprite[] frames = new Sprite[2];
			sprites[10] = frames[0] = new Sprite(sheet.GetSection(180, 52, 16, 16), -8, -8); // chain
			frames[1] = new Sprite(sheet.GetSection(147, 69, 64, 16), -32, -8); // platform
			
			int[] angles = {112, 16, 32, 48, 96, 80, 64, 0, 128, 0};
			int[] indexes = {0, 0, 0, 0, 0, 0, 1, 1, 1, 0};
			
			for (int i = 0; i < angles.Length; i++)
			{
				double angle = (angles[i] / 256.0) * Math.PI;
				sprites[i] = new Sprite(frames[indexes[i]], (int)(Math.Cos(angle) * 96), (int)(Math.Sin(angle) * 96));
			}
			
			BitmapBits bitmap = new BitmapBits(193, 193);
			bitmap.DrawCircle(6, 96, 96, 96);
			debug = new Sprite(bitmap, -96, -96);
			
			properties[0] = new PropertySpec("Mode", typeof(int), "Extended",
				"Leader objects should be followed by 8 follwer objects.", null, new Dictionary<string, int>
				{
					{ "Leader (Clockwise)", 1 },
					{ "Leader (Counter-Clockwise)", 2 },
					{ "Follower", 0 }
				},
				(obj) => ((obj.PropertyValue == 1) || (obj.PropertyValue == 2)) ? (int)obj.PropertyValue : 0,
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
				default:
				case 0: return "Follower";
				case 1: return "Leader (Clockwise)";
				case 2: return "Leader (Counter-Clockwise)";
			}
		}

		public override Sprite Image
		{
			get { return sprites[10]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[10];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int index = LevelData.Objects.IndexOf(obj);
			
			// It's kinda funky, this is is like a recursive way of updating the next platform (in case the first platform had its Mode changed, we need to update all the platforms that follow it)
			if (((index + 1) < LevelData.Objects.Count) && (LevelData.Objects[index + 1].Type == obj.Type) && (LevelData.Objects[index + 1].PropertyValue == 0))
				LevelData.Objects[index + 1].UpdateSprite();
			
			int offset = 0;
			while (index > 0)
			{
				if ((LevelData.Objects[index].Type == obj.Type) && ((LevelData.Objects[index].PropertyValue == 1) || (LevelData.Objects[index].PropertyValue == 2)))
					break;
				
				index--;
				offset++;
				
				if (offset == 9)
				{
					return sprites[9];
				}
			}
			
			return sprites[offset];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return debug;
		}
	}
}