using System;
using Eco.Core.Items;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Gameplay.Housing.PropertyValues;
using static Eco.Gameplay.Housing.PropertyValues.HomeFurnishingValue;

namespace Eco.Mods.TechTree
{
		[Serialized]
	[LocDisplayName("Paper Milling Workbench")]
	[Ecopedia("Work Stations", "Craft Tables", true, display: InPageTooltip.DynamicTooltip)]
	[AllowPluginModules(Tags = new string[]
	{
		"BasicUpgrade"
	}, ItemTypes = new Type[]
	{
		typeof(PaperMillingUpgradeItem)
	})]
	public class PaperMillingWorkBenchItem : WorldObjectItem<PaperMillingWorkBenchObject>, IPersistentData
	{
						public override LocString DisplayDescription
		{
			get
			{
				return Localizer.DoStr("A table for Paper Milling. Size: 2x2x3");
			}
		}

						[TooltipChildren(new Type[]
		{

		})]
		public HomeFurnishingValue HousingTooltip
		{
			get
			{
				return PaperMillingWorkBenchItem.HousingVal;
			}
		}

						[TooltipChildren(new Type[]
		{

		})]
		public static HomeFurnishingValue HousingVal
		{
			get
			{
				return new HomeFurnishingValue
				{
					Category = RoomCategory.Industrial,
					TypeForRoomLimit = Localizer.DoStr(""),
				};
			}
		}

								[Serialized]
		[TooltipChildren(new Type[]
		{

		})]
		public object PersistentData { get; set; }
	}
}
