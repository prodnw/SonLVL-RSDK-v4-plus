using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Linq;
using RSDKv3_4;

namespace SonicRetro.SonLVL.API
{
	[TypeConverter(typeof(PositionConverter))]
	[Serializable]
	public class Position
	{
		[NonSerialized]
		private readonly Entry ent;
		private ushort x, y;
		[Description("The horizontal component of the position.")]
		[TypeConverter(typeof(UInt16HexConverter))]
		public ushort X { get { if (ent != null) x = ent.X; return x; } set { x = value; if (ent != null) ent.X = value; } }
		[Description("The vertical component of the position.")]
		[TypeConverter(typeof(UInt16HexConverter))]
		public ushort Y { get { if (ent != null) y = ent.Y; return y; } set { y = value; if (ent != null) ent.Y = value; } }

		public Position() { }

		public Position(Entry item)
		{
			ent = item;
			x = item.X;
			y = item.Y;
		}

		public Position(string data)
		{
			string[] a = data.Split(',');
			X = ushort.Parse(a[0], System.Globalization.NumberStyles.HexNumber);
			Y = ushort.Parse(a[1], System.Globalization.NumberStyles.HexNumber);
		}

		public Position(ushort x, ushort y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return X.ToString("X4") + ", " + Y.ToString("X4");
		}

		public ushort[] ToArray()
		{
			ushort[] result = new ushort[2];
			result[0] = X;
			result[1] = Y;
			return result;
		}

		public ushort this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return X;
					case 1:
						return Y;
					default:
						throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						X = value;
						return;
					case 1:
						Y = value;
						return;
					default:
						throw new IndexOutOfRangeException();
				}
			}
		}
	}

	public class PositionConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(Position))
				return true;
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is Position)
				return ((Position)value).ToString();
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
				return true;
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
				return new Position((string)value);
			return base.ConvertFrom(context, culture, value);
		}
	}

	[Serializable]
	public abstract class Entry : IComparable<Entry>
	{
		[Browsable(false)]
		public virtual ushort X { get; set; }
		[NonSerialized]
		protected Position pos;
		[NonSerialized]
		protected Sprite _sprite;
		[NonSerialized]
		protected Rectangle _bounds;

		[Category("Standard")]
		[Description("The location of the item within the level.")]
		public Position Position
		{
			get
			{
				return pos;
			}
			set
			{
				X = value.X;
				Y = value.Y;
				pos = new Position(this);
			}
		}
		[Browsable(false)]
		public virtual ushort Y { get; set; }

		[Browsable(false)]
		public Sprite Sprite => _sprite;
		[Browsable(false)]
		public Rectangle Bounds => _bounds;

		public abstract void UpdateSprite();

		public void AdjustSpritePosition(int x, int y)
		{
			_bounds.Offset(x, y);
		}

		[ReadOnly(true)]
		[ParenthesizePropertyName(true)]
		[Category("Meta")]
		[Description("The name of the item.")]
		public abstract string Name { get; }

		public void ResetPos() { pos = new Position(this); }

		public virtual Entry Clone()
		{
			Entry result = (Entry)MemberwiseClone();
			result.pos = new Position(result);
			return result;
		}

		int IComparable<Entry>.CompareTo(Entry other)
		{
			int c = X.CompareTo(other.X);
			if (c == 0) c = Y.CompareTo(other.Y);
			return c;
		}
	}

	[Serializable]
	public abstract class ObjectEntry : Entry, ICustomTypeDescriptor
	{
		[Browsable(false)]
		public abstract Scene.Entity Entity { get; }

		[DefaultValue(0)]
		[Description("The ID number of the object.")]
		[Editor(typeof(IDEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[TypeConverter(typeof(ByteHexConverter))]
		public byte ID
		{
			get => Entity.type;
			set => Entity.type = value;
		}

		[DefaultValue(0)]
		[Description("The subtype of the object.")]
		[Editor(typeof(SubTypeEditor), typeof(System.Drawing.Design.UITypeEditor))]
		[TypeConverter(typeof(ByteHexConverter))]
		public byte SubType
		{
			get => Entity.propertyValue;
			set => Entity.propertyValue = value;
		}

		public override ushort X
		{
			get => (ushort)(Entity.xpos >> 16);
			set => Entity.xpos = value << 16;
		}

		public override ushort Y
		{
			get => (ushort)(Entity.ypos >> 16);
			set => Entity.ypos = value << 16;
		}

		public override void UpdateSprite()
		{
			ObjectDefinition def = LevelData.GetObjectDefinition(ID);
			_sprite = def.GetSprite(this);
			_bounds = def.GetBounds(this);
			if (_bounds.IsEmpty)
			{
				_bounds = _sprite.Bounds;
				_bounds.Offset(X, Y);
			}
		}

		public override string Name
		{
			get
			{
				if (ID == 0) return "Blank Object";
				return LevelData.GetObjectDefinition(ID).Name;
			}
		}

		[ReadOnly(true)]
		[ParenthesizePropertyName(true)]
		[Category("Meta")]
		[Description("The script file for the object.")]
		public string Script
		{
			get
			{
				if (ID == 0) return string.Empty;
				return LevelData.GetObjectDefinition(ID).Script;
			}
		}

		public static ObjectEntry Create(Scene.Entity entity)
		{
			switch (entity)
			{
				case RSDKv3.Scene.Entity v3ent:
					return new V3ObjectEntry(v3ent);
				case RSDKv4.Scene.Entity v4ent:
					return new V4ObjectEntry(v4ent);
				default:
					throw new NotImplementedException();
			}
		}

		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		string ICustomTypeDescriptor.GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		string ICustomTypeDescriptor.GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(new Attribute[0]);
		}

		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection result = TypeDescriptor.GetProperties(this, attributes, true);

			ObjectDefinition objdef = LevelData.GetObjectDefinition(ID);
			if (objdef.CustomProperties == null || objdef.CustomProperties.Length == 0) return result;
			List<PropertyDescriptor> props = new List<PropertyDescriptor>(result.Count);
			foreach (PropertyDescriptor item in result)
				props.Add(item);

			foreach (PropertySpec property in objdef.CustomProperties)
			{
				List<Attribute> attrs = new List<Attribute>();

				// Additionally, append the custom attributes associated with the
				// PropertySpec, if any.
				if (property.Attributes != null)
					attrs.AddRange(property.Attributes);

				// Create a new property descriptor for the property item, and add
				// it to the list.
				PropertySpecDescriptor pd = new PropertySpecDescriptor(property,
					property.Name, attrs.ToArray());
				props.Add(pd);
			}

			return new PropertyDescriptorCollection(props.ToArray(), true);
		}

		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}

	[Serializable]
	public class V3ObjectEntry : ObjectEntry
	{
		private readonly RSDKv3.Scene.Entity entity;

		public V3ObjectEntry(RSDKv3.Scene.Entity entity)
		{
			this.entity = entity;
			pos = new Position(this);
		}

		public override Scene.Entity Entity => entity;

		public override Entry Clone()
		{
			return new V3ObjectEntry(entity.Clone());
		}
	}

	[Serializable]
	public class V4ObjectEntry : ObjectEntry
	{
		private readonly RSDKv4.Scene.Entity entity;

		public V4ObjectEntry(RSDKv4.Scene.Entity entity)
		{
			this.entity = entity;
			pos = new Position(this);
		}

		public override Scene.Entity Entity => entity;

		public override Entry Clone()
		{
			return new V4ObjectEntry(entity.Clone());
		}

		[DefaultValue(0)]
		[Description("The object’s state. Can be used any way the object needs.")]
		public int State
		{
			get => entity.state ?? 0;
			set
			{
				if (value == 0)
					entity.state = null;
				else
					entity.state = value;
			}
		}

		[DefaultValue(0)]
		[Description("Determines the flip of the sprites when drawing.")]
		public Tiles128x128.Block.Tile.Directions Direction
		{
			get => entity.direction ?? Tiles128x128.Block.Tile.Directions.FlipNone;
			set
			{
				if (value == 0)
					entity.direction = null;
				else
					entity.direction = value;
			}
		}

		[DefaultValue(0x200)]
		[Description("The object’s scale, generally used with generally used with DrawSpriteFX and FX_ROTATE or FX_ROTOZOOM.\nUses a 9 - bit bitshifted value, so 0x200(512) == 1.0.")]
		[TypeConverter(typeof(Int32HexConverter))]
		public int Scale
		{
			get => entity.scale ?? 0x200;
			set
			{
				if (value == 0x200)
					entity.scale = null;
				else
					entity.scale = value;
			}
		}

		[DefaultValue(0)]
		[Description("The object’s rotation, generally used with DrawSpriteFX and FX_ROTATE or FX_ROTOZOOM(ranges from 0 - 511).")]
		public int Rotation
		{
			get => entity.rotation ?? 0;
			set
			{
				if (value == 0)
					entity.rotation = null;
				else
					entity.rotation = value;
			}
		}

		[DefaultValue(3)]
		[Description("The object’s drawing layer: is 3 by default. Manages what drawList the object is placed in after ObjectMain.")]
		public byte DrawOrder
		{
			get => entity.drawOrder ?? 3;
			set
			{
				if (value == 3)
					entity.drawOrder = null;
				else
					entity.drawOrder = value;
			}
		}

		[DefaultValue(0)]
		[Description("The object’s priority value, determines how the engine handles object activity, by default it’s set to PRIORITY_ACTIVE_BOUNDS.")]
		public RSDKv4.Scene.Entity.Priorities Priority
		{
			get => entity.priority ?? RSDKv4.Scene.Entity.Priorities.ActiveBounds;
			set
			{
				if (value == 0)
					entity.priority = null;
				else
					entity.priority = value;
			}
		}

		[DefaultValue(0)]
		[Description("The object’s transparency from 0 to 255.")]
		public byte Alpha
		{
			get => entity.alpha ?? 0;
			set
			{
				if (value == 0)
					entity.alpha = null;
				else
					entity.alpha = value;
			}
		}

		[DefaultValue(0)]
		[Description("The object’s animation processing speed.")]
		public int AnimationSpeed
		{
			get => entity.animationSpeed ?? 0;
			set
			{
				if (value == 0)
					entity.animationSpeed = null;
				else
					entity.animationSpeed = value;
			}
		}

		[DefaultValue(0)]
		[Description("The object’s frame ID.")]
		public byte Frame
		{
			get => entity.frame ?? 0;
			set
			{
				if (value == 0)
					entity.frame = null;
				else
					entity.frame = value;
			}
		}

		[DefaultValue(0)]
		[Description("Determines the blending mode used with DrawSpriteFX & FX_INK.")]
		public RSDKv4.Scene.Entity.InkEffects InkEffect
		{
			get => entity.inkEffect ?? 0;
			set
			{
				if (value == 0)
					entity.inkEffect = null;
				else
					entity.inkEffect = value;
			}
		}

		[DefaultValue(0)]
		[Description("Integer values used for long-term storage. What they are used for varies on an object-by-object basis.")]
		public int Value0
		{
			get => entity.value0 ?? 0;
			set
			{
				if (value == 0)
					entity.value0 = null;
				else
					entity.value0 = value;
			}
		}

		[DefaultValue(0)]
		[Description("Integer values used for long-term storage. What they are used for varies on an object-by-object basis.")]
		public int Value1
		{
			get => entity.value1 ?? 0;
			set
			{
				if (value == 0)
					entity.value1 = null;
				else
					entity.value1 = value;
			}
		}

		[DefaultValue(0)]
		[Description("Integer values used for long-term storage. What they are used for varies on an object-by-object basis.")]
		public int Value2
		{
			get => entity.value2 ?? 0;
			set
			{
				if (value == 0)
					entity.value2 = null;
				else
					entity.value2 = value;
			}
		}

		[DefaultValue(0)]
		[Description("Integer values used for long-term storage. What they are used for varies on an object-by-object basis.")]
		public int Value3
		{
			get => entity.value3 ?? 0;
			set
			{
				if (value == 0)
					entity.value3 = null;
				else
					entity.value3 = value;
			}
		}
	}

	[Serializable]
	public class TileData : IEquatable<TileData>, ICloneable
	{
		public BitmapBits Tile { get; set; }
		public TileConfig.CollisionMask Mask1 { get; set; }
		public TileConfig.CollisionMask Mask2 { get; set; }

		public TileData(BitmapBits tile, TileConfig.CollisionMask mask1, TileConfig.CollisionMask mask2)
		{
			Tile = tile;
			Mask1 = mask1;
			Mask2 = mask2;
		}

		public bool Equals(TileData other) => Tile.Bits.FastArrayEqual(other.Tile.Bits) && Mask1.Equal(other.Mask1) && Mask2.Equal(other.Mask2);

		public TileData Clone() => new TileData(new BitmapBits(Tile), Mask1.Clone(), Mask2.Clone());

		object ICloneable.Clone() => Clone();

		public void Flip(bool xflip, bool yflip)
		{
			Tile.Flip(xflip, yflip);
			Mask1.Flip(xflip, yflip);
			Mask2.Flip(xflip, yflip);
		}
	}

	public class ScrollData
	{
		public ushort StartPos { get; set; }
		public bool Deform { get; set; }
		public decimal ParallaxFactor { get; set; }
		public decimal ScrollSpeed { get; set; }

		public ScrollData(ushort pos = 0)
		{
			StartPos = pos;
			ParallaxFactor = 1;
		}

		public ScrollData(ushort pos, Backgrounds.ScrollInfo info)
		{
			StartPos = pos;
			Deform = info.deform;
			ParallaxFactor = info.parallaxFactor / 256m;
			ScrollSpeed = info.scrollSpeed / 64m;
		}

		public RSDKv3.Backgrounds.ScrollInfo GetInfoV3() => new RSDKv3.Backgrounds.ScrollInfo() { deform = Deform, parallaxFactor = (short)(ParallaxFactor * 256), scrollSpeed = (byte)(ScrollSpeed * 64) };

		public RSDKv4.Backgrounds.ScrollInfo GetInfoV4() => new RSDKv4.Backgrounds.ScrollInfo() { deform = Deform, parallaxFactor = (short)(ParallaxFactor * 256), scrollSpeed = (byte)(ScrollSpeed * 64) };
	}
}
