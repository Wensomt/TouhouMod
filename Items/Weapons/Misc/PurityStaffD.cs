using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TouhouMod.Items.Weapons.Misc
{
	public class PurityStaffD : DivineItem
	{
        private int count = 0;
		private int countB = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Purity Stick D");
			Tooltip.SetDefault("Right click for focused attack");
	    }		
		public override void SafeSetDefaults()
		{
			item.damage = 28;
			item.mana = 0;
			item.useTime = 5;
			item.useAnimation = 20;
			item.reuseDelay = 30;
			item.value = 400000;
			item.width = 32;
			item.height = 32;
			item.noMelee = true;
			item.rare = 6;
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
            float numberProjectiles = 2;
			float rotation = MathHelper.ToRadians(2);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("Talisman"), damage, 0, player.whoAmI);
			}
            if (player.altFunctionUse == 2)
            {
                count++;
                if (count % 3 == 0)
                {
					Projectile.NewProjectile(position.X - speedY / 2f, position.Y - speedX / 2f, speedX / 8f * 7f, speedY / 8f * 7f, mod.ProjectileType("PersuasionNeedle"), damage, 1, player.whoAmI);
					Projectile.NewProjectile(position.X +  speedY / 2f, position.Y + speedX / 2f, speedX / 8f * 7f, speedY / 8f * 7f, mod.ProjectileType("PersuasionNeedle"), damage, 1, player.whoAmI);
                }
				if (count % 2 == 0)
				{
					Projectile.NewProjectile(position.X +  speedY, position.Y + speedX, speedX / 8f * 7f, speedY / 8f * 7f, mod.ProjectileType("PersuasionNeedle"), damage, 1, player.whoAmI);
					Projectile.NewProjectile(position.X - speedY, position.Y - speedX, speedX / 8f * 7f, speedY / 8f * 7f, mod.ProjectileType("PersuasionNeedle"), damage, 1, player.whoAmI);
				}

                return false;
            }
            else
            {
                countB++;
                if (countB % 6 == 0)
                {
					numberProjectiles = 4;
					rotation = MathHelper.ToRadians(24);
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
			recipe.AddIngredient(mod.ItemType("PurityStaffC"));
			recipe.AddIngredient(mod.ItemType("PowerItemC"), 100);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}