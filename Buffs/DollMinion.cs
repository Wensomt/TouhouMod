using Terraria;
using Terraria.ModLoader;

namespace TouhouMod.Buffs
{
	public class DollMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Doll Fighter");
			Description.SetDefault("The doll will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			TouhouPlayer modPlayer = player.GetModPlayer<TouhouPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("DollMinion")] > 0)
			{
				modPlayer.dollMinion = true;
			}
			if (!modPlayer.dollMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}