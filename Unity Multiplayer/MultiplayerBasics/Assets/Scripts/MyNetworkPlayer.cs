using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string displayName = "Missing Name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    private Color displayColor = new Color(0, 0, 0);

    [SerializeField]
    private TMP_Text displayNameText = null;
    [SerializeField]
    private Renderer displayColorRenderer = null;

    #region Server

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        // * This method cannot be called from a Client, is only
        // * meant to be used from the server
        displayName = newDisplayName;
    }

    [Server]
    public void SetDisplayColor(Color newColor)
    {
        displayColor = newColor;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        // * This method can be called from a Client
        if (newDisplayName.Length < 3)
        {
            return;
        }

        RpcLogNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client
    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        displayColorRenderer.material.SetColor("_BaseColor", newColor);
    }

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        displayNameText.text = newName;
    }

    [ContextMenu("Set my name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My new name");
    }

    [ClientRpc]
    private void RpcLogNewName(string newName)
    {
        Debug.Log($"Player's name: {newName}");
    }

    #endregion
}
