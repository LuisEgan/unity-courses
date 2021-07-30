using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    AudioSource audioSource;
    Scene scene;
    bool collisionsEnables = true;

    enum State { Alive, Dying, Transcending };
    State state = State.Alive;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float successDelay = 1f;
    [SerializeField] float deathDelay = 1f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem mainEngineFX;
    [SerializeField] ParticleSystem deathFX;
    [SerializeField] ParticleSystem successFX;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            ProccesInput();
        }

        HandleDebugInput();
    }

    private void HandleDebugInput()
    {
        if (!Debug.isDebugBuild) return;

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionsEnables = !collisionsEnables;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || !collisionsEnables) return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                OnFinish();
                break;
            default:
                OnDeath();
                break;
        }
    }

    private void OnFinish()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successFX.Play();
        Invoke("LoadNextLevel", successDelay);
    }

    private void OnDeath()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        deathFX.Play();
        Invoke("HandleDeath", deathDelay);
    }

    private void HandleDeath()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = scene.buildIndex + 1;
        nextSceneIndex = SceneManager.sceneCountInBuildSettings == nextSceneIndex ? 0 : nextSceneIndex;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ProccesInput()
    {
        Thrust();
        Rotate();
    }


    private void Rotate()
    {
        // take manual control of rotation
        rigidBody.freezeRotation = true;

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }

        // resume physics control of rotation
        rigidBody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            DoThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineFX.Stop();
        }
    }

    private void DoThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineFX.Play();
    }
}
