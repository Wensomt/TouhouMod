using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.WhirlBlades
{
	public class TungWhirlBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tungsten Whirl Blade");
	    }		
		public override void SetDefaults()
		{
			item.damage = 28;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 6;
			item.value = 1000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("TungWhirlBlade");
			item.shootSpeed = 14f;
			item.noUseGraphic = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position.Y -= 12;
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return player.FindBuffIndex(mod.BuffType("MotionSickness")) == -1;
		}
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TungstenBar, 16);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
    }
}