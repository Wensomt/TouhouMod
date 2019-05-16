using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Tools.Picks
{
	public class GothicPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gothic Pickaxe");
	    }			
		public override void SetDefaults()
		{
			item.damage = 48;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 6;
			item.useAnimation = 18;
			item.pick = 180;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 50000;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("DollFragment"), 5);
			recipe.AddIngredient(ItemID.HallowedBar , 5);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}