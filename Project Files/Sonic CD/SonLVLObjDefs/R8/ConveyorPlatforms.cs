using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R8
{
	class ConveyorPlatforms : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite[] sprites = new Sprite[7];
		
		public override void Init(ObjectData data)
		{
			sprites[0] = new Sprite(LevelData.GetSpriteSheet("Global/Display.gif").GetSection(173, 67, 16, 16), -8, -8);
			sprites[6] = new Sprite(LevelData.GetSpriteSheet("R8/Objects.gif").GetSection(107, 98, 32, 16), -16, -8);
			
			int[] points = {0, 90, 180, 268, 358};
			
			// adapted from the original code
			for (int i = 0; i < points.Length; i++)
			{
				int sx = 0, sy = 0;
				if (points[i] < 160)
				{
					sx = points[i] - 64;
					sy = (points[i] >> 1) - 58;
				}
				else if (points[i] < 224)
				{
					double angle = (((points[i] - 144) << 2)/512) * Math.PI;
					sx = ((int)(Math.Sin(angle) * 512 * 0xC00) >> 16) + 80;
					sy = ((int)(Math.Cos(angle) * 512 * -0xC00) >> 16) + 40;
				}
				else if (points[i] < 384)
				{
					sx = 62 - (points[i] - 224);
					sy = 57 - ((points[i] - 224) >> 1);
				}
				else
				{
					double angle = (((points[i] - 368) << 2)/512) * Math.PI;
					sx = ((int)(Math.Sin(angle) * 512 * -0xC00) >> 16) + 80;
					sy = ((int)(Math.Cos(angle) * 512 * 0xC00) >> 16) + 40;
				}
				
				sprites[i+1] = new Sprite(sprites[6], sx, sy);
			}
			
			properties[0] = new PropertySpec("Mode", typeof(int), "Extended",
				"Master objects should be followed by 5 child platforms.", null, new Dictionary<string, int>
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
			get { return sprites[6]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[(subtype == 0) ? 0 : 6];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			int index = LevelData.Objects.IndexOf(obj);
			
			if (obj.PropertyValue == 0)
			{
				index++;
				for (int i = 0; (i < 5) && (index < LevelData.Objects.Count); i++, index++)
				{
					LevelData.Objects[index].UpdateSprite();
				}
				return sprites[0];
			}
			
			index--;
			int offset = 1;
			while (index > 0)
			{
				if ((LevelData.Objects[index].Type == obj.Type) && (LevelData.Objects[index].PropertyValue == 0))
					break;
				
				index--;
				offset++;
				
				if (offset == 6)
				{
					return sprites[6];
				}
			}
			
			ObjectEntry other = LevelData.Objects[index];
			
			return new Sprite(sprites[offset], other.X - obj.X, other.Y - obj.Y);
		}
	}
}