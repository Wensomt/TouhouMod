using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.Items.Weapons.Parsee
{
	public class BulbStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bulb Staff");
			Tooltip.SetDefault("Summon an envy bulb to spit a few bursts of solid envy");
	    }			
		public override void SetDefaults(){
			item.damage = 20;
			item.summon = true;
			item.mana = 16;
			item.width = 38;
			item.height = 36;
			item.useTime = 60;
			item.useAnimation = 60;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 8;
			item.value = 100000;
			item.rare = 4;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
			item.shoot = mod.ProjectileType("EnvyBulb");
			item.shootSpeed = 0f;
			item.sentry = true;
		}
	}
}