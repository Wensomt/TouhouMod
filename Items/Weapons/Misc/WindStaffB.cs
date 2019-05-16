using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Misc
{
	public class WindStaffB : ModItem
	{
        private int count = 0;
		private int countB = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wind Maiden Staff B");
			Tooltip.SetDefault("Right click for focused attack");
	    }			
		public override void SetDefaults()
		{
			item.damage = 12;
			item.summon = true;
			item.mana = 0;
			item.useTime = 5;
			item.useAnimation = 10;
			item.reuseDelay = 20;
			item.value = 200000;
			item.width = 32;
			item.height = 32;
			item.noMelee = true;
			item.rare = 3;
			item.shootSpeed = 24f;
			item.knockBack = 0;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("Talisman2");
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
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Talisman2"), damage, 0, player.whoAmI);
            if (player.altFunctionUse == 2)
            {
                countB++;
                if (countB % 2 == 0)
                {
						Projectile.NewProjectile(position.X, position.Y, speedX / 2f, speedY / 2f, mod.ProjectileType("Snake"), damage/4*3, 1, player.whoAmI);
                }
                return false;
            }
            else
            {
                count++;
                if (count % 10 == 0)
                {
					float numberProjectiles = 2;
					float rotation = MathHelper.ToRadians(32);
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(speedX / 2f, speedY / 2f).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
						Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("Frog"), damage, 1, player.whoAmI);
					}
                }

                return false;
            }
        }
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("WindStaff"));
			recipe.AddIngredient(mod.ItemType("PowerItemA"), 100);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}