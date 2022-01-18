using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class MandelBrot
    {
        int _x;
        int _y;
      
        public bool inMandel;
        float widthRatio;
        float heightRatio;
        public void InitSize(int width, int height)
        {
            widthRatio = width / 3;
            heightRatio = height / 2;
            _x = width;
            _y = height;
        }
        public UInt32 GetColor(int x, int y)
        {
            var intensity = 1.0 - CheckInMandel(0, x, y, 0);
            //intensity = .5f;
            var c = (((UInt32)(intensity * 255.0f)) << 8);
            if (inMandel == false)
            {
                return 0xFF000000 | c;
            }
            
            else
            {
                return 0xFF0000FF | c;
            }

        }

        public float CheckInMandel(int iter, int x, int y, int maxIter)
        {
            var complex = CompleX(x, y);
            var currentValue = new MyComplex(0.0f, 0.0f);
            inMandel = true;
            int i = 0;
            for (i = 0; i <400; i++)
            {
                var newValue = SquaredCompleX(currentValue);
                newValue.real += complex.real;
                newValue.imag += complex.imag;

                currentValue = newValue;
               
                if((currentValue.real * currentValue.real + currentValue.imag * currentValue.imag) > 6)
                {
                    inMandel = false;
                    break;
                }
            }
            return Math.Min(1.0f, i / 20.0f);
           
        }

        public struct MyComplex
        {
            public MyComplex(float r, float i)
            {
                real = r;
                imag = i;
            }
            public float real;
            public float imag;
        }
        public MyComplex CompleX(int screenX, int screenY)
        {
            var value = new MyComplex();
            value.real = (screenX / widthRatio) + -2;
            value.imag = (screenY / heightRatio) + -1;
            return value;
        }

        public MyComplex SquaredCompleX(MyComplex value)
        {
            return new MyComplex ((value.real * value.real) - (value.imag * value.imag),
                (2 * value.real * value.imag) );

        }
    }
}
