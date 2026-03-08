using System;
using UnityEngine;
using System.Collections.Generic;

namespace Script
{
    public interface IStage {
        bool Check(); 
    }

    public class Stage1 : IStage
    {
        public bool Check()
        {
            if (Input.anyKeyDown)
            {
                return true;
            }
            return false;
        }
    }
}