using System;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Property;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
		[Serialized]
	[RequireComponent(typeof(PropertyAuthComponent), null)]
	[RequireComponent(typeof(MinimapComponent), null)]
	[RequireComponent(typeof(LinkComponent), null)]
	[RequireComponent(typeof(CraftingComponent), null)]
	[RequireComponent(typeof(HousingComponent), null)]
	[RequireComponent(typeof(SolidGroundComponent), null)]
	[RequireComponent(typeof(PluginModulesComponent), null)]
	[RequireComponent(typeof(RoomRequirementsComponent), null)]
	[RequireRoomContainment]
	[RequireRoomVolume(35)]
	[RequireRoomMaterialTier(1.8f, new Type[]
	{
		typeof(PaperMillingLavishReqTalent),
		typeof(PaperMillingFrugalReqTalent)
	})]
	public class PaperMillingWorkBenchObject : WorldObject, IRepresentsItem
	{
						public override LocString DisplayName
		{
			get
			{
				return Localizer.DoStr("Paper Milling WorkBench");
			}
		}

						public override TableTextureMode TableTexture
		{
			get
			{
				return (TableTextureMode)1;
			}
		}

						public virtual Type RepresentedItemType
		{
			get
			{
				return typeof(PaperMillingWorkBenchItem);
			}
		}

				protected override void Initialize()
		{
			base.GetComponent<MinimapComponent>(null).Initialize(Localizer.DoStr("Crafting"));
			base.GetComponent<HousingComponent>(null).HomeValue = PaperMillingWorkBenchItem.HousingVal;
		}

				public override void Destroy()
		{
			base.Destroy();
		}
	}
}
