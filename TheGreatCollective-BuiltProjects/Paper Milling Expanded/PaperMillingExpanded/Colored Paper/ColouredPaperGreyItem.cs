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

namespace Eco.Mods.TechTree
{
	[Serialized]
	[LocDisplayName("Coloured Paper Grey")]
	[Weight(125)]
	[Fuel(100f)]
	[Tag("Fuel", 1)]
	[Currency]
	[Tag("Currency", 1)]
	[Ecopedia("Items", "Products", true, display: InPageTooltip.DynamicTooltip)]
	[Tag("Burnable Fuel", 1)]
	[Tag("ColouredPaper", 1)]
	public class ColouredPaperGreyItem : Item
	{
        public override LocString DisplayDescription => Localizer.DoStr("It's Grey Coloured Paper .");
    }

	[RequiresSkill(typeof(PaperMillingSkill), 2)]
	public class ColouredPaperGreyRecipe : RecipeFamily
	{
		public ColouredPaperGreyRecipe()
		{
			base.Recipes = new List<Recipe>
			{
				new Recipe("ColouredPaperGrey", Localizer.DoStr("Coloured Paper Grey"), new IngredientElement[]
				{
					new IngredientElement(typeof(PaperItem), 5f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
					new IngredientElement(typeof(GreyDyeItem), 1f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
				}, new CraftingElement[]
				{
					new CraftingElement<ColouredPaperGreyItem>(3f)
				})
			};
			this.ExperienceOnCraft = 1f;
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(35f, typeof(PaperMillingSkill));
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(ColouredPaperGreyRecipe), 0.3f, typeof(PaperMillingSkill), new Type[]
			{
				typeof(PaperMillingFocusedSpeedTalent),
				typeof(PaperMillingParallelSpeedTalent)
			});
			base.Initialize(Localizer.DoStr("Coloured Paper Grey"), typeof(ColouredPaperGreyRecipe));
			CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
		}
	}
}
