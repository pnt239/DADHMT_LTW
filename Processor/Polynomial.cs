using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;

namespace TabletC.Processor
{
	public class Polynomial
	{
		private readonly double[] _coefficients; // danh sách các hệ số
		
		public Polynomial()
		{
            // mặc định 0.
			_coefficients = new double[1];
			_coefficients[0] = 0;
		}

		// Nhập hệ số theo thứ tự mũ của biến tăng dần.
		// a + b*x + c*x^2 + ....
		// a -> b -> c -> .....
		public Polynomial(params double[] coeffs)
		{
			if (coeffs == null || coeffs.Length < 1)
			{
				_coefficients = new double[1];
				_coefficients[0] = 0;
			}
			else
			{
				_coefficients = new double[coeffs.Length];
				for (int i = 0; i < coeffs.Length; i++)
					_coefficients[i] = coeffs[i];
			}
		}

		// Số mũ cao nhất của phương trình.
		public int Degree
		{
			get
			{
				return _coefficients.Length - 1;
			}
		}

	    public double Solve(double x)
	    {
	        return _coefficients.Select((t, i) => t*Math.Pow(x, i)).Sum();
	    }

	    // Đạo hàm.
		public Polynomial Derivative()
		{
			var buf = new double[Degree];

			for (int i = 0; i < buf.Length; i++)
				buf[i] = (i + 1) * _coefficients[i + 1];

			return new Polynomial(buf);
		}

		// Tích phân.
        public Polynomial Antiderivative()
		{
			var buf = new double[Degree + 2];
			buf[0] = 0;

			for (int i = 1; i < buf.Length; i++)
				buf[i] = _coefficients[i - 1] / i;

			return new Polynomial(buf);
		}

	    public double Integral(double a, double b)
	    {
	        Polynomial init = Antiderivative();
            return init.Solve(b) - init.Solve(a);
	    }
		// Tính x^degree
		private static Polynomial Monomial(int degree)
		{
			if (degree == 0) return new Polynomial(1);

			var coeffs = new double[degree + 1];

			for (int i = 0; i < degree; i++)
				coeffs[i] = 0;

			coeffs[degree] = 1;

			return new Polynomial(coeffs);
		}

		// Nạp chồng toán tử
		public static Polynomial operator +(Polynomial p, Polynomial q)
		{
			var degree = Math.Max(p.Degree, q.Degree);
			var coeffs = new double[degree + 1];

			for (var i = 0; i <= degree; i++)
			{
				if (i > p.Degree) coeffs[i] = q._coefficients[i];
				else if (i > q.Degree) coeffs[i] = p._coefficients[i];
				else coeffs[i] = p._coefficients[i] + q._coefficients[i];
			}

			return new Polynomial(coeffs);
		}

		public static Polynomial operator -(Polynomial p, Polynomial q)
		{
			return p + (-q);
		}

		public static Polynomial operator -(Polynomial p)
		{
			var coeffs = new double[p.Degree + 1];

			for (var i = 0; i < coeffs.Length; i++)
				coeffs[i] = -p._coefficients[i];

			return new Polynomial(coeffs);
		}

		public static Polynomial operator *(double d, Polynomial p)
		{
			double[] coeffs = new double[p.Degree + 1];

			for (int i = 0; i < coeffs.Length; i++)
				coeffs[i] = d * p._coefficients[i];

			return new Polynomial(coeffs);
		}

		public static Polynomial operator *(Polynomial p, double d)
		{
			double[] coeffs = new double[p.Degree + 1];

			for (int i = 0; i < coeffs.Length; i++)
				coeffs[i] = d * p._coefficients[i];

			return new Polynomial(coeffs);
		}

		public static Polynomial operator *(Polynomial p, Polynomial q)
		{
			int degree = p.Degree + q.Degree;
			var r = new Polynomial();

			for (int i = 0; i <= p.Degree; i++)
				for (int j = 0; j <= q.Degree; j++)
					r += (p._coefficients[i] * q._coefficients[j]) * Monomial(i + j);

			return r;
		}

		public static Polynomial operator /(Polynomial p, double d)
		{
			var coeffs = new double[p.Degree + 1];

			for (int i = 0; i < coeffs.Length; i++)
				coeffs[i] = p._coefficients[i] / d;

			return new Polynomial(coeffs);
		}

        public override string ToString()
        {
            var ret = string.Format("{0:F02}x^{1}", _coefficients[0], 0);
            for (int i = 1; i < _coefficients.Length; i++)
            {
                if (_coefficients[i] >= 0)
                    ret += "+";
                ret += string.Format("{0:F02}x^{1}", _coefficients[i], i);
            }

            return ret;
        }
	}
}
