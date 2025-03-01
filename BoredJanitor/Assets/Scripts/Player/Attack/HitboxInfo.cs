using System.Collections;
using UnityEngine;

public class HitboxInfo
{
    public Vector3 size;
    public Vector2 offset;

    public HitboxInfo(Vector3 size, Vector2 offset){
        this.size = size;
        this.offset = offset;
    }
}