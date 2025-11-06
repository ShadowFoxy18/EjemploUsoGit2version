using UnityEngine;

public class CreadorDeObjetosEnRaton : MonoBehaviour
{
    [SerializeField]
    GameObject[] prefabs;

    [SerializeField]
    Material[] materials;


    int currentPrefab;

    int currentMaterial;

    GameObject currentGameObject = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    int RandomNumber(int min, int max)
    {
        return Random.Range(min, max);
    }

    int VerifyNumber(int min, int max)
    {
        int randomNumber = RandomNumber(min, max);
        while (randomNumber == currentMaterial)
        {
            randomNumber = RandomNumber(min, max);
        }
        currentMaterial = RandomNumber(min, max);

        return currentMaterial;
    }

    void ComprobarTeclado()
    {
        int oldNumber = currentPrefab;
        if (Input.GetKeyUp(KeyCode.Alpha1) || (Input.GetKeyUp(KeyCode.Keypad1)))
        {
            currentPrefab = 0;
        } else 
        if (Input.GetKeyUp(KeyCode.Alpha2) || (Input.GetKeyUp(KeyCode.Keypad2)))
        {
            currentPrefab = 1;
        } else
        if (Input.GetKeyUp(KeyCode.Alpha3) || (Input.GetKeyUp(KeyCode.Keypad3)))
        {
            currentPrefab = 2;
        }

        if (currentGameObject != null && oldNumber != currentPrefab)
        {
            DestroyImmediate(currentGameObject);
        }
    }

    void CambiarMaterial()
    {
        //RandomColor
        if (Input.GetKey(KeyCode.R))
        {
            currentGameObject.GetComponent<MeshRenderer>().material = materials[VerifyNumber(0, materials.Length)];
        }
    }

    void MoverObjeto()
    {
        if (currentGameObject != null)
        {

            Debug.Log(Input.mousePosition);

            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(myRay.origin, myRay.direction * 100, Color.red);


            RaycastHit hit;


            currentGameObject.SetActive(false);
            if (Physics.Raycast(myRay, out hit, 100f))
            {
                //currentGameObject = Instantiate(prefabs[RandomNumber(currentPrefab, prefabs.Length)]);
                currentGameObject.transform.position = hit.point + Vector3.up;
            }
            currentGameObject.SetActive(true);


        }
    }

    void CrearObjeto()
        {
            if (currentGameObject == null)
            {
                currentGameObject = Instantiate(prefabs[currentPrefab]);
                currentGameObject.transform.position = Vector3.zero;
            }
        }

    void ComprobarClickOn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentGameObject = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ComprobarTeclado();

        CrearObjeto();

        MoverObjeto();
        
        CambiarMaterial();

        ComprobarClickOn();
    }
}
