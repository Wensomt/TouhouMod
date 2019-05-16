using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment.Armor.War
{
    [AutoloadEquip(EquipType.Body)]		
	public class WarChestpiece : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("War Chestpiece");
			Tooltip.SetDefault("+3 max minions"
			+ "\n10% decreased minion damage"
			+ "\n'Power of the masses'");
	    }
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 120000;
			item.rare = 5;
			item.defense = 17;
		}
        public override void UpdateEquip(Player player)
		{
			player.maxMinions += 3;
            player.minionDamage -= 0.10f;
		}
        public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("DollFragment"), 6);
			recipe.AddIngredient(ItemID.HallowedBar , 9);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}