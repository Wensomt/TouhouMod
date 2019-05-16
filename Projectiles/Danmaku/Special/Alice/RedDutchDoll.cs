using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Special.Alice
{
	public class RedDutchDoll : ModNPC
	{
		
		private int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("RedDutchDoll");
			//Tooltip.SetDefault("Red-Haired Dutch Doll");
		}		
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 200;
			npc.damage = 0;
			npc.defense = 8;
			npc.knockBackResist = 0f;
			npc.width = 50;
			npc.height = 50;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 3f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
            npc.scale = 0.7f;
		}
		public override void AI()
		{
            timer++;
            
            if (timer == 10)
            {
                if (Main.netMode != 1)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
                    Vector2 speed = new Vector2(1.5f, 0f).RotatedByRandom(MathHelper.ToRadians(360));
                    for (int i = 0; i < 6; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, speed.X * ((float)(Main.rand.NextDouble() * 0.2 + 0.90)), speed.Y * ((float)(Main.rand.NextDouble() * 0.2 + 0.90)), mod.ProjectileType("RiceShotRed"), 22, 0f);
                        }
                        speed = speed.RotatedBy(MathHelper.ToRadians(60));

                    }
                }
            }
            if (timer == 120)
                npc.life = 0;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PointItemC"));
        }
    }
}