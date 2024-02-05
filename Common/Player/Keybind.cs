using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using ARPGItemSystem.Common.Systems;
using Ionic.Zlib;
using Microsoft.Xna.Framework;
using ARPGItemSystem.Common.GlobalItems.Weapon;
using ARPGItemSystem.Common.GlobalItems.Armor;
using ARPGItemSystem.Common.GlobalItems.Accessory;
using System.Reflection.Metadata;
using System;
using Terraria.Graphics.CameraModifiers;

namespace ARPGItemSystem.Common.Players
{
    // See Common/Systems/KeybindSystem for keybind registration.
    public class Keybind : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            string type = "";
            if (KeybindSystem.CraftKeyBind.JustPressed)
            {
                // Check if item is valid
                if (Player.HeldItem.maxStack > 1)
                {
                    Main.NewText("Invalid item to craft");
                    return;
                }
                else if (Player.HeldItem.damage > 0) type = "weapon";
                else if (Player.HeldItem.accessory) type = "accessory";
                else if (!Player.HeldItem.vanity) type = "armor";
            

                // Calculate cost
                var itemValue = Player.HeldItem.value *2;

                int platinum = itemValue / 1000000;
                int gold = (itemValue / 10000) % 100;
                int silver = (itemValue / 100) % 100;
                int copper = itemValue % 100;

                string price = "";
                if (platinum > 0) price += $"{platinum} platinum ";
                if (gold > 0) price += $"{gold} gold ";
                if (silver > 0) price += $"{silver} silver ";
                if (copper > 0) price += $"{copper} copper ";
                if (platinum + gold + silver + copper == 0) price += $"{copper} copper ";
                //if (copper > 0) price += $"{copper} [i:71] ";

                // Check if player can afford the item cost
                if (Player.BuyItem(itemValue))
                {
                    // Pay the cost and reroll item
                    switch (type)
                    {
                        case "weapon":
                            Player.HeldItem.GetGlobalItem<WeaponManager>().Reroll(Player.HeldItem);
                            break;
                        case "armor":
                            Player.HeldItem.GetGlobalItem<ArmorManager>().Reroll(Player.HeldItem);
                            break;
                        case "accessory":
                            Player.HeldItem.GetGlobalItem<AccessoryManager>().Reroll(Player.HeldItem);
                            break;
                        case "":
                            Main.NewText("Invalid item to craft");
                            break;
                    }
                    Main.combatText[CombatText.NewText(Player.getRect(), Color.SpringGreen, $"Used {price}to reroll {Player.HeldItem.Name}")].lifeTime = 60;
                    type = "";
                }
                else
                {
                    Main.combatText[CombatText.NewText(Player.getRect(), Color.DarkRed, $"Need {price}to reroll {Player.HeldItem.Name}!")].lifeTime = 60;
                }
            }
        }
    }
}
