using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Buffs
{
	public class HarukeiBlessing : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Harukei Blessing");
			Description.SetDefault("Greatly increased regen, damage, defense, and movement speed");
		}
		public override void Update(Player player , ref int buffIndex)
		{
            player.lifeRegen = 20;
            player.meleeDamage += 0.20f;
			player.thrownDamage += 0.20f;
			player.rangedDamage += 0.20f;
			player.magicDamage += 0.20f;
			player.minionDamage += 0.20f;
            player.statDefense += 12;
			player.moveSpeed += 0.30f;
		}
	}
}