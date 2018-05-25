using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class in charge of detecting the position of the mouse at the moment of the click and mapping it out for other components to use
 * */

public class MouseClickInput : MonoBehaviour {

    public PlayerManager player;

	void Update () {
        Ray ray;
        RaycastHit hit;

        if (Input.GetMouseButtonDown (0)) {
            ray = Camera.main.ScreenPointToRay (Input.mousePosition);

            if (Physics.Raycast (ray, out hit)) {
                if (player != null)
                    player.AttackStart (hit.point);
            }
        }
        if (Input.GetMouseButtonDown (1)) {
            ray = Camera.main.ScreenPointToRay (Input.mousePosition);

            if (Physics.Raycast (ray, out hit)) {
                if (player != null)
                    player.BlockStart (hit.point);
            }
        }
    }
}
