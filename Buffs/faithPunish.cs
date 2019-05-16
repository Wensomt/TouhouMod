using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Buffs
{
	public class faithPunish : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = false;
			DisplayName.SetDefault("Faithful Punishment");
			Description.SetDefault("The gods are not impressed with your lack of faith");
			Main.debuff[Type] = true;
			
		}
		public override void Update(Player player , ref int buffIndex)
		{
			player.moveSpeed -= 0.60f;
			player.AddBuff(23,1);
			player.AddBuff(24,2);
		}
	}
}