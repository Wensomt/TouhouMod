using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment.Armor.Moonlight
{
    [AutoloadEquip(EquipType.Body)]		
	public class MoonlightChestpiece : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moonlight Chestpiece");
			Tooltip.SetDefault("4% increased magic damage"
			+ "\n+10 max mana");
	    }			
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 5000;
			item.rare = 2;
			item.defense = 5;
		}

		public override void UpdateEquip(Player player)
		{
			player.statManaMax2 += 10;
            player.magicDamage *= 1.04f;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MoonlightDust"), 6);
			recipe.AddIngredient(ItemID.Silk, 6);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}

	}
}