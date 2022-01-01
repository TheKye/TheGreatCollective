using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Pipes.LiquidComponents;
using Eco.Gameplay.Pipes.Gases;
using Eco.Gameplay.Property;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Math;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Mods.TechTree;
using Eco.Core.Plugins.Interfaces;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Core.Items;
using Eco.Gameplay.Modules;

namespace NyElectrics
{
    [Serialized]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(ChimneyComponent))]
    [RequireComponent(typeof(LiquidProducerComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]
    [RequireRoomMaterialTier(1.8f, typeof(MasonryLavishReqTalent), typeof(MasonryFrugalReqTalent))]
    public partial class NyElectricCementKilnObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Electric Cement Kiln");

        public virtual Type RepresentedItemType => typeof(NyElectricCementKilnItem);

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Crafting"));
            this.GetComponent<HousingComponent>().HomeValue = NyElectricCementKilnItem.HousingVal;
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.GetComponent<LiquidProducerComponent>().Setup(typeof(SmogItem), 1, this.NamedOccupancyOffset("ChimneyOut"));

            this.GetComponent<PowerConsumptionComponent>().Initialize(3000f);
        }

        public override void Destroy() => base.Destroy();

        static NyElectricCementKilnObject()
        {
            WorldObject.AddOccupancy<NyElectricCementKilnObject>(new List<BlockOccupancy>() 
            { 
                new BlockOccupancy(new Vector3i(0, 1, 0), typeof(PipeSlotBlock), new Quaternion(-0.7071068f, 0f, 0f, 0.7071068f), "ChimneyOut"), 
                new BlockOccupancy(new Vector3i(0, 0, 0)), 
                new BlockOccupancy(new Vector3i(1, 0, 0)), 
                new BlockOccupancy(new Vector3i(1, 1, 0)), 
                new BlockOccupancy(new Vector3i(2, 0, 0)), 
                new BlockOccupancy(new Vector3i(2, 1, 0)), 
                new BlockOccupancy(new Vector3i(3, 0, 0)), 
                new BlockOccupancy(new Vector3i(3, 1, 0)), 
            });

            /* Linked Recipes */
            CraftingComponent.AddRecipe(typeof(NyElectricCementKilnObject), RecipeFamily.AllRecipes.FirstOfTypeOrDefault<ReinforcedConcreteRecipe>());
            CraftingComponent.AddRecipe(typeof(NyElectricCementKilnObject), RecipeFamily.AllRecipes.FirstOfTypeOrDefault<CementRecipe>());
            CraftingComponent.AddRecipe(typeof(NyElectricCementKilnObject), RecipeFamily.AllRecipes.FirstOfTypeOrDefault<MasonryAdvancedUpgradeRecipe>());
        }
    }

    [Serialized]
    [LocDisplayName("Electric Cement Kiln")]
    [Ecopedia("Work Stations", "Craft Tables", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]
    [LiquidProducer(typeof(SmogItem), 1)]
    [AllowPluginModules(Tags = new[] { "AdvancedUpgrade" }, ItemTypes = new[] { typeof(MasonryAdvancedUpgradeItem) })]
    public partial class NyElectricCementKilnItem : WorldObjectItem<NyElectricCementKilnObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A superior replacement for the cement kiln that use electric power.");

        static NyElectricCementKilnItem() { }

        [TooltipChildren] public HomeFurnishingValue HousingTooltip => HousingVal;
        public static readonly HomeFurnishingValue HousingVal = new()
        {
            Category = HomeFurnishingValue.RoomCategory.Industrial,
            TypeForRoomLimit = Localizer.DoStr(""),
        };
    }

    [RequiresSkill(typeof(IndustrySkill), 0)]
    public partial class NyElectricCementKilnRecipe : RecipeFamily
    {
        public NyElectricCementKilnRecipe()
        {
            var product = new Recipe(
                "Electric Cement Kiln",
                Localizer.DoStr("Electric Cement Kiln"),
                new IngredientElement[]
                {
                    new IngredientElement(typeof(BasicCircuitItem), 1, typeof(IndustrySkill)),
                    new IngredientElement(typeof(BrickItem), 80, typeof(IndustrySkill)),
                    new IngredientElement(typeof(SteelBarItem), 50, typeof(IndustrySkill)),
                },
                new CraftingElement<NyElectricCementKilnItem>()
            );
            this.Recipes = new List<Recipe> { product };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(1000, typeof(IndustrySkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(NyElectricCementKilnRecipe), 10, typeof(IndustrySkill));
            this.Initialize(Localizer.DoStr("Electric Cement Kiln"), typeof(NyElectricCementKilnRecipe));
            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }
}