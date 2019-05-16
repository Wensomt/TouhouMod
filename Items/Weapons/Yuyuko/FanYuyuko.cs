using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Yuyuko
{
	public class FanYuyuko : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yuyuko's Fan");
			Tooltip.SetDefault("Blast targets away with a layered wave of forceful shots!");
	    }		
		public override void SetDefaults()
		{
			item.damage = 182;
			item.magic = true;
			item.mana = 30;
			item.width = 50;
			item.height = 33;
			item.useTime = 60;
			item.useAnimation = 60;
			item.useStyle = 5;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.knockBack = 12;
			item.value = 200000;
			item.rare = 8;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("YuyukoFanShot");
			item.shootSpeed = 16f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float spdX = speedX;
			float spdY = speedY;
			type = mod.ProjectileType("YuyukoFanShot");
			//Front Wave
			float numberProjectiles = 8;
			float rotation = MathHelper.ToRadians(60);
			for (int j = 6; j > 0; j--)
			{
				//position += Vector2.Normalize(new Vector2(spdX, spdY)) * 45f;
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX * j / 6, speedY * j / 6).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
					Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
				}
			}
			return false;
		}
		
	}
	
}