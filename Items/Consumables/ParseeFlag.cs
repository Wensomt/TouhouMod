using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Consumables
{
	public class ParseeFlag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Parsee's Flag");
			Tooltip.SetDefault("Summons The Jealousy Beneath the Earth's Crust, Parsee");
	    }
		public override void SetDefaults()
		{
			item.width = 48;
			item.height = 38;
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
			return /*!Main.dayTime &&*/ !NPC.AnyNPCs(mod.NPCType("Parsee"));
		}
		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("Parsee"));
			return true;
		}
		
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(9);
			recipe.AddIngredient(ItemID.Silk , 3);
			recipe.AddIngredient(ItemID.Bone , 20);
			recipe.AddIngredient(ItemID.Emerald);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}