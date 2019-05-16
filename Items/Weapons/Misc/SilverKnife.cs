using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Misc
{
	public class SilverKnife : ModItem
	{
        private int count = 0;
		private int countB = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Silver Knife A");
			Tooltip.SetDefault("Right click for focused attack");
	    }		
		public override void SetDefaults()
		{
			item.damage = 10;
			item.thrown = true;
			item.mana = 0;
			item.useTime = 20;
			item.useAnimation = 20;
			item.value = 100000;
			item.width = 32;
			item.height = 32;
			item.noMelee = true;
			item.rare = 2;
			item.shootSpeed = 24f;
			item.knockBack = 0;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("SilverKnifeBlue");
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
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SilverKnifeBlue"), damage, 0, player.whoAmI);
            if (player.altFunctionUse == 2)
            {
				countB++;
                if (countB % 3 == 0)
                {
						Projectile.NewProjectile(position.X - speedY / 4f, position.Y - speedX / 4f, speedX, speedY, mod.ProjectileType("SilverKnifePurple"), damage, 1, player.whoAmI);
                        Projectile.NewProjectile(position.X + speedY / 4f, position.Y + speedX / 4f, speedX, speedY, mod.ProjectileType("SilverKnifePurple"), damage, 1, player.whoAmI);
                
				}
                return false;

            }
            else
            {
                count++;
                if (count % 3 == 0)
                {
					float numberProjectiles = 2;
					float rotation = MathHelper.ToRadians(32);
					position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
						Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SilverKnifePurple"), damage, 1, player.whoAmI);
					}
                }

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