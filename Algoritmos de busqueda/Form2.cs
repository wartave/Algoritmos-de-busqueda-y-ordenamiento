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
    public partial class Form2 : Form
    {
        private int[] data; // Arreglo de datos a ordenar
        private PlotView plotView; // Control PlotView
        private OxyPlot.Series.BarSeries B�squedaSecuencialGraph;
        private OxyPlot.Series.BarSeries BusquedaBinariaGraph;

        public Form2()
        {

            InitializeComponent();
            InitializePlot();

        }
        private void InitializePlot()
        {

            var plotModel = new PlotModel
            {
                Title = "Tiempos de ejecuci�n de algoritmos",

            };
            plotModel.Legends.Add(

                new Legend
                {
                    LegendOrientation = LegendOrientation.Horizontal,
                    LegendPosition = LegendPosition.BottomCenter,
                    LegendPlacement = LegendPlacement.Outside

                });
            B�squedaSecuencialGraph = new OxyPlot.Series.BarSeries()
            {

                XAxisKey = "InsertionSort",
                YAxisKey = "tiempo",
                Title = " B�squeda Secuencial",
                FillColor = OxyPlot.OxyColors.Blue
            };


            BusquedaBinariaGraph = new OxyPlot.Series.BarSeries()
            {
                XAxisKey = "InsertionSort",
                YAxisKey = "tiempo",
                Title = "Busqueda Binaria",
                FillColor = OxyPlot.OxyColors.Green
            };
            // Calcula el valor m�ximo entre todas las barras

            // Configura el eje X como CategoryAxis
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
                Minimum = 0, // Establece el valor m�nimo del eje Y en 0
                Maximum = 100 * 1.1, // Ajusta el valor m�ximo en un 10% m�s alto que el m�ximo de las barras

            };

            plotModel.Axes.Add(categoryAxis);

            // Configura el eje Y como LinearAxis para valores num�ricos
            var linearAxis = new CategoryAxis
            {
                AxisDistance = 5.0f,
                Position = AxisPosition.Bottom,
                Title = "Algoritmo",
                Key = "tiempo"
            };
            plotModel.Axes.Add(linearAxis);
            plotModel.Series.Add(B�squedaSecuencialGraph);
            plotModel.Series.Add(BusquedaBinariaGraph);

            // Asigna el modelo al control PlotView
            plotView1.Model = plotModel;
        }
        private int[] GenerateLargeArray(int size)
        {
            if (size <= 0 || size > 1000000)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "El tama�o del arreglo debe estar entre 1 y 1,000,000.");
            }

            Random random = new Random();
            HashSet<int> uniqueNumbers = new HashSet<int>();

            while (uniqueNumbers.Count < size)
            {
                int randomNumber = random.Next(1, 1000001);
                uniqueNumbers.Add(randomNumber);
            }

            int[] array = uniqueNumbers.ToArray();
            return array;
        }
        private async void startBtn_Click(object sender, EventArgs e)
        {
            data = GenerateLargeArray(1000000); // Genera un arreglo de 100,000 elementos
            var B�squedaSecuencialTimer = new System.Timers.Timer(100);
            var quickSortTimer = new System.Timers.Timer(100);
            var insertionSortTimer = new System.Timers.Timer(100);
            if (int.TryParse(txtInput.Text, out int target))
            {
                var actions = new List<Func<Task>>
    {
        async () => await CorrerAlgoritmoParalelo("B�squeda Secuencial", () => B�squedaSecuencial(data,target), B�squedaSecuencialTimer, label1, B�squedaSecuencialGraph),
        async () => await CorrerAlgoritmoParalelo("B�squeda Binaria", () => BusquedaBinaria(data,target), insertionSortTimer, label2, BusquedaBinariaGraph)
    };

                // Ejecuta los algoritmos en paralelo utilizando Task.Run
                await Task.WhenAll(actions.Select(action => Task.Run(action)));
            }





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
                    // Agrega los valores de las barras
                    bar.Items.Add(new OxyPlot.Series.BarItem(elapsedMilliseconds, 0));
                    // Refresca el gr�fico
                    labeltxt.BeginInvoke((MethodInvoker)delegate
                    {
                        labeltxt.Text = $"Tiempo ({algorithmName}): {stopwatch.Elapsed.TotalMilliseconds:0.00} ms";
                    });
                    plotView1.InvalidatePlot(true);
                });

                timer.Enabled = false;
            });
        }

        private bool B�squedaSecuencial(int[] array, int target)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == target)
                {
                    return true; // El elemento fue encontrado
                }
            }
            return false; // El elemento no fue encontrado
        }
        private bool BusquedaBinaria(int[] array, int target)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (array[mid] == target)
                {
                    return true; // El elemento fue encontrado
                }

                if (array[mid] < target)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }

            return false; // El elemento no fue encontrado
        }

    }
}