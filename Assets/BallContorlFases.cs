// using UnityEngine;

// public class BallControl : MonoBehaviour
// {
//     public float speed = 0.8f;              // Velocidade da bola
//     public float minYFactor = 0.3f;        // Evita loop horizontal
//     public float paddleInfluence = 2f;     // Quanto a posição na raquete influencia o ângulo

//     private Rigidbody2D rb;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();

//         // Direção inicial levemente diagonal
//         Vector2 dir = new Vector2(Random.Range(-0.5f, 0.5f), 1f);
//         rb.linearVelocity = dir.normalized * speed;
//     }

//     void FixedUpdate()
//     {
//         Vector2 v = rb.linearVelocity;

//         // Mantém velocidade constante
//         v = v.normalized * speed;

//         // Anti-loop horizontal
//         if (Mathf.Abs(v.y) < speed * minYFactor)
//         {
//             float ySign = Mathf.Sign(v.y) == 0 ? 1 : Mathf.Sign(v.y);
//             float newY = ySign * speed * minYFactor;
//             float newX = Mathf.Sign(v.x) * Mathf.Sqrt(speed * speed - newY * newY);
//             v = new Vector2(newX, newY);
//         }

//         rb.linearVelocity = v;
//     }

//     void OnCollisionEnter2D(Collision2D coll)
//     {
//         if (coll.gameObject.CompareTag("Brick"))
//         {
//             Destroy(coll.gameObject);
//         }

//         if (coll.gameObject.CompareTag("Player"))
//         {
//             float hitPoint = transform.position.x - coll.transform.position.x;
//             float width = coll.collider.bounds.size.x/2 ;

//             float xFactor = hitPoint / width;

//             Vector2 newDir = new Vector2(xFactor * paddleInfluence, 1f);
//             rb.linearVelocity = newDir.normalized * speed;
//         }
//     }
// }

using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float speed = 3f;             // agora isso é “devagar” de verdade
    public float minY = 0.25f;           // evita ficar só na horizontal
    public float paddleInfluence = 1.2f; // influencia do hit na raquete

    private Rigidbody2D rb;
    private Vector2 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // direção inicial
        dir = new Vector2(Random.Range(-0.4f, 0.4f), 1f).normalized;

        // importante pra mover com MovePosition
        rb.gravityScale = 0f;
        rb.linearDamping = 0f;
    }

    void FixedUpdate()
    {
        // anti-loop horizontal
        if (Mathf.Abs(dir.y) < minY)
        {
            dir.y = (dir.y >= 0 ? 1 : -1) * minY;
            dir = dir.normalized;
        }

        // move com velocidade constante (controlada por speed)
        Vector2 nextPos = rb.position + dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(nextPos);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // destruir blocos
        if (coll.gameObject.CompareTag("Brick"))
            Destroy(coll.gameObject);

        // refletir direção pela normal da colisão
        Vector2 normal = coll.contacts[0].normal;
        dir = Vector2.Reflect(dir, normal).normalized;

        // rebote inteligente na raquete
        if (coll.gameObject.CompareTag("Player"))
        {
            float hitPoint = transform.position.x - coll.transform.position.x;
            float halfWidth = coll.collider.bounds.size.x / 2f;
            float xFactor = Mathf.Clamp(hitPoint / halfWidth, -1f, 1f);

            dir = new Vector2(xFactor * paddleInfluence, Mathf.Abs(dir.y)).normalized;
        }

        if (coll.gameObject.CompareTag("Brick"))
        {
            Destroy(coll.gameObject);
            GameManager.I.AddBrickScore();
            GameManager.I.CheckLevelCleared();
        }

        if (coll.gameObject.CompareTag("BottomWall"))
        {
            GameManager.I.LoseLife();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BottomWall"))
        {
            GameManager.I.LoseLife();   // perde vida
            GameManager.I.ResetScore(); // zera pontos
            // Respawn();  // se seu respawn já acontece em outro lugar, não precisa
        }
    }
}