using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {

    /* References to all dynamic attributes of the UI Canvas*/
    public Image playerHealth;
    public Image opponentHealth;

    public Image playerMana;
    public Image opponentMana;

    public Image castBar;
    public Image castBarFill;
    public Image spellThumbNail;
    public Text timeToCast;

    public Image p2CastBar;
    public Image p2CastFill;
    public Image p2ThumbNail;

    public Image[] player1Buffs;
    public Image[] player1Debuffs;

    public Image[] player2Buffs;
    public Image[] player2Debuffs;

	// Use this for initialization
	void Start () {
        timeToCast.text = "";
        toggleCastBarVisible();
        togglePlayerTwoCastBarVisible();

        for(int i = 0; i < player1Buffs.Length; i++)
        {
            player1Buffs[i].enabled = false;
            player1Debuffs[i].enabled = false;
            player2Buffs[i].enabled = false;
            player2Debuffs[i].enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void setPlayerOneHealth(float _amount)
    {
        playerHealth.fillAmount = _amount;
    }

    public void setPlayerTwoHealth(float _amount)
    {
        opponentHealth.fillAmount = _amount;
    }

    public void setPlayerOneMana(float _amount)
    {
        playerMana.fillAmount = _amount;
    }

    public void setPlayerTwoMana(float _amount)
    {
        opponentMana.fillAmount = _amount;
    }

    //A lazy method that simply toggles all the elements of the cast bar
    //to either visible or invisible
    private void toggleCastBarVisible()
    {
        castBar.enabled = !castBar.enabled;
        castBarFill.enabled = !castBarFill.enabled;
        spellThumbNail.enabled = !spellThumbNail.enabled;
        timeToCast.enabled = !timeToCast.enabled;
    }

    private void togglePlayerTwoCastBarVisible()
    {
        p2CastBar.enabled = !p2CastBar.enabled;
        p2CastFill.enabled = !p2CastFill.enabled;
        p2ThumbNail.enabled = !p2ThumbNail.enabled;
    }

    public bool isPlayerTwoCastVisible()
    {
        return p2CastBar.enabled;
    }

    //Returns whether or not the cast bar is visible
    public bool isCastBarVisible()
    {
        return castBarFill.enabled;
    }

    public void HandleCastBarInit(int _index, Sprite _thumbnail)
    {
        if (_index == 0)
        {
            if (!isCastBarVisible())
            {
                toggleCastBarVisible();
                castBarFill.fillAmount = 0.0f;
                timeToCast.text = "0.0";
                spellThumbNail.sprite = _thumbnail;
            }
        }
        else
        {
            if(!isPlayerTwoCastVisible())
            {
                togglePlayerTwoCastBarVisible();
                p2CastFill.fillAmount = 0.0f;
                p2ThumbNail.sprite = _thumbnail;
            }
        }
    }

    public void HandleCastBarToggle(int _index)
    {
        if (_index == 0) toggleCastBarVisible();
        else togglePlayerTwoCastBarVisible();
    }

    public void FillCastBar(int _index, float _elapsedTime, float _reqTime)
    {
        if(_index == 0)
        {
            castBarFill.fillAmount = _elapsedTime / _reqTime;
            timeToCast.text = _elapsedTime.ToString().Substring(0, 4);
        }
        else
        {
            p2CastFill.fillAmount = _elapsedTime / _reqTime;
        }
    }

    public Image GetNextAvailableBuff(int _playerIndex)
    {
        if (_playerIndex == 0)
        {
            for (int i = 0; i < player1Buffs.Length; i++)
                if (player1Buffs[i].enabled == false) return player1Buffs[i];
        }
        else
        {
            for (int i = 0; i < player2Buffs.Length; i++)
                if (player2Buffs[i].enabled == false) return player2Buffs[i];
        }
        return null;
    }

    public Image GetNextAvailableDebuff(int _playerIndex)
    {
        if (_playerIndex == 0)
        {
            for (int i = 0; i < player1Debuffs.Length; i++)
                if (player1Debuffs[i].enabled == false) return player1Debuffs[i];
        }
        else
        {
            for (int i = 0; i < player2Debuffs.Length; i++)
                if (player2Debuffs[i].enabled == false) return player2Debuffs[i];
        }
        return null;
    }
}
