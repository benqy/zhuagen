using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Equipment {
    //装备名称
    public string Name { get; set; }
    //装备序号
    public int ApplyOrder { get; set; }

    //装备词缀列表
    public List<Modifier> ItemModifiers { get; set; }
    
    //生成属性列表
    public Dictionary<string, Attri> ApplyModifiers(Dictionary<string, Attri> inParams) {
        var response = new Dictionary<string, Attri>();
        //遍历此装备拥有的词缀,并找到对应的属性并应用到属性上
        foreach (var m in ItemModifiers) {
            //目标属性不存在当前装备上的此词缀,则跳过
            if (inParams.ContainsKey(m.TargetName)) {
                //If this is the first time the response ran into it, add it
                if (!response.ContainsKey(m.TargetName)) {
                    response.Add(m.TargetName, inParams[m.TargetName]);
                }
                //将词缀的属性值加到目标属性上
                m.Apply(response[m.TargetName]);
            }
        }

        return response;
    }
}