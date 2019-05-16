using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment
{
	public class EnvyBuckler : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Envy Buckler");
			Tooltip.SetDefault("Damage taken is stored as critical hit chance in the next hit(s)"
			+ "\nBonus diminishes by 1% per hit"
			+ "\nMax +50% critical");
	    }	
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 10000;
			item.rare = 3;
			item.accessory = true;
            item.defense = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<TouhouPlayer>(mod).envyShield = true;
            player.meleeCrit += (int)(player.GetModPlayer<TouhouPlayer>(mod).envyShieldBonus/2);
            player.rangedCrit += (int)(player.GetModPlayer<TouhouPlayer>(mod).envyShieldBonus/2);
            player.magicCrit += (int)(player.GetModPlayer<TouhouPlayer>(mod).envyShieldBonus/2);
            player.thrownCrit += (int)(player.GetModPlayer<TouhouPlayer>(mod).envyShieldBonus/2);
			int bonus = player.GetModPlayer<TouhouPlayer>(mod).envyShieldBonus;
			if (bonus > 0 && bonus < 25)
				player.AddBuff(mod.BuffType("CritBonusA"), 3);
			if (bonus >= 25 && bonus < 50)
				player.AddBuff(mod.BuffType("CritBonusB"), 3);
			if (bonus >= 50 && bonus < 75)
				player.AddBuff(mod.BuffType("CritBonusC"), 3);
			if (bonus >= 75 && bonus <= 100)
				player.AddBuff(mod.BuffType("CritBonusD"), 3);
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CobaltShield);
			recipe.AddIngredient(mod.ItemType("EnvyCrystal"), 6);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
}