using SonicRetro.SonLVL.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;

namespace SCDObjectDefinitions.R8
{
	class CrushingPlatform : ObjectDefinition
	{
		private PropertySpec[] properties = new PropertySpec[2];
		private Sprite[] sprites = new Sprite[4];
		private Sprite[] debug = new Sprite[2];
		private Dictionary<byte, int> indexes;
		
		public override void Init(ObjectData data)
		{
			Sprite[] frames = new Sprite[2];
			frames[0] = new Sprite(LevelData.GetSpriteSheet("R8/Objects.gif").GetSection(1, 1, 64, 64), -32, -32);
			frames[1] = new Sprite(frames[0], new Sprite(frames[0], 0, -64), new Sprite(frames[0], 0, 64));
			
			indexes = new Dictionary<byte, int>
			{
				{0, 0},
				{72, 1},
				{5, 2},
				{69, 3}
			};
			
			sprites[0] = new Sprite(frames[0], 0, 32);
			sprites[1] = new Sprite(frames[0], 0, 48);
			sprites[2] = new Sprite(frames[1], 0, 32);
			sprites[3] = new Sprite(frames[1], 0, 48);
			
			BitmapBits bitmap = new BitmapBits(2, 65);
			bitmap.DrawLine(6, 0, 0, 0, 64);
			debug[0] = new Sprite(bitmap, 0, -32);
			
			bitmap = new BitmapBits(2, 97);
			bitmap.DrawLine(6, 0, 0, 0, 96);
			debug[1] = new Sprite(bitmap, 0, -48);
			
			// ..yeah, this object's prop val setup kind of sucks
			
			// 0 - Small, (64 px)
			// 72 - Small, (96 px)
			// 5 - Large (64 px)
			// 69 - Large (96 px)
			// (there's also an inverse version of type 0 but it's broken, so..)
			
			// because of how this object's prop vals are set up, all these properties are kind of really messy too, sorry..
			// a lot of objects in the level have "invalid" prop vals (ie one that just has 4), so 
			
			Dictionary<byte, byte> dist = new Dictionary<byte, byte>
			{
				{0, 72},
				{5, 69}
			};
			
			properties[0] = new PropertySpec("Distance", typeof(int), "Extended",
				"How far this block should move.", null, new Dictionary<string, int>
				{
					{ "64 px", 0 },
					{ "96 px", 1 }
				},
				(obj) => (dist.ContainsKey(obj.PropertyValue)) ? 0 : 1,
				(obj, value) => {
						int val = (int)value;
						if (!indexes.ContainsKey(obj.PropertyValue))
							obj.PropertyValue = 72;
						if (val == 0)
						{
							if (!dist.ContainsKey(obj.PropertyValue))
								obj.PropertyValue = dist.GetKey(obj.PropertyValue);
						}
						else
						{
							if (dist.ContainsKey(obj.PropertyValue))
								obj.PropertyValue = dist[obj.PropertyValue];
						}
					}
				);
			
			Dictionary<byte, byte> size = new Dictionary<byte, byte>
			{
				{5, 0},
				{69, 72}
			};
			
			properties[1] = new PropertySpec("Size", typeof(int), "Extended",
				"How large this block should be. Only large platforms can launch the player.", null, new Dictionary<string, int>
				{
					{ "Small", 0 },
					{ "Large", 1 }
				},
				(obj) => (size.ContainsKey(obj.PropertyValue)) ? 1 : 0,
				(obj, value) => {
						int val = (int)value;
						if (!indexes.ContainsKey(obj.PropertyValue))
							obj.PropertyValue = 69;
						if (val == 0)
						{
							if (size.ContainsKey(obj.PropertyValue))
								obj.PropertyValue = size[obj.PropertyValue];
						}
						else
						{
							if (!size.ContainsKey(obj.PropertyValue))
								obj.PropertyValue = size.GetKey(obj.PropertyValue);
						}
					}
				);
		}
		
		public override ReadOnlyCollection<byte> Subtypes
		{
			get { return new ReadOnlyCollection<byte>(new byte[] {0, 72, 5, 69}); }
		}
		
		public override PropertySpec[] CustomProperties
		{
			get { return properties; }
		}
		
		public override string SubtypeName(byte subtype)
		{
			switch (subtype)
			{
				case 0: return "Small, 64 px";
				case 72: return "Small, 96 px";
				default:
				case 5: return "Large, 64 px";
				case 69: return "Large, 96 px";
			}
		}

		public override Sprite Image
		{
			get { return sprites[0]; }
		}

		public override Sprite SubtypeImage(byte subtype)
		{
			if (indexes.ContainsKey(subtype))
				return sprites[indexes[subtype]];
			else
				return sprites[1];
		}

		public override Sprite GetSprite(ObjectEntry obj)
		{
			if (indexes.ContainsKey(obj.PropertyValue))
				return sprites[indexes[obj.PropertyValue]];
			else
				return sprites[1];
		}
		
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			if (indexes.ContainsKey(obj.PropertyValue))
				return debug[indexes[obj.PropertyValue] & 1];
			else
				return debug[1];
		}
	}
}