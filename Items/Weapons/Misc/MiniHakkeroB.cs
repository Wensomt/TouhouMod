using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Misc
{
	public class MiniHakkeroB : ModItem
	{
        private int count = 0;
        private int countB = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mini Hakkero B");
			Tooltip.SetDefault("Right click for focused attack");
	    }			
		public override void SetDefaults()
		{
			item.damage = 12;
			item.magic = true;
			item.mana = 0;
			item.useTime = 5;
			item.useAnimation = 10;
			item.reuseDelay = 20;
			item.value = 200000;
			item.width = 32;
			item.height = 28;
			item.noMelee = true;
			item.rare = 3;
			item.shootSpeed = 24f;
			item.knockBack = 0;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("MagicShot");
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
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("MagicShot"), damage, 0, player.whoAmI);
            if (player.altFunctionUse == 2)
            {
				count++;
                if (count % 3 == 0)
                {
					Projectile.NewProjectile(position.X, position.Y, speedX / 8f, speedY / 8f, mod.ProjectileType("MagicMissile"), damage * 4, 1, player.whoAmI);
                }
                return false;
            }
            else
            {
                countB++;
                if (countB % 2 == 0)
                {
					float numberProjectiles = 3;
					float rotation = MathHelper.ToRadians(12);
					position += Vector2.Normalize(new Vector2(speedX, speedY));
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
						Projectile.NewProjectile(position.X - speedY * 0.5f, position.Y - speedX * 0.5f, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RainbowLaser"), damage/4*3, 1, player.whoAmI);
                        Projectile.NewProjectile(position.X + speedY * 0.5f, position.Y + speedX * 0.5f, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("RainbowLaser"), damage/4*3, 1, player.whoAmI);
					}
                    
                }
				return false;
            }

        }
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("MiniHakkero"));
			recipe.AddIngredient(mod.ItemType("PowerItemA"), 100);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}