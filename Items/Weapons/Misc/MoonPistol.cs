using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Misc
{
	public class MoonPistol : ModItem
	{
        private int count = 0;
		private int countB = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Pistol A");
			Tooltip.SetDefault("Right click for focused attack");
	    }		
		public override void SetDefaults()
		{
			item.damage = 10;
			item.ranged = true;
			item.useTime = 5;
			item.useAnimation = 5;
			item.reuseDelay = 15;
			item.value = 100000;
			item.width = 32;
			item.height = 32;
			item.noMelee = true;
			item.rare = 2;
			item.shootSpeed = 24f;
			item.knockBack = 0;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Bullet");
			Item.staff[item.type] = true;
			item.useStyle = 5;
			item.noUseGraphic = true;
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Bullet"), damage, 0, player.whoAmI);
            if (player.altFunctionUse == 2)
            {
                countB++;
                if (countB == 2)
                {
						Projectile.NewProjectile(position.X, position.Y, speedX / 2f, speedY / 2f, mod.ProjectileType("BlueBullet"), damage * 2, 1, player.whoAmI);
                        countB = 0;
                }
                return false;
            }
            else
            {
                //count++;
                //if (count == 4)
                //{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(24));
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("PurpleBullet"), damage, 1, player.whoAmI);
                //    count = 0;
                //}
                return false;
            }
        }
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("PowerItemA"), 25);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}