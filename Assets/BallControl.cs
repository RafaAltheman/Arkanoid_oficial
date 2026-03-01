// using UnityEngine;

// [RequireComponent(typeof(Rigidbody2D))]
// [RequireComponent(typeof(Collider2D))]
// public class BallControl : MonoBehaviour
// {
//     [Header("Referências")]
//     [SerializeField] private Transform paddle;  // arraste a raquete aqui no Inspector

//     [Header("Launch")]
//     [SerializeField] private float launchImpulse = 8f;
//     [SerializeField] private Vector2 stuckOffset = new Vector2(0f, 0.35f); // posição da bola em cima da raquete

//     private Rigidbody2D rb;
//     private bool isLaunched = false;

//     void Awake()
//     {
//         rb = GetComponent<Rigidbody2D>();

//         // Física padrão Arkanoid
//         rb.gravityScale = 0f;
//         rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
//         rb.interpolation = RigidbodyInterpolation2D.Interpolate;
//     }

//     void Start()
//     {
//         StickToPaddle();
//     }

//     void Update()
//     {
//         // Enquanto não lançou, bola fica grudada na raquete
//         if (!isLaunched)
//         {
//             StickToPaddle();

//             // Aperta espaço OU clica pra lançar
//             if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
//             {
//                 Launch();
//             }
//         }
//     }

//     private void StickToPaddle()
//     {
//         if (paddle == null) return;

//         rb.linearVelocity = Vector2.zero;
//         transform.position = (Vector2)paddle.position + stuckOffset;
//     }

//     private void Launch()
//     {
//         isLaunched = true;

//         // direção aleatória, sempre pra cima
//         float rand = Random.Range(0f, 1f);
//         Vector2 dir = (rand < 0.5f) ? new Vector2(1f, 1f) : new Vector2(-1f, 1f);

//         rb.AddForce(dir.normalized * launchImpulse, ForceMode2D.Impulse);
//     }

//     // Se bater na raquete, garante que vai pra cima (não fica descendo)
//     void OnCollisionEnter2D(Collision2D coll)
//     {
//         if (coll.collider.CompareTag("Player"))
//         {
//             Vector2 v = rb.linearVelocity;
//             v.y = Mathf.Abs(v.y);
//             rb.linearVelocity = v;
//         }
//     }

//     // Chame isso quando a bola cair (perde vida)
//     public void ResetBall()
//     {
//         isLaunched = false;
//         StickToPaddle();
//     }
// }