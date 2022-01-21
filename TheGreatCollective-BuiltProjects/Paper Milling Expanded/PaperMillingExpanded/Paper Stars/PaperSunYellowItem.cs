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
    [LocDisplayName("Paper Sun Yellow")]
    [Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Housing", 1)]
    [Weight(50)]
    [Tag("PaperHeartBroken", 1)]
    public class PaperSunYellowItem : WorldObjectItem<PaperSunYellowObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Shine?");

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
    public class PaperSunYellowObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Paper Sun Yellow");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;

        public virtual Type RepresentedItemType => typeof(PaperSunYellowItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>(null).HomeValue = PaperSunYellowItem.HousingVal;
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

    [RequiresSkill(typeof(PaperMillingSkill), 4)]
    public class PaperSunYellowRecipe : RecipeFamily
    {
        public PaperSunYellowRecipe()
        {
            base.Recipes = new List<Recipe>
            {
                new Recipe("PaperSunYellow", Localizer.DoStr("Paper Sun Yellow"), new IngredientElement[]
                {
                    new IngredientElement(typeof(ColouredPaperYellowItem), 4f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
                }, new CraftingElement[]
                {
                    new CraftingElement<PaperSunYellowItem>(1f)
                })
            };
            this.ExperienceOnCraft = 2f;
            base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(40f, typeof(PaperMillingSkill));
            base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(PaperSunYellowRecipe), 0.5f, typeof(PaperMillingSkill), new Type[]
            {
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            });
            base.Initialize(Localizer.DoStr("Paper Sun Yellow"), typeof(PaperSunYellowRecipe));
            CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
        }
    }
}
