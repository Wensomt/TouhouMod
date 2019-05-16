using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Alice
{
	public class SCArtfulSacrifice : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magic Sign : Artful Sacrifice");
			Tooltip.SetDefault("Because exploding dolls, that's why...");
	    }			
		public override void SetDefaults()
		{
			item.damage = 1024;
			item.width = 30;
			item.height = 40;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 6;
			item.value = 5000;
			item.rare = 6;
			item.expert = true;
			item.autoReuse = false;
			item.consumable = true;
			item.useAnimation = 80;
			item.useTime = 80;
			item.maxStack = 99;
			item.shootSpeed = 8f;
			item.UseSound = SoundID.Item20;
			item.shoot = mod.ProjectileType("ArtfulSacrifice");
		}	
	}
}