using UnityEngine;


namespace GameJam
{
    public class Upgrader : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var perkInvisible = collision.gameObject.GetComponent<Invisible>();
            if (perkInvisible != null)
            {
                perkInvisible.Upgrade();
                Destroy(gameObject);
            }
        }
    }
}