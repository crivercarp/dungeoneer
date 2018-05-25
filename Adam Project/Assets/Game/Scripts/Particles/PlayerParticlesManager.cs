using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticlesManager : MonoBehaviour {

    [Header ("Particles To Execute")]
    [SerializeField]
    private ParentParticleController BloodParticle;

    [SerializeField]
    private ParentParticleController ShieldParticle;

    public void PlayBloodParticles() {
        if(BloodParticle != null) {
            BloodParticle.PlayChildAndSelfParticles ();
        }
    }

    public void PlayShieldParticles() {
        if (BloodParticle != null) {
            ShieldParticle.PlayChildAndSelfParticles ();
        }
    }

}
