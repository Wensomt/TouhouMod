using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Consumables
{
	public class LesserHarukeiBlessing : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lesser Hakurei Blessing");
			Tooltip.SetDefault("Gently cleanses the soul of those who drink it");
	    }	
		public override void SetDefaults()
		{
			item.useStyle = 2;
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.maxStack = 30;
			item.width = 20;
			item.height = 26;
			item.rare = 2;
			item.value = 1000;
			item.consumable = true;
			item.UseSound = SoundID.Item3;
			return;
		
		}
        public override bool CanUseItem(Player player)
		{
			return player.FindBuffIndex(mod.BuffType("BlessingCooldown")) == -1;
		}
        public override bool UseItem(Player player)
		{
			player.AddBuff(mod.BuffType("BlessingCooldown"), 5400, true);
			player.AddBuff(mod.BuffType("HarukeiBlessing"), 300, true);
			return true;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PowerItemA"), 5);
			recipe.AddIngredient(ItemID.LesserHealingPotion , 3);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 3);
			recipe.AddRecipe();
			
			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(mod.ItemType("PowerItemB"), 5);
			recipe2.AddIngredient(ItemID.LesserHealingPotion , 6);
			recipe2.AddTile(mod.TileType("DanmakuTable"));
			recipe2.SetResult(this, 6);
			recipe2.AddRecipe();

			ModRecipe recipe3 = new ModRecipe(mod);
			recipe3.AddIngredient(mod.ItemType("PowerItemC"), 5);
			recipe3.AddIngredient(ItemID.LesserHealingPotion , 9);
			recipe3.AddTile(mod.TileType("DanmakuTable"));
			recipe3.SetResult(this, 9);
			recipe3.AddRecipe();

			ModRecipe recipe4 = new ModRecipe(mod);
			recipe4.AddIngredient(mod.ItemType("PowerItemD"), 5);
			recipe4.AddIngredient(ItemID.LesserHealingPotion , 12);
			recipe4.AddTile(mod.TileType("DanmakuTable"));
			recipe4.SetResult(this, 12);
			recipe4.AddRecipe();
		}
    }
}