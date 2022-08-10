using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] CameraControl mainMenuCamera;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject characterSelector;
    [SerializeField] Transform characterSpawnPos;
    [SerializeField] GameObject currCharacter;
    [SerializeField] GameObject[] characters;
    [SerializeField] GameObject[] realCharacters;
    [SerializeField] GameManager gameManager;

    [SerializeField] GameObject[] toDestroy;

    private int selectedCharacterID;

    public void onPressCharacterButton()
    {
        mainMenuCamera.goToSelectAngle();
        mainMenu.SetActive(false);
        characterSelector.SetActive(true);
    }

    public void onCharacterConfirm()
    {
        mainMenuCamera.goToOriginalAngle();
        characterSelector.SetActive(false);
        mainMenu.SetActive(true);
        currCharacter.GetComponent<Animator>().SetBool("Selected", true);
    }

    public void switchCharacter(int id)
    {
        Destroy(currCharacter);
        currCharacter = Instantiate(characters[id], characterSpawnPos.position, characterSpawnPos.rotation);
        Destroy(currCharacter.GetComponent<Character>());
        selectedCharacterID = id;
    }

    public void onPressArcadeMode()
    {
        for (int i = 0; i < toDestroy.Length; i++)
        {
            Destroy(toDestroy[i]);
        }
        Destroy(currCharacter);
        Instantiate(realCharacters[selectedCharacterID], characterSpawnPos.position, characterSpawnPos.rotation);
        mainMenuCamera.goToMainAngle();
        gameManager.onGameStart();
        Destroy(mainMenu);
        Destroy(this);

       
    }
}
