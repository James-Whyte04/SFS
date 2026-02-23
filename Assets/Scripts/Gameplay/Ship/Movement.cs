using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


  //  Vector3 localForward = new Vector3(0, 0, 1);
  //  Vector3 worldForward = shipRotation * localForward;  // Assuming you implemented Quaternion * Vector3
  //  Vector3 thrustForce = worldForward * thrustPower;

    //rotation speed multiplier

    public float rotSpeed = 20f;

    //rotation Quaternions

    Quaternion pitch = new Quaternion(0, 0, 0, 1);
    Quaternion yaw = new Quaternion(0, 0, 0, 1);
    Quaternion roll = new Quaternion(0, 0, 0, 1);
    Quaternion rotation = new Quaternion();

    //rotation variables

    float pitchAngle;
    float pitchAccel;
    float pitchMaxSpeed = 0.05f;
    float pitchForce = 0.3f;
    float pitchDecel = 0.05f;


    float yawAngle;
    float yawAccel;
    float yawMaxSpeed = 0.05f;
    float yawForce = 0.2f;
    float yawDecel = 0.05f;



    float rollAngle;
    float rollAccel;
    float rollMaxSpeed = 0.05f;
    float rollForce = 0.3f;
    float rollDecel = 0.05f;

    bool counterThrust = true;

    //movement

    float mass = 2f;
    float thrustForce = 100f;
    float brakingForce = 1.5f;
    float maxVelocity = 100f;
    float accelTracker;

    bool opposeForce = false;
    bool isBraking = false;
    bool steerThrust = true;
    bool movementActive = true;

    Vector3 eulerRot = new Vector3(0, 0, 0);

    Vector3 force = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, 0, 0);
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 position = new Vector3(0, 0, 0);



    Vector3 move = new Vector3(0, 0, 0);

    //pitch yaw roll, x y z

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (counterThrust)
            {
                counterThrust = false;
                Debug.Log("counter thrust deactivated");
            }
            else
            {
                counterThrust = true;
                Debug.Log("counter thrust activated");
            }
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            if (steerThrust)
            {
                steerThrust = false;
                Debug.Log("steer thruster deactivated");
            }
            else
            {
                steerThrust = true;
                Debug.Log("steer thruster activated");
            }
        }


        if (Input.GetKey(KeyCode.Space))
        {
            isBraking = true;
            Debug.Log("braking");
        }
        else
        {
            isBraking = false;
        }

    }

    void FixedUpdate()
    {
        if (movementActive)
        {
            UpdateMovement();
        }
    }

    private void UpdateMovement()
    {

        //pitch axis

        if (Input.GetAxis("Pitch") != 0)
        {
            if (pitchAccel < pitchMaxSpeed && pitchAccel > -pitchMaxSpeed)
            {
                pitchAccel += ((Time.deltaTime * Input.GetAxis("Pitch")) * pitchForce) / mass;
            }
        }

        else if (pitchAccel != 0f && Input.GetAxis("Pitch") == 0 && counterThrust)
        {
            if (pitchAccel > 0f)
            {
                pitchAccel -= (Time.deltaTime * pitchDecel) / mass;
                if (pitchAccel < 0.0005f) { pitchAccel = 0f; }
            }
            else
            {
                pitchAccel += Time.deltaTime * pitchDecel / mass;
                if (pitchAccel > -0.0005f) { pitchAccel = 0f; }
            }
        }

        //yaw axis

        if (Input.GetAxis("Yaw") != 0)
        {
            if (yawAccel < yawMaxSpeed && yawAccel > -yawMaxSpeed)
            {
                yawAccel += ((Time.deltaTime * Input.GetAxis("Yaw")) * yawForce) / mass;
            }
        }

        else if (yawAccel != 0f && Input.GetAxis("Yaw") == 0 && counterThrust)
        {
            if (yawAccel > 0f)
            {
                yawAccel -= Time.deltaTime * yawDecel / mass;
                if (yawAccel < 0.0005f) { yawAccel = 0f; }
            }
            else
            {
                yawAccel += Time.deltaTime * yawDecel / mass;
                if (yawAccel > -0.0005f) { yawAccel = 0f; }
            }
        }


        //roll axis

        if (Input.GetAxis("Roll") != 0)
        {
            if (rollAccel < rollMaxSpeed && rollAccel > -rollMaxSpeed)
            {
                rollAccel += ((Time.deltaTime * Input.GetAxis("Roll")) * rollForce) / mass;
            }
        }

        else if (rollAccel != 0f && Input.GetAxis("Roll") == 0 && counterThrust)
        {
            if (rollAccel > 0f)
            {
                rollAccel -= Time.deltaTime * rollDecel / mass;
                if (rollAccel < 0.0005f) { rollAccel = 0f; }
            }
            else
            {
                rollAccel += Time.deltaTime * rollDecel / mass;
                if (rollAccel > -0.0005f) { rollAccel = 0f; }
            }
        }


        pitchAngle += pitchAccel;
        pitch = new Quaternion(Mathf.Sin(pitchAngle / 2) * 1,
            Mathf.Sin(pitchAngle / 2) * 0,
            Mathf.Sin(pitchAngle / 2) * 0,
            Mathf.Cos(pitchAngle / 2));


        yawAngle += yawAccel;
        yaw = new Quaternion(Mathf.Sin(yawAngle / 2) * 0,
          Mathf.Sin(yawAngle / 2) * 1,
          Mathf.Sin(yawAngle / 2) * 0,
          Mathf.Cos(yawAngle / 2));


        rollAngle += rollAccel;
        roll = new Quaternion(Mathf.Sin(rollAngle / 2) * 0,
          Mathf.Sin(rollAngle / 2) * 0,
          Mathf.Sin(rollAngle / 2) * 1,
          Mathf.Cos(rollAngle / 2));


        Quaternion rp = MyQuat.MultiplyQuats(pitch, roll);
        rotation = MyQuat.MultiplyQuats(yaw, rp);

        transform.rotation = rotation;

       
        
        //movement

        //linear force

        if (Input.GetAxis("Accelerate") != 0 && !isBraking)
        {
            force = MyVector3.ScaleVector(transform.forward, Input.GetAxis("Accelerate"));
            
            if (Input.GetAxis("Accelerate") < 0 && accelTracker != Input.GetAxis("Accelerate"))
            {
                opposeForce = true;
            }

            accelTracker = Input.GetAxis("Accelerate");
        }   

        else
        {
            force = Vector3.zero;
        }


        //acceleration

        if (force != Vector3.zero && Input.GetAxis("Accelerate") != 0 && !isBraking)
        {
            acceleration = MyVector3.DivideVector(MyVector3.ScaleVector(force, thrustForce), mass);
        }

        else
        {
           acceleration = Vector3.zero;
        }

        if (acceleration != Vector3.zero)
        {
            if (MyVector3.Magnitude(velocity) * Input.GetAxis("Accelerate") >= maxVelocity && !opposeForce)
            {
                if (MyVector3.NormalizeVector(acceleration) != MyVector3.NormalizeVector(velocity))
                {
                    velocity = MyVector3.LERP(MyVector3.ScaleVector(MyVector3.NormalizeVector(velocity), maxVelocity), MyVector3.ScaleVector(MyVector3.NormalizeVector(acceleration), maxVelocity), Time.deltaTime);
                }
            }

            else if (MyVector3.Magnitude(velocity) * Input.GetAxis("Accelerate") <= -maxVelocity && !opposeForce)
            {
                if (MyVector3.NormalizeVector(acceleration) != MyVector3.NormalizeVector(velocity))
                {
                    velocity = MyVector3.LERP(MyVector3.ScaleVector(MyVector3.NormalizeVector(velocity), maxVelocity), MyVector3.ScaleVector(MyVector3.NormalizeVector(acceleration), maxVelocity), Time.deltaTime);
                }
            }

            else
            {
                velocity += acceleration * Time.deltaTime;

                if (opposeForce)
                {
                    opposeForce = false;
                }
            }

        }

        else if (velocity != Vector3.zero && !isBraking && steerThrust)
        {
            velocity = MyVector3.LERP(MyVector3.ScaleVector(MyVector3.NormalizeVector(velocity), MyVector3.Magnitude(velocity)), MyVector3.ScaleVector(MyVector3.ScaleVector(transform.forward, accelTracker), MyVector3.Magnitude(velocity)), Time.deltaTime);
        }

        if (isBraking)
        {
            velocity = MyVector3.LERP(velocity, Vector3.zero, Time.deltaTime * brakingForce);

            if (MyVector3.Magnitude(velocity) <= 0.5f)
            {
                velocity = new Vector3(0, 0, 0);
            }
        }


        transform.position += velocity * Time.deltaTime;
    }

    //ROTATING OBJECTS OCCURS IN THE ORDER: ROLL-->PITCH-->YAW
}
//TRS SCALE-->ROTATE-->TRANSLATE