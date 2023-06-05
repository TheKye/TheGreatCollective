// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.World.Blocks;
using System;
using System.Linq;
using Eco.Gameplay.Skills;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Steam Front Loader")]
    [Weight(20000)]
    [AirPollution(0.5f)]
    [Tag("Excavation")]
    [Ecopedia("Crafted Objects", "Vehicles")]
    public partial class SteamFrontLoaderItem : WorldObjectItem<SteamFrontLoaderObject>
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Small scale bucket loader. Great for flat to low slope excavation."); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 2)]
    public partial class SteamFrontLoaderRecipe : RecipeFamily
    {
        public SteamFrontLoaderRecipe()
        {
            this.Recipes = new List<Recipe>
            {
                new Recipe(
                    "SteamFrontLoader",
                    Localizer.DoStr("Steam Front Loader"),
                    new IngredientElement[]
                    {
                new IngredientElement(typeof(IronPlateItem), 20, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                new IngredientElement(typeof(IronPipeItem), 8, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                new IngredientElement(typeof(ScrewsItem), 50, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                new IngredientElement(typeof(LeatherHideItem), 20, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                new IngredientElement("Lumber", 30, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                new IngredientElement(typeof(PortableSteamEngineItem), 1, true),
                new IngredientElement(typeof(IronWheelItem), 4, true),
                new IngredientElement(typeof(IronAxleItem), 2, true),
                    },
                new CraftingElement<SteamFrontLoaderItem>()
                )
            };
            this.ExperienceOnCraft = 25;
            this.LaborInCalories = CreateLaborInCaloriesValue(1000, typeof(MechanicsSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SteamFrontLoaderRecipe), 10, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Steam Front Loader"), typeof(SteamFrontLoaderRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }



    public class SteamFrontLoaderUtilities
    {
        // Mapping for custom stack sizes in vehicles by vehicle type as key
        // We can have different stack sizes in different vehicles with this
        public static Dictionary<Type, StackLimitTypeRestriction> AdvancedVehicleStackSizeMap = new Dictionary<Type, StackLimitTypeRestriction>();

        static SteamFrontLoaderUtilities() => CreateBlockStackSizeMaps();

        private static void CreateBlockStackSizeMaps()
        {
            var blockItems = Item.AllItems.Where(x => x is BlockItem).Cast<BlockItem>().ToList();

            // SteamFrontLoader
            var SteamFrontLoaderMap = new StackLimitTypeRestriction(true, 30);

            SteamFrontLoaderMap.AddListRestriction(blockItems.GetItemsByBlockAttribute<Diggable>(), 30);
            SteamFrontLoaderMap.AddListRestriction(blockItems.GetItemsByBlockAttribute<Minable>(), 30);


            // SteamFrontLoader
            AdvancedVehicleStackSizeMap.Add(typeof(SteamFrontLoaderObject), SteamFrontLoaderMap);
        }

        public static StackLimitTypeRestriction GetInventoryRestriction(object obj) => AdvancedVehicleStackSizeMap.GetOrDefault(obj.GetType());
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(VehicleToolComponent))]
    [RequireComponent(typeof(CustomTextComponent))]
    public class SteamFrontLoaderObject : PhysicsWorldObject
    {
        protected SteamFrontLoaderObject() { }
        public override LocString DisplayName { get { return Localizer.DoStr("Steam Front Loader"); } }

        static SteamFrontLoaderObject()
        {
            WorldObject.AddOccupancy<SteamFrontLoaderObject>(new List<BlockOccupancy>(0));
        }

        private static string[] fuelTagList = new string[]
        {
            "Burnable Fuel"
        };

        private Player Driver { get { return this.GetComponent<VehicleComponent>().Driver; } }

        protected override void Initialize()
        {
            base.Initialize();

            GetComponent<CustomTextComponent>().Initialize(30);
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTagList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);
            this.GetComponent<AirPollutionComponent>().Initialize(0.5f);
            this.GetComponent<VehicleComponent>().Initialize(12, 1.2f, 1);
            this.GetComponent<VehicleToolComponent>().Initialize(4, 700000000, new DirtItem(),
                100, 200, 0, SteamFrontLoaderUtilities.GetInventoryRestriction(this), toolOnMount: true);
        }
    }
}
