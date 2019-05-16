using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Buffs
{
	public class CritBonusC : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("Greater Critical Bonus");
			Description.SetDefault("You have a greatly enhanced critical bonus");
		}
	}
}