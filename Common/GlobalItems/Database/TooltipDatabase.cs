using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader.IO;
using System.IO;
using Terraria.Utilities;
using System;
using System.Linq.Expressions;
using log4net.Core;
using System.Linq;
using ARPGItemSystem.Common.GlobalItems.Weapon;

namespace ARPGItemSystem.Common.GlobalItems.Database
{
    public static class TooltipDatabase
    {
        public static Dictionary<Enum, string> modifierTooltipDatabase = new Dictionary<Enum, string>()
        {
            // Prefix
                // Weapon
            {Weapon.PrefixType.FlatDamageIncrease, "{0}% Increased Base Damage" },
            {Weapon.PrefixType.PercentageDamageIncrease, "{0}% Increased Damage" },
            {Weapon.PrefixType.FlatArmorPen, "{0} Added Armor Penetration" },
            {Weapon.PrefixType.PercentageArmorPen, "Ignore {0}% of target defense" },
            {Weapon.PrefixType.AttackSpeedIncrease, "{0}% Increased Attack Speed" },
            {Weapon.PrefixType.KnockbackIncrease, "{0}% Increased Knockback" },
                // Armor
            {Armor.PrefixType.FlatLifeIncrease, "+{0} Maximum Life" },
            {Armor.PrefixType.FlatDefenseIncrease, "{0}% Increased Base Defense" },
            {Armor.PrefixType.PercentageDefenseIncrease, "{0}% Increased Defense" },
            {Armor.PrefixType.FlatManaIncrease, "+{0} Maximum Mana" },
                // Accessory
            {Accessory.PrefixType.FlatLifeIncrease, "+{0} Maximum Life" },
            {Accessory.PrefixType.FlatDefenseIncrease, "+{0} Additional Defense" },
            {Accessory.PrefixType.FlatManaIncrease, "+{0} Maximum Mana" },


            // Suffix
                // Weapon
            {Weapon.SuffixType.FlatCritChance, "{0}% Addtional Critical Strike Chance" },
            {Weapon.SuffixType.PercentageCritChance, "{0}% Increased Critical Strike Chance" },
            {Weapon.SuffixType.CritMultiplier, "{0}% Increased Critical Strike Damage" },
            {Weapon.SuffixType.ManaCostReduction, "{0}% Reduced Mana Cost" },
            {Weapon.SuffixType.VelocityIncrease, "{0}% Increased Projectile Velocity" },
                // Armor
            {Armor.SuffixType.PercentageGenericDamageIncrease, "{0}% Increased Damage" },
            {Armor.SuffixType.PercentageMeleeDamageIncrease, "{0}% Increased Melee Damage" },
            {Armor.SuffixType.PercentageRangedDamageIncrease, "{0}% Increased Ranged Damage" },
            {Armor.SuffixType.PercentageMagicDamageIncrease, "{0}% Increased Magic Damage" },
            {Armor.SuffixType.PercentageSummonDamageIncrease, "{0}% Increased Summon Damage" },
            {Armor.SuffixType.FlatCritChance, "{0}% Additional Critical Strike Chance" },
            {Armor.SuffixType.ManaCostReduction, "{0}% Reduced Mana Cost" },
                // Accessory
            {Accessory.SuffixType.PercentageGenericDamageIncrease, "{0}% Increased Damage" },
            {Accessory.SuffixType.PercentageMeleeDamageIncrease, "{0}% Increased Melee Damage" },
            {Accessory.SuffixType.PercentageRangedDamageIncrease, "{0}% Increased Ranged Damage" },
            {Accessory.SuffixType.PercentageMagicDamageIncrease, "{0}% Increased Magic Damage" },
            {Accessory.SuffixType.PercentageSummonDamageIncrease, "{0}% Increased Summon Damage" },
            {Accessory.SuffixType.FlatCritChance, "{0}% Additional Critical Strike Chance" },
            {Accessory.SuffixType.ManaCostReduction, "{0}% Reduced Mana Cost" },
        };
    }
}
