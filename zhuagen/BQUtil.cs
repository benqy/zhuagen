using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BQUtil {
    public static int Random(int min,int max){
        return new Random().Next(min, max);
    }

    public static int Random(int max) {
        return new Random().Next(0, max);
    }
}