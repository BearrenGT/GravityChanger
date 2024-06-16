using System;
using BepInEx;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR;
using Utilla;

namespace GorillaTagModTemplateProject
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;

        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnEnable()
        {
            {

            }
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {

            HarmonyPatches.RemoveHarmonyPatches();
        }



        void OnGameInitialized(object sender, EventArgs e)
        {
            /* Code here runs after the game initializes (i.e. GorillaLocomotion.Player.Instance != null) */
        }



        void Update()
        {
            if (inRoom == true)
            {
                if (ControllerInputPoller.instance.rightControllerPrimaryButton)
                {
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Acceleration);
                }
                if (ControllerInputPoller.instance.leftControllerPrimaryButton)
                {
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * (Time.deltaTime * (6.66f / Time.deltaTime)), ForceMode.Acceleration);
                }
                if (ControllerInputPoller.instance.leftControllerSecondaryButton)
                {
                    GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * (Time.deltaTime * (7.77f / Time.deltaTime)), ForceMode.Acceleration);
                }
            }
        }


        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            inRoom = true;

        }

        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            inRoom = false;

        }
    }
}

