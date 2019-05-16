using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Marisa
{
	public class TrueStarBurstStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Star Burst Staff");
			Tooltip.SetDefault("Nothing quite like having your own meteor shower");
	    }		
		public override void SetDefaults(){
			item.damage = 64;
            item.mana = 12;
			item.magic = true;
			item.width = 38;
			item.height = 38;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			item.knockBack = 3;
			item.value = 1000000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("StarBurstLarge");
			item.shootSpeed = 12f;
			item.noMelee = true;
            item.expert = true;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("StarBurstStaff"));
			recipe.AddIngredient(mod.ItemType("LargePowerItemD"));
			recipe.AddIngredient(mod.ItemType("PowerItemD"), 50);
			recipe.AddIngredient(mod.ItemType("StarDust"), 6);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}