using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    
    public LineRenderer lineRenderer;
    public AudioSource boom;
    public float power = 10f;
    public Vector2 minPow;
    public Vector2 maxPow;
    public Rigidbody2D rb;

    Camera cam;
    Vector3[] positions = new Vector3[2];
    Vector3 startPos;
    Vector3 currentPos;
    Vector2 force;

    bool launched;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        lineRenderer.positionCount = 0;
        rb.gravityScale = 0;
        launched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (launched == false) {
            if (Input.GetMouseButtonDown(0)) {
                startPos = cam.ScreenToWorldPoint(Input.mousePosition);
                startPos.z = 1;
            }

            if (Input.GetMouseButton(0)) {
                currentPos = cam.ScreenToWorldPoint(Input.mousePosition);
                renderLine(startPos, currentPos); 
            }

            if (Input.GetMouseButtonUp(0)) {
                rb.gravityScale = 1;
                force = new Vector2(Mathf.Clamp(startPos.x - currentPos.x, minPow.x, maxPow.x), Mathf.Clamp(startPos.y - currentPos.y, minPow.x, maxPow.x));
                rb.AddForce(force * power, ForceMode2D.Impulse);
                lineRenderer.positionCount = 0;
                currentPos = startPos;
                launched = true;
            }
        }
    }
    void renderLine(Vector3 start, Vector3 end)
    {
        end.z = 1;
        lineRenderer.positionCount = 2;
        positions[1] = start;
        positions[0] = end;
        lineRenderer.SetPositions(positions);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Brick") {
            boom.Play();
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (other.gameObject.name == "Down_Wall") {
            this.gameObject.transform.position = new Vector2(0, -3);
            rb.Sleep();
            launched = false;
            rb.gravityScale = 0;
        }
    }
}
