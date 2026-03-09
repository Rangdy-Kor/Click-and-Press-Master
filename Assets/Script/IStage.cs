using UnityEngine.InputSystem;

namespace Script
{
    public interface IStage {
        bool Check(); 
    }

    public class Stage1 : IStage
    {
        public bool Check()
        {
            return Keyboard.current.anyKey.wasPressedThisFrame;
        }
    }

    public class Stage2 : IStage
    {
        public bool Check()
        {
            return Pointer.current != null && Pointer.current.press.wasPressedThisFrame;
        }
    }

    public class Stage3 : IStage
    {
        private int _numberIndex;
        public bool Check()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame && _numberIndex == 0)
            {
                _numberIndex++;
                return false;
            }
            if (Keyboard.current.digit2Key.wasPressedThisFrame && _numberIndex == 1)
            {
                _numberIndex++;
                return false;
            }
            if (Keyboard.current.digit3Key.wasPressedThisFrame && _numberIndex == 2)
            {
                _numberIndex++;
                return false;
            }
            if (Keyboard.current.digit4Key.wasPressedThisFrame && _numberIndex == 3)
            {
                _numberIndex++;
                return false;
            }
            if (Keyboard.current.digit5Key.wasPressedThisFrame && _numberIndex == 4)
            {
                _numberIndex++;
                return false;
            }
            if (Keyboard.current.digit6Key.wasPressedThisFrame && _numberIndex == 5)
            {
                _numberIndex++;
                return false;
            }
            if (Keyboard.current.digit7Key.wasPressedThisFrame && _numberIndex == 6)
            {
                _numberIndex++;
                return false;
            }
            if (Keyboard.current.digit8Key.wasPressedThisFrame && _numberIndex == 7)
            {
                _numberIndex++;
                return false;
            }
            if (Keyboard.current.digit9Key.wasPressedThisFrame && _numberIndex == 8)
            {
                _numberIndex++;
                return false;
            }
            if (Keyboard.current.digit0Key.wasPressedThisFrame && _numberIndex == 9)
            {
                _numberIndex++;
                return true;
            }
            return false;
        }
    }
}