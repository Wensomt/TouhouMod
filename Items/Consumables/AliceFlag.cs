using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Consumables
{
	public class AliceFlag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Alice's Flag");
			Tooltip.SetDefault("Summons the Seven-Colored Puppeteer, Alice");
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
			return !NPC.AnyNPCs(mod.NPCType("Alice"));
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Alice"));
			return true;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(9);
			recipe.AddIngredient(ItemID.Silk , 3);
			recipe.AddIngredient(ItemID.HallowedBar , 3);
			recipe.AddIngredient(ItemID.Book);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}