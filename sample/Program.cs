using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample {
    class Program {
        static void Main(string[] args) {

            for (var i = 0; i < 5; i++) {
                var e = DropCenter.Instance.DropEquipment();
                Console.WriteLine("------------------------");
                Console.WriteLine("名称:" + e.Name);
                Console.WriteLine("品质:" + e.Rarity.ToString());
                Console.WriteLine("等级:" + e.Level.ToString());
                e.ItemModifiers.ForEach(t => {
                    Console.WriteLine(t.ModifierType.TargetName + ":" + t.ModifierValue);
                });
                Console.WriteLine("类型:" + e.EnquipmentType.ToString());
                Console.WriteLine("------------------------");
            }
            Console.Read();
        }
    }
}
