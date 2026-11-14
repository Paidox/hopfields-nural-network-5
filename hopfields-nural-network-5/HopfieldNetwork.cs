using System.Diagnostics;

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
      for (var i = 0; i < n; i++)
      {
        w[i, i] = 0;
      }

      int M = xs.Count;
      if (M == 0) return;

      for (var i = 0; i < n; i++)
      {
        for (var j = 0; j < i; j++)
        {
          double sum = 0;
          for (var m = 0; m < M; m++)
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

      while (!xNew.SequenceEqual(x))
      {
        xNew.CopyTo(x, 0);

        for (var j = 0; j < n; j++)
        {
          double wx = 0;
          for (var i = 0; i < n; i++)
          {
            wx += w[i, j] * x[i];
          }

          xNew[j] = f(wx, x[j]);
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

    private static int f(double wx, int prev)
    {
      if (wx < 0)
      {
        return -1;
      } 
        
      if (wx > 0)
      {
        return 1;
      }
      return prev;
    }
  }
}