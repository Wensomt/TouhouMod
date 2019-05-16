using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment.Armor.Moonlight
{
	[AutoloadEquip(EquipType.Legs)]		
	public class MoonlightLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moonlight Leggings");
			Tooltip.SetDefault("4% increased magic damage"
			+ "\n+5 max mana"
			+ "\n10% increased movement speed");
	    }
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 4000;
			item.rare = 2;
			item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
            player.magicDamage *= 1.04f;
            player.statManaMax2 += 5;
			player.moveSpeed += 0.10f;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MoonlightDust"), 5);
			recipe.AddIngredient(ItemID.Silk, 5);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}