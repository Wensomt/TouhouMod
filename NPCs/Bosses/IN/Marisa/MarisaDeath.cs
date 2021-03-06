using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.NPCs.Bosses.IN.Marisa
{

	public class MarisaDeath : ModNPC
	{
		private int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("MarisaDeath");
			//Tooltip.SetDefault("Ordinary Western Magician, Marisa");
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
			npc.friendly = true;
			npc.scale = 0.75f;
			npc.alpha = 120;
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
			if (timer == 100)
			{
				int x = Main.rand.Next(3) + 3;
				if (Main.expertMode)
					x+= Main.rand.Next(4);

				for (int i = 0 ; i < x ; i++)
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PointItemD"));

				x = Main.rand.Next(3) + 3;
				if (Main.expertMode)
					x+= Main.rand.Next(4);

				for (int i = 0 ; i < x ; i++)
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("PowerItemD"));

				if (Main.expertMode && Main.rand.Next(20) == 1)
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("LargePointItemD"));
				if (Main.expertMode && Main.rand.Next(20) == 1)
					Item.NewItem((int)npc.position.X , (int)npc.position.Y , npc.width , npc.height , mod.ItemType("LargePowerItemD"));
			}
			if (timer % 2 == 0)
			{
				Color? color = new Color(250,250,10);
				if (color.HasValue)
				{
				
					int dust = Dust.NewDust(npc.position, npc.width, npc.height, 20, 0f, 0f, 0, color.Value, 4f);
					Main.dust[dust].velocity *= 16f;
					if (timer % 5 == 0 && timer < 111)
						Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ATTACK3"), 0.15f);
				}
			}
			if (timer == 120)
			{
				Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/DEFEATED"), 0.2f);
				npc.life = 0;
				for (int k = 0 ; k < Main.projectile.Length ; k++)
				{
					if (Main.projectile[k].hostile)
					{
						Main.projectile[k].Kill();
					}
				}
			}
			
			return;
		}
		
		
	}
}