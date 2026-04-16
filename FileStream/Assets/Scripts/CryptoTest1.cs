using UnityEngine;

public class CryptoTest1 : MonoBehaviour
{
    private byte[] encrypted;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            string planeText = "Hello~ AES!";

            encrypted = CryptoUtil.Encrypt(planeText);

            Debug.Log(System.BitConverter.ToString(encrypted));
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            string plainText = CryptoUtil.Decrypt(encrypted);
            Debug.Log(plainText);
        }
    }
}
