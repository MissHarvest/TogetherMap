using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorFollow : MonoBehaviour
{
    [SerializeField] GameObject character;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(character != null)
            transform.position = character.transform.position + (Vector3)offset;
    }

    public void SetTarget(GameObject target)
    {
        character= target;
    }
}
