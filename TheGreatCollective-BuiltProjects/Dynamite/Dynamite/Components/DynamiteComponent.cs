using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eco.Shared.IoC;
using Eco.Gameplay.Auth;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Simulation.Time;
using Eco.World.Blocks;
using Eco.Gameplay.Systems.Messaging.Chat;
using Eco.Shared.Voxel;

namespace Eco.Gameplay.Components
{
    [Serialized]
    public class DynamiteComponent : WorldObjectComponent
    {
        public override void Tick()
        {
            base.Tick();
            bool isFused = this.IsFused;
            if (isFused)
            {
                bool flag = WorldTime.Seconds >= this.Interaction + 3.0;
                if (flag)
                {
                    base.Parent.SetAnimatedState("boom", true);
                    this.Explode(this.a, this.b, this.c, this.d);
                }
                bool flag2 = WorldTime.Seconds >= this.Interaction + 4.0;
                if (flag2)
                {
                    this.IsFused = false;
                    base.Parent.Destroy();
                }
            }
        }

        public void Credit(int e, int f, int g, int h)
        {
            this.a = e;
            this.b = f;
            this.c = g;
            this.d = h;
        }

        public IEnumerable<WorldPosition3i> SpiralDestruction(int size)
        {
            Vector3i offset = new(0, 0, 0);
            Vector3i delta = new(0, 0, -1);
            int num;
            for (int i = size * size; i > 0; i = num - 1)
            {
                yield return base.Parent.Position3i + offset;
                bool flag = offset.x == offset.z || (offset.x < 0 && offset.x == -offset.z) || (offset.x > 0 && offset.x == 1 - offset.z);
                if (flag)
                {
                    delta = new Vector3i(-delta.z, 0, delta.x);
                }
                offset += delta;
                num = i;
            }
            yield break;
        }

        public void Explode(int taille, int min, int max, int condition)
        {
            this.SpiralDestruction(taille).ToList<WorldPosition3i>().ForEach(delegate (WorldPosition3i x)
            {
                float num = Math.Min((float)taille * 0.5f, (float)taille * 0.6f - WorldPosition3i.Distance(x, this.Parent.Position3i));
                int num2 = 0;
                while ((float)num2 < num)
                {
                    Block block = World.World.GetBlock((Vector3i)x + Vector3i.Up * num2);
                    Vector3i vector3i = (Vector3i)x + Vector3i.Up * num2;
                    bool flag = ((float)num2 >= num - 1f && this.rnd.Next(2) > 0) || (float)num2 < num - 1f;
                    if (flag)
                    {
                        bool flag2 = num2 != 0 && !block.Is<PolluteGround>() && !block.Is<Impenetrable>() && !block.Is<UnderWater>() && ServiceHolder<IAuthManager>.Obj.IsAuthorized(PlotUtil.RawPlotPos(vector3i.XZ), this.user, Shared.Items.AccessType.FullAccess);
                        if (flag2)
                        {
                            this.rubble = this.rnd.Next(min, max);
                            bool flag3 = block.Is<Minable>();
                            if (flag3)
                            {
                                bool flag4 = block is IronOreBlock || block is GoldOreBlock || block is CopperOreBlock || block is CoalBlock;
                                if (flag4)
                                {
                                    int num3 = this.user.Talentset.HasTalent(typeof(MiningLuckyBreakTalent)) ? 4 : -1;
                                    RubbleObject.TrySpawnFromBlock(this.player, block.GetType(), vector3i, num3);
                                }
                                else
                                {
                                    bool flag5 = this.rubble > condition;
                                    if (flag5)
                                    {
                                        int num4 = this.user.Talentset.HasTalent(typeof(MiningLuckyBreakTalent)) ? 4 : -1;
                                        RubbleObject.TrySpawnFromBlock(this.player, block.GetType(), vector3i, num4);
                                    }
                                }
                            }
                            World.World.DeleteBlock(vector3i);
                        }
                        block = World.World.GetBlock((Vector3i)x + Vector3i.Down * num2);
                        vector3i = (Vector3i)x + Vector3i.Down * num2;
                        bool flag6 = !block.Is<PolluteGround>() && !block.Is<Impenetrable>() && !block.Is<UnderWater>() && ServiceHolder<IAuthManager>.Obj.IsAuthorized(PlotUtil.RawPlotPos(vector3i.XZ), this.user, Shared.Items.AccessType.FullAccess);
                        if (flag6)
                        {
                            this.rubble2 = this.rnd.Next(min, max);
                            bool flag7 = block.Is<Minable>();
                            if (flag7)
                            {
                                bool flag8 = block is IronOreBlock || block is GoldOreBlock || block is CopperOreBlock || block is CoalBlock;
                                if (flag8)
                                {
                                    int num5 = this.user.Talentset.HasTalent(typeof(MiningLuckyBreakTalent)) ? 4 : -1;
                                    RubbleObject.TrySpawnFromBlock(this.player, block.GetType(), vector3i, num5);
                                }
                                else
                                {
                                    bool flag9 = this.rubble2 > condition;
                                    if (flag9)
                                    {
                                        int num6 = this.user.Talentset.HasTalent(typeof(MiningLuckyBreakTalent)) ? 4 : -1;
                                        RubbleObject.TrySpawnFromBlock(this.player, block.GetType(), vector3i, num6);
                                    }
                                }
                            }
                            World.World.DeleteBlock(vector3i);
                        }
                    }
                    num2++;
                }
            });
        }

        public InteractResult Interact(InteractionContext context)
        {
            this.user = context.Player.User;
            this.player = context.Player;
            this.IsFused = true;
            this.interactionContext = context;
            this.Interaction = WorldTime.Seconds;
            StringBuilder stringBuilder = new();
            stringBuilder.Append(Localizer.DoStr("The dynamite will blow up in 3 seconds... Goodbye !"));
            context.Player.MsgLocStr(stringBuilder.ToString());
            return InteractResult.Success;
        }

        private User user = null;

        private Player player = null;

        private bool IsFused = false;

        public double Interaction = WorldTime.Seconds;

        private readonly Random rnd = new();

        private InteractionContext interactionContext = null;

        public int rubble = 0;

        public int rubble2 = 0;

        private int a = 0;

        private int b = 0;

        private int c = 0;

        private int d = 0;
    }
}
