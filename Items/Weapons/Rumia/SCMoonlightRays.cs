using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Rumia
{
	public class SCMoonlightRays : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SpellCard : Moonlight Rays");
			Tooltip.SetDefault("Summons a burst of powerful lasers");
	    }		
		public override void SetDefaults()
		{
			item.damage = 42;
			item.width = 30;
			item.height = 40;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 6;
			item.value = 500;
			item.rare = 3;
			item.expert = true;
			item.shoot = 10;
			item.autoReuse = false;
			item.consumable = true;
			item.useAnimation = 48;
			item.useTime = 8;
			item.reuseDelay = 50;
			item.maxStack = 99;
			item.UseSound = SoundID.Item20;
			item.shootSpeed = 8f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = Main.rand.Next(3) + 2;
			type = mod.ProjectileType("MoonlightBeam");
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			for (int k = 0 ; k < Main.projectile.Length ; k++)
			{
				if (Main.projectile[k].hostile && Main.projectile[k].damage > 1)
				{
					Main.projectile[k].Kill();
				}
			}
			return false;
		}
		public override bool ConsumeItem(Player player)
		{
			return !(player.itemAnimation < item.useAnimation - 2);
		}
	}
}