using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using static RSDKv3_4.GameConfig;
using System;

namespace SonicRetro.SonLVL.API
{
	[XmlRoot("game")]
	public class GameXML
	{
		[XmlArrayItem("color")]
		public List<ColorXML> palette = new List<ColorXML>();
		[XmlArrayItem("object")]
		public List<ObjectXML> objects = new List<ObjectXML>();
		[XmlArrayItem("variable")]
		public List<VariableXML> variables = new List<VariableXML>();
		[XmlArrayItem("soundfx")]
		public List<SoundFXXML> sounds = new List<SoundFXXML>();
		[XmlArrayItem("player")]
		public List<PlayerXML> players = new List<PlayerXML>();
		[XmlArrayItem("stage")]
		public List<StageXML> presentationStages = new List<StageXML>();
		[XmlArrayItem("stage")]
		public List<StageXML> regularStages = new List<StageXML>();
		[XmlArrayItem("stage")]
		public List<StageXML> specialStages = new List<StageXML>();
		[XmlArrayItem("stage")]
		public List<StageXML> bonusStages = new List<StageXML>();

		public GameXML() { }

		private static readonly XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameXML));
		public static GameXML Load(string filename)
		{
			GameXML result;
			using (StreamReader textReader = File.OpenText(filename))
				result = (GameXML)xmlSerializer.Deserialize(textReader);
			if (result.palette == null)
				result.palette = new List<ColorXML>();
			if (result.objects == null)
				result.objects = new List<ObjectXML>();
			if (result.variables == null)
				result.variables = new List<VariableXML>();
			if (result.sounds == null)
				result.variables = new List<VariableXML>();
			if (result.players == null)
				result.players = new List<PlayerXML>();
			if (result.presentationStages == null)
				result.presentationStages = new List<StageXML>();
			if (result.regularStages == null)
				result.regularStages = new List<StageXML>();
			if (result.specialStages == null)
				result.specialStages = new List<StageXML>();
			if (result.bonusStages == null)
				result.bonusStages = new List<StageXML>();
			return result;
		}

		public void Save(string filename)
		{
			using (StreamWriter textWriter = File.CreateText(filename))
				xmlSerializer.Serialize(textWriter, this);
		}
	}

	public class ColorXML
	{
		[XmlAttribute]
		public byte bank;
		[XmlAttribute]
		public byte index;
		[XmlAttribute]
		public byte r;
		[XmlAttribute]
		public byte g;
		[XmlAttribute]
		public byte b;

		public ColorXML() { }

		public ColorXML(byte bank, byte index, byte r, byte g, byte b)
		{
			this.bank = bank;
			this.index = index;
			this.r = r;
			this.g = g;
			this.b = b;
		}

		public ColorXML(byte bank, byte index, System.Drawing.Color color)
		{
			this.bank = bank;
			this.index = index;
			r = color.R;
			g = color.G;
			b = color.B;
		}

		public ColorXML(byte bank, byte index, RSDKv3_4.Palette.Color color)
		{
			this.bank = bank;
			this.index = index;
			r = color.R;
			g = color.G;
			b = color.B;
		}

		public static explicit operator System.Drawing.Color(ColorXML item) => System.Drawing.Color.FromArgb(item.r, item.g, item.b);

		public static explicit operator RSDKv3_4.Palette.Color(ColorXML item) => new RSDKv3_4.Palette.Color(item.r, item.g, item.b);
	}

	public class ObjectXML : IEquatable<ObjectXML>
	{
		[XmlAttribute]
		public string name;
		[XmlAttribute]
		public string script;
		[XmlAttribute]
		public bool forceLoad;

		public ObjectXML() { }

		public ObjectXML(string name, string script, bool forceLoad = false)
		{
			this.name = name;
			this.script = script;
			this.forceLoad = forceLoad;
		}

		public ObjectXML Clone() => (ObjectXML)MemberwiseClone();

		public bool Equals(ObjectXML other)
		{
			if (other == null) return false;
			if (ReferenceEquals(this, other)) return true;
			return name.Equals(other.name, StringComparison.Ordinal) && script.Equals(other.script, StringComparison.Ordinal) && forceLoad == other.forceLoad;
		}

		public override bool Equals(object obj) => Equals(obj as ObjectXML);

		public override int GetHashCode() => name.GetHashCode() ^ script.GetHashCode() ^ forceLoad.GetHashCode();

		public static explicit operator ObjectInfo(ObjectXML item) => new ObjectInfo() { name = item.name, script = item.script };

		public static implicit operator ObjectXML(ObjectInfo item) => new ObjectXML(item.name, item.script);
	}

	public class VariableXML : IEquatable<VariableXML>
	{
		[XmlAttribute]
		public string name;
		[XmlAttribute]
		public int value;

		public VariableXML() { }

		public VariableXML(string name, int value = 0)
		{
			this.name = name;
			this.value = value;
		}

		public VariableXML Clone() => (VariableXML)MemberwiseClone();

		public bool Equals(VariableXML other)
		{
			if (other == null) return false;
			if (ReferenceEquals(this, other)) return true;
			return name.Equals(other.name, StringComparison.Ordinal) && value == other.value;
		}

		public override bool Equals(object obj) => Equals(obj as VariableXML);

		public override int GetHashCode() => name.GetHashCode() ^ value.GetHashCode();

		public static implicit operator RSDKv3.GameConfig.GlobalVariable(VariableXML item) => new RSDKv3.GameConfig.GlobalVariable(item.name, item.value);

		public static implicit operator RSDKv4.GameConfig.GlobalVariable(VariableXML item) => new RSDKv4.GameConfig.GlobalVariable(item.name, item.value);

		public static implicit operator VariableXML(GlobalVariable item) => new VariableXML(item.name, item.value);
	}

	public class SoundFXXML : IEquatable<SoundFXXML>
	{
		[XmlAttribute]
		public string name;
		[XmlAttribute]
		public string path;

		public SoundFXXML() { }

		public SoundFXXML(string name, string path)
		{
			this.name = name;
			this.path = path;
		}

		public SoundFXXML Clone() => (SoundFXXML)MemberwiseClone();

		public bool Equals(SoundFXXML other)
		{
			if (other == null) return false;
			if (ReferenceEquals(this, other)) return true;
			return name.Equals(other.name, StringComparison.Ordinal) && path.Equals(other.path, StringComparison.Ordinal);
		}

		public override bool Equals(object obj) => Equals(obj as SoundFXXML);

		public override int GetHashCode() => name.GetHashCode() ^ path.GetHashCode();

		public static implicit operator SoundInfo(SoundFXXML item) => new SoundInfo() { name = item.name, path = item.path };

		public static implicit operator SoundFXXML(SoundInfo item) => new SoundFXXML(item.name, item.path);
	}

	public class PlayerXML
	{
		[XmlAttribute]
		public string name;

		public PlayerXML() { }

		public PlayerXML(string name) => this.name = name;
	}

	public class StageXML : IEquatable<StageXML>
	{
		[XmlAttribute]
		public string name;
		[XmlAttribute]
		public string folder;
		[XmlAttribute]
		public string id;
		[XmlAttribute]
		public bool highlight;

		public StageXML() { }

		public StageXML(string name, string folder, string id, bool highlight)
		{
			this.name = name;
			this.folder = folder;
			this.id = id;
			this.highlight = highlight;
		}

		public StageXML Clone() => (StageXML)MemberwiseClone();

		public bool Equals(StageXML other)
		{
			if (other == null) return false;
			if (ReferenceEquals(this, other)) return true;
			return name.Equals(other.name, StringComparison.Ordinal) && folder.Equals(other.folder, StringComparison.Ordinal) && id.Equals(other.id, StringComparison.Ordinal) && highlight == other.highlight;
		}

		public override bool Equals(object obj) => Equals(obj as StageXML);

		public override int GetHashCode() => name.GetHashCode() ^ folder.GetHashCode() ^ id.GetHashCode() ^ highlight.GetHashCode();

		public static implicit operator StageList.StageInfo(StageXML item) => new StageList.StageInfo() { name = item.name, folder = item.folder, actID = item.id, highlighted = item.highlight };

		public static implicit operator StageXML(StageList.StageInfo item) => new StageXML(item.name, item.folder, item.actID, item.highlighted);
	}
}
