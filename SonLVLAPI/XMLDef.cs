using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace SonicRetro.SonLVL.API.XMLDef
{
	[XmlRoot(Namespace = "http://www.sonicretro.org")]
	public class ObjDef
	{
		[XmlAttribute]
		public string Namespace { get; set; }
		[XmlAttribute]
		public string TypeName { get; set; }
		[XmlAttribute]
		public string Language { get; set; }
		[XmlAttribute]
		public string Name { get; set; }
		[XmlAttribute]
		public string Image { get; set; }
		[XmlAttribute]
		public bool RememberState { get; set; }
		[XmlIgnore]
		public bool RememberStateSpecified { get { return !RememberState; } set { } }
		[XmlIgnore]
		public byte DefaultSubtypeValue { get; set; }
		[XmlAttribute]
		public string DefaultSubtype { get { return DefaultSubtypeValue.ToString("X2"); } set { DefaultSubtypeValue = byte.Parse(value, NumberStyles.HexNumber); } }
		[XmlIgnore]
		public bool DefaultSubtypeSpecified { get { return DefaultSubtypeValue != 0; } set { } }
		[XmlAttribute]
		public bool Debug { get; set; }
		[XmlIgnore]
		public bool DebugSpecified { get { return !Debug; } set { } }
		[XmlAttribute]
		public bool Hidden { get; set; }
		[XmlIgnore]
		public bool HiddenSpecified { get { return !Hidden; } set { } }
		[XmlAttribute]
		public int Depth { get; set; }
		[XmlIgnore]
		public bool DepthSpecified { get; set; }
		[XmlArrayItem(typeof(ImageFromSheet))]
		[XmlArrayItem(typeof(ImageFromAnim))]
		public Image[] Images { get; set; }
		public ImageSet[] ImageSets { get; set; }
		public ImageRefList DefaultImage { get; set; }
		public Subtype[] Subtypes { get; set; }
		public Property[] Properties { get; set; }
		public Enum[] Enums { get; set; }
		public DisplayOption[] Display { get; set; }

		static readonly XmlSerializer xs = new XmlSerializer(typeof(ObjDef));

		public static ObjDef Load(string filename)
		{
			using (XmlTextReader xtr = new XmlTextReader(filename))
				return (ObjDef)xs.Deserialize(xtr);
		}

		public void Save(string filename)
		{
			using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filename))
			using (XmlTextWriter xtr = new XmlTextWriter(sw) { Formatting = Formatting.Indented })
				xs.Serialize(xtr, this);
		}
	}

	public class ImageList
	{
		public Image[] Items { get; set; }
	}

	public abstract class Image
	{
		[XmlAttribute]
		public string id { get; set; }
		[XmlAttribute]
		public bool priority { get; set; }
		public XmlPoint Offset { get; set; }
		[XmlIgnore]
		public bool OffsetSpecified { get { return !Offset.IsEmpty; } set { } }
	}

	public class ImageFromSheet : Image
	{
		[XmlAttribute]
		public string sheet { get; set; }
		[XmlAttribute]
		public int sourcex { get; set; }
		[XmlAttribute]
		public int sourcey { get; set; }
		[XmlAttribute]
		public int width { get; set; }
		[XmlAttribute]
		public int height { get; set; }
	}

	public class ImageFromAnim : Image
	{
		[XmlAttribute]
		public string file { get; set; }
		[XmlAttribute]
		public int anim { get; set; }
		[XmlAttribute]
		public int frame { get; set; }
	}

	public struct XmlPoint
	{
		[XmlAttribute]
		public int X { get; set; }
		[XmlIgnore]
		public bool XSpecified { get { return X != 0; } set { } }
		[XmlAttribute]
		public int Y { get; set; }
		[XmlIgnore]
		public bool YSpecified { get { return Y != 0; } set { } }

		public XmlPoint(int x, int y)
			: this()
		{
			X = x;
			Y = y;
		}

		public bool IsEmpty => X == 0 && Y == 0;

		public System.Drawing.Point ToPoint() => new System.Drawing.Point(X, Y);
	}

	public class ImageSetList
	{
		[XmlElement("ImageSet")]
		public ImageSet[] Items { get; set; }
	}

	public class ImageSet
	{
		[XmlAttribute]
		public string id { get; set; }
		[XmlElement("ImageRef")]
		public ImageRef[] Images { get; set; }
	}

	public class ImageRef
	{
		[XmlAttribute]
		public string image { get; set; }
		public XmlPoint Offset { get; set; }
		[XmlIgnore]
		public bool OffsetSpecified { get { return !Offset.IsEmpty; } set { } }
		[XmlAttribute]
		public FlipType xflip { get; set; }
		[XmlIgnore]
		public bool xflipSpecified { get { return xflip != FlipType.NormalFlip; } set { } }
		[XmlAttribute]
		public FlipType yflip { get; set; }
		[XmlIgnore]
		public bool yflipSpecified { get { return yflip != FlipType.NormalFlip; } set { } }
	}

	public enum FlipType
	{
		NormalFlip,
		ReverseFlip,
		NeverFlip,
		AlwaysFlip
	}

	public class SubtypeList
	{
		[XmlElement("Subtype")]
		public Subtype[] Items { get; set; }
	}

	public class Subtype : ImageRefList
	{
		[XmlIgnore]
		public byte subtype { get; set; }
		[XmlAttribute]
		public string id { get { return subtype.ToString("X2"); } set { subtype = byte.Parse(value, NumberStyles.HexNumber); } }
		[XmlAttribute]
		public string name { get; set; }
		[XmlAttribute]
		public string image { get; set; }
		[XmlAttribute]
		public int depth { get; set; }
		[XmlIgnore]
		public bool depthSpecified { get; set; }
	}

	public class Property
	{
		[XmlAttribute]
		public string name { get; set; }
		[XmlAttribute]
		public string displayname { get; set; }
		[XmlIgnore]
		public bool displaynameSpecified { get { return !string.IsNullOrEmpty(displayname); } set { } }
		[XmlAttribute]
		public string type { get; set; }
		[XmlAttribute]
		public string description { get; set; }
		[XmlIgnore]
		public bool descriptionSpecified { get { return !string.IsNullOrEmpty(description); } set { } }
		[XmlAttribute]
		public string source { get; set; }
		[XmlIgnore]
		public bool sourceSpecified { get { return !string.IsNullOrEmpty(source); } set { } }
		[XmlAttribute]
		public int startbit { get; set; }
		[XmlAttribute]
		public int length { get; set; }
	}

	public class EnumList
	{
		[XmlElement("Enum")]
		public Enum[] Items { get; set; }
	}

	public class Enum
	{
		[XmlAttribute]
		public string name { get; set; }
		[XmlElement("EnumMember")]
		public EnumMember[] Items { get; set; }
	}

	public class EnumMember
	{
		[XmlAttribute]
		public string name { get; set; }
		[XmlAttribute]
		public int value { get; set; }
		[XmlIgnore]
		public bool valueSpecified { get; set; }
	}

	public class Display
	{
		[XmlElement("DisplayOption")]
		public DisplayOption[] DisplayOptions { get; set; }
	}

	public class DisplayOption : ImageRefList
	{
		[XmlAttribute("depth")]
		public int Depth { get; set; }
		[XmlElement("Condition")]
		public Condition[] Conditions { get; set; }
		[XmlElement("Line")]
		public Line[] Lines { get; set; }
	}

	public class Condition
	{
		[XmlAttribute]
		public string property { get; set; }
		[XmlAttribute]
		public string value { get; set; }
	}

	public class ImageSetRef
	{
		[XmlAttribute]
		public string set { get; set; }
	}

	public class Line
	{
		[XmlAttribute]
		public byte color { get; set; }
		[XmlAttribute]
		public int x1 { get; set; }
		[XmlAttribute]
		public int y1 { get; set; }
		[XmlAttribute]
		public int x2 { get; set; }
		[XmlAttribute]
		public int y2 { get; set; }
	}

	public class ImageRefList
	{
		[XmlElement(typeof(ImageSetRef))]
		[XmlElement(typeof(ImageRef))]
		public object[] Images { get; set; }
	}
}
