/**
     Couroutine 사용시 return Yield return의 Object가 동적으로 생성되는 것 같다.
    반복적인 생성이 GC를 생성하기 때문에 캐싱하여 사용한다.
*/
using UnityEngine;
using System.Collections.Generic;

public static class Yields {


    //WaitForEndOfFrame을 만들어 두고 반환
    private static WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
    public static WaitForEndOfFrame EndOfFrame
    {
        get { return endOfFrame; }
    }

    //WaitForFixedUpdate를 만들어 두고 반환
    private static WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    public static WaitForFixedUpdate FixedUpdate
    {
        get { return fixedUpdate; }
    }

    //WaitForSeconds 캐싱을 위해 정보를 담아두기 위한 자료구조
    private static Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>(100);

    //WaitForSeconds 정보를 만들어 두고 반환
    public static WaitForSeconds WaitSeconds(float seconds)
    {
        if (!timeInterval.ContainsKey(seconds))
            timeInterval.Add(seconds, new WaitForSeconds(seconds));
        return timeInterval[seconds];
    }
}