using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public GameObject hitboxPreafab;
    public HitboxInfo[,] HitboxInfos = new HitboxInfo[3, 4];
    //public ArrayList HitboxInfos = new ArrayList();
    ArrayList hitboxen = new ArrayList();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HitboxInfos[0, 0] = new HitboxInfo(new Vector3(4, 1), new Vector2(2, 0.5f)); //ligth
                                                                                     //HitboxInfos[0, 1] = new HitboxInfo(new Vector3(2, 2), new Vector2(1.5f, 1)); //heavy
        HitboxInfos[0, 1] = new HitboxInfo(new Vector3(3.2f, 4.2f), new Vector2(1.4f, 2.1f)); //heavy
        HitboxInfos[1, 0] = new HitboxInfo(new Vector3(4f, 1.5f), new Vector2(0f,1f));
        HitboxInfos[1, 1] = new HitboxInfo(new Vector3(3f, 2f), new Vector2(0f, 0f));
    }

    public void CreateHitbox(int weaponTyp, int attackStyle)
    { //float offsetX, float offsetY, Vector3 size
        HitboxInfo info = HitboxInfos[weaponTyp, attackStyle];
        Vector3 myPos = new Vector3(transform.position.x + info.offset.x * transform.localScale.x, transform.position.y + info.offset.y);
        GameObject myHitbox = Instantiate(hitboxPreafab, myPos, Quaternion.identity);
        BoxCollider2D box = myHitbox.GetComponent<BoxCollider2D>();
        box.size = new Vector2(info.size.x, info.size.y);
        myHitbox.transform.parent = this.gameObject.transform;
        myHitbox.GetComponent<Hitbox>().attackIndex = attackStyle;
        myHitbox.GetComponent<Hitbox>().weaponIndex = weaponTyp;
        myHitbox.GetComponent<Hitbox>().playerTrans = transform;
        hitboxen.Add(myHitbox);
    }




}
