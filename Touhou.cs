//css_ref Terraria.dll
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
//using ReLogic.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using System;
using System.Threading;
namespace TouhouMod
{
	public class TouhouMod : Mod
	{
		
		/*public const string rumiaHead = "Touhou/NPCs/Bosses/EoSD/Rumia/Rumia_Head_Boss";
		public const string parseeHead = "Touhou/NPCs/Bosses/SA/Parsee/Parsee_Head_Boss";
		public const string aliceHead = "Touhou/NPCs/Bosses/PCB/Alice/Alice_Head_Boss";
		public const string marisaHead = "Touhou/NPCs/Bosses/IN/Marisa/Marisa_Head_Boss";
		AddBossHeadTexture(rumiaHead);
		AddBossHeadTexture(parseeHead);
		AddBossHeadTexture(aliceHead);
		AddBossHeadTexture(marisaHead);*/
		
		public TouhouMod()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
                AutoloadGores = true,
				AutoloadSounds = true,
			};
		}
		public override void Load()
		{
			
			if (!Main.dedServ)
			{
                if (Main.expertMode)
                {
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/RumiaEx"), ItemType("RumiaTrophy"), TileType("RumiaTrophy"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/ParseeEx"), ItemType("ParseeTrophy"), TileType("ParseeTrophy"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/MarisaEx"), ItemType("MarisaTrophy"), TileType("MarisaTrophy"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/AliceEx"), ItemType("AliceTrophy"), TileType("AliceTrophy")); 
                    //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/SakuyaEx"), ItemType("SakuyaTrophy"), TileType("SakuyaTrophy")); 
                }
                else
                {
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Rumia"), ItemType("RumiaTrophy"), TileType("RumiaTrophy"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Parsee"), ItemType("ParseeTrophy"), TileType("ParseeTrophy"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Marisa"), ItemType("MarisaTrophy"), TileType("MarisaTrophy"));
                    AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Alice"), ItemType("AliceTrophy"), TileType("AliceTrophy")); 
                    //AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/Sakuya"), ItemType("SakuyaTrophy"), TileType("SakuyaTrophy")); 
                }
	
			}
			
			
			
			
		}
	    //[Obsolete]
		/*public override void UpdateMusic(ref int music, ref MusicPriority priority)
		{
			if (Main.myPlayer != -1 && !Main.gameMenu)
			{
				if (Main.LocalPlayer.active && Main.LocalPlayer.FindBuffIndex(this.BuffType("RumiaPresence")) != -1)
				{
					if (Main.expertMode)
					{
                    music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/RumiaB");
					}
					else
					{
					music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/RumiaA");
					}
				}
				
				if (Main.LocalPlayer.active && Main.LocalPlayer.FindBuffIndex(this.BuffType("ParseePresence")) != -1)
				{
					if (Main.expertMode)
					{
                    music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/ParseeB");
					}
					else
					{
					music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/ParseeA");
					}
				}
				
				if (Main.LocalPlayer.active && Main.LocalPlayer.FindBuffIndex(this.BuffType("MarisaPresence")) != -1)
				{
					if (Main.expertMode)
					{
                    music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/MarisaB");
					}
					else
					{
					music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/MarisaA");
					}
				}
				
				if (Main.LocalPlayer.active && Main.LocalPlayer.FindBuffIndex(this.BuffType("AlicePresence")) != -1)
				{
					if (Main.expertMode)
					{
                    music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/AliceB");
					}
					else
					{
					music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/AliceA");
					}
				}
			}
		}*/
		
	}
}