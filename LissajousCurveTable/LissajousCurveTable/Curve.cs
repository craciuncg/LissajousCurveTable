using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LissajousCurveTable
{
    class Curve
    {
        private List<PointF> vertexBuffer;
        public Curve()
        {
            vertexBuffer = new List<PointF>();
        }

        public void AddPoint(float x, float y)
        {
            vertexBuffer.Add(new PointF(x, y));
        }

        public void Clear()
        {
            vertexBuffer.Clear();
        }

        public void Draw(Graphics gfx, SolidBrush brush)
        {
            for (int i = 0; i < vertexBuffer.ToArray().Length; i++)
            {
                float x = vertexBuffer.ToArray()[i].X;
                float y = vertexBuffer.ToArray()[i].Y;
                gfx.FillRectangle(brush, x, y, 1, 1);
            }
        }

        public List<PointF> GetVertexBuffer()
        {
            return vertexBuffer;
        }

    }
}
