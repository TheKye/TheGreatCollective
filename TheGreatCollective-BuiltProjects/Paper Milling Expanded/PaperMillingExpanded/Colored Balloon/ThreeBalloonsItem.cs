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

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Three Balloons")]
    [Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Housing", 1)]
    public class ThreeBalloonsItem : WorldObjectItem<ThreeBalloonsObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("For celebrating.");

        [TooltipChildren]
        public HomeFurnishingValue HousingTooltip => HousingVal;

        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new HomeFurnishingValue
        {
            Category = 0,
            SkillValue = 1f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnPercent = 0.2f
        };
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    public class ThreeBalloonsObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Three Balloons");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;

        public virtual Type RepresentedItemType => typeof(ThreeBalloonsItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>(null).HomeValue = ThreeBalloonsItem.HousingVal;
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

    [RequiresSkill(typeof(PaperMillingSkill), 3)]
    public class ThreeBalloonsRecipe : RecipeFamily
    {
        public ThreeBalloonsRecipe()
        {
            Recipe item = new Recipe("ThreeBalloons", Localizer.DoStr("Three Balloons"), new IngredientElement[]
            {
                new IngredientElement("ColouredPaper", 30f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(ProcessedPaperItem), 10f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
            }, new CraftingElement[]
            {
                new CraftingElement<ThreeBalloonsItem>(1f)
            });
            base.Recipes = new List<Recipe>
            {
                item
            };
            this.ExperienceOnCraft = 1f;
            base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(75f);
            base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(BlueBalloonRecipe), 1f, typeof(PaperMillingSkill), new Type[]
            {
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            });
            base.Initialize(Localizer.DoStr("Three Balloons"), typeof(ThreeBalloonsRecipe));
            CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
        }
    }
}
