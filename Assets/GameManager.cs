using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    [Sirenix.Serialization.OdinSerialize]
    EnumDictionary<Test, State> _dictionary;

    // Start is called before the first frame update
    void Start()
    {
        IEnumerator Test2(int x) {yield break;}
        System.Func<int, IEnumerator> x = Test2;
        
    }

    IEnumerator Run(System.Func<string, IEnumerator> callback)
    {
        //Do stuff to get value;
        yield return callback("action");
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public enum Test
    {
        T1, T2, T3
    };
}
public interface State
{

}

[System.Serializable]
public class State1 : State
{
    
}
[System.Serializable]
public class State2 : State
{
    
}