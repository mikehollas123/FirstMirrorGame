using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerCharacterBase : NetworkBehaviour
{

    [SerializeField]
    private TMP_Text displayNameText = null;

    [SyncVar(hook = "HandlePlayerDisplayNameChange")]
    [SerializeField]
    private string displayName = "missing";



    #region Server
    [Server]
    public void SetDisplayName(string newDispayName)
    {
        displayName = newDispayName;

    }

    private void HandlePlayerDisplayNameChange(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    public void Move(Vector3 movement)
    {

    }

    #endregion


    #region Client


    #endregion

}
