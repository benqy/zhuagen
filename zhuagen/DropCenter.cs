﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DropCenter {
    private DropCenter() {

    }

    public static DropCenter Instance = new DropCenter();

    public bool CheckDrop(){
        return true;
    }

    public Equipment DropEquipment() {
        SuffixModifierType.InitSuffixList();
        PrefixModifierType.InitPrefixList();
        var equip = new Equipment();
        var r = new Random();
        var rarityArr =new Int32[]{0,2,4,6,7};
        equip.Rarity = Rarity.史诗;
        equip.EnquipmentType = (EnquipmentType)r.Next(10);
        equip.Level = r.Next(1,23);
        var modifierCount = (int)equip.Rarity;
        //随机装备后缀
        while (equip.ItemModifiers.Count < modifierCount/2) {
            var modifierType = SuffixModifierType.SuffixModifierTypes[r.Next(0, SuffixModifierType.SuffixModifierTypes.Count)];
            //如果已经包含此属性,则跳过
            if (!equip.ItemModifiers.Select(t => t.ModifierType).Contains(modifierType)) {
                equip.ItemModifiers.Add(new Modifier(modifierType,equip.Level));
            }            
        }

        while (equip.ItemModifiers.Count < modifierCount) {
            var modifierType = PrefixModifierType.PrefixModifierTypes[r.Next(0, PrefixModifierType.PrefixModifierTypes.Count)];
            //如果已经包含此属性,则跳过
            if (!equip.ItemModifiers.Select(t => t.ModifierType).Contains(modifierType)) {
                equip.ItemModifiers.Add(new Modifier(modifierType, equip.Level));
            }
        }
        return equip;
    }
}