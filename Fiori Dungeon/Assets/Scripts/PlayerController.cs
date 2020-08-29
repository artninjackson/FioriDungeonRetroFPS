using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Declaring data variables
    public static PlayerController instance;
    public Rigidbody2D rb;
    public float movementSpeed = 5f;
    private Vector2 movementInput;
    private Vector2 mouseInput;
    public float mouseSensitivity = 5f;
    public Camera cameraView;
    public GameObject hitImpact;
    public int currentAmmo = 10;
    public Animator bowAnimation;
    public Animator anim;
    public int currentHealth;
    public int maximumHealth = 100;
    public int currentArmor;
    public int maximumArmor = 100;
    public GameObject deadScreen;
    private bool hasDied;
    public Text Health, Ammo;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
        Health.text = currentHealth.ToString() + "%";
        Ammo.text = currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasDied)
        {
            //player movement
            movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector3 moveHorizontal = transform.up * -movementInput.x;
            Vector3 moveVertical = transform.right * movementInput.y;
            rb.velocity = (moveHorizontal + moveVertical) * movementSpeed;

            //player view rotation and control
            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y") * mouseSensitivity);
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x,
                transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z - mouseInput.x);

            cameraView.transform.localRotation = Quaternion.Euler(
                cameraView.transform.localRotation.eulerAngles +
                new Vector3(
                0f, mouseInput.y, 0f));

            //Attacks
            if (Input.GetMouseButtonDown(0))
            {
                AudioController.instance.PlayBowShot();
                //bow
                if (currentAmmo > 0)
                {
                    Ray ray = cameraView.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); ///(.5,.5,0) is the center of the screen
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        //Debug.Log("Looking at " + hit.transform.name);
                        Instantiate(hitImpact, hit.point, transform.rotation);

                        if (hit.transform.tag == "Enemy")
                        {
                            hit.transform.parent.GetComponent<EnemyController>().takeDamage();
                        }
                    }
                    else
                    {
                        //Debug.Log("Nothing to see here, chief.");
                    }
                    currentAmmo--;
                    bowAnimation.SetTrigger("Shoot");
                    UpdateAmmoUI();
                }

                //if sword
                //TODO

                //if dagger
                //TODO

            }
            if(movementInput != Vector2.zero)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
    }

    public void takeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        AudioController.instance.PlayPlayerPain();
        if (currentHealth <= 0)
        {
            deadScreen.SetActive(true);
            hasDied = true;
            currentHealth = 0;
        }

        Health.text = currentHealth.ToString() + "%";
    }

    public void addHealth(int healthAmount)
    {
        currentHealth += healthAmount;
        if(currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }

        Health.text = currentHealth.ToString() + "%";
    }

    public void UpdateAmmoUI()
    {
        Ammo.text = currentAmmo.ToString();
    }

    public void UpdateHealthUI()
    {
        Health.text = currentHealth.ToString() +"%";
    }
}
