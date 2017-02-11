//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: The object attached to the player's hand that spawns and fires the
//			arrow
//
//=============================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    public class ArrowHand : MonoBehaviour
    {
        private Hand hand;
        private Longbow bow;

        public bool flameArrow = false;
        public GameObject[] currentArrow;
        // public GameObject currentArrow2;
        public GameObject[] arrow1;
        public GameObject arrowPrefab;
        private float distanceToNockPosition;
        public Arrow[] arrow;
        public Transform[] arrowNockTransform;
        //  public Transform arrowNockTransform2;
        public int NumOfArrowsToShoot = 2;
        public float nockDistance = 0.1f;
        public float lerpCompleteDistance = 0.08f;
        public float rotationLerpThreshold = 0.15f;
        public float positionLerpThreshold = 0.15f;
      //  private GameObject[] arrow1;
        private bool allowArrowSpawn = true;
        private bool nocked;

        private bool inNockRange = false;
        private bool arrowLerpComplete = false;

        private Rigidbody arrowRigidbody;

        public SoundPlayOneshot arrowSpawnSound;

        private AllowTeleportWhileAttachedToHand allowTeleport = null;

        public int maxArrowCount = 10;
        private List<GameObject> arrowList;


        //-------------------------------------------------
        void Awake()
        {
            allowTeleport = GetComponent<AllowTeleportWhileAttachedToHand>();
            allowTeleport.teleportAllowed = true;
            allowTeleport.overrideHoverLock = false;

            arrowList = new List<GameObject>();
        }


        //-------------------------------------------------
        private void OnAttachedToHand(Hand attachedHand)
        {
            hand = attachedHand;
            FindBow();
        }


        //-------------------------------------------------
        private GameObject InstantiateArrow(int i)
        {
           
               arrow1[i] = Instantiate(arrowPrefab, arrowNockTransform[i].position, arrowNockTransform[i].rotation) as GameObject;

                arrow1[i].name = "Bow Arrow" + i;
                arrow1[i].transform.parent = arrowNockTransform[0];
                Util.ResetTransform(arrow1[i].transform);

                arrowList.Add(arrow1[i]);

                while (arrowList.Count > maxArrowCount)
                {
                    GameObject oldArrow = arrowList[0];
                    arrowList.RemoveAt(0);
                    if (oldArrow)
                    {
                        Destroy(oldArrow);
                    }
                }

                return arrow1[i];        
           
        }
     
        //-------------------------------------------------
        private void HandAttachedUpdate(Hand hand)
        {
            if (bow == null)
            {
                FindBow();
            }

            if (bow == null)
            {
                return;
            }
           
            if (allowArrowSpawn && (currentArrow[0] == null)) // If we're allowed to have an active arrow in hand but don't yet, spawn one
            {
                for (int i = 0; i < NumOfArrowsToShoot; i++)
                {
                    currentArrow[i] = InstantiateArrow(i);
                //    currentArrow2 = InstantiateArrow();
                }
                arrowSpawnSound.Play();
            }
            for (int i = 0; i < NumOfArrowsToShoot; i++)
            {
                 distanceToNockPosition = Vector3.Distance(transform.parent.position, bow.nockTransform[i].position);
            }
            // If there's an arrow spawned in the hand and it's not nocked yet
            if (!nocked)
            {
                // If we're close enough to nock position that we want to start arrow rotation lerp, do so
                if (distanceToNockPosition < rotationLerpThreshold)
                {
                    float lerp = Util.RemapNumber(distanceToNockPosition, rotationLerpThreshold, lerpCompleteDistance, 0, 1);
                    for (int i = 0; i < NumOfArrowsToShoot; i++)
                    {
                        arrowNockTransform[i].rotation = Quaternion.Lerp(arrowNockTransform[i].parent.rotation, bow.nockRestTransform[i].rotation, lerp);
                    }
                    //   arrowNockTransform2.rotation = Quaternion.Lerp(arrowNockTransform2.parent.rotation, bow.nockRestTransform.rotation, lerp);
                }
                else // Not close enough for rotation lerp, reset rotation
                {
                    for (int i = 0; i < NumOfArrowsToShoot; i++)
                    { 
                    arrowNockTransform[i].localRotation = Quaternion.identity;
                    }
                  //  arrowNockTransform2.localRotation = Quaternion.identity;
                }

                // If we're close enough to the nock position that we want to start arrow position lerp, do so
                if (distanceToNockPosition < positionLerpThreshold)
                {
                    float posLerp = Util.RemapNumber(distanceToNockPosition, positionLerpThreshold, lerpCompleteDistance, 0, 1);

                    posLerp = Mathf.Clamp(posLerp, 0f, 1f);
                    for (int i = 0; i < NumOfArrowsToShoot; i++)
                    {
                        arrowNockTransform[i].position = Vector3.Lerp(arrowNockTransform[i].parent.position, bow.nockRestTransform[i].position, posLerp);
                    }
                      //  arrowNockTransform2.position = Vector3.Lerp(arrowNockTransform2.parent.position, bow.nockRestTransform.position, posLerp);
                }
                else // Not close enough for position lerp, reset position
                {
                    for (int i = 0; i < NumOfArrowsToShoot; i++)
                    {
                        arrowNockTransform[i].position = arrowNockTransform[i].parent.position;
                    }
                    //  arrowNockTransform2.position = arrowNockTransform2.parent.position;
                }


                // Give a haptic tick when lerp is visually complete
                if (distanceToNockPosition < lerpCompleteDistance)
                {
                    if (!arrowLerpComplete)
                    {
                        arrowLerpComplete = true;
                        hand.controller.TriggerHapticPulse(500);
                    }
                }
                else
                {
                    if (arrowLerpComplete)
                    {
                        arrowLerpComplete = false;
                    }
                }

                // Allow nocking the arrow when controller is close enough
                if (distanceToNockPosition < nockDistance)
                {
                    if (!inNockRange)
                    {
                        inNockRange = true;
                        bow.ArrowInPosition();
                    }
                }
                else
                {
                    if (inNockRange)
                    {
                        inNockRange = false;
                    }
                }

                // If arrow is close enough to the nock position and we're pressing the trigger, and we're not nocked yet, Nock
                if ((distanceToNockPosition < nockDistance) && hand.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger) && !nocked)
                {
                    if (currentArrow[0] == null)
                    {
                        for (int i = 0; i < NumOfArrowsToShoot; i++)
                        {
                            currentArrow[i] = InstantiateArrow(i);
                        }
                     //   currentArrow2 = InstantiateArrow();
                    }

                    nocked = true;
                    bow.StartNock(this);
                    hand.HoverLock(GetComponent<Interactable>());
                    allowTeleport.teleportAllowed = false;
                    for (int i = 0; i < NumOfArrowsToShoot; i++)
                    {
                        currentArrow[i].transform.parent = bow.nockTransform[i];
                        Util.ResetTransform(currentArrow[i].transform);
                        Util.ResetTransform(arrowNockTransform[i]);
                      //    currentArrow2.transform.parent = bow.nockTransform[1];
                      //    Util.ResetTransform(currentArrow2.transform);
                       //   Util.ResetTransform(arrowNockTransform[1]);
                     }

                }
            }


            // If arrow is nocked, and we release the trigger
            if (nocked && (!hand.controller.GetPress(SteamVR_Controller.ButtonMask.Trigger) || hand.controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger)))
            {
                if (bow.pulled) // If bow is pulled back far enough, fire arrow, otherwise reset arrow in arrowhand
                {
                    FireArrow();
                }
                else
                {
                    for (int i = 0; i < NumOfArrowsToShoot; i++)
                   {
                        arrowNockTransform[i].rotation = currentArrow[i].transform.rotation;
                        currentArrow[i].transform.parent = arrowNockTransform[i];
                        Util.ResetTransform(currentArrow[i].transform);

               //     arrowNockTransform[1].rotation = currentArrow2.transform.rotation;
               //     currentArrow2.transform.parent = arrowNockTransform[1];
               //     Util.ResetTransform(currentArrow2.transform);
                       }
                    //  arrowNockTransform2.rotation = currentArrow2.transform.rotation;
                    //    currentArrow.transform.parent = arrowNockTransform;
                    // currentArrow2.transform.parent = arrowNockTransform2;
                    //    Util.ResetTransform(currentArrow.transform);
                    // Util.ResetTransform(currentArrow2.transform);
                    nocked = false;
                    bow.ReleaseNock();
                    hand.HoverUnlock(GetComponent<Interactable>());
                    allowTeleport.teleportAllowed = true;
                }

                bow.StartRotationLerp(); // Arrow is releasing from the bow, tell the bow to lerp back to controller rotation
            }
        }


        //-------------------------------------------------
        private void OnDetachedFromHand(Hand hand)
        {
            Destroy(gameObject);
        }


        //-------------------------------------------------
        private void FireArrow()
        {
            for (int i = 0; i < NumOfArrowsToShoot; i++)
             {
                currentArrow[i].transform.parent = null;
            }
            // currentArrow2.transform.parent = null;
            for (int i = 0; i < NumOfArrowsToShoot; i++)
            {
                arrow[i] = currentArrow[i].GetComponent<Arrow>();
                //   Arrow arrow2 = currentArrow2.GetComponent<Arrow>();
                arrow[i].shaftRB.isKinematic = false;
                arrow[i].shaftRB.useGravity = true;
                arrow[i].shaftRB.transform.GetComponent<BoxCollider>().enabled = true;

                arrow[i].arrowHeadRB.isKinematic = false;
                arrow[i].arrowHeadRB.useGravity = true;
                arrow[i].arrowHeadRB.transform.GetComponent<BoxCollider>().enabled = true;

                arrow[i].arrowHeadRB.AddForce(currentArrow[i].transform.forward * bow.GetArrowVelocity(), ForceMode.VelocityChange);
                arrow[i].arrowHeadRB.AddTorque(currentArrow[i].transform.forward * 10);

            
            }
      
            nocked = false;
            for (int i = 0; i < NumOfArrowsToShoot; i++)
            {
                currentArrow[i].GetComponent<Arrow>().ArrowReleased(bow.GetArrowVelocity());
        //    currentArrow2.GetComponent<Arrow>().ArrowReleased(bow.GetArrowVelocity());
               }
            bow.ArrowReleased();

            allowArrowSpawn = false;
            Invoke("EnableArrowSpawn", 0.5f);
            StartCoroutine(ArrowReleaseHaptics());
           for (int i = 0; i < NumOfArrowsToShoot; i++)
            {
                currentArrow[i] = null;
          //  currentArrow2 = null;
                 }
            allowTeleport.teleportAllowed = true;
        }


        //-------------------------------------------------
        private void EnableArrowSpawn()
        {
            allowArrowSpawn = true;
        }


        //-------------------------------------------------
        private IEnumerator ArrowReleaseHaptics()
        {
            yield return new WaitForSeconds(0.05f);

            hand.otherHand.controller.TriggerHapticPulse(1500);
            yield return new WaitForSeconds(0.05f);

            hand.otherHand.controller.TriggerHapticPulse(800);
            yield return new WaitForSeconds(0.05f);

            hand.otherHand.controller.TriggerHapticPulse(500);
            yield return new WaitForSeconds(0.05f);

            hand.otherHand.controller.TriggerHapticPulse(300);
        }


        //-------------------------------------------------
        private void OnHandFocusLost(Hand hand)
        {
            gameObject.SetActive(false);
        }


        //-------------------------------------------------
        private void OnHandFocusAcquired(Hand hand)
        {
            gameObject.SetActive(true);
        }


        //-------------------------------------------------
        private void FindBow()
        {
            bow = hand.otherHand.GetComponentInChildren<Longbow>();
        }
    }
}
