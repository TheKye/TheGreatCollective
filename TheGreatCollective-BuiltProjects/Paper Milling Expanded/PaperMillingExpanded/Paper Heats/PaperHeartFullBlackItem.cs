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
    [LocDisplayName("Paper Heart Full Black")]
    [Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Housing", 1)]
    [Weight(50)]
    [Tag("PaperHeartFull", 1)]
    public class PaperHeartFullBlackItem : WorldObjectItem<PaperHeartFullBlackObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Love?");

        [TooltipChildren]
        public HomeFurnishingValue HousingTooltip => HousingVal;

        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new()
        {
            Category = 0,
            SkillValue = 0.5f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnPercent = 0.2f
        };
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    public class PaperHeartFullBlackObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Paper Heart Full Black");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;

        public virtual Type RepresentedItemType => typeof(PaperHeartFullBlackItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>(null).HomeValue = PaperHeartFullBlackItem.HousingVal;
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

    [RequiresSkill(typeof(PaperMillingSkill), 4)]
    public class PaperHeartFullBlackRecipe : Recipe
    {
        public PaperHeartFullBlackRecipe()
        {
            Recipe recipe = new Recipe("PaperHeartFullBlack", Localizer.DoStr("Paper Heart Full Black"), new IngredientElement[]
            {
                new IngredientElement(typeof(ColouredPaperBlackItem), 6f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
            }, new CraftingElement[]
            {
                new CraftingElement<PaperHeartFullBlackItem>(1f)
            });
            CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(PaperHeartFullRedRecipe), recipe);
        }
    }
}
