using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Consumables
{
	public class GreaterHarukeiBlessing : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greater Hakurei Blessing");
			Tooltip.SetDefault("Deeply cleanses the soul of those who drink it");
	    }
		public override void SetDefaults()
		{
			item.useStyle = 2;
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.maxStack = 30;
			item.width = 20;
			item.height = 28;
			item.rare = 5;
			item.value = 7500;
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
            player.AddBuff(mod.BuffType("HarukeiBlessing"), 900, true);
			return true;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PowerItemC"), 5);
			recipe.AddIngredient(mod.ItemType("HarukeiBlessing") , 3);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 3);
			recipe.AddRecipe();

			ModRecipe recipe2 = new ModRecipe(mod);
			recipe2.AddIngredient(mod.ItemType("PowerItemD"), 5);
			recipe2.AddIngredient(mod.ItemType("HarukeiBlessing") , 6);
			recipe2.AddTile(mod.TileType("DanmakuTable"));
			recipe2.SetResult(this, 6);
			recipe2.AddRecipe();

			ModRecipe recipe3 = new ModRecipe(mod);
			recipe3.AddIngredient(mod.ItemType("PowerItemD"), 5);
			recipe3.AddIngredient(ItemID.GreaterHealingPotion , 3);
			recipe3.AddTile(mod.TileType("DanmakuTable"));
			recipe3.SetResult(this, 3);
			recipe3.AddRecipe();

		}

    }
}