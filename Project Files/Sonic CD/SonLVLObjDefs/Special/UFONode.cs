using SonicRetro.SonLVL.API;
using System;

namespace SCDObjectDefinitions.Special
{
	public class UFONode : DefaultObjectDefinition
	{
		public override Sprite GetDebugOverlay(ObjectEntry obj)
		{
			for (int i = LevelData.Objects.IndexOf(obj); i >= 0; --i)
				switch (LevelData.Objects[i].Name)
				{
					case "UFO":
						LevelData.Objects[i].UpdateDebugOverlay();
						return null;
					case "UFO Node":
						break;
					default:
						return null;
				}
			return null;
		}

		static readonly PropertySpec[] customProperties = new PropertySpec[]
		{
			new PropertySpec("Travel Time", typeof(decimal), "Extended", "The time (in seconds) it takes for the UFO to travel from this node to the next.", 1m, GetTravelTime, SetTravelTime),
			new PropertySpec("Speed", typeof(double), "Extended", "The speed at which the UFO moves after this node is reached, in pixels per second.", 30m, GetSpeed, SetSpeed)
		};

		public override PropertySpec[] CustomProperties { get { return customProperties; } }

		public override byte DefaultSubtype { get { return 30; } }

		static object GetTravelTime(ObjectEntry obj)
		{
			return obj.PropertyValue / 30m;
		}

		static void SetTravelTime(ObjectEntry obj, object value)
		{
			decimal dval = (decimal)value;
			obj.PropertyValue = (byte)Math.Max(Math.Min(dval * 30m, 255), 0);
		}

		static ObjectEntry GetNextNode(ObjectEntry obj)
		{
			int ind = LevelData.Objects.IndexOf(obj);
			ObjectEntry next = null;
			if (ind == LevelData.Objects.Count - 1 || LevelData.Objects[ind + 1].Name != "UFO Node")
			{
				for (ind--; ind >= 0; --ind)
					if (LevelData.Objects[ind].Name == "UFO Node")
						next = LevelData.Objects[ind];
					else
						break;
			}
			else
				next = LevelData.Objects[ind + 1];
			return next;
		}

		static object GetSpeed(ObjectEntry obj)
		{
			double time = obj.PropertyValue / 30d;
			if (time == 0) return 0d;
			ObjectEntry next = GetNextNode(obj);
			if (next == null) return 0d;
			return Math.Sqrt(Math.Pow(obj.X - next.X, 2) + Math.Pow(obj.Y - next.Y, 2)) / time;
		}

		static void SetSpeed(ObjectEntry obj, object value)
		{
			double dval = (double)value;
			if (dval == 0) return;
			ObjectEntry next = GetNextNode(obj);
			if (next == null) return;
			obj.PropertyValue = (byte)Math.Max(Math.Min(Math.Sqrt(Math.Pow(obj.X - next.X, 2) + Math.Pow(obj.Y - next.Y, 2)) / dval * 30d, 255), 0);
		}
	}
}