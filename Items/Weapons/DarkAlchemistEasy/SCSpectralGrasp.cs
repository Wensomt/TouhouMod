using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.DarkAlchemistEasy
{
	public class SCSpectralGrasp : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Control Sign : Spectral Grasp");
			Tooltip.SetDefault("Directs energy to a singularity of distortive forces");
	    }				
		public override void SetDefaults()
		{
			item.damage = 56;
			item.width = 30;
			item.height = 40;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 4;
			item.expert = true;
			item.autoReuse = false;
			item.consumable = true;
			item.useAnimation = 48;
			item.useTime = 48;
			item.maxStack = 99;
			item.shootSpeed = 8f;
			item.UseSound = SoundID.Item20;
			item.shoot = mod.ProjectileType("SpectralSingularity");
		}	
	}
}