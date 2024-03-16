using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.Menu
{
	class SonicAmy : CharacterSprite
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Menu/SonicAmy.gif").GetSection(1, 1, 254, 254), -127, -127);
		}
	}
	
	class DrEggman : CharacterSprite
	{
		public override Sprite GetFrame()
		{
			return new Sprite(LevelData.GetSpriteSheet("Menu/DrEggman.gif").GetSection(1, 1, 254, 254), -127, -127);
		}
	}
	
	class MetalSonic : CharacterSprite
	{
		public override Sprite GetFrame()
		{
			// (they killed this sheet in Origins.. but left all the other help menu sheets there)
			try
			{
				return new Sprite(LevelData.GetSpriteSheet("Menu/MetalSonic.gif").GetSection(1, 1, 126, 254), -63, -127);
			}
			catch
			{
			}
			
			return new Sprite(LevelData.UnknownSprite);
		}
	}
	
	abstract class CharacterSprite : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[1];
		private Sprite sprite;
		
		public abstract Sprite GetFrame();
		
		public override void Init(ObjectData data)
		{
			sprite = GetFrame();
			
			properties[0] = new PropertySpec("Scale", typeof(decimal), "Extended",
				"How shrunken this sprite should be from its normal size.", null,
				(obj) => (obj.PropertyValue << 1) / 512m,
				(obj, value) => obj.PropertyValue = (byte)((decimal)value));
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {255, 80}); }
		}
		
		public override byte DefaultSubtype
		{
			get { return 80; }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			return Math.Round(((subtype << 1) / 512m) * 100) + "% Scale";
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
	}
}