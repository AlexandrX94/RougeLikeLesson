using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Health
{
   public interface IDamageble
    {
        void TakeDamage(float value);
        void TakeHeal(float value);
    }

}

