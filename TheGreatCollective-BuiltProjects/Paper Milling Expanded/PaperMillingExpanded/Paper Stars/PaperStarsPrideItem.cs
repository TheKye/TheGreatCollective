using System;
using Eco.Core.Items;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Components.Auth;
using Eco.Shared.Items;
using Eco.Gameplay.Skills;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using static Eco.Gameplay.Housing.PropertyValues.HomeFurnishingValue;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Paper Stars Pride")]
    [Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Housing", 1)]
    [Weight(50)]
    [Tag("PaperHeartBroken", 1)]
    public class PaperStarsPrideItem : WorldObjectItem<PaperStarsPrideObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("You can count these.");

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
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(HousingComponent), null)]
    public class PaperStarsPrideObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Paper Stars Pride");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;

        public virtual Type RepresentedItemType => typeof(PaperStarsPrideItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>(null).HomeValue = PaperStarsPrideItem.HousingVal;
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

    [RequiresSkill(typeof(PaperMillingSkill), 4)]
    public class PaperStarsPrideRecipe : Recipe
    {
        public PaperStarsPrideRecipe()
        {
            base.Name = "PaperStarsPride";
            base.DisplayName = Localizer.DoStr("Paper Stars Pride");
            base.Ingredients = new List<IngredientElement>
            {
                new IngredientElement(typeof(ColouredPaperGreenItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(ColouredPaperYellowItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(ColouredPaperRedItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(ColouredPaperPurpleItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(ColouredPaperBlueItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
            };
            base.Items = new List<CraftingElement>
            {
                new CraftingElement<PaperStarsPrideItem>(1f)
            };
            CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(PaperStarsYellowRecipe), this);
        }
    }
}
