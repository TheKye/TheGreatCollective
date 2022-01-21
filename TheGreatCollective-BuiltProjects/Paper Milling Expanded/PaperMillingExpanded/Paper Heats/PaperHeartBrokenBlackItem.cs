using System;
using System.Collections.Generic;
using Eco.Core.Items;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Components;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Objects;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using static Eco.Gameplay.Housing.PropertyValues.HomeFurnishingValue;

namespace Eco.Mods.TechTree
{
		[Serialized]
	[LocDisplayName("Paper Heart Broken Black")]
	[Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
	[Tag("Housing", 1)]
	[Weight(50)]
	[Tag("PaperHeartBroken", 1)]
	public class PaperHeartBrokenBlackItem : WorldObjectItem<PaperHeartBrokenBlackObject>
	{
						public override LocString DisplayDescription
		{
			get
			{
				return Localizer.DoStr("You'll get through this.");
			}
		}

						[TooltipChildren(new Type[]
		{

		})]
		public HomeFurnishingValue HousingTooltip
		{
			get
			{
				return PaperHeartBrokenBlackItem.HousingVal;
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
