using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Items.Equipment.Armor.War
{
    [AutoloadEquip(EquipType.Head)]	
	public class WarHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("War Helmet");
			Tooltip.SetDefault("+2 max minions"
			+ "\n8% decreased minion damage"
			+ "\n'Power of the masses'");
	    }        
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 26;
			item.value = 80000;
			item.rare = 5;
			item.defense = 9;
		}
        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("WarChestpiece") && legs.type == mod.ItemType("WarLeggings");
		}
        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Striking enemies can summon a doll to attack for extra damage";
            player.GetModPlayer<TouhouPlayer>(mod).warSet = 10;
		}
        public override void UpdateEquip(Player player)
		{
			player.maxMinions += 2;
            player.minionDamage -= 0.08f;
		}
        public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("DollFragment"), 4);
			recipe.AddIngredient(ItemID.HallowedBar , 6);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}