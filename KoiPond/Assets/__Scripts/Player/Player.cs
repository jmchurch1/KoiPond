using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject bullet;

    public Transform muzzle;
    public GameObject explosion;
    
    public List<Sprite> rangerSprite = new List<Sprite>();
    public List<Sprite> druidSprite = new List<Sprite>();
    public List<Sprite> assassinSprites = new List<Sprite>();
    public List<Sprite> wizardSprites = new List<Sprite>();
    /*
     *  index 0: facing down
     *  index 1: facing up
     *  index 2: facing side
     */
    
    //public AudioSource bulletSound;
    
    //https://www.youtube.com/watch?v=whzomFgjT50
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float _shotWaitTime = .1f;
    
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private bool _waitingShot = false;
    private Vector2 _movement;

    private int _currCharacter = 0;
    private int _selectedCharacter = 1;
    private int _numCharacters = 4;
    
    

    private float _bulletForce = 500f;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        ScoreCounter.ScoreInstance.PlayCharacter(_currCharacter);
        ScoreCounter.ScoreInstance.SetSelectedCharacter(_selectedCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        // get movement vector values x and y
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        switch (_currCharacter)
        {
            case 0:
                if (_movement.x != 0 || _movement.y != 0)
                {
                    if (Math.Abs(_movement.x) > Math.Abs(_movement.y))
                    {
                        _spriteRenderer.sprite = wizardSprites[2];
                        if (_movement.x > 0)
                            _spriteRenderer.flipX = true;
                        else
                            _spriteRenderer.flipX = false;
                    }
                    else
                    {
                        _spriteRenderer.flipX = false;
                        if (_movement.y > 0)
                            _spriteRenderer.sprite = wizardSprites[1];
                        else
                            _spriteRenderer.sprite = wizardSprites[0];
                    }
                }

                break;
            case 1:
                _spriteRenderer.sprite = assassinSprites[0];
                break;
            case 2:
                _spriteRenderer.sprite = druidSprite[0];
                break;
            case 3:
                _spriteRenderer.sprite = rangerSprite[0];
                break;
        }
        
        
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        

        if (Input.GetButton("Fire1"))
        {
            if (!_waitingShot)
                StartCoroutine("Shoot", ray);
        }

        if (ScoreCounter.ScoreInstance.GetSwitchAvailable())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                StartCoroutine(nameof(Switch));
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ScoreCounter.ScoreInstance.DeselectCharacter(_selectedCharacter);
            _selectedCharacter = (_selectedCharacter + 1) % _numCharacters;
            // make sure player doesn't select current character
            if (_selectedCharacter == _currCharacter) _selectedCharacter = (_selectedCharacter + 1) % _numCharacters;
            Debug.Log(_selectedCharacter);
            ScoreCounter.ScoreInstance.SetSelectedCharacter(_selectedCharacter);
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger entered");
        /*
        if (other.CompareTag("Enemy"))
        {
            SceneManager.LoadScene(1);
        }
        */
    }

    private IEnumerator Shoot(Ray ray)
    {
        _waitingShot = true;
        yield return new WaitForSeconds(_shotWaitTime);
        // play bullet shot sound
        // bulletSound.Play();
        // Instantiate bullet
        GameObject currBullet = Instantiate(bullet, muzzle.position, transform.rotation);
        // Get direction from player to mouse
        Vector3 dir = (ray.origin - transform.position).normalized;
        currBullet.GetComponent<Rigidbody2D>().AddForce(dir * _bulletForce);
        // reset waiting shot bool
        _waitingShot = false;
    }

    IEnumerator Switch()
    {
        yield return new WaitForSeconds(.1f);
        ScoreCounter.ScoreInstance.RemoveAllEnemies();
        yield return new WaitForSeconds(.5f);
        ScoreCounter.ScoreInstance.DeselectCharacter(_currCharacter);
        ScoreCounter.ScoreInstance.PlayCharacter(_selectedCharacter);
        _currCharacter = _selectedCharacter;
        _selectedCharacter = (_selectedCharacter + 1) % _numCharacters;
        ScoreCounter.ScoreInstance.SetSelectedCharacter(_selectedCharacter);
        ScoreCounter.ScoreInstance.SetSwitchAvailable(false);
    }
}
