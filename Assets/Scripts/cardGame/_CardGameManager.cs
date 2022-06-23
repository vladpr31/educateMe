using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _CardGameManager : MonoBehaviour
{
    [SerializeField] GameObject[] Scene;
    public static _CardGameManager Instance;
    public static int gameSize = 2; //Default game size.
    //An instance of the game object.
    [SerializeField]
    private GameObject prefab;
    //Parent Object of the cards.
    [SerializeField]
    private GameObject cardList;
    //Sprite for back of the card.
    [SerializeField]
    private Sprite cardBack;
    //All Front Card Sprites.
    [SerializeField]
    private Sprite[] sprites;
    //Cards List.
    private _Card[] cards;
    //Card placing on panel(GamePanel in unity.)
    [SerializeField]
    private GameObject panel;
    // Preloading sprites to avoid lag.
    [SerializeField]
    private _Card spritePreload;
    //UI of the sliders.
    [SerializeField]
    private Text sizeLabel;
    [SerializeField]
    private Slider sizeSlider;
    //[SerializeField]
    //private Text timeLabel;
    //private float time;
    [SerializeField] AudioClip correctSound;
    [SerializeField] GameObject gameMenu;
    

    private int spriteSelected;
    private int cardSelected;
    private int cardLeft;
    private bool gameStart;
    void Awake()
    {
        Scene[0].SetActive(true);
        Instance = this;
    }
    void Start()
    {
        if (getMap.getMapID() != 5)
        {
            gameStart = false;
            gameSize = 4;
            panel.SetActive(false);
            gameMenu.SetActive(false);
            StartCardGame();
        }
        else
        {
            Scene[0].SetActive(false);
            gameMenu.SetActive(true);
            panel.SetActive(false);
            gameStart = false;
            gameSize = 2;
        }

    }
    // Purpose is to allow preloading of panel, so that it does not lag when it loads
    // Call this in the start method to preload all sprites at start of the script
    private void PreloadCardImage()
    {
        for (int i = 0; i < sprites.Length; i++)
            spritePreload.SpriteID = i;
        spritePreload.gameObject.SetActive(false);
    }
    // Starting the Game.
    public void StartCardGame()
    {
        Scene[0].SetActive(false);
        if (gameStart) return; //returns if the game is already running.
        gameStart = true;
        // Sets the card,cards size and Position.
        SetGamePanel();
        // Renews game variables.
        cardSelected = spriteSelected = -1;
        cardLeft = cards.Length;
        // Allocation of sprites to cards.
        SpriteCardAllocation();
        StartCoroutine(HideFace());
        //time = 0;
    }

    //Initialization of all Cards,Size of cards, Positions all depends on Board Size.
    private void SetGamePanel(){
        int isOdd = gameSize % 2 ; //If game size is odd, Remove 1 card.(Removes from middle of the board).
        panel.SetActive(true);
        cards = new _Card[gameSize * gameSize - isOdd];
        foreach (Transform child in cardList.transform) //Removes gameobjects from the Parent.
        {
            GameObject.Destroy(child.gameObject);
        }
        //Calculating the Position between the cards and the start position of each card, based on the Panel size.
        RectTransform panelsize = panel.transform.GetComponent(typeof(RectTransform)) as RectTransform;
        float row_size = panelsize.sizeDelta.x;
        float col_size = panelsize.sizeDelta.y;
        float scale = 0.9f/gameSize;
        float xInc = (row_size-250f)/gameSize;
        float yInc = (col_size)/gameSize;
        float curX = -xInc * (float)(gameSize / 2);
        float curY = -yInc * (float)(gameSize / 2);

        if(isOdd == 0) {
            curX += xInc / 2;
            curY += yInc / 2;
        }
        float initialX = curX;
        for (int i = 0; i < gameSize; i++) //Y-axis cards.
        {
            curX = initialX;
            for (int j = 0; j < gameSize; j++) //X-axis cards
            {
                GameObject c; //if the board is Odd number, and we have odd Card removes 1 card from the middle of the board
                                // and places it as the last card.
                if (isOdd == 1 && i == (gameSize - 1) && j == (gameSize - 1))
                {
                    int index = gameSize / 2 * gameSize + gameSize / 2;
                    c = cards[index].gameObject;
                }
                else
                {
                    c = Instantiate(prefab); //Creats a prefab, Instantiate function creates a temporary Object(Of the cards)
                                             // the game runs so we wont need to create the same objects over and over.
                    c.transform.parent = cardList.transform; //Assigns parent.
                    int index = i * gameSize + j;
                    cards[index] = c.GetComponent<_Card>();
                    cards[index].ID = index;
                    c.transform.localScale = new Vector3(scale, scale); //modifies the size.
                }
                c.transform.localPosition = new Vector3(curX, curY, 0); //Assigns location.
                curX += xInc;

            }
            curY += yInc;
        }

    }
    
    void ResetFace() //resets the "Face-Down" cards rotation.
    {
        for (int i = 0; i < gameSize; i++)
            cards[i].ResetRotation();
    }
    IEnumerator HideFace() //Displays the card at the start of the game for few seconds and then flips and game starts.
    {
        
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < cards.Length; i++)
            cards[i].Flip();
        yield return new WaitForSeconds(0.5f);
    }
    private void SpriteCardAllocation() //Allocation of pairs of sprite into a card instance in unity.
    {
        int i, j;
        int[] selectedID = new int[cards.Length / 2];
        
        for (i = 0; i < cards.Length/2; i++) //Loop for sprite selection, randomly selects cards.
        {
            int value = Random.Range(0, sprites.Length - 1);
            // we check if previously we slected that sprite/card.
            // if we have more cards than sprites, we reuse those sprites.
            for (j = i; j > 0; j--)
            {
                if (selectedID[j - 1] == value)
                    value = (value + 1) % sprites.Length;
            }
            selectedID[i] = value;
        }

        
        for (i = 0; i < cards.Length; i++) //Deallocates Sprites(Cards animal face).
        {
            cards[i].Active();
            cards[i].SpriteID = -1;
            cards[i].ResetRotation();
        }
        
        for (i = 0; i < cards.Length / 2; i++) //Allocates sprites(cards animal face).
            for (j = 0; j < 2; j++)
            {
                int value = Random.Range(0, cards.Length - 1);
                while (cards[value].SpriteID != -1)
                    value = (value + 1) % cards.Length;

                cards[value].SpriteID = selectedID[i];
            }

    }
    
    public void SetGameSize() //'Difficulty' Slider, this slider creates a board based on the slider.
    {
        gameSize = (int)sizeSlider.value;
        sizeLabel.text = gameSize + " X " + gameSize;
    }
    
    public Sprite GetSprite(int spriteId) //return sprite by ID, sprites are the cards themselves.(with the animals faced).
    {
        return sprites[spriteId];
    }
    
    public Sprite CardBack() //flips the card backwards (the back of the card with no animal).
    {
        return cardBack;
    }
    
    public bool canClick() //check wether u can click on cards or not, applies only when the game starts.
    {
        if (!gameStart)
            return false;
        return true;
    }
    public void cardClicked(int spriteId, int cardId) //when card is clicked.
    {
        // first card facing up.(animal side)
        if (spriteSelected == -1)
        {
            spriteSelected = spriteId;
            cardSelected = cardId;
        }
        else
        { // second card facing up.(animal side)
            if (spriteSelected == spriteId)
            {
                //if 2 cards match, makes those card Inactive which makes them disappear.
                cards[cardSelected].Inactive();
                cards[cardId].Inactive();
                cardLeft -= 2;
                AudioSource.PlayClipAtPoint(correctSound, new Vector3(0, 0, -10), 0.4f);
                CheckGameWin();
            }
            else
            {
                //if not match, flip them back.
                cards[cardSelected].Flip();
                cards[cardId].Flip();
            }
            cardSelected = spriteSelected = -1;
        }
    }
    
    private void CheckGameWin() //if baord is cleared.
    {
        if (cardLeft == 0) //if no cards on board call EndGame().
        {
            EndGame();
        }
    } 
    private void EndGame() //ends the game and return to menu.
    {
        gameStart = false;
        panel.SetActive(false);
        if (getMap.getMapID() != 5)
        {
            Scene[1].SetActive(true);
            Scene[0].SetActive(true);
        }
        else
        {
            gameStart = false;
            gameMenu.SetActive(true);
        }
    }
    public void GiveUp() //Ends Game.
    {
        if (getMap.getMapID() == 5)
        {
            EndGame();
        }
        else
        {
            SceneManager.LoadScene(getMap.getMapID());
        }
    }
  

    
    /*
    private void Update() //function for the timer, which starts as soon as the game is statred.
    { 

        if (gameStart) {
            time += Time.deltaTime;
            timeLabel.text = "Time: " + (float)System.Math.Round(time, 1) + "s";
        }
    }
    */
}
