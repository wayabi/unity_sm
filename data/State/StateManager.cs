// デバッグ用 : ステート名表示フラグ
#define SHOW_STATE

using UnityEngine;
using System;

public class StateManager<T_NUMBER, T_OWNER> where T_OWNER : MonoBehaviour
{

#if UNITY_EDITOR && SHOW_STATE
    /// <summary>
    /// ステート名表示用オブジェクト
    /// </summary>
    private GameObject m_StateObject = null;
#endif

    /// <summary>
    /// ステート更新処理
    /// </summary>
    public void Update()
    {
        m_StateList[(int)(object)m_CurrentStateNo].Update();
    }

    /// <summary>
    /// ステートの追加
    /// </summary>
    /// <param name="state_no">State no.</param>
    /// <param name="type">Type.</param>
    /// <param name="owner">Owner.</param>
    public void AddState(T_NUMBER state_no, Type type, T_OWNER owner)
    {
        State<T_OWNER> state = Activator.CreateInstance(type) as State<T_OWNER>;
        if (state == null)
        {
            Debug.LogError("State is Null!!!");
            return;
        }
        state.Owner = owner;
        m_StateList[(int)(object)state_no] = state;

#if UNITY_EDITOR && SHOW_STATE
        if (m_StateObject == null)
        {
            m_StateObject = new GameObject("StateObject");
            m_StateObject.transform.SetParent(owner.transform);
            m_StateObject.transform.localScale = m_StateObject.transform.localPosition = Vector3.one;
        }
#endif
    }

    /// <summary>
    /// ステートの変更
    /// </summary>
    /// <param name="state_no">State no.</param>
    public void Change(T_NUMBER state_no)
    {
        if (m_FirstTransition == false)
        {
            m_BeforeStateNo = m_CurrentStateNo;
            int index = (int)(object)m_CurrentStateNo;
            if (m_StateList[index] != null)
            {
                if (m_StateList[index] != null)
                {
                    m_StateList[index].OnExit();
                }
            }
        }
		
        m_FirstTransition = false;
		
        if (m_StateList[(int)(object)state_no] != null)
        {
            m_CurrentStateNo = state_no;
            int index = (int)(object)m_CurrentStateNo;
            if (m_StateList[index] != null)
            {
                m_StateList[index].OnEnter();
            }
        }
        else
        {
            Debug.LogError("Illegal State : " + state_no.ToString());
        }
#if UNITY_EDITOR && SHOW_STATE
        if (m_StateObject != null)
        {
            m_StateObject.name = "State:" + state_no.ToString();
        }
#endif
    }

    /// <summary>
    /// ステートを取得
    /// </summary>
    /// <returns>The state.</returns>
    /// <param name="number">Number.</param>
    public State<T_OWNER> GetState(T_NUMBER number)
    {
        return m_StateList[(int)(object)number];
    }

    private State<T_OWNER>[] m_StateList = new State<T_OWNER>[Enum.GetNames(typeof(T_NUMBER)).Length];
    private bool m_FirstTransition = true;

    // 現在のステート
    private T_NUMBER m_CurrentStateNo;

    public T_NUMBER CurrentStateNo
    {
        get { return m_CurrentStateNo; }	
    }

    // 以前のステート
    private T_NUMBER m_BeforeStateNo;

    public T_NUMBER BeforeStateNo
    {
        get { return m_BeforeStateNo; }
    }
}
