using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Buffs
{
	public class CritBonusD : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("Super Critical Bonus");
			Description.SetDefault("You have an extremely enhanced critical bonus");
		}
	}
}