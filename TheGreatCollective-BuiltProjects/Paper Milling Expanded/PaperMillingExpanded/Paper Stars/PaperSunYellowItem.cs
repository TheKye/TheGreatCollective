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
	[LocDisplayName("Paper Sun Yellow")]
	[Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
	[Tag("Housing", 1)]
	[Weight(50)]
	[Tag("PaperHeartBroken", 1)]
	public class PaperSunYellowItem : WorldObjectItem<PaperSunYellowObject>
	{
						public override LocString DisplayDescription
		{
			get
			{
				return Localizer.DoStr("Shine?");
			}
		}

						[TooltipChildren(new Type[]
		{

		})]
		public HomeFurnishingValue HousingTooltip
		{
			get
			{
				return PaperSunYellowItem.HousingVal;
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
					SkillValue = 0.5f,
					TypeForRoomLimit = Localizer.DoStr("Decoration"),
					DiminishingReturnPercent = 0.2f
				};
			}
		}
	}
}
