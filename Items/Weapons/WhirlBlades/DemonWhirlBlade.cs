using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.WhirlBlades
{
	public class DemonWhirlBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Pentagram");
	    }		
		public override void SetDefaults()
		{
			item.damage = 48;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 8;
			item.value = 1000;
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("DemonWhirlBlade");
			item.shootSpeed = 18f;
			item.noUseGraphic = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position.Y -= 16;
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return player.FindBuffIndex(mod.BuffType("MotionSickness")) == -1;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 16);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}