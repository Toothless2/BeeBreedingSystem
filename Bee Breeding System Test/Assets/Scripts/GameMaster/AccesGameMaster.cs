using UnityEngine;
using System.Collections;

namespace GameMaster
{
    public static class AccesGameMaster
    {
        public static GameMaster gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
}
