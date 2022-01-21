using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(PaperMillingSkill), 7)]
    public class PaperMillingWorkBenchUpgradeRecipe : RecipeFamily
    {
        public PaperMillingWorkBenchUpgradeRecipe()
        {
            base.Recipes = new List<Recipe>
            {
                new Recipe("PaperMillingWorkBenchUpgrade", Localizer.DoStr("Paper Milling Basic Upgrade"), new IngredientElement[]
                {
                    new IngredientElement(typeof(BasicUpgradeLvl4Item), 1f, true)
                }, new CraftingElement[]
                {
                    new CraftingElement<PaperMillingUpgradeItem>(1f)
                })
            };
            this.ExperienceOnCraft = 4f;
            base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(10000f, typeof(PaperMillingSkill));
            base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(PaperMillingWorkBenchUpgradeRecipe), 15f, typeof(PaperMillingSkill), new Type[]
            {
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            });
            base.Initialize(Localizer.DoStr("Paper Milling Basic Upgrade"), typeof(PaperMillingWorkBenchUpgradeRecipe));
            CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
        }
    }
}
