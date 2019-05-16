using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Parsee
{
	public class GreenEyeDagger : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Green Eye Daggers");
			Tooltip.SetDefault("The knifes are made of pure envy");
	    }			
		public override void SetDefaults(){
			item.damage = 16;
			item.thrown = true;
			item.width = 14;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 100000;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("EnvyKnife");
			item.shootSpeed = 10f;
			item.noMelee = false;
			item.noUseGraphic = true;
			item.useTurn = true;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = Main.rand.Next(2) + 2;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15)) * (float)(Main.rand.NextFloat() + 1);
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			
			return true;
		}
	}
}