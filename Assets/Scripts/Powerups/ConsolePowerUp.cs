using UnityEngine;

namespace Powerups
{
    public abstract class ConsolePowerUp : AbstractPowerUp
    {
        private float rotationSpeed;
        
        // Start is called before the first frame update
        protected new void Start()
        {
            rotationSpeed = 180;
        }

        // Update is called once per frame
        protected new void Update()
        {
            this.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
        }
    }
}
