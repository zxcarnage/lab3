using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class Field : MonoBehaviour
{
    [SerializeField] private List<Card> _cards;
    [SerializeField] private float _cardDisappereanseDelay;
    [SerializeField] private Transform _container;

    private bool _isCardOpened;
    private List<Card> _cardsInstantiated;
    private Card _currentOpenedCard;

    private void Start()
    {
        _cardsInstantiated = new List<Card>();
        ShuffleCards();
        Initialize();
    }

    private void ShuffleCards()
    {
        Random rng = new Random(); 
        int n = _cards.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            (_cards[k], _cards[n]) = (_cards[n], _cards[k]);
        }  
    }

    private void Initialize()
    {
        foreach (var card in _cards)
        {
            var cardInstantiated = Instantiate(card, _container);
            _cardsInstantiated.Add(cardInstantiated);
            cardInstantiated.CardOpened += OnNewCardOpened;
        }
    }

    private void OnDisable()
    {
        foreach (var card in _cardsInstantiated)
        {
            card.CardOpened -= OnNewCardOpened;
        }
    }

    private void OnNewCardOpened(Card card)
    {
        if (_currentOpenedCard == card)
            return;
        if (!_currentOpenedCard)
        {
            _currentOpenedCard = card;
            return;
        }

        if (_currentOpenedCard.CardInfo.Picture == card.CardInfo.Picture)
        {
            StartCoroutine(PairGuessed(card));
        }
        else
        {
            StartCoroutine(PairUnguessed(card));
        }
            
    }

    private IEnumerator PairUnguessed(Card secondCard)
    {
        Freeze(true);
        yield return new WaitForSeconds(_cardDisappereanseDelay);
        secondCard.CloseCard();
        _currentOpenedCard.CloseCard();
        _currentOpenedCard = null;
        Freeze(false);
    }

    private IEnumerator PairGuessed(Card secondCard)
    {
        Freeze(true);
        _cardsInstantiated.Remove(_currentOpenedCard);
        _cardsInstantiated.Remove(secondCard);
        yield return new WaitForSeconds(_cardDisappereanseDelay);
        Destroy(_currentOpenedCard.gameObject);
        Destroy(secondCard.gameObject);
        Freeze(false);
        if (_cardsInstantiated.Count == 0)
            ServiceLocator.GameUI.ShowWinScreen();
    }

    private void Freeze(bool toFreeze)
    {
        foreach (var card in _cardsInstantiated)
        {
            card.Freeze(toFreeze);
        }
    }
    
}