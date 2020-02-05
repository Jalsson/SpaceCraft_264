using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour {

    
	public virtual void  Behavior()
    {
        Destroy(this.gameObject, 2.0f);

    }
}
