using UnityEngine;

public class RoamBounds : MonoBehaviour
{
    public Transform roamBoundLeft;
    public Transform roamBoundRight;
    public RoamBounds(Transform roamBoundLeft, Transform roamBoundRight) {
        this.roamBoundLeft = roamBoundLeft;
        this.roamBoundRight = roamBoundRight;
    }
    public Vector2[] getPosArr() {
        return new Vector2[] {roamBoundLeft.position,roamBoundRight.position};
    }
}
