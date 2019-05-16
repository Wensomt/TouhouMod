using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.NPCs.Bosses.SA.Parsee
{

	public class ParseeClone : ModNPC
	{
		private int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Parsee Clone");
			//Tooltip.SetDefault("The Jealousy Beneath the Earth's Crust, Parsee");
	    }		
		public override void SetDefaults()
		{
			npc.aiStyle = -1;
			npc.lifeMax = 4000;
			npc.damage = 0;
			npc.defense = 0;
			npc.knockBackResist = 0f;
			npc.width = 83;
			npc.height = 98;
			//Main.npcFrameCount[npc.type] = 5;
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 15f;
			npc.lavaImmune = true;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.scale = 0.95f;

		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.625f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.6f);
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 0f;
			return null;
		}
		public override void AI()
		{
			timer++;
			if (timer % 80 == 0)
			{
				Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK5"), 0.2f);
				Vector2 firstShot = new Vector2(6f, 6f).RotatedByRandom(MathHelper.ToRadians(90));
				float speedX = firstShot.X;
				float speedY = firstShot.Y;
				float numberProjectiles = 8;
				float rotation = MathHelper.ToRadians(360);
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
					Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BigLightShotGreen"), 18 , 0f);
				}
			}
			if (timer == 400)
			{
				npc.life = 0;
			}
			
			return;
		}
		public override void HitEffect(int hitDirection, double damage)
		{
				Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK4"), 0.2f);
				if (damage > 8)
				{
					for (int i = 0; i < 8; i++)
					{
						Vector2 firstShot = new Vector2((float)(Main.rand.NextDouble() * 2 + 1), (float)(Main.rand.NextDouble() * 2 + 1)).RotatedByRandom(MathHelper.ToRadians(360));
						float speedX = firstShot.X;
						float speedY = firstShot.Y;
						Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), speedX, speedY, mod.ProjectileType("BigLightShotGreen"), 18 , 0f);
					}
				}
				else
				{
					Vector2 firstShot = new Vector2((float)(Main.rand.NextDouble() * 2 + 1), (float)(Main.rand.NextDouble() * 2 + 1)).RotatedByRandom(MathHelper.ToRadians(360));
					float speedX = firstShot.X;
					float speedY = firstShot.Y;
					Projectile.NewProjectile(npc.position.X + (npc.width / 2), npc.position.Y + (npc.height / 2), speedX, speedY, mod.ProjectileType("BigLightShotGreen"), 18 , 0f);
				}

		}
		//public override void FindFrame(int frameHeight)
		//{
		//	npc.frame.Y = 4 * frameHeight;
		//}
		
	}
}