using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Cirno
{
	public class IceFlake : ModItem
	{
 		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Flake");
			Tooltip.SetDefault("Throws an icy shard");
	    }			
		public override void SetDefaults(){
			item.damage = 32;
			item.melee = true;
			item.width = 20;
			item.height = 20;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 20000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("IceFlake");
			item.shootSpeed = 8f;
			item.noMelee = false;
			item.noUseGraphic = true;
			item.useTurn = true;
		}
	}
}