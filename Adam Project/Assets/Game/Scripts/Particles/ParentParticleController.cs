using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentParticleController : MonoBehaviour {

	public void PlayChildAndSelfParticles() {
        var parent = GetComponent<ParticleSystem> ();
        var childs = GetComponentsInChildren<ParticleSystem> ();

        parent.Play ();
        foreach (ParticleSystem particle in childs ){
            particle.Play ();
        }
    }
}
