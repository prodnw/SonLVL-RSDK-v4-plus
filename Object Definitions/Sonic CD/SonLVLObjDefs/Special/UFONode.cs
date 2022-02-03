using SonicRetro.SonLVL.API;

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
			new PropertySpec("Travel Time", typeof(decimal), "Extended", "The time (in seconds) it takes for the UFO to travel from this node to the next.", 1m, o => o.PropertyValue / 30m, (o, v) => o.PropertyValue = (byte)((decimal)v * 30m))
		};

		public override PropertySpec[] CustomProperties { get { return customProperties; } }

		public override byte DefaultSubtype { get { return 30; } }
	}
}