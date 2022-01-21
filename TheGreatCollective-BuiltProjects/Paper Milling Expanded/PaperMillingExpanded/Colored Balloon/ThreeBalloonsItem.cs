using System;
using Eco.Core.Items;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Items;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
		[Serialized]
	[LocDisplayName("Three Balloons")]
	[Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
	[Tag("Housing", 1)]
	public class ThreeBalloonsItem : WorldObjectItem<ThreeBalloonsObject>
	{
						public override LocString DisplayDescription
		{
			get
			{
				return Localizer.DoStr("For celebrating.");
			}
		}

						[TooltipChildren(new Type[]
		{

		})]
		public HomeFurnishingValue HousingTooltip
		{
			get
			{
				return ThreeBalloonsItem.HousingVal;
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
					Category = 0,
					SkillValue = 1f,
					TypeForRoomLimit = Localizer.DoStr("Decoration"),
					DiminishingReturnPercent = 0.2f
				};
			}
		}
	}
}
