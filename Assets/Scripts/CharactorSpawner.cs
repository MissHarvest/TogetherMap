using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharactorSpawner : MonoBehaviour
{
    public GameObject SpawnCharacter(string id, string name, int character)
    {
        var player = Resources.Load<GameObject>("Player");
        var playerObject = Instantiate(player, transform.position, Quaternion.identity);
        playerObject.GetComponent<Player>().ChangeName(name);
        playerObject.GetComponent<Player>().ChangeCharacter(character);

        if(GameManager.Instance.UID == id)
        {
            GameManager.Instance.Player = playerObject.GetComponent<Player>();
            playerObject.GetComponent<Player>().ChangeNameColor();
            Camera.main.GetComponent<CharactorFollow>().SetTarget(playerObject);
        }

        GameManager.Instance.AddUser(id, playerObject);
        return playerObject;
    }

    public void SpawnCharacter(string id, string name, int character, Vector2 pos)
    {
        var go = SpawnCharacter(id, name, character);
        go.transform.position = (Vector3)(pos);
    }
}
