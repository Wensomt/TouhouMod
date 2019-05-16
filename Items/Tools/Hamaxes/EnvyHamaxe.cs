using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Tools.Hamaxes
{
	public class EnvyHamaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Envy Hamaxe");
			Tooltip.SetDefault("Sacrifices hamaxe power for speed");
	    }				
		public override void SetDefaults()
		{
			item.damage = 20;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 4;
			item.useAnimation = 16;
			item.axe = 8;
			item.hammer = 30;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
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