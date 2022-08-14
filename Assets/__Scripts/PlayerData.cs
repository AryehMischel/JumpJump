
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "PlayerObject", order = 1)]
public class PlayerData : ScriptableObject
{

   ///public Vector3 Jump = new Vector3(0, 6, 0);

   public float Jump = 0;

   public bool GhostMode = false;
}