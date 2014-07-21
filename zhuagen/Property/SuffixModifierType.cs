using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


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

#region 后缀列表

/// <summary>
/// 词缀:学者,属性:智力
/// </summary>
public class SavantModifierType : SuffixModifierType {
    public override string ModifierName {
        get { return "学者之"; }
    }

    public override string TargetName {
        get { return "智力"; }
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

}




/// <summary>
/// 词缀:猫鼬,属性:敏捷
/// </summary>
public class MongooseModifierType : SuffixModifierType {
    public override string ModifierName {
        get { return "猫鼬之"; }
    }


    public override Dictionary<int, float[]> ValueRange {
        get {
            return new Dictionary<int, float[]>(){
                {1,new float[]{1,5}},
                {11,new float[]{6,11}},
                {22,new float[]{12,20}}
            };
        }
    }

    public override string TargetName {
        get { return "敏捷"; }
    }

    public float ApplyModifier(Modifier m, float InitialValue) {
        return m.ModifierValue + InitialValue;
    }
}


/// <summary>
/// 词缀:野蛮,属性:力量
/// </summary>
public class BruteModifierType : SuffixModifierType {
    public override string ModifierName {
        get { return "野蛮之"; }
    }

    public override Dictionary<int, float[]> ValueRange {
        get {
            return new Dictionary<int, float[]>(){
                {1,new float[]{1,5}},
                {11,new float[]{6,11}},
                {22,new float[]{12,20}}
            };
        }
    }

    public override string TargetName {
        get { return "力量"; }
    }

    public float ApplyModifier(Modifier m, float InitialValue) {
        return m.ModifierValue + InitialValue;
    }
}

#endregion
