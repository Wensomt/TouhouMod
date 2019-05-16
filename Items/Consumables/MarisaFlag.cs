using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Consumables
{
	public class MarisaFlag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marisa's Flag");
			Tooltip.SetDefault("Summons the Ordinary Western Magician, Marisa");
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
			return !Main.dayTime && !NPC.AnyNPCs(mod.NPCType("Marisa"));
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Marisa"));
			return true;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(9);
			recipe.AddIngredient(ItemID.Silk , 3);
			recipe.AddIngredient(ItemID.ChlorophyteBar , 3);
			recipe.AddIngredient(mod.ItemType("MoonlightDust"));
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}