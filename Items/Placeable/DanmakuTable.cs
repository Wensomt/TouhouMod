using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Placeable
{
	public class DanmakuTable : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Danmaku Table");
			Tooltip.SetDefault("A magical workbench that can work with items from another world");
	    }		
		public override void SetDefaults()
		{
			item.width = 64;
			item.height = 50;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = 10000;
			item.createTile = mod.TileType("DanmakuTable");
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 12);
			recipe.AddIngredient(mod.ItemType("YinYangOrb"));
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();




		}
    }
}