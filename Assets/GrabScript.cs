using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour
{
    private GameObject targetItem = null;
    private bool isHolding = false;

    // Key untuk grab dan release
    public KeyCode grabKey = KeyCode.G;
    public KeyCode releaseKey = KeyCode.R;

    void Update()
    {
        if (Input.GetKeyDown(grabKey) && targetItem != null && !isHolding)
        {
            // Ambil item
            targetItem.transform.SetParent(transform); // Menempel ke ujung lengan
            targetItem.transform.localPosition = Vector3.zero;
            targetItem.transform.localRotation = Quaternion.identity;

            // Nonaktifkan physics
            Rigidbody rb = targetItem.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;

            isHolding = true;
        }

        if (Input.GetKeyDown(releaseKey) && isHolding)
        {
            // Lepaskan item
            targetItem.transform.SetParent(null);

            Rigidbody rb = targetItem.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;

            isHolding = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Simpan objek yang masuk trigger
        if (other.CompareTag("Item")) // Pastikan objek punya tag "Item"
        {
            targetItem = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset jika keluar
        if (other.CompareTag("Item") && !isHolding)
        {
            targetItem = null;
        }
    }
}