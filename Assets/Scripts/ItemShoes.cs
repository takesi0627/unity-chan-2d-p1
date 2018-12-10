using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShoes : Item {
    public override void UseItem()
    {
        FindObjectOfType<PlayerCharacter>().TwoStepJump = true;
    }
}
