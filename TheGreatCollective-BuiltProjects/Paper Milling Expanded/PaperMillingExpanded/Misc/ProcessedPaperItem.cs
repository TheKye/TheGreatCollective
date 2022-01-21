using System;
using System.Collections.Generic;
using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Processed Paper")]
    [Weight(250)]
    [Fuel(300f)]
    [Tag("Fuel", 1)]
    [Currency]
    [Tag("Currency", 1)]
    [Ecopedia("Items", "Products", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Burnable Fuel", 1)]
    public class ProcessedPaperItem : Item
    {
        public override LocString DisplayDescription => Localizer.DoStr("It's processed paper.");
    }

    [RequiresSkill(typeof(PaperMillingSkill), 2)]
    public class ProcessedPaperRecipe : RecipeFamily
    {
        public ProcessedPaperRecipe()
        {
            base.Recipes = new List<Recipe>
            {
                new Recipe("ProcessedPaper", Localizer.DoStr("Processed Paper"), new IngredientElement[]
                {
                    new IngredientElement(typeof(PaperItem), 6f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
                }, new CraftingElement[]
                {
                    new CraftingElement<ProcessedPaperItem>(2f)
                })
            };
            this.ExperienceOnCraft = 1f;
            base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(45f, typeof(PaperMillingSkill));
            base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(ProcessedPaperRecipe), 0.5f, typeof(PaperMillingSkill), new Type[]
            {
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            });
            base.Initialize(Localizer.DoStr("Processed Paper"), typeof(ProcessedPaperRecipe));
            CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
        }
    }
}
