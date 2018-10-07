
    /*
       Author: Craciun Cristian-George
       Version: 1.0
   */
    class Vector2
    {
        public float x, y;

        public Vector2()
        {
            x = 0;
            y = 0;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void Mult(float val)
        {
            x *= val;
            y *= val;
        }
    }

