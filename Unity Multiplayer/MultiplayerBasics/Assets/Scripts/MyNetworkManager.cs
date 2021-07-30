using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        Debug.Log("New player logged in!");
        Debug.Log($"Total players: {this.numPlayers}");

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();
        
        player.SetDisplayName($"Player {this.numPlayers}");

        Color playerColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        player.SetDisplayColor(playerColor);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        Debug.Log("Connected!");
    }
}
