using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Buffs
{
	public class ParseePresence : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("Parsee's Presence");
			Description.SetDefault("Parsee's magic takes effect on your movement speed");
			Main.debuff[Type] = true;
		}
		public override void Update(Player player , ref int buffIndex)
		{
			player.moveSpeed -= 0.05f;
		}
	}
}