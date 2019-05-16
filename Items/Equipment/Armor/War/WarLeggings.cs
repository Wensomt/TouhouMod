using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment.Armor.War
{
	[AutoloadEquip(EquipType.Legs)]		
	public class WarLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("War Leggings");
			Tooltip.SetDefault("+2 max minions"
			+ "\n6% decreased minion damage"
			+ "\n8% increased movement speed"
			+ "\n'Power of the masses'");
	    }        		
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 100000;
			item.rare = 5;
			item.defense = 10;
		}
        public override void UpdateEquip(Player player)
		{
            player.maxMinions += 2;
            player.minionDamage -= 0.06f;
			player.moveSpeed += 0.08f;
		}
        public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("DollFragment"), 5);
			recipe.AddIngredient(ItemID.HallowedBar , 8);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}