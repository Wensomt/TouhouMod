using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Items.Equipment.Armor.Envy
{
    [AutoloadEquip(EquipType.Head)] 	
	public class EnvyHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Envy Helm");
			Tooltip.SetDefault("8% increase ranged crit"
			+ "\n6% increased ranged damage");
	    }	
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 26;
			item.value = 8000;
			item.rare = 3;
			item.defense = 5;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("EnvyChestpiece") && legs.type == mod.ItemType("EnvyLeggings");
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+20% extra critical damage";
            player.GetModPlayer<TouhouPlayer>(mod).critBonus = 1.2f;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 8;
            player.rangedDamage *= 1.06f;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("EnvyCrystal"), 4);
			recipe.AddIngredient(ItemID.HellstoneBar , 4);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}