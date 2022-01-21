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
using Eco.EM.Artistry;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Pile Of Books")]
    [Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Housing", 1)]
    [Weight(500)]
    public class PileOfBooksItem : WorldObjectItem<PileOfBooksObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Bookworms, unite!");

        [TooltipChildren]
        public HomeFurnishingValue HousingTooltip => HousingVal;

        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new()
        {
            Category = 0,
            SkillValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnPercent = 0.5f
        };
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    public class PileOfBooksObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Pile Of Books");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;

        public virtual Type RepresentedItemType => typeof(PileOfBooksItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>(null).HomeValue = PileOfBooksItem.HousingVal;
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

    [RequiresSkill(typeof(PaperMillingSkill), 5)]
    public class PileOfBooksRecipe : RecipeFamily
    {
        public PileOfBooksRecipe()
        {
            Recipe item = new Recipe("PileOfBooks", Localizer.DoStr("Pile Of Books"), new IngredientElement[]
            {
                new IngredientElement(typeof(RedDyeItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(BlueDyeItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(GreenDyeItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(YellowDyeItem), 2f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(PaperItem), 60f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
            }, new CraftingElement[]
            {
                new CraftingElement<PileOfBooksItem>(1f)
            });
            base.Recipes = new List<Recipe>
            {
                item
            };
            this.ExperienceOnCraft = 2f;
            base.LaborInCalories = CreateLaborInCaloriesValue(60f, typeof(PaperMillingSkill));
            base.CraftMinutes = CreateCraftTimeValue(typeof(PileOfBooksRecipe), 6f, typeof(PaperMillingSkill), new Type[]
            {
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            });
            base.Initialize(Localizer.DoStr("Pile Of Books"), typeof(PileOfBooksRecipe));
            CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
        }
    }
}
