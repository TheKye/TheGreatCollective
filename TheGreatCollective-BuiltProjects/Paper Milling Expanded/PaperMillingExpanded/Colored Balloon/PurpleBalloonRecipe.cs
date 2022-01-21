using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Skills;
using Eco.Shared.Localization;

namespace Eco.Mods.TechTree
{
		[RequiresSkill(typeof(PaperMillingSkill), 3)]
	public class PurpleBalloonRecipe : Recipe
	{
				public PurpleBalloonRecipe()
		{
			base.Name = "PurpleBalloon";
			base.DisplayName = Localizer.DoStr("Purple Balloon");
			base.Ingredients = new List<IngredientElement>
			{
				new IngredientElement(typeof(ColouredPaperPurpleItem), 4f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
				new IngredientElement(typeof(PlantFibersItem), 10f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
			};
			base.Items = new List<CraftingElement>
			{
				new CraftingElement<PurpleBalloonItem>(1f)
			};
			CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(BlueBalloonRecipe), this);
		}
	}
}
