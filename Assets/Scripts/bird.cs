using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bird : MonoBehaviour
{
    Vector3 _initialPosition;
    bool _birdWasLaunched;
    float _timeSittingAround;
    bool saApasa = true;


    [SerializeField] private float _launchPower = 500;

    private void Awake()
    {
        _initialPosition = transform.position;
    }
    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);


        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if (transform.position.x > 30 ||
           transform.position.y > 7 ||
           transform.position.x < -12 ||
           transform.position.y < -7 ||
           _timeSittingAround > 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnMouseDown()
    {
        if (saApasa)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<LineRenderer>().enabled = true;
        }

    }

    private void OnMouseUp()
    {
        if (saApasa)
        {
            GetComponent<SpriteRenderer>().color = Color.white;

            Vector2 directionToInitialPosition = _initialPosition - transform.position;
            GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
            GetComponent<Rigidbody2D>().gravityScale = 1;
            _birdWasLaunched = true;

            GetComponent<LineRenderer>().enabled = false;

            saApasa = false;
        }
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (saApasa &&
            newPosition.x <=5 &&
            newPosition.y <=4.50 &&
            newPosition.x >=-12 &&
            newPosition.y >=-5.50)
        {
            transform.position = new Vector3(newPosition.x, newPosition.y);
        }
    }

}
