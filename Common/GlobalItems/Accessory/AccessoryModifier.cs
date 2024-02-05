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
using ARPGItemSystem.Common.GlobalItems.Database;

namespace ARPGItemSystem.Common.GlobalItems.Accessory
{
    public enum ModifierType
    {
        None,
        Prefix,
        Suffix
    }
    public enum PrefixType
    {
        None, // 0
        FlatLifeIncrease, // 1
        FlatDefenseIncrease, // 2
        FlatManaIncrease, // 3
    }
    public enum SuffixType
    {
        None, // 0
        PercentageGenericDamageIncrease, // 1
        PercentageMeleeDamageIncrease, // 2
        PercentageRangedDamageIncrease, // 3
        PercentageMagicDamageIncrease, // 4
        PercentageSummonDamageIncrease, // 5
        FlatCritChance, // 6
        ManaCostReduction, // 7
    }

    public struct AccessoryModifier
    {
        public ModifierType modifierType;
        public PrefixType prefixType = PrefixType.None;
        public SuffixType suffixType = SuffixType.None;
        public int magnitude = 0;
        public string tooltip = "";

        public AccessoryModifier(ModifierType type, int magnitude, string tooltip, PrefixType prefixType = PrefixType.None, SuffixType suffixType = SuffixType.None)
        {
            // Indicate if a prefix or suffix is being generated
            modifierType = type;
            this.magnitude = magnitude;
            this.tooltip = tooltip;
            this.prefixType = prefixType;
            this.suffixType = suffixType;
        }

        public AccessoryModifier(ModifierType type, List<int> excludeList, int tier = 0)
        {
            // Indicate if a prefix or suffix is being generated
            modifierType = type;
            GenerateModifier(modifierType, excludeList, tier);
        }

        public void GenerateModifier(ModifierType type, List<int> excludeList, int tier = 0)
        {
            List<int> IDs = new List<int>();
            Random random = new Random();

            if (type == ModifierType.Prefix)
            {
                IDs.AddRange(Enumerable.Range(1, Enum.GetNames(typeof(PrefixType)).Length - 1));
                // Exclude modifiers that already on the item
                IDs = IDs.Where(val => !excludeList.Contains(val)).ToList();
                // Generate random prefix
                prefixType = (PrefixType)IDs[random.Next(0, IDs.Count)];
                // Get magnitude based on tier
                magnitude = random.Next(TierDatabase.modifierTierDatabase[prefixType][tier].minValue, TierDatabase.modifierTierDatabase[prefixType][tier].maxValue + 1);
                // Get display tooltip
                tooltip = TooltipDatabase.modifierTooltipDatabase[prefixType];
            }
            if (type == ModifierType.Suffix)
            {
                IDs.AddRange(Enumerable.Range(1, Enum.GetNames(typeof(SuffixType)).Length - 1));
                // Exclude modifiers that already on the item
                IDs = IDs.Where(val => !excludeList.Contains(val)).ToList();
                // Generate random suffix
                suffixType = (SuffixType)IDs[random.Next(0, IDs.Count)];
                // Get magnitude based on tier
                magnitude = random.Next(TierDatabase.modifierTierDatabase[suffixType][tier].minValue, TierDatabase.modifierTierDatabase[suffixType][tier].maxValue + 1);
                // Get display tooltip
                tooltip = TooltipDatabase.modifierTooltipDatabase[suffixType];
            }
        }

    }
}
