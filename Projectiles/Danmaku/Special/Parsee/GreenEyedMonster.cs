using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Danmaku.Special.Parsee
{
	public class GreenEyedMonster : ModNPC
	{
		
		private int timer = 0;
		
		public override void SetDefaults()
		{
			//npc.name = "GreenEyedMonster";
			//npc.displayName = "Green Eyed Monster";
			npc.friendly = true;
			npc.aiStyle = -1;
			npc.lifeMax = 10000;
			npc.damage = 0;
			npc.defense = 8;
			npc.knockBackResist = 0f;
			npc.width = 30;
			npc.height = 30;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 30f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.alpha = 255;
		}
		public override void AI()
		{
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			if (player.position.X > npc.position.X)
				npc.position.X += 2.2f;// 1.75f;
            if (player.position.X <= npc.position.X)
				npc.position.X -= 2.2f;// 1.75f;
            if (player.position.Y > npc.position.Y)
				npc.position.Y += 2.2f;// 1.75f;
            if (player.position.Y <= npc.position.Y)
                npc.position.Y -= 2.2f;// 1.75f;

            if (Main.netMode != 1)
            {
                if (Main.rand.Next(4) == 1)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.05f);
                    Projectile.NewProjectile(npc.position.X + (npc.width / 2) - 32 + Main.rand.Next(64), npc.position.Y + (npc.height / 2) - 32 + Main.rand.Next(64), 0f, 0f, mod.ProjectileType("LargeShotGreen"), 16, 0f);
                }
                if (Main.rand.Next(5) == 1)
                {
                    Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.05f);
                    Projectile.NewProjectile(npc.position.X + (npc.width / 2) - 32 + Main.rand.Next(64), npc.position.Y + (npc.height / 2) - 32 + Main.rand.Next(64), 0f, 0f, mod.ProjectileType("SmallShotGreen"), 12, 0f);
                }
            }
			timer++;
			
			if (timer == 600)
			{
				npc.life = 0;
				timer = 0;
				for (int k = 0 ; k < Main.projectile.Length ; k++)
				{
					if (Main.projectile[k].hostile && Main.projectile[k].damage > 1 )
					{
						Main.projectile[k].Kill();
					}
				}
			}
			
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 0f;
			return null;
		}
	}
}