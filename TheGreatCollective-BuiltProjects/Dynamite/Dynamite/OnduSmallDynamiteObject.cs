using System;
using System.Collections.Generic;
using System.Text;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireComponent(typeof(DynamiteComponent), null)]
    public class OnduSmallDynamiteObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Small Dynamite");

        public virtual Type RepresentedItemType => typeof(OnduSmallDynamiteObject.OnduSmallDynamiteItem);

        public override InteractResult OnActLeft(InteractionContext context) => InteractResult.NoOp;

        public override InteractResult OnActRight(InteractionContext context)
        {
            bool flag = !this.start;
            InteractResult result;
            if (flag)
            {
                this.start = true;
                base.SetAnimatedState("fumee", true);
                base.GetComponent<DynamiteComponent>(null).Credit(6, 1, 3, 0);
                result = base.GetComponent<DynamiteComponent>(null).Interact(context);
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(Localizer.DoStr("Already actived !"));
                context.Player.ErrorLocStr(stringBuilder.ToString());
                result = InteractResult.Success;
            }
            return result;
        }

        public override InteractResult OnActInteract(InteractionContext context) => InteractResult.NoOp;

        protected override void Initialize() => base.Initialize();

        public override void Destroy() => base.Destroy();

        public bool start = false;

        [Serialized]
        [LocDisplayName("Small Dynamite")]
        public class OnduSmallDynamiteItem : WorldObjectItem<OnduSmallDynamiteObject>
        {
            public override LocString DisplayDescription => Localizer.DoStr("Small dynamite. It has a destruction diameter of 6 blocks and gives 100% rubbles.");
            public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
        }

        [RequiresSkill(typeof(MechanicsSkill), 1)]
        public class OnduSmallDynamiteRecipe : RecipeFamily
        {
            public OnduSmallDynamiteRecipe()
            {
                Recipe item = new("SmallDynamite", Localizer.DoStr("Small Dynamite"), new IngredientElement[]
                {
                    new IngredientElement(typeof(PaperItem), 6f, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                    new IngredientElement("Rock", 10f, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                    new IngredientElement(typeof(CoalItem), 10f, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent))
                }, new CraftingElement[]
                {
                    new CraftingElement<OnduSmallDynamiteObject.OnduSmallDynamiteItem>(1f)
                });
                base.Recipes = new List<Recipe>
                {
                    item
                };
                this.ExperienceOnCraft = 1f;
                base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(100f, typeof(MechanicsSkill));
                base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(OnduSmallDynamiteObject.OnduSmallDynamiteRecipe), 1f, typeof(MechanicsSkill), new Type[]
                {
                    typeof(MechanicsFocusedSpeedTalent),
                    typeof(MechanicsParallelSpeedTalent)
                });
                base.Initialize(Localizer.DoStr("Small Dynamite"), typeof(OnduSmallDynamiteObject.OnduSmallDynamiteRecipe));
                CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
            }
        }
    }
}
