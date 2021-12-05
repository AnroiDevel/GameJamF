using UnityEngine;


namespace GameJam
{
    public class PerkPlant : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var perkInvisible = collision.gameObject.GetComponent<Invisible>();
            if (perkInvisible != null)
                if (!perkInvisible.IsInvisibleOn)
                {
                    perkInvisible.TakePerk();
                    Destroy(gameObject);
                }
        }
    }
}