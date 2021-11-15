using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("RayCast variables")]
    public float backWallCheckDistance = .25f;
    public float frontWallCheckDistance = 0.3f;

    [Header("Jump")]
    public float jumpSpeed = 15f;

    [Header("Movement")]
    public float moveSpeed = 15f;

    [Header("LayerMasks")]
    public LayerMask whatIsGround;
}
