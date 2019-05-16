using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Tools.Axes
{
	public class StarryAxe : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 72;
			item.melee = true;
			item.width = 50;
			item.height = 50;
			item.useTime = 14;
			item.useAnimation = 28;
			item.axe = 12;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 500000;
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("StarDust"), 5);
			recipe.AddIngredient(ItemID.ChlorophyteBar , 5);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
}