using Terraria;
using Terraria.ModLoader;

namespace TouhouMod.Items.Bags
{
	public class RumiaTreasureBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag : Rumia");
			Tooltip.SetDefault("Right click to open");
	    }		
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 38;
			item.height = 32;
			item.rare = 4;
			item.expert = true;
			bossBagNPC = mod.NPCType("Rumia");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("RumiaTrophy"));
			}
			int x = Main.rand.Next(5);
			for (int i = 0; i < 3; i++)
			{
				if (x == 0)
				{
					player.QuickSpawnItem(mod.ItemType("MoonlightBow"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 1)
				{
					player.QuickSpawnItem(mod.ItemType("MoonlightBurst"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 2)
				{
					player.QuickSpawnItem(mod.ItemType("MoonlightBlade"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 3)
				{
					player.QuickSpawnItem(mod.ItemType("MoonlightThrower"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
				}
				else if (x == 4)
				{
					player.QuickSpawnItem(mod.ItemType("MoonOrbStaff"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
				}
			}
			x = (Main.rand.Next(10) + 6);
			for (int i = 0 ; i <= x ; i++)
				player.QuickSpawnItem(mod.ItemType("MoonlightDust"));
			x = (Main.rand.Next(3) + 1);
			for (int i = 0 ; i <= x ; i++)
				player.QuickSpawnItem(mod.ItemType("SCMoonlightRays"));
		}
	}
}