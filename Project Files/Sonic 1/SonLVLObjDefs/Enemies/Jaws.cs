using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S1ObjectDefinitions.Enemies
{
	class Jaws : ObjectDefinition
	{
		private Sprite[] sprites = new Sprite[2];
		private PropertySpec[] properties = new PropertySpec[2];

		public override void Init(ObjectData data)
		{
			switch (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1])
			{
				case '4':
				default:
					sprites[0] = new Sprite(LevelData.GetSpriteSheet("LZ/Objects.gif").GetSection(1, 105, 48, 24), -16, -12);
					break;
				case '7':
					sprites[0] = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(1, 264, 48, 24), -16, -12);
					break;
			}
			
			sprites[1] = new Sprite(sprites[0], true, false);
			
			properties[0] = new PropertySpec("Swim Time", typeof(int), "Extended",
				"How long the Jaws will swim in a direction at a time, to be multiplied by 64 frames in-game.", null,
				(obj) => obj.PropertyValue & 0x7f,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x7f) | (byte)((int)value) & 0x7f));

			properties[1] = new PropertySpec("Direction", typeof(int), "Extended",
				"Which way the Jaws will be facing initially.", null, new Dictionary<string, int>
				{
					{ "Left", 0 },
					{ "Right", 0x80 }
				},
				(obj) => obj.PropertyValue & 0x80,
				(obj, value) => obj.PropertyValue = (byte)((obj.PropertyValue & ~0x80) | (byte)((int)value)));
		}

		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new List<byte>()); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}

		public override string SubtypeName(byte subtype)
		{
			return subtype + "";
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			return sprites[0];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			return sprites[(obj.PropertyValue & 0x80) >> 7];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			int dist = (obj.PropertyValue & 0x7f) << 4;
			BitmapBits bitmap = new BitmapBits(dist + 1, 2);
			bitmap.DrawLine(6, 0, 0, dist, 0); // LevelData.ColorWhite
			return new Sprite(bitmap, (obj.PropertyValue > 0x7f) ? 0 : -dist, 0);
		}
	}
}