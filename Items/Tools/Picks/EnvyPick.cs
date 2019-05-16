using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Tools.Picks
{
	public class EnvyPick : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Envy Pickaxe");
			Tooltip.SetDefault("Sacrifices pickaxe power for speed");
	    }				
		public override void SetDefaults()
		{
			item.damage = 10;
			item.melee = true;
			item.width = 34;
			item.height = 34;
			item.useTime = 3;
			item.useAnimation = 12;
			item.pick = 25;
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