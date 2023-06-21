using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{
    public Light flickeringLight;
    public void PlayFlickeringAnimation()
    {
        Animation anim = GetComponent<Animation>();
        anim.Play();
       


    }
}
