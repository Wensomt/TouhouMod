using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Items.Equipment.Armor.Starry
{
    [AutoloadEquip(EquipType.Head)]	
	public class StarryHood : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starry Hood");
			Tooltip.SetDefault("6% increased magic damage"
			+ "\n2% increase magic crit"
			+ "\n+40 max mana");
	    }
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 26;
			item.value = 160000;
			item.rare = 7;
			item.defense = 10;
		}
        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("StarryRobes") && legs.type == mod.ItemType("StarryLeggings");
		}
        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Striking enemies can create stationary stars that recover life and mana";
            player.GetModPlayer<TouhouPlayer>(mod).starrySet = 10;
		}
        public override void UpdateEquip(Player player)
		{
            player.magicCrit += 2;
            player.magicDamage += 0.06f;
            player.statManaMax2 += 40;
		}
         public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("StarDust"), 5);
			recipe.AddIngredient(ItemID.ChlorophyteBar , 10);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}