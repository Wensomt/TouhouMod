using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Tools.Hammers
{
	public class GothicHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doll's Warhammer");
	    }					
		public override void SetDefaults()
		{
			item.damage = 64;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 8;
			item.useAnimation = 32;
			item.hammer = 80;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 500000;
			item.rare = 5;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("DollFragment"), 5);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
}