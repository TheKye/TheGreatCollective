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
using Eco.Shared.Math;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Paper Bookshelf")]
    [Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Housing", 1)]
    public class PaperBookshelfItem : WorldObjectItem<PaperBookshelfObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("A place to store knowledge and information; leads to the town hall.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
        [TooltipChildren]
        public HomeFurnishingValue HousingTooltip => HousingVal;

        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new()
        {
            Category = 0,
            SkillValue = 2f,
            TypeForRoomLimit = Localizer.DoStr("Shelves"),
            DiminishingReturnPercent = 0.5f
        };
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
    public class PaperBookshelfObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Paper Bookshelf");

        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        public virtual Type RepresentedItemType => typeof(PaperBookshelfItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>(null).HomeValue = PaperBookshelfItem.HousingVal;
            PublicStorageComponent component = base.GetComponent<PublicStorageComponent>(null);
            component.Initialize(16);
            component.Storage.AddInvRestriction(new NotCarriedRestriction());
        }

        public override void Destroy() => base.Destroy();
    }

    [RequiresSkill(typeof(PaperMillingSkill), 3)]
    public class PaperBookshelfRecipe : RecipeFamily
    {
        public PaperBookshelfRecipe()
        {
            Recipe item = new("PaperBookshelf", Localizer.DoStr("Paper Bookshelf"), new IngredientElement[]
            {
                new IngredientElement(typeof(PaperItem), 50f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement("Lumber", 14f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement("WoodBoard", 16f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
            }, new CraftingElement[]
            {
                new CraftingElement<PaperBookshelfItem>(1f)
            });
            base.Recipes = new List<Recipe>
            {
                item
            };
            this.ExperienceOnCraft = 2f;
            base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(200f, typeof(PaperMillingSkill));
            base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(PaperBookshelfRecipe), 2f, typeof(PaperMillingSkill), new Type[]
            {
                typeof(PaperMillingFocusedSpeedTalent),
                typeof(PaperMillingParallelSpeedTalent)
            });
            base.Initialize(Localizer.DoStr("Paper Bookshelf"), typeof(PaperBookshelfRecipe));
            CraftingComponent.AddRecipe(typeof(PaperMillingWorkBenchObject), this);
        }
    }
}
