using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossController : MonoBehaviour
{

    [SerializeField] GameObject rightHand;
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject eye;
    [SerializeField] float delayBetweenStates = 1f;
    [SerializeField] float rotationSpeed = 40f;
    private Health health;
    private float direction = 1f;
    enum states
    {
        rotate,
        dash,
        slap,
        slam,
        idle,
        wait,
        reversing,
        charging,
        turning
    }

    enum locations
    {
        left,
        center,
        right
    }

    enum parts
    {
        leftHand,
        eye,
        rightHand
    }

    private states current = states.wait;
    private states last = states.idle;
    private locations spot = locations.center;
    private bool inPlace = true;
    private bool delaying = false;
    private parts raised = parts.leftHand;

    //private float currentDegree = 1f;

    // Start is called before the first frame update
    private void Awake()
    {
        health = GetComponent<Health>();
        Random.InitState((int)System.DateTime.Now.Ticks);
        StartCoroutine(stateDelay(delayBetweenStates));
    }

    private void Update()
    {
        if(current == states.idle)
        {
            decideNextState();
        }

        if (current == states.rotate)
        {
            transform.RotateAround(new Vector3(5f, 0f, -5f), Vector3.up, rotationSpeed * Time.deltaTime * direction);

            if(spot == locations.center)
            {
                if (direction == 1f)
                {
                    if(transform.position.x > 5)
                    {
                        transform.position = new Vector3(5f, 0f, 15f);
                        transform.LookAt((2 * transform.position) - new Vector3(5f, 0f, -5f));
                        current = states.wait;
                        StartCoroutine(stateDelay(delayBetweenStates));
                    }
                }
                else
                {
                    if (transform.position.x < 5)
                    {
                        transform.position = new Vector3(5f, 0f, 15f);
                        transform.LookAt((2 * transform.position) - new Vector3(5f, 0f, -5f));
                        current = states.wait;
                        StartCoroutine(stateDelay(delayBetweenStates));
                    }
                }
            }

            if (transform.position.z < -5f)
            {
                if (direction == 1f)
                {
                    transform.position = new Vector3(25f, 0f, -5f);
                    transform.LookAt((2 * transform.position) - new Vector3(5f, 0f, -5f));
                    current = states.wait;
                    StartCoroutine(stateDelay(delayBetweenStates));
                }
                else
                {
                    transform.position = new Vector3(-15f, 0f, -5f);
                    transform.LookAt((2 * transform.position) - new Vector3(5f, 0f, -5f));
                    current = states.wait;
                    StartCoroutine(stateDelay(delayBetweenStates));
                }
            }
        }

        if(current == states.dash)
        {
            if(spot == locations.center)
            {
                inPlace = false;
                locations nextSpot = (locations)(Random.Range(0, 2) * 2);
                if(spot < nextSpot)
                {
                    direction = 1f;
                }
                else
                {
                    direction = -1f;
                }
                spot = nextSpot;
            }
            if (!inPlace)
            {
                transform.RotateAround(new Vector3(5f, 0f, -5f), Vector3.up, rotationSpeed * Time.deltaTime * direction);
                if (transform.position.z < -5f)
                {
                    if (direction == 1f)
                    {
                        transform.position = new Vector3(25f, 0f, -5f);
                        transform.LookAt((2 * transform.position) - new Vector3(5f, 0f, -5f));
                        StartCoroutine(tempDelay(delayBetweenStates));
                        delaying = true;
                        inPlace = true;
                    }
                    else
                    {
                        transform.position = new Vector3(-15f, 0f, -5f);
                        transform.LookAt((2 * transform.position) - new Vector3(5f, 0f, -5f));
                        StartCoroutine(tempDelay(delayBetweenStates));
                        delaying = true;
                        inPlace = true;
                    }
                }

            }
            else if (!delaying)
            {
                raised = (parts)Random.Range(0, 3);
                StartCoroutine(reverse());
            }
        }

        if(current == states.reversing)
        {
            if (spot == locations.left)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 2);
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 2);
            }
            if(raised == parts.leftHand)
            {
                leftHand.transform.Translate(Vector3.up * Time.deltaTime * 3);
            }
            if (raised == parts.eye)
            {
                eye.transform.Translate(Vector3.up * Time.deltaTime * 3);
            }
            if (raised == parts.rightHand)
            {
                rightHand.transform.Translate(Vector3.up * Time.deltaTime * 3);
            }
        }

        if(current == states.charging)
        {
            if(spot == locations.left)
            {
                transform.Translate(Vector3.back * Time.deltaTime * 20);
                if (transform.position.x > 25f)
                {
                    transform.position = new Vector3(25f, 0f, -5f);
                    spot = locations.right;
                    current = states.wait;
                    StartCoroutine(turn());
                }
            }
            else
            {
                transform.Translate(Vector3.back * Time.deltaTime * 20);
                if (transform.position.x < -15f)
                {
                    transform.position = new Vector3(-15f, 0f, -5f);
                    spot = locations.left;
                    current = states.wait;
                    StartCoroutine(turn());
                }
            }
        }

        if(current == states.turning)
        {
            if (spot == locations.left)
            {
                transform.Rotate(new Vector3(0f, 180f, 0f) * Time.deltaTime);
            }
            else
            {
                transform.Rotate(new Vector3(0f, 180f, 0f) * Time.deltaTime);
            }
            if (raised == parts.leftHand)
            {
                leftHand.transform.Translate(Vector3.down * Time.deltaTime * 6);
            }
            if (raised == parts.eye)
            {
                eye.transform.Translate(Vector3.down * Time.deltaTime * 6);
            }
            if (raised == parts.rightHand)
            {
                rightHand.transform.Translate(Vector3.down * Time.deltaTime * 6);
            }
        }

    }

    private void decideNextState()
    {
        current = (states)Random.Range(0, 2);
        while (last == current)
        {
            current = (states)Random.Range(0, 2);
        }
        last = current;
        if(current == states.rotate)
        {
            locations nextSpot = (locations)Random.Range(0, 3);
            while(nextSpot == spot)
            {
                nextSpot = (locations)Random.Range(0, 3);
            }
            if(nextSpot < spot)
            {
                direction = -1f;
            }
            else
            {
                direction = 1f;
            }
            spot = nextSpot;
        }
        
    }

    private IEnumerator stateDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        current = states.idle;
    }

    private IEnumerator tempDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        delaying = false;
    }

    private IEnumerator reverse()
    {
        current = states.reversing;
        yield return new WaitForSeconds(2f);
        current = states.charging;
    }

    private IEnumerator turn()
    {
        yield return new WaitForSeconds(1f);
        current = states.turning;
        yield return new WaitForSeconds(1f);
        transform.LookAt((2 * transform.position) - new Vector3(5f, 0f, -5f));
        current = states.wait;
        StartCoroutine(stateDelay(delayBetweenStates));
    }


    public void takeHit(int damage)
    {
        health.takeDamage(damage);
    }
}
