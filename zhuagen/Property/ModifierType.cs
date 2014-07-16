using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Modifier {
    public string TargetName { get; set; }
    public float ModifierValue { get; set; }
    //The other stuff is kind of pointless... but this is where the magic happens... All in a modifier type.
    public ModifierType ModifierType { get; set; }
    //Let the modifier apply it's own values... off the type... yea
    //I did that on purpose ;-)
    public void Apply(Attri a) {
        a.Value = ModifierType.ApplyModifier(this, a.Value);
    }
}

//Decoration... Wonderful
//This base class gives you a interface to work with... Hell, it could be an interface but I decided
//to type abstract today.
public abstract class ModifierType {
    public abstract string ModifierName { get; }
    public abstract float ApplyModifier(Modifier m, float InitialValue);
}


//A concrete type of ModifierType... This is what determines how the modifier value is applied.
//This gives you more flexibility than hard coding modifier types.  If you really wanted to you could
//serialize these and store lambda expressions in the DB so you not only have type driven logic, you could have
//data driven behavior.
public class FlatModifier : ModifierType {
    //The names can be really handy if you want to expose calculations to players.
    public override string ModifierName { get { return "Flat Effect"; } }
    //And finally... let the calculation happen!  Time to bubble back up!
    public override float ApplyModifier(Modifier m, float InitialValue) {
        return InitialValue + m.ModifierValue;
    }
}