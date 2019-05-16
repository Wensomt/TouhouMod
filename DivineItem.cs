using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;
using System.Text;

namespace TouhouMod
{

	public abstract class DivineItem : ModItem
	{
		
		public virtual void SafeSetDefaults() {
		}

		public sealed override void SetDefaults() {
			SafeSetDefaults();
			item.melee = false;
			item.ranged = false;
			item.magic = false;
			item.thrown = false;
			item.summon = false;
		}

		public override void GetWeaponDamage(Player player, ref int damage) {
			damage = (int)(damage * DivineDamagePlayer.ModPlayer(player).divineDamage + 5E-06f);
		}

		public override void GetWeaponKnockback(Player player, ref float knockback) {
			knockback = knockback + DivineDamagePlayer.ModPlayer(player).divineKnockback;
		}

		public override void GetWeaponCrit(Player player, ref int crit) {
			crit = crit + DivineDamagePlayer.ModPlayer(player).divineCrit;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips) {
			TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
			if (tt != null) {
				string[] splitText = tt.text.Split(' ');
				string damageValue = splitText.First();
				string damageWord = splitText.Last();
				tt.text = damageValue + " divine " + damageWord;
			}
		}
	}
}