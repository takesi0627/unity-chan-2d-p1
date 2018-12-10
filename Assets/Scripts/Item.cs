using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Item : MonoBehaviour {
    static bool m_IsUsing = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_IsUsing)
        {
            m_IsUsing = true;
            UseItem();
            Destroy(gameObject);
        }
        else {
            return;
        }
        m_IsUsing = false;
    }

    abstract public void UseItem();
}
