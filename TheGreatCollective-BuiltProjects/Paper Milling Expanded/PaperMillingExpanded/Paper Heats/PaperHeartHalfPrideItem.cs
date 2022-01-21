﻿using System;
using Eco.Core.Items;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Gameplay.Housing.PropertyValues;
using static Eco.Gameplay.Housing.PropertyValues.HomeFurnishingValue;

namespace Eco.Mods.TechTree
{
		[Serialized]
	[LocDisplayName("Paper Heart Half Pride")]
	[Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
	[Tag("Housing", 1)]
	[Weight(50)]
	[Tag("PaperHeartFull", 1)]
	public class PaperHeartHalfPrideItem : WorldObjectItem<PaperHeartHalfPrideObject>
	{
						public override LocString DisplayDescription
		{
			get
			{
				return Localizer.DoStr("Where's the other half?");
			}
		}

						[TooltipChildren(new Type[]
		{

		})]
		public HomeFurnishingValue HousingTooltip
		{
			get
			{
				return PaperHeartHalfPrideItem.HousingVal;
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
