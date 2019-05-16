using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Buffs
{
	public class MarisaPresence : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("Marisa's Presence");
			Description.SetDefault("Marisa's magic takes effect on your movement speed");
			Main.debuff[Type] = true;
		}
		public override void Update(Player player , ref int buffIndex)
		{
			player.moveSpeed -= 0.05f;
		}
	}
}