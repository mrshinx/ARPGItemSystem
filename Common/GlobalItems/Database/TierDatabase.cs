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
    public struct Tier
    {
        public int minValue;
        public int maxValue;
        public Tier(int min, int max)
        {
            minValue = min; maxValue = max;
        }
    }
    public static class TierDatabase
    {
        public static Dictionary<Enum, List<Tier>> modifierTierDatabase = new Dictionary<Enum, List<Tier>>()
        {
            // Prefix
                // Weapon
            {Weapon.PrefixType.FlatDamageIncrease, new List<Tier>
            {
                new Tier(51,55),
                new Tier(46,50),
                new Tier(41,45),
                new Tier(36,40),
                new Tier(31,35),
                new Tier(26,30),
                new Tier(21,25),
                new Tier(16,20),
                new Tier(11,15),
                new Tier(05,10)
            } },
            {Weapon.PrefixType.PercentageDamageIncrease, new List<Tier>
            {
                new Tier(51,55),
                new Tier(46,50),
                new Tier(41,45),
                new Tier(36,40),
                new Tier(31,35),
                new Tier(26,30),
                new Tier(21,25),
                new Tier(16,20),
                new Tier(11,15),
                new Tier(05,10)
            }},
            {Weapon.PrefixType.FlatArmorPen, new List<Tier>
            {
                new Tier(46,50),
                new Tier(41,45),
                new Tier(36,40),
                new Tier(31,35),
                new Tier(26,30),
                new Tier(21,25),
                new Tier(16,20),
                new Tier(11,15),
                new Tier(06,10),
                new Tier(01,05),
            }},
            {Weapon.PrefixType.PercentageArmorPen, new List<Tier>
            {
                new Tier(28,30),
                new Tier(25,27),
                new Tier(22,24),
                new Tier(19,21),
                new Tier(16,18),
                new Tier(13,15),
                new Tier(10,12),
                new Tier(07,09),
                new Tier(04,06),
                new Tier(01,03)
            }},
            {Weapon.PrefixType.AttackSpeedIncrease, new List<Tier>
            {
                new Tier(31,35),
                new Tier(27,30),
                new Tier(23,26),
                new Tier(19,22),
                new Tier(16,18),
                new Tier(13,15),
                new Tier(10,12),
                new Tier(07,09),
                new Tier(04,06),
                new Tier(01,03)
            }},
            {Weapon.PrefixType.KnockbackIncrease, new List<Tier>
            {
                new Tier(91,100),
                new Tier(81,90),
                new Tier(71,80),
                new Tier(61,70),
                new Tier(51,60),
                new Tier(41,50),
                new Tier(31,40),
                new Tier(21,30),
                new Tier(11,20),
                new Tier(01,10)
            }},
                // Armor
            {Armor.PrefixType.FlatLifeIncrease, new List<Tier>
            {
                new Tier(46,50),
                new Tier(41,45),
                new Tier(36,40),
                new Tier(31,35),
                new Tier(26,30),
                new Tier(21,25),
                new Tier(16,20),
                new Tier(11,15),
                new Tier(06,10),
                new Tier(01,05)
            }},
            {Armor.PrefixType.FlatDefenseIncrease, new List<Tier>
            {
                new Tier(28,30),
                new Tier(25,27),
                new Tier(22,24),
                new Tier(19,21),
                new Tier(16,18),
                new Tier(13,15),
                new Tier(10,12),
                new Tier(07,09),
                new Tier(04,06),
                new Tier(01,03)
            }},
            {Armor.PrefixType.PercentageDefenseIncrease, new List<Tier>
            {
                new Tier(28,30),
                new Tier(25,27),
                new Tier(22,24),
                new Tier(19,21),
                new Tier(16,18),
                new Tier(13,15),
                new Tier(10,12),
                new Tier(07,09),
                new Tier(04,06),
                new Tier(01,03)
            }},
            {Armor.PrefixType.FlatManaIncrease, new List<Tier>
            {
                new Tier(46,50),
                new Tier(41,45),
                new Tier(36,40),
                new Tier(31,35),
                new Tier(26,30),
                new Tier(21,25),
                new Tier(16,20),
                new Tier(11,15),
                new Tier(06,10),
                new Tier(01,05)
            } },
                // Accessory
            {Accessory.PrefixType.FlatLifeIncrease, new List<Tier>
            {
                new Tier(13,15),
                new Tier(13,15),
                new Tier(10,12),
                new Tier(10,12),
                new Tier(05,09),
                new Tier(05,09),
                new Tier(04,06),
                new Tier(04,06),
                new Tier(01,03),
                new Tier(01,03)
            }},
            {Accessory.PrefixType.FlatDefenseIncrease, new List<Tier>
            {
                new Tier(05,06),
                new Tier(05,05),
                new Tier(04,04),
                new Tier(04,04),
                new Tier(03,03),
                new Tier(03,03),
                new Tier(02,02),
                new Tier(02,02),
                new Tier(01,01),
                new Tier(01,01)
            }},
            {Accessory.PrefixType.FlatManaIncrease, new List<Tier>
            {
                new Tier(28,30),
                new Tier(25,27),
                new Tier(22,24),
                new Tier(19,21),
                new Tier(16,18),
                new Tier(13,15),
                new Tier(10,12),
                new Tier(07,09),
                new Tier(04,06),
                new Tier(01,03)
            } },


            // Suffix
                // Weapon
            {Weapon.SuffixType.FlatCritChance, new List<Tier>
            {
                new Tier(19,20),
                new Tier(17,18),
                new Tier(15,16),
                new Tier(13,14),
                new Tier(11,12),
                new Tier(09,10),
                new Tier(07,08),
                new Tier(05,06),
                new Tier(03,04),
                new Tier(01,02)
            }},
            {Weapon.SuffixType.PercentageCritChance, new List<Tier>
            {
                new Tier(37,42),
                new Tier(32,36),
                new Tier(27,31),
                new Tier(23,26),
                new Tier(19,22),
                new Tier(15,18),
                new Tier(11,14),
                new Tier(07,10),
                new Tier(04,06),
                new Tier(01,03)
            }},
            {Weapon.SuffixType.CritMultiplier, new List<Tier>
            {
                new Tier(91,100),
                new Tier(81,90),
                new Tier(71,80),
                new Tier(61,70),
                new Tier(51,60),
                new Tier(41,50),
                new Tier(31,40),
                new Tier(21,30),
                new Tier(11,20),
                new Tier(01,10)
            }},
            {Weapon.SuffixType.ManaCostReduction, new List<Tier>
            {
                new Tier(28,30),
                new Tier(25,27),
                new Tier(22,24),
                new Tier(19,21),
                new Tier(16,18),
                new Tier(13,15),
                new Tier(10,12),
                new Tier(07,09),
                new Tier(04,06),
                new Tier(01,03)
            }},
            {Weapon.SuffixType.VelocityIncrease, new List<Tier>
            {
                new Tier(61,70),
                new Tier(51,60),
                new Tier(41,50),
                new Tier(31,40),
                new Tier(26,30),
                new Tier(21,25),
                new Tier(16,20),
                new Tier(11,15),
                new Tier(06,10),
                new Tier(01,05)
            }},
                // Armor
            {Armor.SuffixType.PercentageGenericDamageIncrease, new List<Tier>
            {
                new Tier(13,15),
                new Tier(11,12),
                new Tier(09,10),
                new Tier(07,08),
                new Tier(06,06),
                new Tier(05,05),
                new Tier(04,04),
                new Tier(03,03),
                new Tier(02,02),
                new Tier(01,01)
            }},
            {Armor.SuffixType.PercentageMeleeDamageIncrease, new List<Tier>
            {
                new Tier(13,20),
                new Tier(11,18),
                new Tier(09,16),
                new Tier(07,14),
                new Tier(06,12),
                new Tier(05,10),
                new Tier(04,08),
                new Tier(03,06),
                new Tier(02,04),
                new Tier(01,02)
            }},
            {Armor.SuffixType.PercentageRangedDamageIncrease, new List<Tier>
            {
                new Tier(13,20),
                new Tier(11,18),
                new Tier(09,16),
                new Tier(07,14),
                new Tier(06,12),
                new Tier(05,10),
                new Tier(04,08),
                new Tier(03,06),
                new Tier(02,04),
                new Tier(01,02)
            }},
            {Armor.SuffixType.PercentageMagicDamageIncrease, new List<Tier>
            {
                new Tier(13,20),
                new Tier(11,18),
                new Tier(09,16),
                new Tier(07,14),
                new Tier(06,12),
                new Tier(05,10),
                new Tier(04,08),
                new Tier(03,06),
                new Tier(02,04),
                new Tier(01,02)
            }},
            {Armor.SuffixType.PercentageSummonDamageIncrease, new List<Tier>
            {
                new Tier(13,20),
                new Tier(11,18),
                new Tier(09,16),
                new Tier(07,14),
                new Tier(06,12),
                new Tier(05,10),
                new Tier(04,08),
                new Tier(03,06),
                new Tier(02,04),
                new Tier(01,02)
            }},
            {Armor.SuffixType.FlatCritChance, new List<Tier>
            {
                new Tier(05,10),
                new Tier(05,10),
                new Tier(04,08),
                new Tier(04,08),
                new Tier(03,06),
                new Tier(03,06),
                new Tier(02,04),
                new Tier(02,04),
                new Tier(01,02),
                new Tier(01,02)
            }},
            {Armor.SuffixType.ManaCostReduction, new List<Tier>
            {
                new Tier(05,10),
                new Tier(04,08),
                new Tier(03,06),
                new Tier(03,06),
                new Tier(03,06),
                new Tier(02,04),
                new Tier(02,04),
                new Tier(02,04),
                new Tier(01,02),
                new Tier(01,02)
            }},
                // Accessory
            {Accessory.SuffixType.PercentageGenericDamageIncrease, new List<Tier>
            {
                new Tier(04,05),
                new Tier(04,05),
                new Tier(03,04),
                new Tier(03,04),
                new Tier(02,03),
                new Tier(02,03),
                new Tier(01,02),
                new Tier(01,02),
                new Tier(01,01),
                new Tier(01,01)
            }},
            {Accessory.SuffixType.PercentageMeleeDamageIncrease, new List<Tier>
            {
                new Tier(06,07),
                new Tier(05,06),
                new Tier(04,05),
                new Tier(04,05),
                new Tier(03,04),
                new Tier(03,04),
                new Tier(02,03),
                new Tier(02,03),
                new Tier(01,02),
                new Tier(01,02)
            }},
            {Accessory.SuffixType.PercentageRangedDamageIncrease, new List<Tier>
            {
                new Tier(06,07),
                new Tier(05,06),
                new Tier(04,05),
                new Tier(04,05),
                new Tier(03,04),
                new Tier(03,04),
                new Tier(02,03),
                new Tier(02,03),
                new Tier(01,02),
                new Tier(01,02)
            }},
            {Accessory.SuffixType.PercentageMagicDamageIncrease, new List<Tier>
            {
                new Tier(06,07),
                new Tier(05,06),
                new Tier(04,05),
                new Tier(04,05),
                new Tier(03,04),
                new Tier(03,04),
                new Tier(02,03),
                new Tier(02,03),
                new Tier(01,02),
                new Tier(01,02)
            }},
            {Accessory.SuffixType.PercentageSummonDamageIncrease, new List<Tier>
            {
                new Tier(06,07),
                new Tier(05,06),
                new Tier(04,05),
                new Tier(04,05),
                new Tier(03,04),
                new Tier(03,04),
                new Tier(02,03),
                new Tier(02,03),
                new Tier(01,02),
                new Tier(01,02)
            }},
            {Accessory.SuffixType.FlatCritChance, new List<Tier>
            {
                new Tier(03,04),
                new Tier(03,04),
                new Tier(03,04),
                new Tier(02,03),
                new Tier(02,03),
                new Tier(02,03),
                new Tier(01,02),
                new Tier(01,02),
                new Tier(01,02),
                new Tier(01,02)
            }},
            {Accessory.SuffixType.ManaCostReduction, new List<Tier>
            {
                new Tier(04,05),
                new Tier(03,04),
                new Tier(03,04),
                new Tier(02,03),
                new Tier(02,03),
                new Tier(02,02),
                new Tier(01,02),
                new Tier(01,02),
                new Tier(01,01),
                new Tier(01,01)
            }},
        };
    }
}
