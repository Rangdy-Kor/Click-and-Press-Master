using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public interface IStage {
        void Init(GameObject spawnedObject);
        bool Check(); 
    }

    public class Stage1 : IStage
    {
        public void Init(GameObject spawnedObject) { }
        
        public bool Check()
        {
            return Keyboard.current.anyKey.wasPressedThisFrame;
        }
    }

    public class Stage2 : IStage
    {
        public void Init(GameObject spawnedObject) { }

        public bool Check()
        {
            return Pointer.current != null && Pointer.current.press.wasPressedThisFrame;
        }
    }

    public class Stage3 : IStage
    {
        public void Init(GameObject spawnedObject) { }

        private static readonly Key[] DigitOrder = {
            Key.Digit1, Key.Digit2, Key.Digit3, Key.Digit4, Key.Digit5,
            Key.Digit6, Key.Digit7, Key.Digit8, Key.Digit9, Key.Digit0
        };
        private static readonly Key[] NumpadOrder = {
            Key.Numpad1, Key.Numpad2, Key.Numpad3, Key.Numpad4, Key.Numpad5,
            Key.Numpad6, Key.Numpad7, Key.Numpad8, Key.Numpad9, Key.Numpad0
        };
        private int _numberIndex;

        public bool Check()
        {
            if (Keyboard.current[DigitOrder[_numberIndex]].isPressed || Keyboard.current[NumpadOrder[_numberIndex]].isPressed)
            {
                _numberIndex++;
                return _numberIndex >= DigitOrder.Length;
            }
            return false;
        }
    }

    public class Stage4 : IStage
    {
        private float _lastClickTime;
        private const float DoubleClickThreshold = 0.3f;

        public void Init(GameObject spawnedObject) { }
        public bool Check()
        {
            if (Keyboard.current.shiftKey.isPressed &&
                Pointer.current != null &&
                Pointer.current.press.wasPressedThisFrame)
            {
                if (Time.time - _lastClickTime < DoubleClickThreshold)
                {
                    return true;
                }
                _lastClickTime = Time.time;
            }
            return false;
        }
    }
    
    public class Stage5 : IStage
    {
        private static readonly Key[] Order = {
            Key.A, Key.B, Key.C, Key.D, Key.E, Key.F, Key.G
        };
        private int _index;

        public void Init(GameObject spawnedObject) { }
        public bool Check()
        {
            if (Keyboard.current[Order[_index]].wasPressedThisFrame)
            {
                _index++;
                return _index >= Order.Length;
            }
            return false;
        }
    }

    public class Stage6 : IStage
    {
        public void Init(GameObject spawnedObject) { }

        public bool Check()
        {
            return Keyboard.current.ctrlKey.isPressed &&
                   Keyboard.current.altKey.isPressed &&
                   Keyboard.current.spaceKey.isPressed;
        }
    }

    public class Stage7 : IStage
    {
        [SerializeField] private Camera mainCamera;
        
        public void Init(GameObject spawnedObject)
        {
            spawnedObject.SetActive(true);
        }

        public bool Check()
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    return hit.collider.CompareTag("Target");
                }
            }
            return false;
        }
    }
}