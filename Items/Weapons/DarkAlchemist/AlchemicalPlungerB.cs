using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.DarkAlchemist
{
	public class AlchemicalPlungerB : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Alchemical Plunger Type B");
			Tooltip.SetDefault("Distorts the chemical properties of ammo with powerful alchemy"
			+ "\nFires a burst of the same random projectile");
	    }			
		public override void SetDefaults()
		{
			item.damage = 84;
			item.ranged = true;
			item.width = 48;
			item.height = 30;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 6;
			item.value = 200000;
			item.rare = 3;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 12f;
			item.useAmmo = AmmoID.Bullet;
		
		
		}
	
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = Main.rand.Next(3) + 4;
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			type = GetProjectileType();
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			
			return true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(1, 0);
		}
		public int GetProjectileType()
		{
			int x = Main.rand.Next(50) + 1;
			if (x == 1)
				return 15;
			if (x == 2)
				return 2;
			if (x == 3)
				return 20;
			if (x == 4)
				return 4;
			if (x == 5)
				return 5;
			if (x == 6)
				return 27;
			if (x == 7)
				return 36;
			if (x == 8)
				return 41;
			if (x == 9)
				return 45;
			if (x == 10)
				return 91;
			if (x == 11)
				return 95;
			if (x == 12)
				return 103;
			if (x == 13)
				return 114;
			if (x == 14)
				return 116;
			if (x == 15)
				return 120;
			if (x == 16)
				return 132;
			if (x == 17)
				return 134;
			if (x == 18)
				return 140;
			if (x == 19)
				return 207;
			if (x == 20)
				return 225;
			if (x == 21)
				return 263;
			if (x == 22)
				return 274;
			if (x == 23)
				return 278;
			if (x == 24)
				return 282;
			if (x == 25)
				return 295;
			if (x == 26)
				return 321;
			if (x == 27)
				return 343;
			if (x == 28)
				return 409;
			if (x == 29)
				return 451;
			if (x == 30)
				return 459;
			if (x == 31)
				return 477;
			if (x == 32)
				return 478;
			if (x == 33)
				return 479;
			if (x == 34)
				return 485;
			if (x == 35)
				return 495;
			if (x == 36)
				return 514;
			if (x == 37)
				return 503;
			if (x == 38)
				return 502;
			if (x == 39)
				return 596;
			if (x == 40)
				return 616;
			if (x == 41)
				return 617;
			if (x == 42)
				return 634;
			if (x == 43)
				return 635;
			if (x == 44)
				return 638;
			if (x == 45)
				return 639;
			if (x == 46)
				return 659;
			if (x == 47)
				return 660;
			if (x == 48)
				return 661;
			if (x == 49)
				return 706;
			if (x == 50)
				return 710;
			
			return 1;
			
		}

	}
	
}