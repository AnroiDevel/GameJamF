using UnityEngine;


namespace GameJam
{
    public class Upgrader : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var invis = collision.GetComponent<Invisible>();
            invis?.Upgrade();
            Destroy(gameObject);
        }
    }

}