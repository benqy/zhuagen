using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


public class Modifier {

    public Modifier(ModifierType modifierType,int equipmentLv) {
        this.ModifierType = modifierType;
        this.ModifierValue = this.ModifierType.GeneralModifierValue(equipmentLv);
    }

    /// <summary>
    /// 影响的属性名称
    /// </summary>
    public string TargetName {
        get {
            return this.ModifierType.TargetName;
        }
    }

    /// <summary>
    /// 词缀值
    /// </summary>
    public float ModifierValue { get; set; }

    /// <summary>
    /// 词缀类型
    /// </summary>
    public ModifierType ModifierType { get; set; }

    /// <summary>
    /// 应用词缀值到属性
    /// </summary>
    /// <param name="a"></param>
    public void Apply(Attri a) {
        a.Value = ModifierType.ApplyModifier(this, a.Value);
    }
}


public enum AffixType {
    /// <summary>
    /// 装备前缀
    /// </summary>
    Prefix = 0,
    /// <summary>
    /// 装备后缀
    /// </summary>
    Suffix = 1
}

/// <summary>
/// 抽象词缀类型,继承此类实现具体的不同装备词缀
/// </summary>
public abstract class ModifierType {
    /// <summary>
    /// 词缀名称
    /// </summary>
    public abstract string ModifierName { get; }

    public abstract string TargetName { get; }

    /// <summary>
    /// 前缀or后缀
    /// </summary>
    public abstract AffixType Affix { get; }

    public abstract Dictionary<int, float[]> ValueRange { get; }

    /// <summary>
    /// 生成随机的词缀值
    /// </summary>
    /// <param name="equipMentLv"></param>
    /// <returns></returns>
    public float GeneralModifierValue(int equipMentLv) {
        var value = 0;
        var range = this.ValueRange
                        .OrderByDescending(t => t.Key)
                        .First(t => t.Key <= equipMentLv).Value;
        value = BQUtil.Random((int)range[0], (int)range[1]);
        return value;
    }

    /// <summary>
    /// 应用词缀值到词缀的属性,不用类型的属性,叠加方式可能不同,比如more=直接相加,increase=递减相加
    /// </summary>
    /// <param name="m">词缀</param>
    /// <param name="InitialValue">初始值</param>
    /// <returns></returns>
    public float ApplyModifier(Modifier m, float InitialValue) {
        return m.ModifierValue + InitialValue;
    }
}


///// <summary>
///// 词缀值
///// </summary>
//public class ModeifierValue {

//    public string Name { get; set; }

//    public float Level { get; set; }

//    public float[] Minimum { get; set; }

//    public float[] Maximum { get; set; }

//}

///// <summary>
///// 词缀范围
///// </summary>
//public class ModeifierRange {
//    public List<ModeifierValue> Ranges { get; set; }

//    public string TragetName { get; set; }

//    public float GeneralByLevel(float level) {
//        var value = 0f;
//        var currrRanges = Ranges.Where(t => t.Level <= level);
//        var currentValue = currrRanges.ToArray()[BQUtil.Random(0, currrRanges.Count())];

//        return value;
//    }
//}


/// <summary>
/// 后缀类型的词缀继承此类
/// </summary>
public abstract class PrefixModifierType : ModifierType {

    /// <summary>
    /// 存储所有前缀列表
    /// </summary>
    public static List<ModifierType> PrefixModifierTypes = new List<ModifierType>();

    public override AffixType Affix {
        get { return AffixType.Prefix; }
    }

    private static bool IsSubClassOf(Type type, Type baseType) {
        var b = type.BaseType;
        while (b != null) {
            if (b.Equals(baseType)) {
                return true;
            }
            b = b.BaseType;
        }
        return false;
    }


    /// <summary>
    /// 初始化所有后缀列表
    /// </summary>
    public static void InitPrefixList() {
        var suffixTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                          where IsSubClassOf(t, typeof(PrefixModifierType))
                          select t;

        foreach (var type in suffixTypes) {
            PrefixModifierTypes.Add((ModifierType)Activator.CreateInstance(type));
        }
    }


}

/// <summary>
/// 后缀类型的词缀继承此类
/// </summary>
public abstract class SuffixModifierType : ModifierType {

    //public SuffixModifierType() {
    //    SuffixModifierType.SuffixModifierTypes.Add(this);
    //}

    /// <summary>
    /// 存储所有后缀列表
    /// </summary>
    public static List<ModifierType> SuffixModifierTypes = new List<ModifierType>();

    public override AffixType Affix {
        get { return AffixType.Suffix; }
    }

    private static bool IsSubClassOf(Type type, Type baseType) {
        var b = type.BaseType;
        while (b != null) {
            if (b.Equals(baseType)) {
                return true;
            }
            b = b.BaseType;
        }
        return false;
    }

    /// <summary>
    /// 初始化所有后缀列表
    /// </summary>
    public static void InitSuffixList() {
        var suffixTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                          where IsSubClassOf(t, typeof(SuffixModifierType))
                          select t;

        foreach (var type in suffixTypes) {
            SuffixModifierTypes.Add((ModifierType)Activator.CreateInstance(type));
        }
    }
}