using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSword : Item {
    public override void UseItem()
    {
        FindObjectOfType<PlayerCharacter>().SwordLevelUp();
    }
}
