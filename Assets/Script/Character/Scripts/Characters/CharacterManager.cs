﻿using UnityEngine;

public abstract class CharacterManager : MonoBehaviour
{
    #region Customizable Public Variables
    [Range(0, 300)]
    public int m_Health = 100;
    public float m_GroundedDistance = 0.2f;
    [Tooltip("Layer Mask for finding cover")]
    public LayerMask m_EnvironmentLayerForCover;
    #endregion Customizable Public Variables

    #region HideInInspector Variables
    //[HideInInspector]
    public bool m_isCrouching;
    //[HideInInspector]
    public bool m_isInCover;
    //[HideInInspector]
    public bool m_isInHighCover;
    //[HideInInspector]
    public float m_charHeight;
    //[HideInInspector]
    public float m_charRadius;
    //[HideInInspector]
    public CharacterType m_CharType;
    [HideInInspector]
    public Animator m_animator;
    #endregion HideInInspector Variables

    #region Protected Variables
    protected SceneCharactersManager m_sceneCharactersManager;
    protected bool m_isGrounded;
    protected CharacterController m_charController;
    #endregion Protected Variables

    protected virtual void Initialized()
    {
        m_sceneCharactersManager = GameObject.FindObjectOfType<SceneCharactersManager>();
        m_animator = GetComponent<Animator>();
        m_charController = GetComponent<CharacterController>();
        m_charHeight = m_charController.height;
        m_charRadius = m_charController.radius;
    }

    public bool CheckForLowOrHighCover()
    {
        Debug.DrawRay(transform.position + new Vector3(0, m_charHeight / 2f, 0), transform.forward, Color.red, 3f);
        if (Physics.Raycast(transform.position + new Vector3(0, m_charHeight / 2f, 0), transform.forward, 2f, m_EnvironmentLayerForCover))
        {
            Debug.DrawRay(transform.position + new Vector3(0, m_charHeight, 0), transform.forward, Color.red, 3f);
            if (Physics.Raycast(transform.position + new Vector3(0, m_charHeight, 0), transform.forward, 2f, m_EnvironmentLayerForCover))
            {
                return true;
            }
            return false;
        }
        return false;
    }

    public virtual void GiveDamage<T>(T character, Vector3 hitPosition) where T: CharacterManager
    {
        Debug.Log("Character Manager Give Damage");
    }

    public virtual void TakeDamage(int danage)
    {
        Debug.Log("Base class....");
    }

    protected virtual bool CheckGroudedOrNot()
    {
        if (Physics.Raycast(transform.position, Vector3.down, m_GroundedDistance))
        {
            return true;
        }

        return m_charController.isGrounded;
    }
}