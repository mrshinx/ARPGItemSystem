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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Terraria.WorldBuilding;
using Terraria.Localization;

namespace ARPGItemSystem.Common.GlobalItems.Weapon
{
    public class WeaponManager : GlobalItem
    {
        public List<WeaponModifier> modifierList = new List<WeaponModifier>();
        public override bool InstancePerEntity => true;

        // This is needed to make sure reference types are cloned properly to new instances
        public override GlobalItem Clone(Item from, Item to)
        {
            var clone = base.Clone(from, to);
            ((WeaponManager)clone).modifierList = modifierList.ToList();
            return clone;
        }

        // Clear vanilla modifier system
        public override bool? PrefixChance(Item item, int pre, UnifiedRandom rand)
        {
            return pre == -3;
        }

        // Only applies to weapon in this script
        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            //Apply to weapons
            return lateInstantiation && entity.damage > 0 && !(entity.maxStack > 1);
        }

        // Roll modifiers on item creation
        public override void OnCreated(Item item, ItemCreationContext context)
        {
            Reroll(item);
        }

        public void Reroll(Item item)
        {
            modifierList.Clear();
            // Add prefixes
            for (int i = 0; i < utils.GetAmountOfPrefixesWeapon(); i++)
            {
                List<int> excludeList = utils.CreateExcludeList(modifierList, ModifierType.Prefix);
                int tier = utils.GetTier();
                modifierList.Add(new WeaponModifier(ModifierType.Prefix, excludeList, item.DamageType, tier));
            }
            // Add suffixes
            for (int i = 0; i < utils.GetAmountOfSuffixesWeapon(); i++)
            {
                List<int> excludeList = utils.CreateExcludeList(modifierList, ModifierType.Suffix);
                int tier = utils.GetTier();
                modifierList.Add(new WeaponModifier(ModifierType.Suffix, excludeList, item.DamageType, tier));
            }
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            foreach (var modifier in modifierList)
            {
                switch (modifier.prefixType)
                {
                    case PrefixType.FlatDamageIncrease:
                        damage.Base += modifier.magnitude / 100f * item.OriginalDamage;
                        break;
                    case PrefixType.PercentageDamageIncrease:
                        damage *= 1 + modifier.magnitude / 100f;
                        break;
                    case PrefixType.FlatArmorPen:
                        player.GetArmorPenetration(DamageClass.Generic) += modifier.magnitude;
                        break;
                }
            }
        }

        public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
        {
            foreach (var modifier in modifierList)
            {
                switch (modifier.suffixType)
                {
                    case SuffixType.FlatCritChance:
                        crit += modifier.magnitude;
                        break;
                    case SuffixType.PercentageCritChance:
                        crit *= 1 + modifier.magnitude / 100f;
                        break;
                }
            }
        }

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            foreach (var modifier in modifierList)
            {
                switch (modifier.prefixType)
                {
                    case PrefixType.PercentageArmorPen:
                        modifiers.ScalingArmorPenetration += modifier.magnitude / 100f;
                        break;
                }
                switch (modifier.suffixType)
                {
                    case SuffixType.CritMultiplier:
                        modifiers.CritDamage += modifier.magnitude / 100f;
                        break;
                }
            }
        }
        public override void ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback)
        {
            foreach (var modifier in modifierList)
            {
                if (modifier.prefixType == PrefixType.KnockbackIncrease)
                {
                    knockback += modifier.magnitude / 100f;
                }
            }
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            foreach (var modifier in modifierList)
            {
                if (modifier.suffixType == SuffixType.VelocityIncrease)
                {
                    velocity *= 1 + modifier.magnitude / 100f;
                }
            }
        }

        public override float UseSpeedMultiplier(Item item, Player player)
        {
            foreach (var modifier in modifierList)
            {
                if (modifier.prefixType == PrefixType.AttackSpeedIncrease)
                {
                    return base.UseSpeedMultiplier(item, player) + modifier.magnitude / 100f;
                }
            }
            return base.UseSpeedMultiplier(item, player);
        }

        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            foreach (var modifier in modifierList)
            {
                if (modifier.suffixType == SuffixType.ManaCostReduction)
                {
                    reduce -= modifier.magnitude / 100f;
                }
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var useManaTip = tooltips.FirstOrDefault(tip => tip.Name == "UseMana" && tip.Mod == "Terraria");
            if (useManaTip is not null)
            {
                useManaTip.Text = Language.GetTextValue("CommonItemTooltip.UsesMana", Main.LocalPlayer.GetManaCost(item));
            }

            foreach (var modifier in modifierList)
            {
                if (modifier.modifierType == ModifierType.Prefix)
                    tooltips.Add(new TooltipLine(Mod, "CustomPrefix", string.Format(modifier.tooltip, modifier.magnitude)) { OverrideColor = Color.LightGreen });
                else
                    tooltips.Add(new TooltipLine(Mod, "CustomPrefix", string.Format(modifier.tooltip, modifier.magnitude)) { OverrideColor = Color.DeepSkyBlue });
            }
        }
        public override void SaveData(Item item, TagCompound tag)
        {
            List<int> prefixIDList, prefixMagnitudeList, suffixIDList, suffixMagnitudeList;
            List<string> prefixTooltipList, suffixTooltipList;
            SerializeData(out prefixIDList, out prefixMagnitudeList, out prefixTooltipList, out suffixIDList, out suffixMagnitudeList, out suffixTooltipList);

            tag["PrefixIDList"] = prefixIDList;
            tag["PrefixMagnitudeList"] = prefixMagnitudeList;
            tag["PrefixTooltipList"] = prefixTooltipList;

            tag["SuffixIDList"] = suffixIDList;
            tag["SuffixMagnitudeList"] = suffixMagnitudeList;
            tag["SuffixTooltipList"] = suffixTooltipList;
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            List<int> prefixIDList = (List<int>)tag.GetList<int>("PrefixIDList");
            List<int> prefixMagnitudeList = (List<int>)tag.GetList<int>("PrefixMagnitudeList");
            List<string> prefixTooltipList = (List<string>)tag.GetList<string>("PrefixTooltipList");

            List<int> suffixIDList = (List<int>)tag.GetList<int>("SuffixIDList");
            List<int> suffixMagnitudeList = (List<int>)tag.GetList<int>("SuffixMagnitudeList");
            List<string> suffixTooltipList = (List<string>)tag.GetList<string>("SuffixTooltipList");

            for (int i = 0; i < prefixIDList.Count; i++)
            {
                modifierList.Add(new WeaponModifier(ModifierType.Prefix, prefixMagnitudeList[i], prefixTooltipList[i], (PrefixType)prefixIDList[i], SuffixType.None));
            }
            for (int i = 0; i < suffixIDList.Count; i++)
            {
                modifierList.Add(new WeaponModifier(ModifierType.Suffix, suffixMagnitudeList[i], suffixTooltipList[i], PrefixType.None, (SuffixType)suffixIDList[i]));
            }
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            List<int> prefixIDList, prefixMagnitudeList, suffixIDList, suffixMagnitudeList;
            List<string> prefixTooltipList, suffixTooltipList;
            SerializeData(out prefixIDList, out prefixMagnitudeList, out prefixTooltipList, out suffixIDList, out suffixMagnitudeList, out suffixTooltipList);

            writer.Write(prefixIDList.Count);
            foreach (var prefixID in prefixIDList)
            {
                writer.Write(prefixID);
            }
            writer.Write(prefixMagnitudeList.Count);
            foreach (var prefixMagnitude in prefixMagnitudeList)
            {
                writer.Write(prefixMagnitude);
            }
            writer.Write(prefixTooltipList.Count);
            foreach (var prefixTooltip in prefixTooltipList)
            {
                writer.Write(prefixTooltip);
            }
            writer.Write(suffixIDList.Count);
            foreach (var suffixID in suffixIDList)
            {
                writer.Write(suffixID);
            }
            writer.Write(suffixMagnitudeList.Count);
            foreach (var suffixMagnitude in suffixMagnitudeList)
            {
                writer.Write(suffixMagnitude);
            }
            writer.Write(suffixTooltipList.Count);
            foreach (var suffixTooltip in suffixTooltipList)
            {
                writer.Write(suffixTooltip);
            }
        }
        public override void NetReceive(Item item, BinaryReader reader)
        {
            List<int> prefixIDList = new List<int>(), prefixMagnitudeList = new List<int>(), suffixIDList = new List<int>(), suffixMagnitudeList = new List<int>();
            List<string> prefixTooltipList = new List<string>(), suffixTooltipList = new List<string>();

            var prefixIDListCount = reader.ReadInt32();
            for (int i = 0; i < prefixIDListCount; i++)
            {
                prefixIDList.Add(reader.ReadInt32());
            }
            var prefixMagnitudeListCount = reader.ReadInt32();
            for (int i = 0; i < prefixMagnitudeListCount; i++)
            {
                prefixMagnitudeList.Add(reader.ReadInt32());
            }
            var prefixTooltipListCount = reader.ReadInt32();
            for (int i = 0; i < prefixTooltipListCount; i++)
            {
                prefixTooltipList.Add(reader.ReadString());
            }
            var suffixIDListCount = reader.ReadInt32();
            for (int i = 0; i < suffixIDListCount; i++)
            {
                suffixIDList.Add(reader.ReadInt32());
            }
            var suffixMagnitudeListCount = reader.ReadInt32();
            for (int i = 0; i < suffixMagnitudeListCount; i++)
            {
                suffixMagnitudeList.Add(reader.ReadInt32());
            }
            var suffixTooltipListCount = reader.ReadInt32();
            for (int i = 0; i < suffixTooltipListCount; i++)
            {
                suffixTooltipList.Add(reader.ReadString());
            }

            for (int i = 0; i < prefixIDList.Count; i++)
            {
                modifierList.Add(new WeaponModifier(ModifierType.Prefix, prefixMagnitudeList[i], prefixTooltipList[i], (PrefixType)prefixIDList[i], SuffixType.None));
            }
            for (int i = 0; i < suffixIDList.Count; i++)
            {
                modifierList.Add(new WeaponModifier(ModifierType.Suffix, suffixMagnitudeList[i], suffixTooltipList[i], PrefixType.None, (SuffixType)suffixIDList[i]));
            }

        }

        private void SerializeData(out List<int> prefixIDList, out List<int> prefixMagnitudeList, out List<string> prefixTooltipList, out List<int> suffixIDList, out List<int> suffixMagnitudeList, out List<string> suffixTooltipList)
        {
            prefixIDList = new List<int>();
            prefixMagnitudeList = new List<int>();
            prefixTooltipList = new List<string>();
            suffixIDList = new List<int>();
            suffixMagnitudeList = new List<int>();
            suffixTooltipList = new List<string>();
            foreach (var modifier in modifierList)
            {
                if (modifier.modifierType == ModifierType.Prefix)
                {
                    prefixIDList.Add((int)modifier.prefixType);
                    prefixMagnitudeList.Add(modifier.magnitude);
                    prefixTooltipList.Add(modifier.tooltip);
                }
                else
                {
                    suffixIDList.Add((int)modifier.suffixType);
                    suffixMagnitudeList.Add(modifier.magnitude);
                    suffixTooltipList.Add(modifier.tooltip);
                }
            }
        }


    }
}
