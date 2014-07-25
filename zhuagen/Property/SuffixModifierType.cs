using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;




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
}

#endregion
