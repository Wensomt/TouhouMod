using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TouhouMod.NPCs
{
	public class TouhouGlobalNPC : GlobalNPC
	{
        public override void NPCLoot(NPC npc)
		{
            if(npc.type == NPCID.EyeofCthulhu)
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("YinYangOrb"));
        }



    }
}