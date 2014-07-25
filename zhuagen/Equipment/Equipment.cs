using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//装备稀有度
public enum Rarity : int {
    普通 = 0,
    魔法 = 2,
    稀有 = 4,
    史诗 = 6,
    传奇 = 7
}

public enum EnquipmentType : int {
    头盔 = 0,
    衣服 = 1,
    护腿 = 2,
    手套 = 3,
    鞋子 = 4,
    护符 = 5,
    戒指 = 6,
    单手 = 7,
    双手 = 8,
    副手 = 9
}

public class Equipment {

    public Equipment() {
        this.ItemModifiers = new List<Modifier>();
    }

    /// <summary>
    /// 装备名称
    /// </summary>
    public string Name {
        get {
            var name = this.EnquipmentType.ToString();
            name = this.ItemModifiers[this.ItemModifiers.Count / 2].ModifierType.ModifierName + name;
            name = this.ItemModifiers[0].ModifierType.ModifierName + name;
            return name;
        }
    }

    /// <summary>
    /// 装备序号
    /// </summary>
    public int ApplyOrder { get; set; }

    /// <summary>
    /// 装备等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 稀有度
    /// </summary>
    public Rarity Rarity { get; set; }


    /// <summary>
    /// 装备类型
    /// </summary>
    public EnquipmentType EnquipmentType { get; set; }

    //public void AddModifiers(Modifier m) {
    //    this.ItemModifiers.Add(m);
    //}

    //装备词缀列表
    public List<Modifier> ItemModifiers { get; set; }

    //计算此装备的属性加成列表
    public Dictionary<string, Attri> ApplyModifiers(Dictionary<string, Attri> inParams) {
        var response = new Dictionary<string, Attri>();
        inParams.Select(t => t.Value).ToList().ForEach(t=>
            response.Add(t.Name, new Attri() {
                Name = t.Name,
                Value = t.Value
            })
        );
        //遍历此装备拥有的词缀,并找到对应的属性并应用到属性上
        foreach (var m in ItemModifiers) {
            //If this is the first time the response ran into it, add it
            if (!response.ContainsKey(m.TargetName)) {
                response.Add(m.TargetName, new Attri() {
                    Name = m.TargetName,
                    Value = 0
                });
            }
            //将词缀的属性值加到目标属性上
            m.Apply(response[m.TargetName]);
        }

        return response;
    }
}