using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace hopfields_nural_network_5
{
  public partial class Form1 : Form
  {
    private const int GridSize = 20;
    private const int CellSize = 15;
    private int[,] gridData = new int[GridSize, GridSize];
    private int[,] noisyGridData = new int[GridSize, GridSize];

    List<TrainingSample> dataset = new List<TrainingSample>();

    private Random rand = new Random();

    public Form1()
    {
      InitializeComponent();

      if (comboB.Items.Count == 0)
      {
        comboB.Items.AddRange(new object[] { "5", "10", "20", "50", "60", "80", "100" });
        comboB.SelectedIndex = 0;
      }

      if (File.Exists("dataset.json"))
      {
        try
        {
          string datasetJson = File.ReadAllText("dataset.json");
          var loadedData = JsonSerializer.Deserialize<List<TrainingSample>>(datasetJson);
          if (loadedData != null)
          {
            dataset = loadedData;
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Error: {ex.Message}");
        }
      }

      canvasBox.Width = GridSize * CellSize;
      canvasBox.Height = GridSize * CellSize;
      canvasBox.Paint += CanvasBox_Paint;
      canvasBox.MouseDown += CanvasBox_MouseAction;
      canvasBox.MouseMove += CanvasBox_MouseAction;

      canvasBox2.Width = GridSize * CellSize;
      canvasBox2.Height = GridSize * CellSize;
      canvasBox2.Paint += CanvasBox2_Paint;

      clearBtn.Click += (s, e) =>
      {
        gridData = new int[GridSize, GridSize];
        noisyGridData = new int[GridSize, GridSize];
        canvasBox.Invalidate();
        canvasBox2.Invalidate();
        valueBox.Text = "";
      };

      rBtn.Click += (s, e) =>
      {
        if (string.IsNullOrWhiteSpace(valueBox.Text))
        {
          MessageBox.Show("ENTER THE NUMBER");
          return;
        }

        int[] currVect = GetNetworkInput();

        dataset.Add(new TrainingSample
        {
          InputVector = currVect,
          ClassLabel = int.Parse(valueBox.Text),
        });

        string jsonString = JsonSerializer.Serialize(dataset, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("dataset.json", jsonString);

        MessageBox.Show($"Successfully added: {dataset.Count}\nLength: {currVect.Length}");
      };


      predictBt.Click += (s, e) =>
      {
        if (string.IsNullOrWhiteSpace(valueBox.Text))
        {
          MessageBox.Show("Enter the target class number!");
          return;
        }

        int targetClass = int.Parse(valueBox.Text);
        var classSamples = dataset.Where(s => s.ClassLabel == targetClass).ToList();

        if (classSamples.Count == 0)
        {
          MessageBox.Show($"No training samples found for class {targetClass}!");
          return;
        }

        var randomSample = classSamples[rand.Next(classSamples.Count)];

        int noisePercent = 5;
        if (comboB.SelectedItem != null)
        {
          int.TryParse(comboB.SelectedItem.ToString(), out noisePercent);
        }

        int[] noisyVector = ApplyNoise(randomSample.InputVector, noisePercent);

        NoisyVectorToGrid(noisyVector);
        canvasBox2.Invalidate();

        List<int[]> classVectors = classSamples.Select(s => s.InputVector).ToList();
        HopfieldModel hopfield = new HopfieldModel(classVectors);

        int[] restoredVector = hopfield.recall(noisyVector, null);

        VectorToGrid(restoredVector);
        canvasBox.Invalidate();

      };

    }

    private int[] ApplyNoise(int[] originalVector, int noisePercent)
    {
      int[] noisyVector = (int[])originalVector.Clone();
      int totalPixels = noisyVector.Length;

      int pixelsToNoise = (int)(totalPixels * (noisePercent / 100.0));

      HashSet<int> indices = new HashSet<int>();

      while (indices.Count < pixelsToNoise)
      {
        indices.Add(rand.Next(totalPixels));
      }

      foreach (int index in indices)
      {
        noisyVector[index] = -originalVector[index];
      }

      return noisyVector;
    }

    public void VectorToGrid(int[] vector)
    {
      int index = 0;
      for (int y = 0; y < GridSize; y++)
      {
        for (int x = 0; x < GridSize; x++)
        {
          gridData[y, x] = (vector[index] == -1) ? 1 : 0;
          index++;
        }
      }
    }

    public void NoisyVectorToGrid(int[] vector)
    {
      int index = 0;
      for (int y = 0; y < GridSize; y++)
      {
        for (int x = 0; x < GridSize; x++)
        {
          noisyGridData[y, x] = (vector[index] == -1) ? 1 : 0;
          index++;
        }
      }
    }

    public void CanvasBox_MouseAction(object? sender, MouseEventArgs e)
    {
      int x = e.X / CellSize;
      int y = e.Y / CellSize;

      if (x >= 0 && x < GridSize && y >= 0 && y < GridSize)
      {
        if (e.Button == MouseButtons.Left)
        {
          gridData[y, x] = 1;
          canvasBox.Invalidate();
        }
        else if (e.Button == MouseButtons.Right)
        {
          gridData[y, x] = 0;
          canvasBox.Invalidate();
        }
      }
    }

    private void CanvasBox_Paint(object? sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;

      for (int y = 0; y < GridSize; y++)
      {
        for (int x = 0; x < GridSize; x++)
        {
          Rectangle rect = new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize);

          if (gridData[y, x] == 1)
          {
            g.FillRectangle(Brushes.Black, rect);
          }
          else
          {
            g.FillRectangle(Brushes.White, rect);
          }

          g.DrawRectangle(Pens.LightGray, rect);
        }
      }
    }

    private void CanvasBox2_Paint(object? sender, PaintEventArgs e)
    {
      Graphics g = e.Graphics;

      for (int y = 0; y < GridSize; y++)
      {
        for (int x = 0; x < GridSize; x++)
        {
          Rectangle rect = new Rectangle(x * CellSize, y * CellSize, CellSize, CellSize);

          if (noisyGridData[y, x] == 1)
          {
            g.FillRectangle(Brushes.Black, rect);
          }
          else
          {
            g.FillRectangle(Brushes.White, rect);
          }

          g.DrawRectangle(Pens.LightGray, rect);
        }
      }
    }

    public int[] GetNetworkInput()
    {
      int[] flatVector = new int[GridSize * GridSize];
      int index = 0;

      for (int y = 0; y < GridSize; y++)
      {
        for (int x = 0; x < GridSize; x++)
        {
          flatVector[index++] = (gridData[y, x] == 1) ? -1 : 1;
        }
      }

      return flatVector;
    }
    private void Form1_Load(object sender, EventArgs e) { }
  }

  public class TrainingSample
  {
    public int[]? InputVector { get; set; }
    public int ClassLabel { get; set; }
  }
}