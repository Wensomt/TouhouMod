using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Alice
{
	public class ExplosiveDoll : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Explosive Doll");
			Tooltip.SetDefault("Because rigging your dolls with explosives is normal... right?");
	    }			
		public override void SetDefaults()
		{
			item.damage = 64;
            item.thrown = true;
			item.width = 50;
			item.height = 50;
			item.useStyle = 1;
			item.noMelee = true;
            item.noUseGraphic = true;
			item.knockBack = 8;
			item.value = 500000;
			item.rare = 5;
			item.autoReuse = false;
			item.useAnimation = 20;
			item.useTime = 20;
			item.shootSpeed = 12f;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("ExplosiveDoll");
		}	
	}
}