using SonicRetro.SonLVL.API;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace S2ObjectDefinitions.CPZ
{
	class BumpingPlatform : ObjectDefinition
	{
		private PropertySpec[] properties;
		private readonly Sprite[] sprites = new Sprite[2];
		private readonly Sprite[] debug = new Sprite[4];

		public override void Init(ObjectData data)
		{
			Sprite img;
			if (LevelData.StageInfo.folder[LevelData.StageInfo.folder.Length-1] == '2')
			{
				img = new Sprite(LevelData.GetSpriteSheet("CPZ/Objects.gif").GetSection(6, 204, 48, 14), -24, -8);
			}
			else
			{
				img = new Sprite(LevelData.GetSpriteSheet("MBZ/Objects.gif").GetSection(469, 692, 48, 14), -24, -8);
			}
			
			// (copy of BumpingPlatform_offsetTable from the game's script)
			int[] offsetTable = new int[8] {
				-0x680000, 0x000000,
				-0xB00000, 0x400000,
				-0x780000, 0x800000,
				 0x670000, 0x000000 };
			
			
			Sprite[] sprs = new Sprite[2];
			sprs[0] = new Sprite(img);
			sprs[0].Offset(-24, 0);
			sprs[1] = new Sprite(img);
			sprs[1].Offset(25, 0);

			sprites[0] = new Sprite(img);
			sprites[1] = new Sprite(sprs);

			BitmapBits overlay = new BitmapBits(1024, 1);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 512, 1);
			debug[0] = new Sprite(overlay, -128, -2);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 768, 1);
			debug[1] = new Sprite(overlay, -192, -2);
			overlay.DrawLine(LevelData.ColorWhite, 0, 0, 1024, 1);
			debug[2] = new Sprite(overlay, -256, -2);

			properties = new PropertySpec[1];
			properties[0] = new PropertySpec("Movement", typeof(int), "Extended",
				"The way this platform moves.", null, new Dictionary<string, int>
				{
					{ "One platform, 256px", 0 },
					{ "Two platforms, 384px", 1 },
					{ "Two platforms, 512px", 2 },
					{ "One platforms, 384px", 3 }
				},
				(obj) => (obj.PropertyValue & 3),
				(obj, value) => obj.PropertyValue = (byte)((int)value));
		}
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] { 0, 1, 2, 3 }); }
		}

		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0:
					return "One platform, 256px";
				case 1:
					return "Two platforms, 384px";
				case 2:
					return "Two platforms, 512px";
				case 3:
					return "Two platforms, 512px";
				default:
					return "One platform, 256px";
			}
		}

		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override byte DefaultSubtype
		{
			get { return 0; }
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
			return new Sprite(sprites[(obj.PropertyValue == 1 | obj.PropertyValue == 2) ? 1 : 0]);
		}

		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			return new Sprite(debug[(obj.PropertyValue == 1 | obj.PropertyValue == 2) ? obj.PropertyValue : 0]);
		}
	}
}
