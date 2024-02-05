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
using ARPGItemSystem.Common.GlobalItems.Armor;

namespace ARPGItemSystem.Common.GlobalItems
{
    public class ProjectileManager : GlobalProjectile
    {
        public List<WeaponModifier> modifierList = new List<WeaponModifier>();
        public override bool InstancePerEntity => true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if((source is EntitySource_ItemUse_WithAmmo itemSource) && !(itemSource.Item.consumable) && !(itemSource.Item.fishingPole>0))
            {
                modifierList = itemSource.Item.GetGlobalItem<WeaponManager>().modifierList;
            }
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            foreach (var modifier in modifierList)
            {
                switch (modifier.prefixType)
                {
                    case Weapon.PrefixType.PercentageArmorPen:
                        modifiers.ScalingArmorPenetration += modifier.magnitude / 100f;
                        break;
                }
                switch (modifier.suffixType)
                {
                    case Weapon.SuffixType.CritMultiplier:
                        modifiers.CritDamage += modifier.magnitude / 100f;
                        break;
                }
            }
        }

    }
}
