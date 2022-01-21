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
    [LocDisplayName("Tissue Rolls")]
    [Ecopedia("Housing Objects", "Washroom", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Housing", 1)]
    [Weight(250)]
    public class TissueRollsItem : WorldObjectItem<TissueRollsObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("For your umm, needs.");

        [TooltipChildren]
        public HomeFurnishingValue HousingTooltip => HousingVal;

        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new()
        {
            Category = RoomCategory.Bathroom,
            SkillValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Bathroom"),
            DiminishingReturnPercent = 0.5f
        };
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(HousingComponent), null)]
    [RequireComponent(typeof(SolidGroundComponent), null)]
    public class TissueRollsObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Tissue Rolls");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;

        public virtual Type RepresentedItemType => typeof(TissueRollsItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>().HomeValue = TissueRollsItem.HousingVal;
        }

        public override void Destroy() => base.Destroy();
    }

    [RequiresSkill(typeof(PaperMillingSkill), 5)]
    public class TissueRollsRecipe : RecipeFamily
    {
        public TissueRollsRecipe()
        {
            Recipe item = new Recipe("TissueRolls", Localizer.DoStr("Tissue Rolls"), new IngredientElement[]
            {
                new IngredientElement(typeof(IronBarItem), 2f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(ProcessedPaperItem), 40f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
            }, new CraftingElement[]
            {
                new CraftingElement<TissueRollsItem>(1f)
            });
            base.Recipes = new List<Recipe>
            {
                item
            };
            this.ExperienceOnCraft = 2f;
            base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(60f, typeof(PaperMillingSkill));
            base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(TissueRollsRecipe), 6f, typeof(PaperMillingSkill), new Type[]
            {
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            });
            base.Initialize(Localizer.DoStr("Tissue Rolls"), typeof(TissueRollsRecipe));
            CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
        }
    }
}
