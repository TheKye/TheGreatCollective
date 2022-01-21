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
    [LocDisplayName("Paper Cat")]
    [Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Housing", 1)]
    [Weight(250)]
    public class PaperCatItem : WorldObjectItem<PaperCatObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A black cat.");

        [TooltipChildren]
        public HomeFurnishingValue HousingTooltip => HousingVal;

        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new()
        {
            Category = 0,
            SkillValue = 1.5f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnPercent = 0.2f
        };
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    public class PaperCatObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Paper Cat");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;

        public virtual Type RepresentedItemType => typeof(PaperCatItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>(null).HomeValue = PaperCatItem.HousingVal;
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

    [RequiresSkill(typeof(PaperMillingSkill), 5)]
    public class PaperCatRecipe : RecipeFamily
    {
        public PaperCatRecipe()
        {
            Recipe item = new Recipe("PaperCat", Localizer.DoStr("Paper Cat"), new IngredientElement[]
            {
                new IngredientElement(typeof(ColouredPaperBlackItem), 10f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
            }, new CraftingElement[]
            {
                new CraftingElement<PaperCatItem>(1f)
            });
            base.Recipes = new List<Recipe>
            {
                item
            };
            this.ExperienceOnCraft = 2f;
            base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(40f, typeof(PaperMillingSkill));
            base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(PaperCatRecipe), 3f, typeof(PaperMillingSkill), new Type[]
            {
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            });
            base.Initialize(Localizer.DoStr("Paper Cat"), typeof(PaperCatRecipe));
            CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
        }
    }
}
