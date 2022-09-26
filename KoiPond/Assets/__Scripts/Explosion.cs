using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private bool _stopGrowing = false;
    void Start()
    {
        InvokeRepeating("IncreaseSize", 0f,.01f);
    }
    
    private void IncreaseSize()
    {
        // check whether the sprite should continue growing
        if (!_stopGrowing)
            gameObject.transform.localScale *= 1.02f;
        else
            gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1f);

        // stop scale increase
        if (gameObject.transform.localScale.x > 10)
        {
            _stopGrowing = true;
        }

        if (gameObject.GetComponent<SpriteRenderer>().color.a < 20)
        {
            //Destroy(gameObject);
        }
    }
}
