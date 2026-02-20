using UnityEngine;
using UnityEngine.UI;

namespace GameKits.HealthSystem.Scripts
{
    public class HealthManagerUI : MonoBehaviour
    {
        [SerializeField] Image health;

        private void Update()
        {
            if (transform.parent.transform.localScale.x  < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }

        public void UpdateBar(float maxHealth, float currentHealth)
        {
            float amount = currentHealth / maxHealth;
            amount = Mathf.Clamp(amount, 0, 1);

            health.fillAmount = amount;
        }
    }
}