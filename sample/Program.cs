using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample {
    class Program {
        static void Main(string[] args) {

            var character = new Character(new Dictionary<string, float>() {
                {"生命",100f},
                {"力量",8f},
                {"敏捷",4f},
                {"智力",2f}
            });
            character.AddEquipment(DropCenter.Instance.DropEquipment());
            character.AddEquipment(DropCenter.Instance.DropEquipment());

            var c = character.FetchModifiedAttributeutes();
            Console.WriteLine("===========人物属性============");
            c.Select(t => t.Value).ToList().ForEach(t => {
                Console.WriteLine(t.Name + ":" + t.Value);
            });
            Console.WriteLine("===========装备列表============");
            character.Equipments.ForEach(e => {
                Console.WriteLine("------------"+e.Name + "------------");
                Console.WriteLine("品质:" + e.Rarity.ToString());
                Console.WriteLine("等级:" + e.Level.ToString());
                e.ItemModifiers.ForEach(t => {
                    Console.WriteLine(t.ModifierType.TargetName + ":" + t.ModifierValue);
                });
                Console.WriteLine("类型:" + e.EnquipmentType.ToString());
                Console.WriteLine("------------------------");
            });
            Console.Read();
        }
    }
}
