using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Marisa
{
	public class StarDuster : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starduster");
			Tooltip.SetDefault("Spray your enemies with the force of the cosmos");
	    }		
		public override void SetDefaults()
		{
			item.damage = 48;
			item.ranged = true;
			item.width = 52;
			item.height = 18;
			item.useTime = 1;
			item.useAnimation = 10;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 6;
			item.value = 750000;
			item.rare = 7;
			item.UseSound = SoundID.Item13;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 3f;
			item.useAmmo = AmmoID.Bullet;
		
		}
	
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = mod.ProjectileType("StarDuster");
			int numberProjectiles = Main.rand.Next(2) + 1;
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
				Projectile.NewProjectile(position.X + speedX * 4f, position.Y + speedY * 4f, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			
			return false;
		}
        public override bool ConsumeAmmo(Player player)
		{
            if (Main.rand.Next(100) < 75)
                return false;
			return !(player.itemAnimation < item.useAnimation - 2);
		}


	}
	
}