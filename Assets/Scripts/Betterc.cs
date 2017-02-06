using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Betterc : MonoBehaviour
{
    public bool TriggerP = false;
    private SteamVR_TrackedObject trackeodbject;
    private SteamVR_Controller.Device device;
    // Use this for initialization
    void Start()
    {
        trackeodbject = gameObject.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackeodbject.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            TriggerP = true;
            Debug.Log("trigger was pressed");
        }

    }
}
