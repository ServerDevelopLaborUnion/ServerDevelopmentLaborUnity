using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CanvasGroup canvasGroup;
    public GameObject tankPrefab;
    public CinemachineVirtualCamera virtualCamera;

    public int myId;

    private GameObject player;
    Vector3 initPosition;
    
    private bool readyToStart = false;
    private bool refreshData = false;

    private int shootSocketId = -1;
    private int onDamageSocketId = -1;
    private float onDamage = 0f;
    private bool onDamageData = false;
    private bool shootData = false;

    private bool deletePlayerData = false;
    private int deletePlayerSocketID = -1;
    private Vector3 rotate = Vector3.zero;

    private List<TransformVO> dataList;

    private Dictionary<int, GameObject> players; //접속한 플레이어들을 저장하는 변수

    void Awake()
    {
        instance = this;
        players = new Dictionary<int, GameObject>();
    }

    void Update()
    {
        if (readyToStart)   {
            MakeGamePlayer();
        }

        if(deletePlayerData)
        {
            deletePlayerData = false;
            GameObject p = null;
            players.TryGetValue(deletePlayerSocketID, out p);
            if (p == null)
            {
                Debug.LogError("deleteplayerdata 소켓이름이 같은 것이없다");
                return;
            }
            players.Remove(deletePlayerSocketID);
            Destroy(p);
        }

        if(refreshData){
            foreach(TransformVO tv in dataList){
                if(tv.socketId != myId){ //내가 아니라면 
                    GameObject p = null;
                    players.TryGetValue(tv.socketId, out p);
                    if(p == null){ //해당 유저는 새롭게 접속한 것이니 갱신해서 넣어준다.
                        p = MakeRemotePlayer(tv);
                        players.Add(tv.socketId, p);
                    }

                    //생성된 애들에 대해서는 데이터를 갱신한다.
                    PlayerRPC remoteRPC = p.GetComponent<PlayerRPC>();
                    remoteRPC.SetTransform(tv.point, tv.rotation);
                }
            }
            refreshData = false;
        }

        if(onDamageData)
        {
            onDamageData = false;
            GameObject p = null;
            players.TryGetValue(onDamageSocketId, out p);
            if(p == null)
            {
                Debug.LogError("OnDamageData 소켓이름이 같은 것이없다");
                return;
            }
            PlayerRPC pRPC = p.GetComponent<PlayerRPC>();
            pRPC.OnDamage(onDamage, onDamageSocketId);
        }

        if(shootData)
        {
            shootData = false;
            GameObject p = null;
            players.TryGetValue(shootSocketId, out p);
            if (p == null)
            {
                Debug.LogError("shootData 소켓이름이 같은 것이없다");
                return;
            }
            PlayerRPC pRPC = p.GetComponent<PlayerRPC>();
            pRPC.Shoot(this.rotate);
        }
    }

    public void SetRefreshData(List<TransformVO> list){
        dataList = list;
        refreshData = true;
    }

    public void SetOnDamageData(int socketId, float damage)
    {
        this.onDamageSocketId = socketId;
        onDamage = damage;
        onDamageData = true;
    }
    public void SetShootData(int socketId, Vector3 rotation)
    {
        this.shootSocketId = socketId;
        rotate = rotation;
        shootData = true;
    }

    public void DeletPlayer(int socketId)
    {
        deletePlayerData = true;
        deletePlayerSocketID = socketId;
    }

    private GameObject MakeRemotePlayer(TransformVO data){
        GameObject remotePlayer = Instantiate(tankPrefab, data.point, Quaternion.identity);
        remotePlayer.GetComponent<SpriteRenderer>().color = Color.red;
        PlayerRPC remoteRPC = remotePlayer.GetComponent<PlayerRPC>();
        remoteRPC.SetRemote();
        remoteRPC.SetTransform(data.point, data.rotation);
        return remotePlayer;
    }

    private void MakeGamePlayer(){
        readyToStart = false;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;

        player = Instantiate(tankPrefab, initPosition, Quaternion.identity);

        virtualCamera.Follow = player.transform;
        players.Add(myId, player);
    }

    public void ChangeToGame(Vector3 position, Vector3 rotation, int id){
        this.myId = id;
        this.initPosition = position;
        readyToStart = true;
    }
}
