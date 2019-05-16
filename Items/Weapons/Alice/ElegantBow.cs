using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Alice
{
	public class ElegantBow : ModItem
	{
		int type = 0;
		int arrowType = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rain Bow");
			Tooltip.SetDefault("A colorful death-bringer");
	    }			
		public override void SetDefaults()
		{
			item.damage = 24;
			item.ranged = true;
			item.width = 20;
			item.height = 40;
			item.useTime = 8;
			item.useAnimation = 24;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5;
			item.value = 500000;
			item.rare = 5;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 12f;
			item.useAmmo = AmmoID.Arrow;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			switch(arrowType % 6)
			{
				case 0:
					type = ProjectileID.FireArrow;
					break;
				case 1:
					type = ProjectileID.IchorArrow;
					break;
				case 2:
					type = ProjectileID.JestersArrow;
					break;
				case 3:
					type = ProjectileID.CursedArrow;
					break;
				case 4:
					type = ProjectileID.FrostburnArrow;
					break;
				case 5:
					type = ProjectileID.ShadowFlameArrow;
					break;
			}
            
            Vector2 velocity = new Vector2(speedX , speedY).RotatedByRandom(MathHelper.ToRadians(5));
			speedX = velocity.X;
			speedY = velocity.Y;

			arrowType++;

		    return true;
		}
    }
}