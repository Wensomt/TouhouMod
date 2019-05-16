using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Equipment.Armor.Starry
{
	[AutoloadEquip(EquipType.Legs)]		
	public class StarryLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starry Leggings");
			Tooltip.SetDefault("2% increased magic damage"
			+ "\n4% increased magic crit"
			+ "\n+20 max mana"
			+ "\n12% increased movement speed");
	    }
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 200000;
			item.rare = 7;
			item.defense = 14;
		}
        public override void UpdateEquip(Player player)
		{
            player.magicDamage += 0.02f;
            player.magicCrit += 4;
            player.statManaMax2 += 20;
			player.moveSpeed += 0.12f;
		}
        public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("StarDust"), 7);
			recipe.AddIngredient(ItemID.ChlorophyteBar , 14);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}