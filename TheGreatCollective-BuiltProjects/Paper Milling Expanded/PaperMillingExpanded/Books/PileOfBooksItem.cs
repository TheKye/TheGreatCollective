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
using Eco.EM.Framework.Resolvers;
using Eco.Shared.Math;

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
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
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
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
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
    public class PileOfBooksRecipe : RecipeFamily, IConfigurableRecipe
    {
        static RecipeDefaultModel Defaults => new()
        {
            ModelType = typeof(PileOfBooksRecipe).Name,
            Assembly = typeof(PileOfBooksRecipe).AssemblyQualifiedName,
            HiddenName = "Pile of Books",
            LocalizableName = Localizer.DoStr("Pile of Books"),
            IngredientList = new()
            {
                new EMIngredient("RedDyeItem", false, 1f),
                new EMIngredient("BlueDyeItem", false, 1f),
                new EMIngredient("GreenDyeItem", false, 1f),
                new EMIngredient("YellowDyeItem", false, 2f),
                new EMIngredient("PaperItem", false, 60f)
            },
            ProductList = new()
            {
                new EMCraftable("PileOfBooksItem"),
            },
            BaseExperienceOnCraft = 2,
            BaseLabor = 60,
            LaborIsStatic = false,
            BaseCraftTime = 6,
            CraftTimeIsStatic = false,
            CraftingStation = "PaperMillingWorkBenchItem",
            RequiredSkillType = typeof(PaperMillingSkill),
            RequiredSkillLevel = 5,
            IngredientImprovementTalents = typeof(PaperMillingLavishResourcesTalent),
            SpeedImprovementTalents = new Type[] { typeof(PaperMillingFocusedSpeedTalent), typeof(PaperMillingParallelSpeedTalent) },
        };

        static PileOfBooksRecipe() { EMRecipeResolver.AddDefaults(Defaults); }

        public PileOfBooksRecipe()
        {
            this.Recipes = EMRecipeResolver.Obj.ResolveRecipe(this);
            this.LaborInCalories = EMRecipeResolver.Obj.ResolveLabor(this);
            this.CraftMinutes = EMRecipeResolver.Obj.ResolveCraftMinutes(this);
            this.ExperienceOnCraft = EMRecipeResolver.Obj.ResolveExperience(this);
            this.Initialize(Defaults.LocalizableName, GetType());
            CraftingComponent.AddRecipe(EMRecipeResolver.Obj.ResolveStation(this), this);
        }
    }
}
