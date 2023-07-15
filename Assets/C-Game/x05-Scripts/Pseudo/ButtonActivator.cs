using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonActivator : MonoBehaviour
{
    [SerializeField] ActivationManager activationManager;
    public GameObject[] m_ObjectsAffectedToDeactivate;
    public GameObject[] m_ObjectsAffectedToActivate;

    public void DeactivateObjects()
    {
        activationManager.DeactivateObjects(m_ObjectsAffectedToActivate);
        activationManager.ActivateObjects(m_ObjectsAffectedToDeactivate);
    }

    public void ActivateObjects()
    {
        activationManager.ActivateObjects(m_ObjectsAffectedToActivate);
        activationManager.DeactivateObjects(m_ObjectsAffectedToDeactivate);
    }
}