using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment.Armor.Envy
{
	[AutoloadEquip(EquipType.Legs)]	
	public class EnvyLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Envy Leggings");
			Tooltip.SetDefault("4% increased ranged crit"
			+ "\n2% increased ranged damage"
			+ "\n12% increased movement speed");
	    }			
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = 2;
			item.defense = 6;
		}

		public override void UpdateEquip(Player player)
		{
            player.rangedCrit += 4;
            player.rangedDamage *= 1.02f;
			player.moveSpeed += 0.12f;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("EnvyCrystal"), 5);
			recipe.AddIngredient(ItemID.HellstoneBar , 5);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}