using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;


namespace TouhouMod
{
	public class DivineDamagePlayer : ModPlayer
	{
		public static DivineDamagePlayer ModPlayer(Player player) {
			return player.GetModPlayer<DivineDamagePlayer>();
		}
		public float divineDamage = 1f;
		public float divineKnockback;
		public int divineCrit;

		public override void ResetEffects() {
			ResetVariables();
		}

		public override void UpdateDead() {
			ResetVariables();
		}

		private void ResetVariables() {
			divineDamage = 1f;
			divineKnockback = 0f;
			divineCrit = 0;
		}
	}
}