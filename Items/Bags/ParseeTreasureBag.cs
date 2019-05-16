using Terraria;
using Terraria.ModLoader;

namespace TouhouMod.Items.Bags
{
	public class ParseeTreasureBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Treasure Bag : Parsee");
			Tooltip.SetDefault("Right click to open");
	    }
		public override void SetDefaults()
		{
			item.maxStack = 999;
			item.consumable = true;
			item.width = 38;
			item.height = 32;
			item.rare = 5;
			item.expert = true;
			bossBagNPC = mod.NPCType("Parsee");
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(5) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("ParseeTrophy"));
			}
			int x = Main.rand.Next(5);
			for (int i = 0; i < 3; i++)
			{
				if (x == 0)
				{
					player.QuickSpawnItem(mod.ItemType("EnvyThrow"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 1)
				{
					player.QuickSpawnItem(mod.ItemType("GreenEyedBeast"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 2)
				{
					player.QuickSpawnItem(mod.ItemType("BloomingEnvyStaff"));
					x += Main.rand.Next(2) + 1;
				}
				else if (x == 3)
				{
					player.QuickSpawnItem(mod.ItemType("BulbStaff"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
					}
				else if (x == 4)
				{
					player.QuickSpawnItem(mod.ItemType("GreenEyeDagger"));
					x += Main.rand.Next(2) + 1;
					if (x > 4)
						x -= 5;
					}
				}
			
			x = (Main.rand.Next(10) + 6);
			for (int i = 0 ; i <= x ; i++)
				player.QuickSpawnItem(mod.ItemType("EnvyCrystal"));
			
			x = (Main.rand.Next(2) + 1);
			for (int i = 0 ; i <= x ; i++)
				player.QuickSpawnItem(mod.ItemType("SCGreenEyedBeast"));
		}
	}
}