using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Consumables
{
	public class RumiaFlag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rumia's Flag");
			Tooltip.SetDefault("Summons the Yokai of the Dusk, Rumia");
	    }
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 19;
			item.maxStack  = 30;
			item.value = 2500;
			item.rare = 2;
			item.useAnimation = 30;
			item.useTime = 30;
			item.useStyle = 4;
			item.consumable = true;
		}
		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime && !NPC.AnyNPCs(mod.NPCType("Rumia"));
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Rumia"));
			return true;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(9);
			recipe.AddIngredient(ItemID.Silk , 3);
			recipe.AddIngredient(57 , 3);
			recipe.AddIngredient(75);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
			
			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(9);
			recipe2.AddIngredient(ItemID.Silk , 3);
			recipe2.AddIngredient(1257 , 3);
			recipe2.AddIngredient(75);
			recipe2.AddTile(mod.TileType("DanmakuTable"));
			recipe2.SetResult(this, 1);
			recipe2.AddRecipe();
		}
	}
}