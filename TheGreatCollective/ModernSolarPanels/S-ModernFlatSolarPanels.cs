// [DoNotLocalize]
using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared.Math;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Gameplay.Housing.PropertyValues;
using static Eco.Gameplay.Housing.PropertyValues.HomeFurnishingValue;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(PowerGeneratorComponent))]
    [RequireComponent(typeof(HousingComponent))]
    public partial class ModernFlatSolarPanelsObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Modern Flat SolarPanels");

        public virtual Type RepresentedItemType => typeof(ModernFlatSolarPanelsItem);

        static ModernFlatSolarPanelsObject()
        {
            WorldObject.AddOccupancy<ModernFlatSolarPanelsObject>(new List<BlockOccupancy>(){
                new BlockOccupancy(new Vector3i(0, 0, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(0, 1, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(0, 2, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(0, 3, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(-1, 0, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(-1, 1, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(-1, 2, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(-1, 3, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(-2, 0, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(-2, 1, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(-2, 2, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(-2, 3, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(1, 0, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(1, 1, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(1, 2, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(1, 3, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(2, 0, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(2, 1, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(2, 2, 0), typeof(BuildingWorldObjectBlock)),
                new BlockOccupancy(new Vector3i(2, 3, 0), typeof(BuildingWorldObjectBlock)),
                });
        }

        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Power"));
            this.GetComponent<PowerGridComponent>().Initialize(30, new ElectricPower());
            this.GetComponent<PowerGeneratorComponent>().Initialize(800);
            this.GetComponent<HousingComponent>().HomeValue = ModernFlatSolarPanelsItem.HousingVal;

        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

    [Serialized]
    [LocDisplayName("Modern Flat SolarPanels")]
    public partial class ModernFlatSolarPanelsItem : WorldObjectItem<ModernFlatSolarPanelsObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Generates electrical power from the sun! It also stores energy to work at night.");

        static ModernFlatSolarPanelsItem() { }

        [TooltipChildren] public static HomeFurnishingValue HousingTooltip => HousingVal;
        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new()
        {
            Category = RoomCategory.Industrial,
            TypeForRoomLimit = Localizer.DoStr("Solar"),
        };

        [Tooltip(8)] private static LocString PowerProductionTooltip => new(string.Format(Localizer.DoStr("Produces: {0}w"), Text.Info(800)));
    }

    [RequiresSkill(typeof(ElectronicsSkill), 0)]
    public partial class ModernFlatSolarPanelsRecipe : RecipeFamily
    {
        public ModernFlatSolarPanelsRecipe()
        {
            this.Recipes = new List<Recipe>
            {
            new Recipe(
                "Modern Flat SolarPanels",
                Localizer.DoStr("Modern Flat SolarPanels"),
                new IngredientElement[]
                {
                new IngredientElement(typeof(SteelBarItem), 50, typeof(ElectronicsSkill), typeof(ElectronicsLavishResourcesTalent)),
                new IngredientElement(typeof(ServoItem),22, typeof(ElectronicsSkill), typeof(ElectronicsLavishResourcesTalent)),
                new IngredientElement(typeof(BasicCircuitItem),20, typeof(ElectronicsSkill), typeof(ElectronicsLavishResourcesTalent)),
                },
                new CraftingElement[]
                {
                    new CraftingElement<ModernFlatSolarPanelsItem>()
                }
            )};
            this.ExperienceOnCraft = 25;
            this.CraftMinutes = CreateCraftTimeValue(typeof(ModernFlatSolarPanelsRecipe), 50, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent), typeof(ElectronicsParallelSpeedTalent));
            this.Initialize(Localizer.DoStr("Modern Flat SolarPanels"), typeof(ModernFlatSolarPanelsRecipe));
            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }
}