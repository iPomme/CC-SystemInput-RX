using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class UIManager : MonoBehaviour
{

    public Transform ToMove;
    
    // Start is called before the first frame update
    void Start()
    {
        InputSystem.OnMove().Subscribe(v =>
        {
            Debug.LogFormat("The received vector is '{0}'", v);
            ToMove.Rotate(Time.deltaTime * 100 * new Vector3(v.y, v.x, 0));
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
