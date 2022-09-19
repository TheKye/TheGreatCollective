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
using Eco.Shared.Math;

namespace Eco.Mods.TechTree
{
    [Serialized]
	[LocDisplayName("Blue Balloon")]
	[Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
	[Tag("Housing", 1)]
	[Tag("Balloon", 1)]
	public class BlueBalloonItem : WorldObjectItem<BlueBalloonObject>
	{
		public override LocString DisplayDescription => Localizer.DoStr("For celebrating.");
		public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
		[TooltipChildren]
        public HomeFurnishingValue HousingTooltip => HousingVal;

        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new()
        {
            Category = 0,
            SkillValue = 0.2f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnPercent = 0.1f
        };
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
    public class BlueBalloonObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Blue Balloon");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;
        public virtual Type RepresentedItemType => typeof(BlueBalloonItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>(null).HomeValue = BlueBalloonItem.HousingVal;
        }

        public override void Destroy() => base.Destroy();
    }

	[RequiresSkill(typeof(PaperMillingSkill), 3)]
	public class BlueBalloonRecipe : RecipeFamily
	{
		public BlueBalloonRecipe()
		{
			base.Recipes = new List<Recipe>
			{
				new Recipe("BlueBalloon", Localizer.DoStr("Blue Balloon"), new IngredientElement[]
				{
					new IngredientElement(typeof(ColouredPaperBlueItem), 4f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
					new IngredientElement(typeof(PlantFibersItem), 10f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
				}, new CraftingElement[]
				{
					new CraftingElement<BlueBalloonItem>(1f)
				})
			};
			this.ExperienceOnCraft = 1f;
			base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(75f);
			base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(BlueBalloonRecipe), 0.2f, typeof(PaperMillingSkill), new Type[]
			{
				typeof(PaperMillingFocusedSpeedTalent),
				typeof(PaperMillingParallelSpeedTalent)
			});
			base.Initialize(Localizer.DoStr("Blue Balloon"), typeof(BlueBalloonRecipe));
			CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
		}
	}
}
