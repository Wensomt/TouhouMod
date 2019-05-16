using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Sakuya
{
	public class SCPhantomicKiller : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Buriallusion : Phantomic Killer");
			Tooltip.SetDefault("Death from all directions");
	    }		
		public override void SetDefaults()
		{
			item.damage = 164;
			item.width = 30;
			item.height = 40;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 9;
			item.expert = true;
			item.autoReuse = false;
			item.consumable = true;
			item.useAnimation = 80;
			item.useTime = 80;
			item.maxStack = 99;
			item.shootSpeed = 8f;
			item.UseSound = SoundID.Item20;
			item.shoot = mod.ProjectileType("SpectralKiller");
		}	
	}
}