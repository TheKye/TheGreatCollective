using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
    [RequiresSkill(typeof(PaperMillingSkill), 1)]
    public class BulkPaperRecipe : RecipeFamily
    {
        public BulkPaperRecipe()
        {
            base.Recipes = new List<Recipe>
            {
                new Recipe("BulkPaper", Localizer.DoStr("Bulk Paper"), 
                new IngredientElement[]
                {
                    new IngredientElement(typeof(CelluloseFiberItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
                }, new CraftingElement[]
                {
                    new CraftingElement<PaperItem>(2f)
                })
            };
            this.ExperienceOnCraft = 1f;
            base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(35f, typeof(PaperMillingSkill));
            base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(BulkPaperRecipe), 0.1f, typeof(PaperMillingSkill), new Type[]
            {
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            });
            base.Initialize(Localizer.DoStr("Bulk Paper"), typeof(BulkPaperRecipe));
            CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
        }
    }
}
