using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


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
/// 词缀:火星,属性:火炕
/// </summary>
public class MarModifierType : PrefixModifierType {
    public override string ModifierName {
        get { return "火星"; }
    }

    public override string TargetName {
        get { return "火抗"; }
    }

    //词缀范围
    public override Dictionary<int, float[]> ValueRange {
        get {
            return new Dictionary<int, float[]>(){
                {1,new float[]{1,5}},
                {11,new float[]{6,11}},
                {22,new float[]{12,20}}
            };
        }
    }

    public override float ApplyModifier(Modifier m, float InitialValue) {
        return m.ModifierValue + InitialValue;
    }
}



/// <summary>
/// 词缀:冰霜,属性:冰抗
/// </summary>
public class IceModifierType : PrefixModifierType {
    public override string ModifierName {
        get { return "冰霜"; }
    }

    public override string TargetName {
        get { return "冰抗"; }
    }

    //词缀范围
    public override Dictionary<int, float[]> ValueRange {
        get {
            return new Dictionary<int, float[]>(){
                {1,new float[]{1,5}},
                {11,new float[]{6,11}},
                {22,new float[]{12,20}}
            };
        }
    }

    public override float ApplyModifier(Modifier m, float InitialValue) {
        return m.ModifierValue + InitialValue;
    }
}

/// <summary>
/// 词缀:剧毒,属性:毒抗
/// </summary>
public class PosionModifierType : PrefixModifierType {
    public override string ModifierName {
        get { return "剧毒"; }
    }

    public override string TargetName {
        get { return "毒抗"; }
    }

    //词缀范围
    public override Dictionary<int, float[]> ValueRange {
        get {
            return new Dictionary<int, float[]>(){
                {1,new float[]{1,5}},
                {11,new float[]{6,11}},
                {22,new float[]{12,20}}
            };
        }
    }

    public override float ApplyModifier(Modifier m, float InitialValue) {
        return m.ModifierValue + InitialValue;
    }
}