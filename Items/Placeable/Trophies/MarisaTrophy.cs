using Terraria.ModLoader;

namespace TouhouMod.Items.Placeable.Trophies
{
	public class MarisaTrophy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Marisa Trophy ");
            Tooltip.SetDefault("Love-colored Master Spark");
            
	    }		
		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("MarisaTrophy");
			item.width = 24;
			item.height = 24;
			item.rare = 4;
			item.value = 10000;
			item.accessory = true;
		}
	}
}