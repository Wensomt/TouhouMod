using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Misc
{
	public class RoukankenF : ModItem
	{
        private int count = 0;
		private int countB = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Roukanken F");
            Tooltip.SetDefault("Left click to slash the enemy" +
                "\nRight click for shoot");
        }		
		public override void SetDefaults()
		{
			item.damage = 45;
			item.melee = true;
			item.mana = 0;
			item.useTime = 10;
			item.useAnimation = 10;
			//item.reuseDelay = 30;
			item.value = 750000;
			item.width = 32;
			item.height = 32;
			item.noMelee = true;
			item.rare = 8;
			item.shootSpeed = 27f;
			item.knockBack = 0;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("SlashS");
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
			//Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SlashS"), damage, 0, player.whoAmI);
            if (player.altFunctionUse == 2)
            {
				countB++;
                if (countB % 1 == 0)
                {
                    Projectile.NewProjectile(position.X + speedY * 0.5f, position.Y + speedX * 0.5f, speedX, speedY, mod.ProjectileType("SlashS"), damage*2, 0, player.whoAmI);
                    Projectile.NewProjectile(position.X - speedY * 0.5f, position.Y - speedX * 0.5f, speedX, speedY, mod.ProjectileType("SlashS"), damage*2, 0, player.whoAmI);

                }
                return false;

            }
            else
            {
                    //float numberProjectiles = 3;
					//float rotation = MathHelper.ToRadians(32);
					//position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
					
					//for (int i = 0; i < numberProjectiles; i++)
					//{
						// float plus = i * 12f;
						 
               
                //}
                //co
                count++;
                if (count % 6 == 0)
                {

                    if (speedX > 0f)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SLASH"), 0.5f);
                        Projectile.NewProjectile(position.X + speedX * 4f, position.Y + speedY * 3f, speedX * 0.001f, speedY * 0.001f, mod.ProjectileType("Slash"), damage * 2, 1, player.whoAmI);
                        Projectile.NewProjectile(position.X + speedX * 8f, position.Y + speedY * 6f, speedX * 0.001f, speedY * 0.001f, mod.ProjectileType("Slash"), damage * 2, 1, player.whoAmI);
                    }
                    else
                    {
                        Main.PlaySound(SoundLoader.customSoundType, -1, -1, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/SLASH"), 0.5f);
                        Projectile.NewProjectile(position.X + speedX, position.Y + speedY * 3f, speedX * 0.001f, speedY * 0.001f, mod.ProjectileType("Slash"), damage * 2, 1, player.whoAmI);
                        Projectile.NewProjectile(position.X + speedX * 4f, position.Y + speedY * 6f, speedX * 0.001f, speedY * 0.001f, mod.ProjectileType("Slash"), damage * 2, 1, player.whoAmI);
                    }

                    //count++;Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SilverKnifePurple"), damage, 1, player.whoAmI);
                }
                //float numberProjectiles = 2;
                //float rotation = MathHelper.ToRadians(32);
                //position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
                //for (int i = 0; i < numberProjectiles; i++)
                //{
                //Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                //Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SilverKnifePurple"), damage, 1, player.whoAmI);
                //}
                // }

                return false;
            }

        }
		public override void AddRecipes(){
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("RoukankenE"));
			recipe.AddIngredient(mod.ItemType("PowerItemE"), 100);
			recipe.AddTile(mod.TileType("DanmakuTable"));
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}