using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Alice
{
	public class TrueGrimoireofAlice : ModItem
	{
 		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Grimoire of Alice");
			Tooltip.SetDefault("Summons a doll to fight for you.");
	    }			
		public override void SetDefaults()
		{
			item.damage = 32;
			item.summon = true;
			item.mana = 10;
			item.width = 28;
			item.height = 30;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = Item.buyPrice(0, 75, 0, 0);
			item.rare = 6;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("DollMinion");
			item.shootSpeed = 10f;
			item.buffType = mod.BuffType("DollMinion");
			item.buffTime = 3600;
			item.expert = true;
		}	
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("GrimoireofAlice"));
			recipe.AddIngredient(mod.ItemType("LargePowerItemC"));
			recipe.AddIngredient(mod.ItemType("PowerItemC"), 50);
			recipe.AddIngredient(mod.ItemType("DollFragment"), 6);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}