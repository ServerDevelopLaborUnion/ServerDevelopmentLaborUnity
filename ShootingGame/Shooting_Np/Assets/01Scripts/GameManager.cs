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

    private List<TransformVO> dataList;

    private Dictionary<int, GameObject> players; //접속한 플레이어들을 저장하는 변수

    void Awake()
    {
        instance = this;
        players = new Dictionary<int, GameObject>();
    }

    void Update()
    {
        if(readyToStart)   {
            MakeGamePlayer();
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
    }

    public void SetRefreshData(List<TransformVO> list){
        dataList = list;
        refreshData = true;
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
