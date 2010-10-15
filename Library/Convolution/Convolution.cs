// $Id$

using System;
using System.Collections.Generic;

namespace Library.Convolution
{
	/// <summary>
	/// Alter Falter
	/// </summary>
	public static class Convolution
	{
		/// <summary>
		/// Berechnet die Faltung <code>y(t)=u(t)*g(t)</code>
		/// </summary>
		/// <param name="u">Das Signal, <code>u(t)</code></param>
		/// <param name="g">Das System, <code>g(t)</code></param>
		/// <returns>Die Faltung, <code>y(t)</code></returns>
		public static double[] Convolve(IList<double> u, IList<double> g)
		{
			if (u == null) throw new ArgumentNullException("u");
			if (g == null) throw new ArgumentNullException("g");
			if (u.Count == 0) throw new ArgumentException("Signalvektor ist leer", "u");
			if (g.Count == 0) throw new ArgumentException("Systemvektor ist leer", "g");

			// y(t) = u(t) x g(t) = sum( u(tau)*g(t-tau) )
			// http://wiki.delphigl.com/index.php/Convolution-Filter#Ein_klein_wenig_Theorie

			int newLength = u.Count + g.Count - 1;
			double[] y = new double[newLength];
			for (int t = 0; t < y.Length; ++t)
			{
				double yt = 0;

				// tau ist die Laufvariable der Summe
				for (int tau = 0; tau < u.Count; ++tau)
				{
					//value += u[tau]*g[t - tau];
					//value += u[t - tau]*g[tau];

					// u(tau)
					double uTau = u[tau];

					// g(t-tau)
					double gTminusTau = 0;
					int tMinusTau = t - tau;

					// Für Bereiche außerhalb der Systemantwort ist der Faktor g(t-tau) = 0.
					// Anders gesagt: Nur im Bereich 0 >= (t-tau) < Anzahl der Koeffizienten
					// sind gültige Werte vorhanden.
					if (tMinusTau >= 0 && tMinusTau < g.Count) gTminusTau = g[tMinusTau];

					// Multiplizieren und aufsummieren
					yt += uTau*gTminusTau;
				}

				y[t] = yt;
			}

			return y;
		}

		/// <summary>
		/// Berechnet die Faltung <code>y(t)=u(t)*g(t)</code>
		/// </summary>
		/// <param name="u">Das Signal, <code>u(t)</code></param>
		/// <param name="g">Das System, <code>g(t)</code></param>
		/// <returns>Die Faltung, <code>y(t)</code></returns>
		public static double[] Convolve(IList<double> u, Func<int, double> g)
		{
			if (u == null) throw new ArgumentNullException("u");
			if (g == null) throw new ArgumentNullException("g");
			if (u.Count == 0) throw new ArgumentException("Signalvektor ist leer", "u");

			// y(t) = u(t) x g(t) = sum( u(tau)*g(t-tau) )
			// http://wiki.delphigl.com/index.php/Convolution-Filter#Ein_klein_wenig_Theorie

			double[] y = new double[u.Count];
			for (int t = 0; t < y.Length; ++t)
			{
				double yt = 0;

				// tau ist die Laufvariable der Summe
				for (int tau = 0; tau < u.Count; ++tau)
				{			
					yt += u[tau] * g(t-tau);
				}

				y[t] = yt;
			}

			return y;
		}

		#region Erzeugen von Impulsen

		/// <summary>
		/// Erzeugt einen Dirac-Stoß
		/// </summary>
		/// <returns></returns>
		public static double[] DiracImpulse()
		{
			return new double[] {1};
		}

		/// <summary>
		/// Erzeugt einen Sigma-Impuls (1 bzw. <paramref name="amplitude"/> für alle Elemente)
		/// </summary>
		/// <remarks>Auch als Heaviside-Funktion bekannt.</remarks>
		/// <param name="length">Die Länge des Impulses</param>
		/// <param name="amplitude">Die Amplitude</param>
		/// <returns></returns>
		public static double[] SigmaImpulse(int length, double amplitude = 1.0D)
		{
			if (length < 1) throw new ArgumentOutOfRangeException("length", length, "length must be in range 1..n");
			double[] array = new double[length];
			if (amplitude != 0.0D)
			{
				for (int i = 0; i < length; ++i)
				{
					array[i] = amplitude;
				}
			}
			return array;
		}

		/// <summary>
		/// Erzeugt einen Null-Impuls (0 für alle Elemente)
		/// </summary>
		/// <param name="length">Die Länge des Impulses</param>
		/// <returns></returns>
		public static double[] ZeroImpulse(int length)
		{
			if (length < 1) throw new ArgumentOutOfRangeException("length", length, "length must be in range 1..n");
			return new double[length]; // Knaller.
		}
		
		/// <summary>
		/// Erzeugt einen (Standard-)Rechteckimpuls der Fläche 1
		/// </summary>
		/// <param name="start">Startindex (bzw. Zeitpunkt) des Rechteckimpulses. Inklusive.</param>
		/// <param name="ende">Endindex (bzw. Zeitpunkt) des Rechteckimpulses. Inklusive.</param>
		/// <returns>Rechteckimpuls</returns>
		public static double[] RectImpulse(int start, int ende)
		{
			if (start < 0) throw new ArgumentOutOfRangeException("start", start, "Startindex muss im Bereich 0..n liegen");
			if (ende <= start) throw new ArgumentOutOfRangeException("ende", ende, "Endindex muss im Bereich start..n liegen");
			
			double[] array = new double[ende+1];

			double width = ende - start;
			if (width == 1) return DiracImpulse();

			// Amplitude berechnen, damit Fläche 1 wird.
			double amplitude = 1.0D / width;

			// Füllen
			for (int i = start; i <= ende; ++i)
			{
				array[i] = amplitude;
			}
			return array;
		}

		/// <summary>
		/// Erzeugt einen Rechteckimpuls mit einer definierten Amplitude
		/// </summary>
		/// <param name="start">Startindex (bzw. Zeitpunkt) des Rechteckimpulses. Inklusive.</param>
		/// <param name="ende">Endindex (bzw. Zeitpunkt) des Rechteckimpulses. Inklusive.</param>
		/// <param name="amplitude">Die Amplitude des Rechteckimpulses</param>
		/// <returns>Rechteckimpuls</returns>
		public static double[] RectImpulse(int start, int ende, double amplitude)
		{
			if (start < 0) throw new ArgumentOutOfRangeException("start", start, "Startindex muss im Bereich 0..n liegen");
			if (ende <= start) throw new ArgumentOutOfRangeException("ende", ende, "Endindex muss im Bereich start..n liegen");

			double[] array = new double[ende+1];
			for (int i = start; i <= ende; ++i)
			{
				array[i] = amplitude;
			}
			return array;
		}

		#endregion

		#region Extension Methods

		/// <summary>
		/// Berechnet die Faltung <code>y(t)=u(t)*g(t)</code>
		/// </summary>
		/// <param name="u">Das Signal, <code>u(t)</code></param>
		/// <param name="g">Das System, <code>g(t)</code></param>
		/// <returns>Die Faltung, <code>y(t)</code></returns>
		public static double[] ConvolveWith(this IList<double> u, IList<double> g)
		{
			return Convolve(u, g);
		}

		#endregion
	}
}
