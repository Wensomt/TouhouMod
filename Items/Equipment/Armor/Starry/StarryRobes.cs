using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment.Armor.Starry
{
    [AutoloadEquip(EquipType.Body)]		
	public class StarryRobes : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starry Robes");
			Tooltip.SetDefault("3% increased magic damage"
			+ "\n2% increased magic crit"
			+ "\n+30 max mana");
	    }		
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 240000;
			item.rare = 7;
			item.defense = 20;
		}
        public override void UpdateEquip(Player player)
		{
			player.magicDamage += 0.03f;
            player.magicCrit += 2;
            player.statManaMax2 += 20;
		}
        public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("StarDust"), 8);
			recipe.AddIngredient(ItemID.ChlorophyteBar , 16);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}       
    }
}