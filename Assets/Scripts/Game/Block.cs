using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour {

    public static byte NowFlyingBlocksCount = 0;
    public static Block[] blocks = new Block[0];

    public byte ownerId = 0;
    public new Transform transform;
    private Vector3 _velocity = new Vector3();

    public Vector3 velocity {get {return _velocity;}
        set {
            _velocity = value;

            if(velocity != Vector3.zero)
                NowFlyingBlocksCount++;    
        }}
    public GameObject recentCell;

    public static Color32[] playersColors;



    
    private void OnEnable() {
        transform = GetComponent<Transform>();

        Array.Resize(ref blocks, blocks.Length + 1);
        blocks[blocks.Length - 1] = this;
    }


    private void Update() {
        transform.position += velocity*Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collider) {
        if(velocity == new Vector3()) return;
        if(collider.gameObject == recentCell) return;
        if(!collider.gameObject.CompareTag("Cell")) return;

        velocity = new Vector3();
        NowFlyingBlocksCount--;
        collider.gameObject.GetComponent<Cell>().AddBlock(this);
    }

    public void SetOwner(byte ownerId) {
        // Debug.Log("recent: " + this.ownerId + ", new: " + ownerId);
        this.ownerId = ownerId;
        GetComponent<SpriteRenderer>().color = playersColors[ownerId];
    }

}
