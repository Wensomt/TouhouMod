using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment
{
	public class WarCharm : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("War Charm");
			Tooltip.SetDefault("+2 Max Minions"
			+ "\n12% decreased summon damage");
	    }	
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 28;
			item.value = 100000;
			item.rare = 5;
			item.accessory = true;
		}
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.maxMinions += 4;
            player.minionDamage -= 0.18f;
		}
        public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SummonerEmblem);
			recipe.AddIngredient(mod.ItemType("DollFragment"), 6);
            recipe.AddIngredient(ItemID.HallowedBar , 8);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

    }
}