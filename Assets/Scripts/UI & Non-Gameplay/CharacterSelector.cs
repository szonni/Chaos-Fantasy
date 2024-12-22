using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public CharacterData characterData;

    // Create a singleton, set DontDestroyOnLoad so the game object is not destroyed when scene changes
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Prevent the creation of another instance of this class
        else
        {
            Debug.Log("CharacterSelector duplicate destroyed: " + this);
            Destroy(gameObject);
        }
    }
    
    // Static method so we can use this method by calling the class directly and not needing objects
    // Used in CharacterHandler
    public static CharacterData LoadData()
    {
        return instance.characterData;
    }

    public void SelectCharacter(CharacterData character)
    {
        characterData = character;
    }

    // Method to destroy singleton since it is no longer needed after transfering data
    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
