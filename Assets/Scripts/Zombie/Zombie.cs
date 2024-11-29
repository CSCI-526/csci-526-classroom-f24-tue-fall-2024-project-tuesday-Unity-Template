using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public bool isIlluminated = false;

    public void SetIlluminated(bool illuminated)
    {
        isIlluminated = illuminated;
    }
}
