using Terraria.ModLoader;

namespace TouhouMod.Items.Placeable.Trophies
{
	public class RumiaTrophy : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rumia Trophy ");
            Tooltip.SetDefault("Apparitions Stalk the Night");
	    }				
		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("RumiaTrophy");
			item.width = 24;
			item.height = 24;
			item.rare = 4;
			item.value = 10000;
			item.accessory = true;
		}
	}
}