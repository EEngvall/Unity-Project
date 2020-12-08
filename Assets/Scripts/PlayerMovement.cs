using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    public int health = 10;
    [Header("Visuals")]
    public GameObject model;
    public float rotationSpeed = 2;
    [Header("Equipment")]
    public Sword sword;
    public GameObject bombPrefab;
    public float throwingSpeed;
    public int bombAmount = 5;
    public Bow bow;
    public int arrowAmount = 15;

    [Header("Movement")]
    public float movingVelocity = 10f;
    public float jumpVelocity = 10f;
    public float knockbackForce = 100f;
    private bool canJump;
    private Quaternion targetModelRotation;
    private Rigidbody playerRigidbody;
    private float knockbackTimer;


    void Start() {
        bow.gameObject.SetActive(false);

        playerRigidbody = GetComponent<Rigidbody>();
        targetModelRotation = Quaternion.Euler(0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
           RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f)) {
            canJump = true;
        }
        
        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, targetModelRotation, Time.deltaTime*rotationSpeed);

        if (knockbackTimer > 0) {
            knockbackTimer -= Time.deltaTime;
        } else MonitorMovement();
    }
    void MonitorMovement() 
    {
        playerRigidbody.velocity = new Vector3(
            0,
            playerRigidbody.velocity.y,
            0
        );
        if (Input.GetKey("w") | Input.GetKey("up")) {
            playerRigidbody.velocity = new Vector3(
            playerRigidbody.velocity.x,
            playerRigidbody.velocity.y,
            movingVelocity
            );
            targetModelRotation = Quaternion.Euler(0, 0, 0);
            //model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0, 180, 0), Time.deltaTime*rotationSpeed);
            //transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey("a") | Input.GetKey("left")) {
            playerRigidbody.velocity = new Vector3(
            -movingVelocity,
            playerRigidbody.velocity.y,
            playerRigidbody.velocity.z
            );
            targetModelRotation = Quaternion.Euler(0, 270, 0);
            //model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime*rotationSpeed);
            //transform.position += Vector3.left * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey("d") | Input.GetKey("right")) {
            playerRigidbody.velocity = new Vector3(
            movingVelocity,
            playerRigidbody.velocity.y,
            playerRigidbody.velocity.z
            );
            targetModelRotation = Quaternion.Euler(0, 90, 0);
            //model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0, 270, 0), Time.deltaTime*rotationSpeed);
            //transform.position += Vector3.right * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey("s") | Input.GetKey("down")) {
            playerRigidbody.velocity = new Vector3(
            playerRigidbody.velocity.x,
            playerRigidbody.velocity.y,
            -movingVelocity
            );
            targetModelRotation = Quaternion.Euler(0, 180, 0);
            //model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime*rotationSpeed);
            //transform.position += Vector3.back * Time.deltaTime * movementSpeed;
        }
     
        if (canJump && Input.GetKeyDown("space")) {
            canJump = false;
            //playerRigidbody.AddForce(0, jumpForce, 0);
            playerRigidbody.velocity = new Vector3 (
                playerRigidbody.velocity.x,
                jumpVelocity,
                playerRigidbody.velocity.z
            );
        }
        //Check equipment interaction
        if (Input.GetKeyDown("e")) {
            sword.gameObject.SetActive(true);
            bow.gameObject.SetActive(false);
            sword.Attack();
        }
        if (Input.GetKeyDown("q")) {
            ThrowBomb();
        }
        if (Input.GetKeyDown("r")){
            if (arrowAmount >0) {
                bow.gameObject.SetActive(true);
                sword.gameObject.SetActive(false);
                bow.Attack();
                arrowAmount--;
            }
        }
        
    }
    private void ThrowBomb() {
        if (bombAmount <=0) {
            return;
        }
        GameObject bombObject = Instantiate (bombPrefab);
        bombObject.transform.position = transform.position + model.transform.forward;

        Vector3 throwingDirection = (model.transform.forward + Vector3.up).normalized;
        bombObject.GetComponent<Rigidbody>().AddForce(throwingDirection * throwingSpeed);

        bombAmount--;
    }

    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.GetComponent<EnemyBullet>() != null) {
            Hit((transform.position - otherCollider.transform.position).normalized);
        }
    }
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Enemy>()) {
            Hit((transform.position - collision.transform.position).normalized);
        }
    }
    private void Hit(Vector3 direction) {
        Vector3 knockbackDirection = (direction + Vector3.up).normalized;
        playerRigidbody.AddForce(knockbackDirection*knockbackForce);
        knockbackTimer = 1f;
        Debug.Log(knockbackDirection);
        health--;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
