using System.Collections;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public GameObject hitboxPreafab;
    public HitboxInfo[,] HitboxInfos = new HitboxInfo[3,4];
    //public ArrayList HitboxInfos = new ArrayList();
    ArrayList hitboxen = new ArrayList();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HitboxInfos[0,0] = new HitboxInfo(new Vector3(4,1), new Vector2(2,-1)); //ligth
        HitboxInfos[0,1] = new HitboxInfo(new Vector3(2,2), new Vector2(1.5f,1)); //heavy
    }

    void CreateHitbox(int weaponTyp, int attackStyle){ //float offsetX, float offsetY, Vector3 size
        HitboxInfo info = HitboxInfos[weaponTyp,attackStyle];
        Vector3 myPos = new Vector3(transform.position.x + info.offset.x, transform.position.y + info.offset.y);
        GameObject myHitbox = Instantiate(hitboxPreafab, myPos, Quaternion.identity);
        BoxCollider2D box = myHitbox.GetComponent<BoxCollider2D>();
        box.size = info.size;
        myHitbox.transform.parent = this.gameObject.transform;
        hitboxen.Add(myHitbox);
    }

}