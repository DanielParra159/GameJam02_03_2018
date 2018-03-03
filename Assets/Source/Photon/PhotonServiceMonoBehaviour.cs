using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ExitGames.Client.Photon;
using Photon;

namespace Source.Photon
{
    [SuppressMessage("ReSharper", "ClassCannotBeInstantiated", Justification = "Singelton")]
    public sealed partial class ServiceSingelton
    {
        public interface IRoom
        {
            void LeaveRoom();
            void StopFallbackSendAckThread();
            void ReconnectAndRejoin();
            bool InRoom { get; }
            bool Connected { get; }
            bool Connecting { get; }
            PhotonPlayer LocalPlayer { get; }
            Room CurrentRoom { get; }
            bool IsMasterClient { get; }
            AuthenticationValues AuthValues { get; }
            void ConnectUsingSettings(string gameVersion);
            void JoinRandomRoom();
            void ReJoinRoom(string roomName);
            void CreateRoom(string settings, RoomOptions roomOptions, TypedLobby typedLobby);
            bool TurnIsOver { get; }
            int Turn { get; }
            float RemainingSecondsInTurn { get; }
            float TurnDuration { get; }
            bool IsCompletedByAll { get; }
            bool IsPlayerTurnFinished(PhotonPlayer player);
            void BeginTurn();
            void SendMovement(object movement, bool finished);
            event Action<int> OnTurnBegins;
            event Action<int> OnTurnCompleted;
            event Action<PhotonPlayer, int, object> OnPlayerMove;
            event Action<PhotonPlayer, int, object> OnPlayerFinished;
            event Action<int> OnTurnTimeEnds;
            event Action OnConnected;
            event Action OnRoomLeft;
            event Action<PhotonPlayer> OnClientMasterSwitched;
            event Action<object[]> OnCreateRoomFailed;
            event Action<object[]> OnJoinRoomFailed;
            event Action OnRoomCreated;
            event Action OnLobbyJoined;
            event Action OnLobbyLeft;
            event Action<DisconnectCause> OnFailedToConnect;
            event Action OnDisconnected;
            event Action<DisconnectCause> OnRoomConnectionFailed;
            event Action<PhotonMessageInfo> OnInstantiate;
            event Action OnReceivedListRoomUpdate;
            event Action OnRoomJoined;
            event Action<PhotonPlayer> OnPlayerConnected;
            event Action<PhotonPlayer> OnPlayerDisconnected;
            event Action<object[]> OnRandomJoinFailed;
            event Action OnMasterConnected;
            event Action OnMaxConcurrentUsersReached;
            event Action<Hashtable> OnCustomRoomPropertiesChanged;
            event Action<object[]> OnPlayerPropertiesChanged;
            event Action OnFriendListUpdated;
            event Action<string> OnAuthenticationCustomFailed;
            event Action<Dictionary<string, object>> OnAuthenticationCustomResponse;
            event Action<OperationResponse> OnRpcWebResponse;
            event Action<object[]> OnRequestOwnership;
            event Action OnStatisticsLobbyUpdate;
            event Action<PhotonPlayer> OnPlayerActivityChanged;
            event Action<object[]> OnTransferedOwnership;
        }

        public class PhotonServiceMonoBehaviour : PunBehaviour, IRoom, IPunTurnManagerCallbacks
        {
            public bool TurnIsOver
            {
                get { return _turnManager.IsOver; }
            }

            public int Turn
            {
                get { return _turnManager.Turn; }
            }

            public float RemainingSecondsInTurn
            {
                get { return _turnManager.RemainingSecondsInTurn; }
            }

            public float TurnDuration
            {
                get { return _turnManager.TurnDuration; }
            }

            public bool IsCompletedByAll
            {
                get { return _turnManager.IsCompletedByAll; }
            }

            public bool IsPlayerTurnFinished(PhotonPlayer player)
            {
                return _turnManager.GetPlayerFinishedTurn(player);
            }

            public void BeginTurn()
            {
                _turnManager.BeginTurn();
            }

            public void SendMovement(object move, bool finished)
            {
                _turnManager.SendMove(move, finished);
            }

            public event Action<int> OnTurnBegins;
            public event Action<int> OnTurnCompleted;
            public event Action<PhotonPlayer, int, object> OnPlayerMove;
            public event Action<PhotonPlayer, int, object> OnPlayerFinished;
            public event Action<int> OnTurnTimeEnds;
            public event Action OnConnected;
            public event Action OnRoomLeft;
            public event Action<PhotonPlayer> OnClientMasterSwitched;
            public event Action<object[]> OnCreateRoomFailed;
            public event Action<object[]> OnJoinRoomFailed;
            public event Action OnRoomCreated;
            public event Action OnLobbyJoined;
            public event Action OnLobbyLeft;
            public event Action<DisconnectCause> OnFailedToConnect;
            public event Action OnDisconnected;
            public event Action<DisconnectCause> OnRoomConnectionFailed;
            public event Action<PhotonMessageInfo> OnInstantiate;
            public event Action OnReceivedListRoomUpdate;
            public event Action OnRoomJoined;
            public event Action<PhotonPlayer> OnPlayerConnected;
            public event Action<PhotonPlayer> OnPlayerDisconnected;
            public event Action<object[]> OnRandomJoinFailed;
            public event Action OnMasterConnected;
            public event Action OnMaxConcurrentUsersReached;
            public event Action<Hashtable> OnCustomRoomPropertiesChanged;
            public event Action<object[]> OnPlayerPropertiesChanged;
            public event Action OnFriendListUpdated;
            public event Action<string> OnAuthenticationCustomFailed;
            public event Action<Dictionary<string, object>> OnAuthenticationCustomResponse;
            public event Action<OperationResponse> OnRpcWebResponse;
            public event Action<object[]> OnRequestOwnership;
            public event Action OnStatisticsLobbyUpdate;
            public event Action<PhotonPlayer> OnPlayerActivityChanged;
            public event Action<object[]> OnTransferedOwnership;

            PunTurnManager _turnManager;
            AuthenticationValues _authValues;

            public void LeaveRoom()
            {
                PhotonNetwork.LeaveRoom();
            }

            public void ConnectUsingSettings(object settings)
            {
                PhotonNetwork.ConnectUsingSettings(null);
            }

            public void StopFallbackSendAckThread()
            {
                PhotonHandler.StopFallbackSendAckThread();
            }

            public void ReconnectAndRejoin()
            {
                PhotonNetwork.ReconnectAndRejoin();
            }

            public bool InRoom
            {
                get { return PhotonNetwork.inRoom; }
            }

            public bool Connected
            {
                get { return PhotonNetwork.connected; }
            }

            public bool Connecting
            {
                get { return PhotonNetwork.connecting; }
            }

            public PhotonPlayer LocalPlayer
            {
                get { return PhotonNetwork.player; }
            }

            public Room CurrentRoom
            {
                get { return PhotonNetwork.room; }
            }

            public bool IsMasterClient
            {
                get { return PhotonNetwork.isMasterClient; }
            }

            public void ConnectUsingSettings(string gameVersion)
            {
                PhotonNetwork.ConnectUsingSettings(gameVersion);
            }

            public void JoinRandomRoom()
            {
                PhotonNetwork.JoinRandomRoom();
            }

            public void ReJoinRoom(string roomName)
            {
                PhotonNetwork.ReJoinRoom(roomName);
            }

            public void CreateRoom(string settings, RoomOptions roomOptions, TypedLobby typedLobby)
            {
                PhotonNetwork.CreateRoom(settings, roomOptions, typedLobby);
            }

            public AuthenticationValues AuthValues
            {
                get
                {
                    if (PhotonNetwork.AuthValues == null)
                    {
                        PhotonNetwork.AuthValues = new AuthenticationValues();
                    }
                    
                    return PhotonNetwork.AuthValues;
                }
            }

            public void Construct(float turnDuration)
            {
                _turnManager = gameObject.AddComponent<PunTurnManager>();
                _turnManager.TurnManagerListener = this;
                _turnManager.TurnDuration = turnDuration;
            }

            public override void OnConnectedToPhoton()
            {
                OnConnectedHandler();
            }

            public override void OnLeftRoom()
            {
                OnRoomLeftHandler();
            }

            public override void OnMasterClientSwitched(PhotonPlayer newMasterClient)
            {
                OnClientMasterSwitchedHandler(newMasterClient);
            }

            public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
            {
                OnCreateRoomFailedHandler(codeAndMsg);
            }

            public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
            {
                OnJoinRoomFailedHandler(codeAndMsg);
            }

            public override void OnCreatedRoom()
            {
                OnRoomCreatedHandler();
            }

            public override void OnJoinedLobby()
            {
                OnLobbyJoinedHandler();
            }

            public override void OnLeftLobby()
            {
                OnLobbyLeftHandler();
            }

            public override void OnFailedToConnectToPhoton(DisconnectCause cause)
            {
                OnFailedToConnectHandler(cause);
            }

            public override void OnDisconnectedFromPhoton()
            {
                OnDisconnectedHandler();
            }

            public override void OnConnectionFail(DisconnectCause cause)
            {
                OnRoomConnectionFailedHandler(cause);
            }

            public override void OnPhotonInstantiate(PhotonMessageInfo info)
            {
                OnInstantiateHandler(info);
            }

            public override void OnReceivedRoomListUpdate()
            {
                OnReceivedListRoomUpdateHandler();
            }

            public override void OnJoinedRoom()
            {
                OnRoomJoinedHandler();
            }

            public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
            {
                OnPlayerConnectedHandler(newPlayer);
            }

            public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
            {
                OnPlayerDisconnectedHandler(otherPlayer);
            }

            public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
            {
                OnRandomJoinFailedHandler(codeAndMsg);
            }

            public override void OnConnectedToMaster()
            {
                OnMasterConnectedHandler();
            }

            public override void OnPhotonMaxCccuReached()
            {
                OnMaxConcurrentUsersReachedHandler();
            }

            public override void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
            {
                OnCustomRoomPropertiesChangedHandler(propertiesThatChanged);
            }

            public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
            {
                OnPlayerPropertiesChangedHandler(playerAndUpdatedProps);
            }

            public override void OnUpdatedFriendList()
            {
                OnFriendListUpdatedHandler();
            }

            public override void OnCustomAuthenticationFailed(string debugMessage)
            {
                OnAuthenticationCustomFailedHandler(debugMessage);
            }

            public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
            {
                OnAuthenticationCustomResponseHandler(data);
            }

            public override void OnWebRpcResponse(OperationResponse response)
            {
                OnRpcWebResponseHandler(response);
            }

            public override void OnOwnershipRequest(object[] viewAndPlayer)
            {
                OnRequestOwnershipHandler(viewAndPlayer);
            }

            public override void OnLobbyStatisticsUpdate()
            {
                OnStatisticsLobbyUpdateHandler();
            }

            public override void OnPhotonPlayerActivityChanged(PhotonPlayer otherPlayer)
            {
                OnPlayerActivityChangedHandler(otherPlayer);
            }

            public override void OnOwnershipTransfered(object[] viewAndPlayers)
            {
                OnTransferedOwnershipHandler(viewAndPlayers);
            }

            void OnDestroy()
            {
                Destroy(_turnManager);
            }

            void OnConnectedHandler()
            {
                var handler = OnConnected;
                if (handler != null) handler();
            }

            void OnRoomLeftHandler()
            {
                var handler = OnRoomLeft;
                if (handler != null) handler();
            }

            void OnClientMasterSwitchedHandler(PhotonPlayer obj)
            {
                var handler = OnClientMasterSwitched;
                if (handler != null) handler(obj);
            }

            void OnCreateRoomFailedHandler(object[] obj)
            {
                var handler = OnCreateRoomFailed;
                if (handler != null) handler(obj);
            }

            void OnJoinRoomFailedHandler(object[] obj)
            {
                var handler = OnJoinRoomFailed;
                if (handler != null) handler(obj);
            }

            void OnRoomCreatedHandler()
            {
                var handler = OnRoomCreated;
                if (handler != null) handler();
            }

            void OnLobbyJoinedHandler()
            {
                var handler = OnLobbyJoined;
                if (handler != null) handler();
            }

            void OnLobbyLeftHandler()
            {
                var handler = OnLobbyLeft;
                if (handler != null) handler();
            }

            void OnFailedToConnectHandler(DisconnectCause obj)
            {
                var handler = OnFailedToConnect;
                if (handler != null) handler(obj);
            }

            void OnDisconnectedHandler()
            {
                var handler = OnDisconnected;
                if (handler != null) handler();
            }

            void OnRoomConnectionFailedHandler(DisconnectCause obj)
            {
                var handler = OnRoomConnectionFailed;
                if (handler != null) handler(obj);
            }

            void OnInstantiateHandler(PhotonMessageInfo obj)
            {
                var handler = OnInstantiate;
                if (handler != null) handler(obj);
            }

            void OnReceivedListRoomUpdateHandler()
            {
                var handler = OnReceivedListRoomUpdate;
                if (handler != null) handler();
            }

            void OnRoomJoinedHandler()
            {
                var handler = OnRoomJoined;
                if (handler != null) handler();
            }

            void OnPlayerConnectedHandler(PhotonPlayer obj)
            {
                var handler = OnPlayerConnected;
                if (handler != null) handler(obj);
            }

            void OnPlayerDisconnectedHandler(PhotonPlayer obj)
            {
                var handler = OnPlayerDisconnected;
                if (handler != null) handler(obj);
            }

            void OnRandomJoinFailedHandler(object[] obj)
            {
                var handler = OnRandomJoinFailed;
                if (handler != null) handler(obj);
            }

            void OnMasterConnectedHandler()
            {
                var handler = OnMasterConnected;
                if (handler != null) handler();
            }

            void OnMaxConcurrentUsersReachedHandler()
            {
                var handler = OnMaxConcurrentUsersReached;
                if (handler != null) handler();
            }

            void OnCustomRoomPropertiesChangedHandler(Hashtable obj)
            {
                var handler = OnCustomRoomPropertiesChanged;
                if (handler != null) handler(obj);
            }

            void OnPlayerPropertiesChangedHandler(object[] obj)
            {
                var handler = OnPlayerPropertiesChanged;
                if (handler != null) handler(obj);
            }

            void OnFriendListUpdatedHandler()
            {
                var handler = OnFriendListUpdated;
                if (handler != null) handler();
            }

            void OnAuthenticationCustomFailedHandler(string obj)
            {
                var handler = OnAuthenticationCustomFailed;
                if (handler != null) handler(obj);
            }

            void OnAuthenticationCustomResponseHandler(Dictionary<string, object> obj)
            {
                var handler = OnAuthenticationCustomResponse;
                if (handler != null) handler(obj);
            }

            void OnRpcWebResponseHandler(OperationResponse obj)
            {
                var handler = OnRpcWebResponse;
                if (handler != null) handler(obj);
            }

            void OnRequestOwnershipHandler(object[] obj)
            {
                var handler = OnRequestOwnership;
                if (handler != null) handler(obj);
            }

            void OnStatisticsLobbyUpdateHandler()
            {
                var handler = OnStatisticsLobbyUpdate;
                if (handler != null) handler();
            }

            void OnPlayerActivityChangedHandler(PhotonPlayer obj)
            {
                var handler = OnPlayerActivityChanged;
                if (handler != null) handler(obj);
            }

            void OnTransferedOwnershipHandler(object[] obj)
            {
                var handler = OnTransferedOwnership;
                if (handler != null) handler(obj);
            }

            void OnTurnBeginsHandler(int obj)
            {
                var handler = OnTurnBegins;
                if (handler != null) handler(obj);
            }

            void OnTurnCompletedHandler(int obj)
            {
                var handler = OnTurnCompleted;
                if (handler != null) handler(obj);
            }

            void OnPlayerMoveHandler(PhotonPlayer arg1, int arg2, object arg3)
            {
                var handler = OnPlayerMove;
                if (handler != null) handler(arg1, arg2, arg3);
            }

            void OnPlayerFinishedHandler(PhotonPlayer arg1, int arg2, object arg3)
            {
                var handler = OnPlayerFinished;
                if (handler != null) handler(arg1, arg2, arg3);
            }

            void OnTurnTimeEndsHandler(int obj)
            {
                var handler = OnTurnTimeEnds;
                if (handler != null) handler(obj);
            }

            void IPunTurnManagerCallbacks.OnTurnBegins(int turn)
            {
                OnTurnBeginsHandler(turn);
            }

            void IPunTurnManagerCallbacks.OnTurnCompleted(int turn)
            {
                OnTurnCompletedHandler(turn);
            }

            void IPunTurnManagerCallbacks.OnPlayerMove(PhotonPlayer player, int turn, object move)
            {
                OnPlayerMoveHandler(player, turn, move);
            }

            void IPunTurnManagerCallbacks.OnPlayerFinished(PhotonPlayer player, int turn, object move)
            {
                OnPlayerFinishedHandler(player, turn, move);
            }

            void IPunTurnManagerCallbacks.OnTurnTimeEnds(int turn)
            {
                OnTurnTimeEndsHandler(turn);
            }
        }
    }
}