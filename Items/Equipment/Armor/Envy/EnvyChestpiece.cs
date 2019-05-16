using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment.Armor.Envy
{
    [AutoloadEquip(EquipType.Body)]	
	public class EnvyChestpiece : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Envy Chestpiece");
			Tooltip.SetDefault("4% increased ranged crit"
			+ "\n2% increased ranged damage");
	    }	
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 12000;
			item.rare = 2;
			item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 4;
            player.rangedDamage *= 1.02f;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("EnvyCrystal"), 6);
			recipe.AddIngredient(ItemID.HellstoneBar , 6);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
}