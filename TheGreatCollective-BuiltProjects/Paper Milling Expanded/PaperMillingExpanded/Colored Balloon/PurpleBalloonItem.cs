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
	[LocDisplayName("Purple Balloon")]
	[Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
	[Tag("Housing", 1)]
	[Tag("Balloon", 1)]
	public class PurpleBalloonItem : WorldObjectItem<PurpleBalloonObject>
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
				return PurpleBalloonItem.HousingVal;
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
					SkillValue = 0.2f,
					TypeForRoomLimit = Localizer.DoStr("Decoration"),
					DiminishingReturnPercent = 0.1f
				};
			}
		}
	}
}
