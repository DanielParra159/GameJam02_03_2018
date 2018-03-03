using System.Collections;
using Source.DemoRockPaperScissors.Scripts.Types;
using Source.Photon;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Source.DemoRockPaperScissors.Scripts
{
    public class RpsCore : MonoBehaviour
    {
        [SerializeField] Button _rock;
        [SerializeField] Button _paper;
        [SerializeField] Button _scrissors;
        [SerializeField] Button _connect;
        [SerializeField] Button _rejoin;
        [SerializeField] RectTransform _connectParent;
        [SerializeField] RectTransform _gameParent;
        [SerializeField] RectTransform _reconnectParent;
        [SerializeField] RectTransform _timerFillImage;
        [SerializeField] Text _turnText;
        [SerializeField] Text _timeText;
        [SerializeField] Text _remotePlayerText;
        [SerializeField] Text _localPlayerText;
        [SerializeField] Image _winOrLossImage;
        [SerializeField] Image _localSelectionImage;
        [SerializeField] Image _remoteSelectionImage;
        [SerializeField] HandType _remoteSelection;
        [SerializeField] HandType _localSelection;
        [SerializeField] HandType _randomHand;
        [SerializeField] Sprite _selectedRock;
        [SerializeField] Sprite _selectedPaper;
        [SerializeField] Sprite _selectedScissors;
        [SerializeField] Sprite _spriteWin;
        [SerializeField] Sprite _spriteLose;
        [SerializeField] Sprite _spriteDraw;
        [SerializeField] InputField _inputField;

        ResultType _result;
        string previousRoomPlayerPrefKey = "PUN:Demo:RPS:PreviousRoom";
        string _previousRoom;

        const string NickNamePlayerPrefsKey = "NickName";

        bool _isShowingResults;
        private ServiceSingelton.IRoom _room;

        void Start()
        {
            _room = ServiceSingelton.Instance.CreatePun(5f);

            _room.OnRoomLeft += OnLeftRoom;
            _room.OnRoomJoined += OnJoinedRoom;
            _room.OnPlayerConnected += OnPhotonPlayerConnected;
            _room.OnPlayerDisconnected += OnPhotonPlayerDisconnected;
            _room.OnFailedToConnect += OnConnectionFail;
            _room.OnMasterConnected += OnConnectedToMaster;
            _room.OnLobbyJoined += OnJoinedLobby;
            _room.OnRandomJoinFailed += OnPhotonRandomJoinFailed;
            _room.OnJoinRoomFailed += OnPhotonJoinRoomFailed;

            _room.OnTurnTimeEnds += OnTurnTimeEnds;
            _room.OnPlayerFinished += OnPlayerFinished;
            _room.OnTurnBegins += OnTurnBegins;


            _inputField.text = PlayerPrefs.HasKey(NickNamePlayerPrefsKey)
                ? PlayerPrefs.GetString(NickNamePlayerPrefsKey)
                : "";

            _gameParent.gameObject.SetActive(false);
            _reconnectParent.gameObject.SetActive(false);

            _connect.onClick.AddListener(OnClickConnect);
            _rejoin.onClick.AddListener(OnClickReConnectAndRejoin);
            _rock.onClick.AddListener(OnClickRock);
            _paper.onClick.AddListener(OnClickPaper);
            _scrissors.onClick.AddListener(OnClickScissors);

            _localSelectionImage.gameObject.SetActive(false);
            _remoteSelectionImage.gameObject.SetActive(false);
            StartCoroutine(CycleRemoteHandCoroutine());

            RefreshUiViews();
        }

        void OnDestroy()
        {
            _connect.onClick.RemoveListener(OnClickConnect);
            _rejoin.onClick.RemoveListener(OnClickReConnectAndRejoin);
            _rock.onClick.RemoveListener(OnClickRock);
            _paper.onClick.RemoveListener(OnClickPaper);
            _scrissors.onClick.RemoveListener(OnClickScissors);

            _room.OnRoomLeft -= OnLeftRoom;
            _room.OnRoomJoined -= OnJoinedRoom;
            _room.OnPlayerConnected -= OnPhotonPlayerConnected;
            _room.OnPlayerDisconnected -= OnPhotonPlayerDisconnected;
            _room.OnFailedToConnect -= OnConnectionFail;
            _room.OnMasterConnected -= OnConnectedToMaster;
            _room.OnLobbyJoined -= OnJoinedLobby;
            _room.OnRandomJoinFailed -= OnPhotonRandomJoinFailed;
            _room.OnJoinRoomFailed -= OnPhotonJoinRoomFailed;
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                _room.LeaveRoom();
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                _room.ConnectUsingSettings("0");
                PhotonHandler.StopFallbackSendAckThread();
            }

            if (!_room.InRoom)
            {
                return;
            }

            if (_room.Connected && _reconnectParent.gameObject.GetActive())
            {
                _reconnectParent.gameObject.SetActive(false);
            }

            if (!_room.Connected && !_room.Connecting && !_reconnectParent.gameObject.GetActive())
            {
                _reconnectParent.gameObject.SetActive(true);
            }

            if (_room.CurrentRoom.PlayerCount > 1)
            {
                if (_room.TurnIsOver)
                {
                    return;
                }

                if (_turnText != null)
                {
                    _turnText.text = _room.Turn.ToString();
                }

                if (_room.Turn > 0 && _timeText != null && !_isShowingResults)
                {
                    _timeText.text = _room.RemainingSecondsInTurn.ToString("F1") + " SECONDS";

                    _timerFillImage.anchorMax = new Vector2(1f - _room.RemainingSecondsInTurn / _room.TurnDuration, 1f);
                }
            }

            UpdatePlayerTexts();

            Sprite selected = SelectionToSprite(_localSelection);
            if (selected != null)
            {
                _localSelectionImage.gameObject.SetActive(true);
                _localSelectionImage.sprite = selected;
            }

            if (_room.IsCompletedByAll)
            {
                selected = SelectionToSprite(_remoteSelection);
                if (selected != null)
                {
                    _remoteSelectionImage.color = new Color(1, 1, 1, 1);
                    _remoteSelectionImage.sprite = selected;
                }
            }
            else
            {
                EnableButtons(_room.CurrentRoom.PlayerCount > 1);

                if (_room.CurrentRoom.PlayerCount < 2)
                {
                    _remoteSelectionImage.color = new Color(1, 1, 1, 0);
                }

                // if the turn is not completed by all, we use a random image for the remote hand
                else if (_room.Turn > 0 && !_room.IsCompletedByAll)
                {
                    // alpha of the remote hand is used as indicator if the remote player "is active" and "made a turn"
                    PhotonPlayer remote = _room.LocalPlayer.GetNext();
                    float alpha = 0.5f;
                    if (_room.IsPlayerTurnFinished(remote))
                    {
                        alpha = 1;
                    }

                    if (remote != null && remote.IsInactive)
                    {
                        alpha = 0.1f;
                    }

                    _remoteSelectionImage.color = new Color(1, 1, 1, alpha);
                    _remoteSelectionImage.sprite = SelectionToSprite(_randomHand);
                }
            }
        }

        void EnableButtons(bool state)
        {
            _rock.interactable = state;
            _paper.interactable = state;
            _scrissors.interactable = state;
        }

        void OnTurnBegins(int turn)
        {
            Debug.Log("OnTurnBegins() turn: " + turn);
            _localSelection = HandType.None;
            _remoteSelection = HandType.None;

            _winOrLossImage.gameObject.SetActive(false);

            _localSelectionImage.gameObject.SetActive(false);
            _remoteSelectionImage.gameObject.SetActive(true);

            _isShowingResults = false;

            EnableButtons(true);
        }


        void OnTurnCompleted(int obj)
        {
            Debug.Log("OnTurnCompleted: " + obj);

            CalculateWinAndLoss();
            UpdateScores();
            OnEndTurn();
        }

        void OnPlayerFinished(PhotonPlayer player, int turn, object move)
        {
            Debug.Log("OnTurnFinished: " + player + " turn: " + turn + " action: " + move);

            if (player.IsLocal)
            {
                _localSelection = (HandType) (byte) move;
            }
            else
            {
                _remoteSelection = (HandType) (byte) move;
            }
        }

        void OnTurnTimeEnds(int obj)
        {
            if (!_isShowingResults)
            {
                Debug.Log("OnTurnTimeEnds: Calling OnTurnCompleted");
                OnTurnCompleted(-1);
            }
        }

        void UpdateScores()
        {
            if (_result == ResultType.LocalWin)
            {
                _room.LocalPlayer.AddScore(1);
            }
        }

        void StartTurn()
        {
            if (_room.IsMasterClient)
            {
                _room.BeginTurn();
            }
        }

        void MakeTurn(HandType selection)
        {
            _room.SendMovement(selection, true);
        }

        void OnEndTurn()
        {
            StartCoroutine(ShowResultsBeginNextTurnCoroutine());
        }

        IEnumerator ShowResultsBeginNextTurnCoroutine()
        {
            EnableButtons(false);
            _isShowingResults = true;
            // yield return new WaitForSeconds(1.5f);

            if (_result == ResultType.Draw)
            {
                _winOrLossImage.sprite = _spriteDraw;
            }
            else
            {
                _winOrLossImage.sprite = _result == ResultType.LocalWin ? _spriteWin : _spriteLose;
            }

            _winOrLossImage.gameObject.SetActive(true);

            yield return new WaitForSeconds(2.0f);

            StartTurn();
        }


        void EndGame()
        {
            Debug.Log("EndGame");
        }

        void CalculateWinAndLoss()
        {
            _result = ResultType.Draw;
            if (_localSelection == _remoteSelection)
            {
                return;
            }

            if (_localSelection == HandType.None)
            {
                _result = ResultType.LocalLoss;
                return;
            }

            if (_remoteSelection == HandType.None)
            {
                _result = ResultType.LocalWin;
            }

            if (_localSelection == HandType.Rock)
            {
                _result = (_remoteSelection == HandType.Scissors) ? ResultType.LocalWin : ResultType.LocalLoss;
            }

            if (_localSelection == HandType.Paper)
            {
                _result = (_remoteSelection == HandType.Rock) ? ResultType.LocalWin : ResultType.LocalLoss;
            }

            if (_localSelection == HandType.Scissors)
            {
                _result = (_remoteSelection == HandType.Paper) ? ResultType.LocalWin : ResultType.LocalLoss;
            }
        }

        Sprite SelectionToSprite(HandType hand)
        {
            switch (hand)
            {
                case HandType.None:
                    break;
                case HandType.Rock:
                    return _selectedRock;
                case HandType.Paper:
                    return _selectedPaper;
                case HandType.Scissors:
                    return _selectedScissors;
            }

            return null;
        }

        void UpdatePlayerTexts()
        {
            PhotonPlayer remote = _room.LocalPlayer.GetNext();
            PhotonPlayer local = _room.LocalPlayer;

            if (remote != null)
            {
                // should be this format: "name        00"
                _remotePlayerText.text = remote.NickName + "        " + remote.GetScore().ToString("D2");
            }
            else
            {
                _timerFillImage.anchorMax = new Vector2(0f, 1f);
                _timeText.text = "";
                _remotePlayerText.text = "waiting for another player        00";
            }

            if (local != null)
            {
                // should be this format: "YOU   00"
                _localPlayerText.text = "YOU   " + local.GetScore().ToString("D2");
            }
        }

        IEnumerator CycleRemoteHandCoroutine()
        {
            while (true)
            {
                // cycle through available images
                _randomHand = (HandType) Random.Range(1, 4);
                yield return new WaitForSeconds(0.5f);
                yield break;
            }
        }

        void OnClickRock()
        {
            MakeTurn(HandType.Rock);
        }

        void OnClickPaper()
        {
            MakeTurn(HandType.Paper);
        }

        void OnClickScissors()
        {
            MakeTurn(HandType.Scissors);
        }

        void OnClickConnect()
        {
            _room.ConnectUsingSettings("0");
            _room.StopFallbackSendAckThread();

            ApplyUserIdAndConnect();
        }

        void OnClickReConnectAndRejoin()
        {
            _room.ReconnectAndRejoin();
            _room.StopFallbackSendAckThread();
        }

        void RefreshUiViews()
        {
            _timerFillImage.anchorMax = new Vector2(0f, 1f);

            _connectParent.gameObject.SetActive(!_room.InRoom);
            _gameParent.gameObject.SetActive(_room.InRoom);


            EnableButtons(_room.CurrentRoom != null && _room.CurrentRoom.PlayerCount > 1);
        }

        void OnLeftRoom()
        {
            Debug.Log("OnLeftRoom()");
            RefreshUiViews();
        }

        void OnJoinedRoom()
        {
            Debug.Log("Joined room: " + _room.CurrentRoom.Name);
            _previousRoom = _room.CurrentRoom.Name;
            PlayerPrefs.SetString(previousRoomPlayerPrefKey, _previousRoom);

            RefreshUiViews();

            if (_room.CurrentRoom.PlayerCount == 2)
            {
                if (_room.Turn == 0)
                {
                    // when the room has two players, start the first turn (later on, joining players won't trigger a turn)
                    StartTurn();
                }
            }
            else
            {
                Debug.Log("Waiting for another player");
            }
        }

        void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            Debug.Log("Other player arrived");

            if (_room.CurrentRoom.PlayerCount == 2)
            {
                if (_room.Turn == 0)
                {
                    // when the room has two players, start the first turn (later on, joining players won't trigger a turn)
                    StartTurn();
                }
            }
        }

        void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            Debug.Log("Other player disconnected! " + otherPlayer.ToStringFull());
        }

        void OnConnectionFail(DisconnectCause cause)
        {
            _reconnectParent.gameObject.SetActive(true);
        }

        void OnConnectedToMaster()
        {
            if (PlayerPrefs.HasKey(previousRoomPlayerPrefKey))
            {
                _previousRoom = PlayerPrefs.GetString(previousRoomPlayerPrefKey);
                PlayerPrefs.DeleteKey(previousRoomPlayerPrefKey);
                if (!string.IsNullOrEmpty(_previousRoom))
                {
                    _room.ReJoinRoom(_previousRoom);
                    _previousRoom = null;
                }
            }

            _room.JoinRandomRoom();
        }

        void OnJoinedLobby()
        {
            OnConnectedToMaster();
        }

        void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            _room.CreateRoom(null, new RoomOptions {MaxPlayers = 2, PlayerTtl = 20000}, null);
        }

        void OnPhotonJoinRoomFailed(object[] codeAndMsg)
        {
            _previousRoom = null;
            PlayerPrefs.DeleteKey(previousRoomPlayerPrefKey);
        }

        void ApplyUserIdAndConnect()
        {
            string nickName = "DemoNick";
            if (_inputField != null && !string.IsNullOrEmpty(_inputField.text))
            {
                nickName = _inputField.text;
                PlayerPrefs.SetString(NickNamePlayerPrefsKey, nickName);
            }

            _room.AuthValues.UserId = nickName;

            _room.LocalPlayer.NickName = nickName;
            _room.ConnectUsingSettings("0");

            // this way we can force timeouts by pausing the client (in editor)
            PhotonHandler.StopFallbackSendAckThread();
        }
    }
}