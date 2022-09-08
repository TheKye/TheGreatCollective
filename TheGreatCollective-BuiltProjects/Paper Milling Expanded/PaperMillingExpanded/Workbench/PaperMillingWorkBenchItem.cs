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
using Eco.Gameplay.Property;
using Eco.Gameplay.Modules;
using Eco.Shared.Math;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Paper Milling Workbench")]
    [Ecopedia("Work Stations", "Craft Tables", true, display: InPageTooltip.DynamicTooltip)]
    [AllowPluginModules(Tags = new string[] { "BasicUpgrade" }, ItemTypes = new Type[] { typeof(PaperMillingUpgradeItem) })]
    public class PaperMillingWorkBenchItem : WorldObjectItem<PaperMillingWorkBenchObject>, IPersistentData
    {
        public override LocString DisplayDescription => Localizer.DoStr("A table for Paper Milling. Size: 2x2x3");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
        [TooltipChildren]
        public HomeFurnishingValue HousingTooltip => HousingVal;

        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new()
        {
            Category = RoomCategory.Industrial,
            TypeForRoomLimit = Localizer.DoStr(""),
        };

        [Serialized]
        [TooltipChildren]
        public object PersistentData { get; set; }
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(35)]
    [RequireRoomMaterialTier(1.8f, new Type[] { typeof(PaperMillingLavishReqTalent), typeof(PaperMillingFrugalReqTalent) })]
    public class PaperMillingWorkBenchObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Paper Milling WorkBench");

        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        public virtual Type RepresentedItemType => typeof(PaperMillingWorkBenchItem);

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

    [RequiresSkill(typeof(PaperMillingSkill), 0)]
    public class PaperMillingWorkBenchRecipe : RecipeFamily
    {
        public PaperMillingWorkBenchRecipe()
        {
            Recipe item = new Recipe("PaperMillingWorkBench", Localizer.DoStr("Paper Milling Workbench"), new IngredientElement[]
            {
                new IngredientElement("Wood", 10f, false),
                new IngredientElement(typeof(PaperItem), 50f, false),
                new IngredientElement("WoodBoard", 40f, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent))
            }, new CraftingElement[]
            {
                new CraftingElement<PaperMillingWorkBenchItem>(1f)
            });
            base.Recipes = new List<Recipe>
            {
                item
            };
            base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(100f);
            base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(10f);
            base.Initialize(Localizer.DoStr("Paper Milling Workbench"), typeof(PaperMillingWorkBenchRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}
