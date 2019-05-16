using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment
{
	public class MoonlightAmulet : ModItem
	{
        private int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moonlight Amulet");
			Tooltip.SetDefault("Spawns magicial orbs of moonlight to create light and"
			+ "\ngive nearby players +1 armor penatration for each orb");
	    }	
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 28;
			item.value = 10000;
			item.rare = 2;
			item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			counter++;
            if (counter % 240 == 0)
            {
                Projectile.NewProjectile(player.position.X - 120 + Main.rand.Next(240), player.position.Y - 120 + Main.rand.Next(240), 0f, 0f, mod.ProjectileType("MoonlightOrbEnhance"), 0, 0, player.whoAmI);
            }
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MoonlightDust"), 6);
			recipe.AddIngredient(85, 3);
			recipe.AddIngredient(177);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
}