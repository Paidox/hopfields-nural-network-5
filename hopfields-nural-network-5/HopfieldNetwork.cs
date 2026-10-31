using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace hopfields_nural_network_5
{
  public class HopfieldModel
  {
    private double[,] w;
    private int n;
    private List<int[]> xs;

    public HopfieldModel(List<int[]> xs)
    {
      Debug.Assert(xs.Count > 0);

      this.xs = xs;
      n = xs[0].Length;
      w = new double[n, n];

      store();
    }

    private void store()
    {
      for (var i = 0; i < n; ++i)
        w[i, i] = 0;

      int M = xs.Count;
      if (M == 0) return;

      for (var i = 0; i < n; ++i)
      {
        for (var j = 0; j < i; ++j)
        {
          double sum = 0;
          for (var m = 0; m < M; ++m)
          {
            sum += xs[m][i] * xs[m][j];
          }

          w[i, j] = sum / M;
          w[j, i] = w[i, j];
        }
      }
    }

    public int[] recall(int[] probe, Form1 form = null)
    {
      Debug.Assert(probe.Length == n);

      var x = new int[n];
      var xNew = new int[n];
      probe.CopyTo(xNew, 0);

      while (!xNew.SequenceEqual<int>(x))
      {
        xNew.CopyTo(x, 0);

        for (var j = 0; j < n; ++j)
        {
          double wx = 0;
          for (var i = 0; i < n; ++i)
          {
            wx += w[i, j] * x[i];
          }

          xNew[j] = Math.Sign(wx);
          if (xNew[j] == 0) xNew[j] = 1;
        }

        if (form != null)
        {
          Thread.Sleep(100);
          form.Invoke(new Action(() => {
            form.VectorToGrid(xNew);
            form.canvasBox.Invalidate();
          }));
        }
      }

      return xNew;
    }

    public int[] recall1(int[] probe, Form1 form = null, int hammingEpsilon = 0)
    {
      Debug.Assert(probe.Length == n);

      var x = new int[n];
      probe.CopyTo(x, 0);

      var xNew = new int[n];
      int[] hammingDistanceHistory = { 1, 2, 3, 4, 5 };
      var curIndex = 0;

      while (hammingDistanceHistory[curIndex] > hammingEpsilon && !isArrayStable(hammingDistanceHistory))
      {
        xNew.CopyTo(x, 0);

        for (var j = 0; j < n; ++j)
        {
          double wx = 0;
          for (var i = 0; i < n; ++i)
          {
            wx += w[i, j] * xNew[i];
          }

          xNew[j] = wx < 0 ? -1 : 1;
        }

        hammingDistanceHistory[curIndex] = hammingDistance(x, xNew);
        curIndex = (curIndex + 1) % hammingDistanceHistory.Length;

        if (form != null)
        {
          form.Invoke(new Action(() => {
            form.VectorToGrid(x);
            form.canvasBox.Invalidate();
          }));
        }
      }

      return x;
    }

    public static int hammingDistance(int[] x, int[] y)
    {
      Debug.Assert(x.Length == y.Length);

      var distance = 0;
      for (var i = 0; i < x.Length; i++)
      {
        distance += x[i] == y[i] ? 0 : 1;
      }

      return distance;
    }

    private static bool isArrayStable(int[] arr)
    {
      Debug.Assert(arr.Length > 0);

      for (var i = 1; i < arr.Length; i++)
      {
        if (arr[i - 1] != arr[i])
          return false;
      }
      return true;
    }
  }
}