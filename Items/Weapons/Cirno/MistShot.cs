using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Cirno
{
	public class MistShot : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mist Shot");
			Tooltip.SetDefault("Turns bullets into multiple mist rays");
	    }		
		public override void SetDefaults()
		{
			item.damage = 14;
			item.ranged = true;
			item.width = 30;
			item.height = 40;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 6;
			item.value = 20000;
			item.rare = 3;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 3f;
			item.useAmmo = AmmoID.Bullet;
		
		
		}
	
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = mod.ProjectileType("MistyBeam");
			int numberProjectiles = Main.rand.Next(4) + 1;
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			
			return true;
		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			if(Main.rand.Next(2) == 1)
				target.AddBuff(44, 120);
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(1, 0);
		}

	}
	
}