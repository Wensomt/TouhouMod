using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Marisa
{
	public class StarryNight : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starry Night");
			Tooltip.SetDefault("Call upon the wrath of the cosmos");
	    }				
		public override void SetDefaults()
		{
			item.damage = 84;
			item.summon = true;
			item.mana = 16;
			item.width = 40;
			item.height = 40;
			item.useTime = 40;
			item.useAnimation = 40;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 8;
			item.value = 750000;
			item.rare = 7;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("StarryNight");
			item.shootSpeed = 0f;
			item.sentry = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0 ; i < Main.projectile.Length ; i++)
			{
				if (Main.projectile[i].type == mod.ProjectileType("StarryNight"))
					Main.projectile[i].Kill();
			}
			Projectile.NewProjectile(player.Center, new Vector2(0f,0f), mod.ProjectileType("StarryNight"), damage, knockBack, player.whoAmI);
			return false;
		}
	}
}