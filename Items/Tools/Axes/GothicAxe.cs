using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Tools.Axes
{
	public class GothicAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doll's Waraxe");
	    }		
		public override void SetDefaults()
		{
			item.damage = 64;
			item.melee = true;
			item.width = 56;
			item.height = 56;
			item.useTime = 12;
			item.useAnimation = 36;
			item.axe = 10;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 250000;
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