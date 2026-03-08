using System;
using UnityEngine;
using System.Collections.Generic;
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
}