using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Enums;

namespace TouhouMod.Projectiles.Friendly.MarisaWeapons
{
	public class HealingStar : ModProjectile
	{
        private int timer = 0;
        private float scale = 0.5f;
		public override void SetDefaults(){
			//projectile.name = "Healing Star";
            projectile.width = 35;
            projectile.height = 35;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
			projectile.light = 1f;
			projectile.timeLeft = 300;
		}
        public override void AI()
        {
            timer++;
            projectile.scale = scale;
            projectile.rotation += 0.2f;
            Player owner = Main.player[projectile.owner];

            //Heals player
            if (Distance(owner.position, projectile.position) < 180f && timer % 3 == 0 && projectile.scale > 0f)
            {
                if (owner.statLife < owner.statLifeMax2)
                    owner.statLife += 3;
                if (owner.statMana < owner.statManaMax2)
                    owner.statMana += 5;
                CreateDust();
                if (Main.expertMode) //Recovers up to 60/100 health/mana on expert
                    scale -= 0.025f;
                else //Recovers up to 75/125 health/mana on normal
                    scale -= 0.02f;
                
                owner.AddBuff(mod.BuffType("HealingStar"), 10, true);
            }

            //Kills the projectile if the "healing power" has been depleted
            if (projectile.scale < 0f)
            {
                for (int i = 0 ; i < 12 ; i++)
                    CreateDust();
                projectile.Kill();
            }

        }
        private float Distance(Vector2 v1, Vector2 v2)
        {
            float dx = v1.X - v2.X;
            float dy = v1.Y - v2.Y;
            dx = (float)Math.Pow(dx,2);
            dy = (float)Math.Pow(dy,2);
            return (float)(Math.Sqrt(dx + dy));
        }
        public virtual void CreateDust()
		{
			Color? color = new Color(250,0,0);
			if (color.HasValue)
			{
				
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0f, 0f, 0, color.Value);
				Main.dust[dust].velocity *= 3f;
			}
		}

    }
}