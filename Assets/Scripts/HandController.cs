using UnityEngine;

public class HandController : MonoBehaviour
{
    
    private GameObject hand;
    private GameObject candidatSelection;
    private GameObject selected;

    private float movSpeed = 0.005f;

    private float gkeyDelay = 0.5f;
    private float timePassed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // timePassed += Time.deltaTime;
        KeyboardMovements();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Handable")){
            candidatSelection = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
		if(other.gameObject.CompareTag("Handable") && (other.gameObject == selected || other.gameObject == candidatSelection) ){
            candidatSelection = null;
            selected = null;
        }
    }

    void KeyboardMovements()
    {
        if (Input.GetKey("d"))
        {
            Move(Vector3.right);
        }
        
        if (Input.GetKey("q"))
        {
            Move(Vector3.left);
        }
        
        if (Input.GetKey("z"))
        {
            Move(Vector3.forward);
        }
        
        if (Input.GetKey("s"))
        {
            Move(Vector3.back);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Move(Vector3.up);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            Move(Vector3.down);
        }

        if (Input.GetKey(KeyCode.LeftArrow)) {
            gameObject.transform.Rotate(Vector3.forward);
            ApplyRotationSelectedObject(Vector3.forward);
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            gameObject.transform.Rotate(Vector3.back);
            ApplyRotationSelectedObject(Vector3.back);
        }
        
        if (Input.GetKeyDown(KeyCode.G) && candidatSelection != null && selected == null)
        {
            selected = candidatSelection;
        }

        if (Input.GetKeyUp(KeyCode.G) && selected != null)
        {
            selected = null;
        }
    }

    private void Move(Vector3 direction){
        gameObject.transform.Translate(direction * movSpeed);
    }

    private void ApplyRotationSelectedObject(Vector3 rotation) {
        if (selected != null) 
        {
            float a = Mathf.Clamp(selected.transform.rotation.z, -0.35f, 0.35f);
            if (a == 0.35f || a >= -0.35f)
            {
                selected.transform.Rotate(rotation);
            }
        }
    }

}
