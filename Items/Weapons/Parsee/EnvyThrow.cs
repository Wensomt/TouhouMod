using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Parsee
{
	public class EnvyThrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Envy Throw");
			Tooltip.SetDefault("It glows with envy");
	    }		
		public override void SetDefaults()
		{
			item.CloneDefaults(3283);
			item.damage = 38;
			item.melee = true;
			item.width = 30;
			item.height = 26;
			item.rare = 4;
			item.value = 100000;
			item.channel = true;
			item.useTime = 25;
			item.useAnimation = 25;
			item.shoot = mod.ProjectileType("EnvyThrowP");
		}
		
	}	
}