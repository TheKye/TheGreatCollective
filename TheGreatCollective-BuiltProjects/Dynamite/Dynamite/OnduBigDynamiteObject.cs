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
using Eco.Shared.Serialization;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent), null)]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent), null)]
    [RequireComponent(typeof(DynamiteComponent), null)]
    public class OnduBigDynamiteObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Big Dynamite");

        public virtual Type RepresentedItemType => typeof(OnduBigDynamiteItem);

        public override InteractResult OnActLeft(InteractionContext context) => InteractResult.NoOp;

        public override InteractResult OnActRight(InteractionContext context)
        {
            bool flag = !this.start;
            InteractResult result;
            if (flag)
            {
                this.start = true;
                base.SetAnimatedState("fumee", true);
                base.GetComponent<DynamiteComponent>().Credit(9, 1, 11, 9);
                result = base.GetComponent<DynamiteComponent>().Interact(context);
            }
            else
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append(Localizer.DoStr("Already actived !"));
                context.Player.MsgLocStr(stringBuilder.ToString());
                result = InteractResult.Success;
            }
            return result;
        }

        public override InteractResult OnActInteract(InteractionContext context) => InteractResult.NoOp;

        protected override void Initialize() => base.Initialize();

        public override void Destroy() => base.Destroy();

        public bool start = false;

        [Serialized]
        [LocDisplayName("Big Dynamite")]
        public class OnduBigDynamiteItem : WorldObjectItem<OnduBigDynamiteObject>
        {
            public override LocString DisplayDescription => Localizer.DoStr("Big dynamite. It has a destruction diameter of 9 blocks and gives gives 10 % stone rubbles.");
        }

        [RequiresSkill(typeof(OilDrillingSkill), 1)]
        public class OnduBigDynamiteRecipe : RecipeFamily
        {
            public OnduBigDynamiteRecipe()
            {
                Recipe item = new("BigDynamite", Localizer.DoStr("Big Dynamite"), new IngredientElement[]
                {
                    new IngredientElement(typeof(PaperItem), 6f, typeof(OilDrillingSkill), typeof(OilDrillingLavishResourcesTalent)),
                    new IngredientElement("Rock", 10f, typeof(OilDrillingSkill), typeof(OilDrillingLavishResourcesTalent)),
                    new IngredientElement(typeof(CoalItem), 14f, typeof(OilDrillingSkill), typeof(OilDrillingLavishResourcesTalent))
                }, new CraftingElement[]
                {
                    new CraftingElement<OnduBigDynamiteObject.OnduBigDynamiteItem>(1f)
                });
                base.Recipes = new List<Recipe>
                {
                    item
                };
                this.ExperienceOnCraft = 1f;
                base.LaborInCalories = RecipeFamily.CreateLaborInCaloriesValue(100f, typeof(OilDrillingSkill));
                base.CraftMinutes = RecipeFamily.CreateCraftTimeValue(typeof(OnduBigDynamiteObject.OnduBigDynamiteRecipe), 1f, typeof(OilDrillingSkill), new Type[]
                {
                    typeof(OilDrillingFocusedSpeedTalent),
                    typeof(OilDrillingParallelSpeedTalent)
                });
                base.Initialize(Localizer.DoStr("Big Dynamite"), typeof(OnduBigDynamiteObject.OnduBigDynamiteRecipe));
                CraftingComponent.AddRecipe(typeof(MachinistTableObject), this);
            }
        }
    }
}
