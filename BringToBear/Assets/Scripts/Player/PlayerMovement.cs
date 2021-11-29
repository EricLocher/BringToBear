using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject playerShip;
    public GameObject background;
    public Rigidbody2D rb;
    public float stabilizeSpeed;
    public float thrust = 200;
    public float turnSpeed = 5;

    public Vector2 lookDirection;
    public bool boost;

    bool driftMode;
    float thrust = 200;
    float turnSpeed = 5;
    float angle;
    float cameraAngle;
    

    void Start()
    {
        lookDirection = Vector2.zero;
    }

    void Update()
    {
        Debug.Log(angle);
        driftMode = false;

        lookDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * -1;

        Rotate();

        if (driftMode)
        {
            turnSpeed = 10;
            cameraAngle = Camera.main.transform.eulerAngles.z;
        }
        else
        {
            turnSpeed = 3;
            cameraAngle = 0;
            Stabilize();
        }

       // GravityAdjuster();

    }

    

    public void Rotate()
    {
        //TODO: Fix issue where direction gets screwy. Seems to be an issue where we need to differentiate between -180 / 180 etc.

        angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;

        Quaternion _newRotation = Quaternion.Euler(0, 0, 0);
        if (lookDirection != Vector2.zero)
        {
            _newRotation = Quaternion.Euler(new Vector3(0, 0, angle + cameraAngle));
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, _newRotation, Time.deltaTime * turnSpeed);
    }

    public void Thrust(float thrustPower)
    {
        lookDirection = dir;
        Debug.Log(lookDirection);
        Rotate();
    }

    public void SetDriftMode(bool state)
    {
        driftMode = state;
    }

    public bool GetDriftMode()
    {
        return driftMode;
    }

    public void Thrust(float thrustPower)
    {
        if (thrustPower > 0) { thrust = 300 * thrustPower; }
        else { thrust = 200; }

        rb.AddForce(transform.up * thrust * Time.deltaTime, ForceMode2D.Impulse);
    }

    public void Brake(float brakePower)
    {
        rb.AddForce(-rb.velocity * (5 * brakePower));        
    }


    public void Respawn()
    {
        transform.position = new Vector3(-10, 0, 0);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        //GameObject newShip = Instantiate(playerShip, new Vector3(0, 0, 0), Quaternion.identity);
        //Destroy(gameObject);
    }

    private void Stabilize()
    {
        if (angle == 0 || angle == 180 || angle == -180) { return; }
        else if ((angle < -90 && angle > -180) || (angle > 90 && angle < 180)) { return; }

        Vector2 stabilizeVector = Vector2.zero;
        Vector2 vel = rb.velocity.normalized;

        if (angle > 0 && vel != Vector2.zero)
        {
            stabilizeVector = Vector2.left;
        }
        else if (angle < 0 && vel != Vector2.zero)
        {
            stabilizeVector = Vector2.right;
        }

        stabilizeVector = (stabilizeVector).normalized;
        rb.AddForce(stabilizeVector * stabilizeSpeed * Time.deltaTime, ForceMode2D.Impulse);

    }


    public void GravityAdjuster()
    {
        Bounds _bg = background.GetComponent<SpriteRenderer>().bounds;
        float _Posx = transform.position.x - _bg.center.x;

        if (Mathf.Abs(_Posx) > _bg.extents.x)
        {

            if (rb.gravityScale < 6)
                rb.gravityScale = 6;
            else
                rb.gravityScale += Time.deltaTime;
        }

        //TODO: Make this dynamic to the viewport rather than hard-coded y-values
        else if (transform.position.y < -12)
        {
            if (rb.gravityScale < 1)
                rb.gravityScale = 1;
            else
                rb.gravityScale -= Time.deltaTime * 2;
        }
        else if (transform.position.y > 9)
        {
            if (rb.gravityScale > 6)
                rb.gravityScale = 6;
            else
                rb.gravityScale += Time.deltaTime * 10;
        }
        else
        {
            rb.gravityScale = 3;
        }
    }



}
