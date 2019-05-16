using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;


namespace TouhouMod.Items.Weapons.Misc
{
	public class PurityStaff : DivineItem
	{
        private int count = 0;
		private int countB = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Purity Stick A");
			Tooltip.SetDefault("Right click for focused attack");
	    }		
		public override void SafeSetDefaults()
		{
			item.damage = 10;
			item.mana = 0;
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
			item.shoot = mod.ProjectileType("Talisman");
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
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Talisman"), damage, 0, player.whoAmI);
            if (player.altFunctionUse == 2)
            {
				count++;
                if (count % 2 == 0)
                {
					Projectile.NewProjectile(position.X, position.Y, speedX / 8f * 7f, speedY / 8f * 7f, mod.ProjectileType("PersuasionNeedle"), damage, 1, player.whoAmI);
                }

                return false;
            }
            else
            {
                countB++;
                if (countB % 6 == 0)
                {
					float numberProjectiles = 2;
					float rotation = MathHelper.ToRadians(24);
					position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
					for (int i = 0; i < numberProjectiles; i++)
					{
						Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
						Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("HomingAmulet"), damage, 1, player.whoAmI);
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