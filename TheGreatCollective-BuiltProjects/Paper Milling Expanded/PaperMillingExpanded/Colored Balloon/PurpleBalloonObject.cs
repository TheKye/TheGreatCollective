using System;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
		[Serialized]
	[RequireComponent(typeof(PropertyAuthComponent), null)]
	[RequireComponent(typeof(HousingComponent), null)]
	[RequireComponent(typeof(SolidGroundComponent), null)]
	public class PurpleBalloonObject : WorldObject, IRepresentsItem
	{
						public override LocString DisplayName
		{
			get
			{
				return Localizer.DoStr("Purple Balloon");
			}
		}

						public override TableTextureMode TableTexture
		{
			get
			{
				return (TableTextureMode)2;
			}
		}

						public virtual Type RepresentedItemType
		{
			get
			{
				return typeof(PurpleBalloonItem);
			}
		}

				protected override void Initialize()
		{
			base.GetComponent<HousingComponent>(null).HomeValue = PurpleBalloonItem.HousingVal;
		}

				public override void Destroy()
		{
			base.Destroy();
		}
	}
}
