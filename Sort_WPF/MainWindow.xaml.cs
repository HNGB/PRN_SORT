using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace Sort_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        List<int> list3 = new List<int>();
        List<int> list4 = new List<int>();
        public MainWindow()
        {
            InitializeComponent();

            int number;
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                number = random.Next(1, 10001);
                list1.Add(number);

            }
            list2 = new List<int>(list1);
            list3 = new List<int>(list1);
            list4 = new List<int>(list1);
            txtList1.Text = string.Join(",", list1);
            txtList2.Text = string.Join(",", list2);
            txtList3.Text = string.Join(",", list3);
            txtList4.Text = string.Join(",", list4);

        }

        public static void BubbleSort(List<int> arr)
        {
            int n = arr.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
        }

        public static void QuickSort(List<int> arr, int low, int high)
        {
            if (low < high)
            {
                int pivot = Partition(arr, low, high);

                QuickSort(arr, low, pivot - 1);
                QuickSort(arr, pivot + 1, high);
            }
        }

        private static int Partition(List<int> arr, int low, int high)
        {
            int pivot = arr[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            int temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;

            return i + 1;
        }

        public static List<int> MergeSort(List<int> unsortedList)
        {
            if (unsortedList.Count <= 1)
            {
                return unsortedList;
            }

            int middleIndex = unsortedList.Count / 2;
            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();

            for (int i = 0; i < middleIndex; i++)
            {
                leftList.Add(unsortedList[i]);
            }

            for (int i = middleIndex; i < unsortedList.Count; i++)
            {
                rightList.Add(unsortedList[i]);
            }

            leftList = MergeSort(leftList);
            rightList = MergeSort(rightList);

            return Merge(leftList, rightList);
        }

        private static List<int> Merge(List<int> leftList, List<int> rightList)
        {
            List<int> result = new List<int>();

            while (leftList.Count > 0 || rightList.Count > 0)
            {
                if (leftList.Count > 0 && rightList.Count > 0)
                {
                    if (leftList[0] <= rightList[0])
                    {
                        result.Add(leftList[0]);
                        leftList.Remove(leftList[0]);
                    }
                    else
                    {
                        result.Add(rightList[0]);
                        rightList.Remove(rightList[0]);
                    }
                }
                else if (leftList.Count > 0)
                {
                    result.Add(leftList[0]);
                    leftList.Remove(leftList[0]);
                }
                else if (rightList.Count > 0)
                {
                    result.Add(rightList[0]);
                    rightList.Remove(rightList[0]);
                }
            }

            return result;
        }

        public static void InsertionSort(List<int> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                int currentValue = list[i];
                int j = i - 1;

                while (j >= 0 && list[j] > currentValue)
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = currentValue;
            }
        }



        public void SortSync(List<int> list1, List<int> list2, List<int> list3, List<int> list4)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            BubbleSort(list1);
            stopwatch.Stop();
            long bubbleSortTime = stopwatch.ElapsedMilliseconds;
            txtList1.Text = string.Join(",", list1);
            txtTimeBBSort.Text = bubbleSortTime.ToString();

            stopwatch.Start();
            QuickSort(list2, 0, list2.Count - 1);
            stopwatch.Stop();
            long quickSortTime = stopwatch.ElapsedMilliseconds;
            txtList2.Text = string.Join(",", list2);
            txtTimeQSort.Text = quickSortTime.ToString();

            stopwatch.Start();
            MergeSort(list3);
            stopwatch.Stop();
            long mergeSortTime = stopwatch.ElapsedMilliseconds;
            txtList3.Text = string.Join(",", list3);
            txtTimeMSort.Text = mergeSortTime.ToString();

            stopwatch.Start();
            InsertionSort(list4);
            stopwatch.Stop();
            long insertionSortTime = stopwatch.ElapsedMilliseconds;
            txtList4.Text = string.Join(",", list4);
            txtTimeISort.Text = insertionSortTime.ToString();
        }

        public async Task SortAsync(List<int> list1, List<int> list2, List<int> list3, List<int> list4)
        {
            Task sort1 = Task.Run(() => BubbleSort(list1));
            txtList1.Text = string.Join(",", list1);
            Task sort2 = Task.Run(() => QuickSort(list2, 0, list2.Count - 1));
            txtList2.Text = string.Join(",", list2);
            Task sort3 = Task.Run(() => MergeSort(list3));
            txtList3.Text = string.Join(",", list3);
            Task sort4 = Task.Run(() => InsertionSort(list4));
            txtList4.Text = string.Join(",", list4);

            await Task.WhenAll(sort1, sort2, sort3, sort4);
        }

        public static void SortParallel(List<int> list1, List<int> list2, List<int> list3, List<int> list4)
        {
            Task sort1 = Task.Run(() => BubbleSort(list1));
            Task sort2 = Task.Run(() => QuickSort(list2, 0, list2.Count - 1));
            Task sort3 = Task.Run(() => MergeSort(list3));
            Task sort4 = Task.Run(() => InsertionSort(list4));

            Task.WaitAll(sort1, sort2, sort3, sort4);
        }

        public static void SortParallelAsync(List<int> list1, List<int> list2, List<int> list3, List<int> list4)
        {
            Task sort1 = Task.Run(() => BubbleSort(list1));
            Task sort2 = Task.Run(() => QuickSort(list2, 0, list2.Count - 1));
            Task sort3 = Task.Run(() => MergeSort(list3));
            Task sort4 = Task.Run(() => InsertionSort(list4));

            Task.WhenAll(sort1, sort2, sort3, sort4).Wait();
        }

        private void btnAsync_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSync_Click(object sender, RoutedEventArgs e)
        {
            SortSync(list1, list2, list3, list4);
            long totalTime = long.Parse(txtTimeBBSort.Text.ToString()) + long.Parse(txtTimeQSort.Text.ToString())
                + long.Parse(txtTimeMSort.Text.ToString()) + long.Parse(txtTimeISort.Text.ToString());
            txtTotalTime.Text = totalTime.ToString() + " ms";
        }

        private void btnPararell_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPararellAsync_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}