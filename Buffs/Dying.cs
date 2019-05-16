using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Buffs
{
	public class Dying : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Dying");
			Description.SetDefault("Something isn't right...");
		}
		public override void Update(Player player , ref int buffIndex)
		{
			player.AddBuff(mod.BuffType("Dying"), 1);
			player.AddBuff(20 , 2);
			player.AddBuff(23 , 3);
			player.AddBuff(24 , 4);
			player.AddBuff(30 , 5);
			player.AddBuff(31 , 6);
			player.AddBuff(32 , 7);
			player.AddBuff(39 , 8);
			player.AddBuff(47 , 9);
			player.AddBuff(46 , 10);
			player.AddBuff(67 , 11);
			player.AddBuff(68 , 12);
			player.AddBuff(70 , 13);
			player.AddBuff(144 , 14);
			player.AddBuff(156 , 15);
			player.AddBuff(164 , 16);
			
		}
	}
}