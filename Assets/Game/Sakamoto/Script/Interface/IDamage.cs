using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void ApplyDamage(float amount);

    void ApplyHeal(float amount);
}
