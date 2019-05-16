using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Buffs
{
	public class BlessingCooldown : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Blessed Cooldown");
			Description.SetDefault("You cannot be blessed again");
		}
	}
}