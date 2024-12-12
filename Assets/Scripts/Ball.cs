using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    private GameManager gm;
    public Rigidbody rb;
    public UnityEngine.UI.Image levelBar;
    private Vector2 firstPos;
    private Vector2 SecondPos;
    private Vector2 currentPos;
    public float moveSpeed;
    public float currentGroundNumber;
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    
    void Update()
    {
        Swipe();
        levelBar.fillAmount = currentGroundNumber / gm.groundNumbers;
        if(levelBar.fillAmount == 1){
            gm.LevelUpdate();
        }
    }

    private void Swipe(){
        if(Input.GetMouseButtonDown(0)){
            firstPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        }

        if(Input.GetMouseButtonUp(0)){
            SecondPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

            currentPos = new Vector2(
                SecondPos.x - firstPos.x,
                SecondPos.y - firstPos.y
            );

            currentPos.Normalize();

            if(currentPos.y < 0 && currentPos.x  > -0.5f && currentPos.x < 0.5f){
                rb.velocity = Vector3.back * moveSpeed;
            }else if(currentPos.y > 0 && currentPos.x  > -0.5f && currentPos.x < 0.5f){
                rb.velocity = Vector3.forward * moveSpeed;
            }else if(currentPos.x < 0 && currentPos.y  > -0.5f && currentPos.y < 0.5f){
                rb.velocity = Vector3.left * moveSpeed;
            }else if(currentPos.x > 0 && currentPos.y  > -0.5f && currentPos.y < 0.5f){
                rb.velocity = Vector3.right * moveSpeed;
            }
        }
 
    }
    private void OnCollisionEnter(Collision other){
        if(other.gameObject.GetComponent<MeshRenderer>().material.color != Color.red){
             if(other.gameObject.tag == "Ground"){
                other.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Constraints();
                currentGroundNumber++;
            }
        }
       
    }

    private void Constraints(){
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
}
