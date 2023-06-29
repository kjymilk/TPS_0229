using UnityEngine;
using UnityEngine.UI;


public class PlayerCtrl : MonoBehaviour
{
    // Hpbar ������ ����
    private Image hpBar;

    // ������Ʈ�� ó���� ����
    private Transform tr;
    // Animation ������Ʈ�� ������ ����
    private Animation anim;
    // �̵� �ӷ� ���� (public���� ����Ǿ� �ν����� �信 �����)
    public float moveSpeed = 10.0f;
    // ȸ�� �ӵ� ����
    public float turnSpeed = 80.0f;
    // Start is called before the first frame update


    // �ʱ� ���� ��
    private readonly float initHp = 100.0f;
    // ���� ���� ��
    public float currHp;

    // ��������Ʈ ����
    public delegate void PlayerDieHandler();
    // �̺�Ʈ ����
    public static event PlayerDieHandler OnPlayerDie;


    void Start()
    {
        // Hpbar ����
        hpBar = GameObject.FindGameObjectWithTag("HP_BAR")?.GetComponent<Image>();
        DisplayHealth();

        // HP �ʱ�ȭ
        currHp = initHp;

        // Transform ������Ʈ�� ������ ������ ����
        tr = GetComponent<Transform>();
        anim = GetComponent<Animation>();
        anim.Play("Idle");
    }
    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // -1.0f ~ 0.0f ~ +1.0f
        float v = Input.GetAxis("Vertical"); // -1.0f ~ 0.0f ~ +1.0f
        float r = Input.GetAxis("Mouse X");
        // �����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        // Translate(�̵� ���� * �ӷ� * Time.deltaTime)
        tr.Translate(moveDir.normalized * Time.deltaTime * moveSpeed);
        // Vector3.up ���� �������� turnSpeed��ŭ�� �ӵ��� ȸ��
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);
        // ���ΰ� ĳ������ �ִϸ��̼� ����
        PlayerAnim(h, v);
    }

    // �浹�� Collider�� IsTrigger �ɼ��� üũ���� �� �߻�
    void OnTriggerEnter(Collider coll)
    {
        // �浹�� Collider�� ������ PUNCH�̸� Player�� HP ����
        if (currHp >= 0.0f && coll.CompareTag("PUNCH"))
        {
            currHp -= 10.0f;
            DisplayHealth();
            Debug.Log($"Player hp = {currHp / initHp}");
            // Player�� ������ 0 �����̸� ��� ó��
            if (currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    // Player�� ��� ó��
    void PlayerDie()
    {


        // ���ΰ� ��� �̺�Ʈ ȣ��(�߻�)
        OnPlayerDie();

        // GameManager ��ũ��Ʈ��  IsGameOver ������Ƽ ���� ����
        //ameObject.Find("GameMgr").GetComponent<GameManager>().IsGameOver = true;
        GameManager.instance.IsGameOver = true;


    }
    void DisplayHealth()
    {
        hpBar.fillAmount = currHp / initHp;
    }
    void PlayerAnim(float h, float v)
    {
        // Ű���� �Է°��� �������� ������ �ִϸ��̼� ����
        if (v >= 0.1f)
        {
            anim.CrossFade("RunF", 0.25f); // ���� �ִϸ��̼� ����
        }
        else if (v <= -0.1f)
        {
            anim.CrossFade("RunB", 0.25f); // ���� �ִϸ��̼� ����
        }
        else if (h >= 0.1f)
        {
            anim.CrossFade("RunR", 0.25f); // ������ �̵� �ִϸ��̼� ����
        }
        else if (h <= -0.1f)
        {
            anim.CrossFade("RunL", 0.25f); // ���� �̵� �ִϸ��̼� ����
        }
        else
        {
            anim.CrossFade("Idle", 0.25f); // ���� �� Idle �ִϸ��̼� ����
        }
    }

}

