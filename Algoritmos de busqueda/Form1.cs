using System;
using System.Diagnostics;
using System.Reflection;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace Algoritmos_de_busqueda
{
    public partial class Form1 : Form
    {
        private int[] data; // Arreglo de datos a ordenar
        private PlotView plotView; // Control PlotView
        private OxyPlot.Series.BarSeries BubbleSortGraph;
        private OxyPlot.Series.BarSeries QuickSortGraph;
        private OxyPlot.Series.BarSeries InsertionSortGraph;

        public Form1()
        {

            InitializeComponent();
            InitializePlot();

        }
        private void InitializePlot()
        {

            var plotModel = new PlotModel
            {
                Title = "Tiempos de ejecución de algoritmos",

            };
            plotModel.Legends.Add(

                new Legend
                {
                    LegendOrientation = LegendOrientation.Horizontal,
                    LegendPosition = LegendPosition.BottomCenter,
                    LegendPlacement = LegendPlacement.Outside

                });
            BubbleSortGraph = new OxyPlot.Series.BarSeries()
            {

                XAxisKey = "InsertionSort",
                YAxisKey = "tiempo",
                Title = "Ordenamiento de la Burbuja",
                FillColor = OxyPlot.OxyColors.Blue
            };

            QuickSortGraph = new OxyPlot.Series.BarSeries()
            {
                XAxisKey = "InsertionSort",
                YAxisKey = "tiempo",
                Title = "Quick Sort",
                FillColor = OxyPlot.OxyColors.Orange
            };

            InsertionSortGraph = new OxyPlot.Series.BarSeries()
            {
                XAxisKey = "InsertionSort",
                YAxisKey = "tiempo",
                Title = "Método de Inserción",
                FillColor = OxyPlot.OxyColors.Green
            };


            var categoryAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Tiempo (ms)",
                Key = "InsertionSort",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                AxisTickToLabelDistance = 0,
                MinimumPadding = 0.1,
                MaximumPadding = 0.1,
                Minimum = 0,
                Maximum = 10000 * 1.1,

            };

            plotModel.Axes.Add(categoryAxis);

            var linearAxis = new CategoryAxis
            {
                AxisDistance = 5.0f,
                Position = AxisPosition.Bottom,
                Title = "Algoritmo",
                Key = "tiempo"
            };
            plotModel.Axes.Add(linearAxis);
            plotModel.Series.Add(BubbleSortGraph);
            plotModel.Series.Add(QuickSortGraph);
            plotModel.Series.Add(InsertionSortGraph);


            plotView1.Model = plotModel;
        }
        private int[] GenerateLargeArray(int size)
        {
            Random random = new Random();
            int[] array = new int[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(1, 100001);
            }

            return array;
        }
        private async void startBtn_Click(object sender, EventArgs e)
        {
            data = GenerateLargeArray(100000);



            var bubbleSortTimer = new System.Timers.Timer(100);
            var quickSortTimer = new System.Timers.Timer(100);
            var insertionSortTimer = new System.Timers.Timer(100);

            var actions = new List<Func<Task>>
    {
        async () => await CorrerAlgoritmoParalelo("Ordenamiento de la Burbuja", () => BubbleSort(data), bubbleSortTimer, label1, BubbleSortGraph),
        async () => await CorrerAlgoritmoParalelo("Quick Sort", async () => await QuickSortAsync(data, 0, data.Length - 1), quickSortTimer, label2, QuickSortGraph),
        async () => await CorrerAlgoritmoParalelo("Método de Inserción", () => InsertionSort(data), insertionSortTimer, label3, InsertionSortGraph)
    };


            await Task.WhenAll(actions.Select(action => Task.Run(action)));
        }


        private async Task CorrerAlgoritmoParalelo(string algorithmName, Action algorithm, System.Timers.Timer timer, Label labeltxt, OxyPlot.Series.BarSeries bar)
        {
            var stopwatch = new Stopwatch();

            timer.Elapsed += (sender, e) =>
            {
                if (stopwatch.IsRunning)
                {
                    labeltxt.BeginInvoke((MethodInvoker)delegate
                    {
                        labeltxt.Text = $"Tiempo ({algorithmName}): {stopwatch.Elapsed.TotalMilliseconds:0.00} ms";
                    });
                }
            };

            stopwatch.Start();

            timer.Enabled = true;

            await Task.Run(() =>
            {
                algorithm();

                stopwatch.Stop();
                double elapsedMilliseconds = stopwatch.Elapsed.TotalMilliseconds;

                plotView1.Invoke((MethodInvoker)delegate
                {
                    bar.Items.Clear();
                    bar.Items.Add(new OxyPlot.Series.BarItem(elapsedMilliseconds, 0));
                    labeltxt.BeginInvoke((MethodInvoker)delegate
                    {
                        labeltxt.Text = $"Tiempo ({algorithmName}): {stopwatch.Elapsed.TotalMilliseconds:0.00} ms";
                    });
                    plotView1.InvalidatePlot(true);
                });

                timer.Enabled = false;
            });
        }

        private async Task QuickSortAsync(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    await Task.Run(() => QuickSortAsync(arr, left, pivot - 1));
                }
                if (pivot + 1 < right)
                {
                    await Task.Run(() => QuickSortAsync(arr, pivot + 1, right));
                }
            }
        }
        private void BubbleSort(int[] array)
        {
            int n = array.Length;
            bool swapped;

            do
            {
                swapped = false;
                for (int i = 1; i < n; i++)
                {
                    if (array[i - 1] > array[i])
                    {
                        int temp = array[i - 1];
                        array[i - 1] = array[i];
                        array[i] = temp;
                        swapped = true;
                    }
                }
                n--;
            } while (swapped);

        }

        private void QuickSort(int[] arr, int left, int right)
        {

            if (left < right)
            {
                int pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    QuickSort(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    QuickSort(arr, pivot + 1, right);
                }
            }
        }
        private int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;

                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;


                }
                else
                {
                    return right;
                }
            }
        }
        private void InsertionSort(int[] array)
        {
            int n = array.Length;
            for (int i = 1; i < n; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = key;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}