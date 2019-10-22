using UnityEngine;

public class HandController : MonoBehaviour
{
    
    private GameObject hand;
    private GameObject candidatSelection;
    private GameObject selected;

    private float movSpeed = 0.005f;
    private bool grabObjectFlag = false;

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
        timePassed += Time.deltaTime;
        KeyboardMovements();
        grabObjectFlag = false;
    }

    private void FixedUpdate() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("handable")){
            candidatSelection = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("handable")){
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

        if(Input.GetKey("a")) {
            gameObject.transform.Rotate(Vector3.forward);
            ApplyRotationSelectedObject(Vector3.forward);
        }

        if(Input.GetKey("e")) {
            gameObject.transform.Rotate(Vector3.back);
            ApplyRotationSelectedObject(Vector3.back);
        }

        if(Input.GetKey("g")){
            if(timePassed >= gkeyDelay){

                if (candidatSelection != null && selected == null)
                {
                    Debug.Log("lol");
                    grabObjectFlag = true;
                    selected = candidatSelection;
                } 
                else if(selected != null && !grabObjectFlag) {
                    Debug.Log("coucoulol");
                    gameObject.transform.Translate(Vector3.left / 8);
                    selected = null;
                }

                timePassed = 0;
            }
        }
    }

    private void Move(Vector3 direction){
        gameObject.transform.Translate(direction * movSpeed);
    }

    private void ApplyRotationSelectedObject(Vector3 rotation) {
        if(selected != null){
            selected.transform.Rotate(rotation);
        }
    }

}
