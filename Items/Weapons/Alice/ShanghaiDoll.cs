using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
 
namespace TouhouMod.Items.Weapons.Alice
{
    public class ShanghaiDoll : ModItem
    {
 		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Doll Shanghai");
			Tooltip.SetDefault("Wither targets with a cursed beam");
	    }			       
		public override void SetDefaults()
        {
            item.damage = 38;
            item.noMelee = true;
            item.noUseGraphic = false;
            item.magic = true;
            //item.channel = true;
            item.mana = 16;
            item.rare = 5;
            item.width = 30;
            item.height = 47;
            //item.useTime = 7;
            item.useTime = 40;
            item.UseSound = SoundID.Item13;
            item.useStyle = 5;
            item.shootSpeed = 24f;
            //item.useAnimation = 7;
            item.useAnimation = 40;
            item.shoot = mod.ProjectileType("ShanghaiBeam");
            item.value = Item.sellPrice(0, 10, 0, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            position.X += speedX * 3f;
            position.Y += speedY * 3f;
            Projectile.NewProjectile(position, new Vector2(speedX,speedY), mod.ProjectileType("ShanghaiLaserEnd"), damage, knockBack, player.whoAmI);

            return true;
        }
    }
}