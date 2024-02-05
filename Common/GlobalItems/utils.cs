using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPGItemSystem.Common.GlobalItems.Accessory;
using ARPGItemSystem.Common.GlobalItems.Armor;
using ARPGItemSystem.Common.GlobalItems.Weapon;
using Terraria;

namespace ARPGItemSystem.Common.GlobalItems
{
    internal static class utils
    {
        internal static int GetAmountOfSuffixesWeapon()
        {
            int maxCount = 1;
            int minCount = 1;
            if (NPC.downedBoss2) maxCount += 1;
            if (Main.hardMode) minCount += 1;
            if (NPC.downedMechBossAny) maxCount += 1;

            Random random = new Random();
            return random.Next(minCount,maxCount+1);
        }
        internal static int GetAmountOfPrefixesWeapon()
        {
            int maxCount = 1;
            int minCount = 1;
            if (NPC.downedBoss3) maxCount += 1;
            if (Main.hardMode) minCount += 1;
            if (NPC.downedGolemBoss) maxCount += 1;

            Random random = new Random();
            return random.Next(minCount, maxCount + 1);
        }
        internal static int GetAmountOfSuffixesArmor()
        {
            int maxCount = 1;
            int minCount = 0;
            if (NPC.downedBoss2) minCount += 1;
            if (Main.hardMode) maxCount += 1;

            Random random = new Random();
            return random.Next(minCount, maxCount + 1);
        }
        internal static int GetAmountOfPrefixesArmor()
        {
            int maxCount = 1;
            int minCount = 1;
            if (NPC.downedGolemBoss) maxCount += 1;

            Random random = new Random();
            return random.Next(minCount, maxCount + 1);
        }
        internal static int GetAmountOfSuffixesAccessory()
        {
            int maxCount = 1;
            int minCount = 0;
            if (Main.hardMode) minCount += 1;

            Random random = new Random();
            return random.Next(minCount, maxCount + 1);
        }
        internal static int GetAmountOfPrefixesAccessory()
        {
            int maxCount = 1;
            int minCount = 0;
            if (NPC.downedGolemBoss) minCount += 1;

            Random random = new Random();
            return random.Next(minCount, maxCount + 1);
        }
        internal static int GetTier()
        {
            Random random = new Random();
            int maximumTier = 8;
            int minimumTier = 10;
            if (NPC.downedSlimeKing) maximumTier -= 1;
            if (NPC.downedBoss2) minimumTier -= 1;
            if (NPC.downedBoss3) maximumTier -= 1;
            if (Main.hardMode) maximumTier -= 1;
            if (NPC.downedQueenSlime) minimumTier -= 1;
            if (NPC.downedMechBossAny) maximumTier -= 1;
            if (NPC.downedGolemBoss) minimumTier -= 1;
            if (NPC.downedPlantBoss) { maximumTier -= 1; }
            if (NPC.downedFishron) { minimumTier -= 1; }
            if (NPC.downedEmpressOfLight) { maximumTier -= 1; minimumTier -= 1; }
            if (NPC.downedAncientCultist) { maximumTier -= 1; }
            if (NPC.downedMoonlord) { maximumTier -= 1; minimumTier -= 1; }

            return random.Next(maximumTier, minimumTier);
        }

        internal static List<int> CreateExcludeList(List<WeaponModifier> modifierList, Weapon.ModifierType type)
        {
            // Prepare both exclude list for prefix and suffix
            var excludePrefixListEnum = modifierList.Select(o => o.prefixType).ToList();
            var excludeSuffixListEnum = modifierList.Select(o => o.suffixType).ToList();
            List<int> excludeListInt = new List<int>();

            switch (type)
            {
                case Weapon.ModifierType.Prefix:
                    foreach (var item in excludePrefixListEnum)
                    {
                        excludeListInt.Add((int)item);
                    }
                    break;
                case Weapon.ModifierType.Suffix:
                    foreach (var item in excludeSuffixListEnum)
                    {
                        excludeListInt.Add((int)item);
                    }
                    break;
            }

            return excludeListInt;
        }
        internal static List<int> CreateExcludeList(List<ArmorModifier> modifierList, Armor.ModifierType type)
        {
            // Prepare both exclude list for prefix and suffix
            var excludePrefixListEnum = modifierList.Select(o => o.prefixType).ToList();
            var excludeSuffixListEnum = modifierList.Select(o => o.suffixType).ToList();
            List<int> excludeListInt = new List<int>();

            switch (type)
            {
                case Armor.ModifierType.Prefix:
                    foreach (var item in excludePrefixListEnum)
                    {
                        excludeListInt.Add((int)item);
                    }
                    break;
                case Armor.ModifierType.Suffix:
                    foreach (var item in excludeSuffixListEnum)
                    {
                        excludeListInt.Add((int)item);
                    }
                    break;
            }

            return excludeListInt;
        }
        internal static List<int> CreateExcludeList(List<AccessoryModifier> modifierList, Accessory.ModifierType type)
        {
            // Prepare both exclude list for prefix and suffix
            var excludePrefixListEnum = modifierList.Select(o => o.prefixType).ToList();
            var excludeSuffixListEnum = modifierList.Select(o => o.suffixType).ToList();
            List<int> excludeListInt = new List<int>();

            switch (type)
            {
                case Accessory.ModifierType.Prefix:
                    foreach (var item in excludePrefixListEnum)
                    {
                        excludeListInt.Add((int)item);
                    }
                    break;
                case Accessory.ModifierType.Suffix:
                    foreach (var item in excludeSuffixListEnum)
                    {
                        excludeListInt.Add((int)item);
                    }
                    break;
            }

            return excludeListInt;
        }
    }
}
