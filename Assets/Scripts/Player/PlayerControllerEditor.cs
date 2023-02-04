using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;

namespace Scripts.Player
{
    [CustomEditor(typeof(PlayerController))]
    public class PlayerControllerEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PlayerController playerController = (PlayerController)target;

            playerController.RigidbodyConstraints();
        }
    }
}
