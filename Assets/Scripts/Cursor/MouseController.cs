using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Cursor2
{
    public class MouseController : MonoBehaviour
    {
        private MouseController instance;

        public MouseController Instance
        {
            get
            {
                return instance;
            }

            set 
            {
                if(Instance == null) 
                {
                    instance = value;
                }
                else
                {
                    Destroy(value.gameObject);
                }
            }
        }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Update()
        {

        }

        /// <summary>
        /// Returns mouse position
        /// </summary>
        /// <param name="mousePositionType">Type of mouse position</param>
        /// <returns>Vector2 of mouse position. Default type is in viewport coordinates.</returns>
        public static Vector2 GetMousePosition(MousePositionType mousePositionType = MousePositionType.ScreenToViewport)
        {
            Vector2 position = new Vector2(0, 0);

            switch (mousePositionType)
            {
                case MousePositionType.ScreenToViewport:
                    position = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
                    break;
                case MousePositionType.ViewportToScreen:
                    position = Camera.main.ViewportToScreenPoint(Mouse.current.position.ReadValue());
                    break;
                case MousePositionType.ScreenToWorld:
                    position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    break;
                case MousePositionType.WorldToScreen:
                    position = Camera.main.WorldToScreenPoint(Mouse.current.position.ReadValue());
                    break;
            }

            return position;
        }
    }
    /// <summary>
    /// Type of mouse positions
    /// </summary>
    public enum MousePositionType
    {
        ScreenToViewport,
        ViewportToScreen,
        ScreenToWorld,
        WorldToScreen
    }
}
