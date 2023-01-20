using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestSetter : MonoBehaviour
{
    [Button]
    void SetTopBottom(float topY, float bottomY)
    {
        if (bottomY > topY) SetTopBottom(bottomY, topY);
        
        float height = topY - bottomY;

        transform.localScale = transform.localScale.With(y: height);
        transform.position = transform.position.With(y: bottomY + height / 2);
    }
}
