using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    public GameObject Player;
    public GameObject cutscene;
    public GameObject mainCam;

    private void OnTriggerEnter(Collider other)
    {
        cutscene.SetActive(true);
        Player.SetActive(false);
        mainCam.SetActive(false);
    }
}
