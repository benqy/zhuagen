using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class Character {

    public Character(Dictionary<string,float> attrs = null) {
        this._rawAttributeutes = new Dictionary<string, Attri>();
        this._modifiedAttributeutes = new Dictionary<string, Attri>();
        this.Equipments = new List<Equipment>();
        //初始化人物属性
        if (attrs != null) {
            attrs.ToList().ForEach(t => {
                this.AddAttributeute(new Attri() {
                    Name = t.Key,
                    Value = t.Value
                });
            });
        }
    }

    //人物原始属性
    private Dictionary<string, Attri> _rawAttributeutes;
    //修正后属性(实际属性)
    private Dictionary<string, Attri> _modifiedAttributeutes;
    //是否已经计算过修正后属性了,防止重复计算
    private bool _areModifiedAttributesCurrent { get; set; }
    //装备列表
    public List<Equipment> Equipments { get; private set; }

    /// <summary>
    /// 添加装备
    /// </summary>
    /// <param name="e">装备</param>
    public void AddEquipment(Equipment e) {
        Equipments.Add(e);
        _areModifiedAttributesCurrent = false;
    }

    /// <summary>
    /// 为人物直接添加初始属性
    /// </summary>
    /// <param name="attr"></param>
    public void AddAttributeute(Attri attr) {
        _rawAttributeutes.Add(attr.Name, attr);
    }

    /// <summary>
    /// 计算装备修正后的人物属性
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, Attri> FetchModifiedAttributeutes() {
        //已计算则跳过
        if (!_areModifiedAttributesCurrent) {
            var traceItemAttributes = _rawAttributeutes;
            //将装备属性累加到修正后属性上
            foreach (var e in Equipments.OrderBy(x => x.ApplyOrder)) {
                traceItemAttributes = e.ApplyModifiers(traceItemAttributes);
            }
            _modifiedAttributeutes = traceItemAttributes;
        }

        return _modifiedAttributeutes;
    }
}