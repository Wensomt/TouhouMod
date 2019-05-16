using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Marisa
{
	public class StarBurstStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Burst Staff");
			Tooltip.SetDefault("No, it does not shoot candy...");
	    }				
		public override void SetDefaults()
		{
			item.damage = 48;
            item.mana = 2;
			item.magic = true;
			item.width = 38;
			item.height = 38;
			item.useTime = 6;
			item.useAnimation = 6;
			item.useStyle = 5;
			item.knockBack = 3;
			item.value = 750000;
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("StarBurst");
			item.shootSpeed = 16f;
			item.noMelee = false;
		}
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 speed = new Vector2(speedX,speedY).RotatedByRandom(MathHelper.ToRadians(18));
            speedX = speed.X;
            speedY = speed.Y;

            return true;
        }
	}
}