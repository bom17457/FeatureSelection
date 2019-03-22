using System;
using System.Collections.Generic;
using System.Linq;

namespace Greedy
{
    class MainClass
    {


        class Node
        {
            public int column_index = 0;
            public char column_name;
            public double correlation = 0;

            public Node(char node_name, int node_index, double correlation)
            {
                this.column_name = node_name;
                this.column_index = node_index;
                this.correlation = correlation;
            }

            public String ToObject()
            {
                return "{index:" + column_index + ", ColumnName:" + column_name + ", correlation:" + correlation + "}";
            }
        }

        public static void Main(string[] args)
        {
            List<List<Object>> data = getTrainDataList();
            GreedySearch(data);
        }

        private static List<List<Object>> getTrainDataList()
        {
            List<List<Object>> x = new List<List<object>>();

            int train_row = GetTrainData().GetLength(0);
            int train_col = GetTrainData().GetLength(1);

            for (int i = 0; i < train_row; i++)
            {
                List<Object> jArray = new List<Object>();
                for (int j = 0; j < train_col; j++)
                {
                    jArray.Add(GetTrainData()[i, j]);
                }
                x.Add(jArray);
            }
            return x;
        }

        private static Double[,] GetTrainData()
        {
            Double[,] x =
            {
                {0.27,3.72,1.01,5.65,3.29,4.68,1,0.9,1,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1,1,1,1,0.8,1,208},
                { 1.02,4.96,1.01,5.65,2.19,4.68,1.0,0.9,1.0,1.24,0.91,1.0,1.0,0.87,0.85,0.88,0.81,1.1,1.0,1.0,1.0,0.8,1.0,195 },
                { 2.52,3.72,1.01,5.65,3.29,4.68,1,0.9,1,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1,1,1,1,0.8,1,162 },
                { 4.02,3.72,3.04,2.83,0,3.12,1.1,1,1,1.07,1.11,1,1,0.87,0.85,0.88,0.81,0.88,0.91,0.91,1,0.8,1,113 },
                { 4.28,4.96,1.01,5.65,3.29,4.68,1,0.9,1.17,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1.1,1,1,1,0.8,1,277 },
                { 4.48,4.96,1.01,5.65,1.1,4.68,1.1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,0.91,0.91,1,0.8,1,175 },
                { 5,4.96,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1,0.91,0.91,1,0.8,1,120 },
                { 5.11,2.48,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,0.88,0.91,0.91,1,0.8,1,189 },
                {5.29,1.24,1.01,5.65,0,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,1.19,1.2,1,0.8,1,417},
                {7.44,4.96,1.01,5.65,1.1,4.68,1,0.9,1.17,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,0.91,0.91,1,0.8,1,302},
                {7.7,4.96,1.01,5.65,0,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,1,0.91,1,0.8,1,567},
                {8.41,4.96,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1,0.91,0.91,1,0.8,1,153},
                {8.8,4.96,1.01,5.65,2.19,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,1.09,1,1,0.8,1,142},
                {9.99,4.96,1.01,5.65,3.29,4.68,1,0.9,1,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1.1,1,1,1,0.8,1,526},
                {25.84,4.96,1.01,5.65,1.1,4.68,1.1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,0.91,0.91,1,0.8,1,202},
                {26.22,4.96,1.01,4.24,1.1,4.68,1,0.9,1,0.95,1,1,1,0.87,0.85,1,0.81,0.81,0.91,0.91,1,0.8,1,175},
                {30.13,3.72,1.01,5.65,2.19,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1,1.09,1,1,0.8,1,206},
                {32.56,4.96,1.01,5.65,2.19,4.68,1,0.9,1,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1.1,0.91,0.91,1,0.8,1,794},
                {36.27,4.96,1.01,5.65,1.1,4.68,1,0.9,1,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1.1,0.91,0.91,1,0.8,1,667},
                {38.85,4.96,1.01,4.24,1.1,4.68,1,0.9,1,0.95,1,1,1,0.87,0.85,1,0.81,0.81,0.85,0.84,1,0.8,1,230},
                {42.81,3.72,3.04,5.65,1.1,4.68,1,0.9,0.87,1,0.91,1,1,0.87,0.85,0.88,0.81,1,1.09,1.09,1,0.8,1,439},
                {42.9,2.48,1.01,5.65,2.19,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,0.88,0.91,0.91,1,0.8,1,361},
                {45.14,2.48,3.04,5.65,1.1,4.68,1,0.9,0.87,1,0.91,1,1,0.87,0.85,0.88,0.81,0.88,1,1,1,0.8,1,486},
                {55.55,4.96,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,0.91,0.91,1,0.8,1,763},
                {57.77,2.48,2.03,1.41,0,3.12,1.1,1,1,1.07,1.11,1.29,1,0.87,0.85,0.88,0.81,0.88,0.91,0.91,1,0.8,1,405},
                {59.14,1.24,1.01,5.65,2.19,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,0.81,0.85,0.84,1,0.8,1,331},
                {62.78,6.2,3.04,4.24,0,3.12,1,1,1,1.07,1.11,1,1,0.87,0.85,0.88,0.81,0.88,0.91,0.91,1,0.8,1,422},
                {67.32,1.24,1.01,5.65,0,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,0.81,0.85,0.84,1,0.8,1,234},
                {79.82,4.96,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1,0.91,0.91,1,0.8,1,636},
                {112.28,4.96,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,0.91,0.91,1,0.8,1,1278},
            };
            return x;
        }

        private static double[] GetColumn(List<List<Object>> data, int column)
        {
            int row = data.Count;
            double[] x = new double[row];
            for (int i = 0; i < row; i++)
            {
                x[i] = Double.Parse((data.ElementAt(i).ElementAt(column).ToString()));
            }
            return x;
        }

        private static double[] GetColumn(double[,] data, int column)
        {
            int row = data.GetLength(0);
            double[] x = new double[row];

            for (int i = 0; i < row; i++)
            {
                x[i] = data[i, column];
            }
            return x;
        }

        private static void CreateChild(List<List<Object>> Data, List<Node> Target, List<Node> best_node)
        {
            List<int> arrayIndex = new List<int>();
            List<int> remainIndex = new List<int>();

            int column_length = Data.ElementAt(0).Count-1;
            for (int i = 0; i < column_length; i++)
            {
                remainIndex.Add(i);
            }

            //add best_node set to arrayIndex
            for (int i = 0; i < best_node.Count; i++)
            {
                arrayIndex.Add(best_node.ElementAt(i).column_index);
                remainIndex.Remove(best_node.ElementAt(i).column_index);
            }

            double[,] r_FactorFactor;
            double compute_factor_factor = 0.0f;

            double[,] r_FactorEffort;
            double compute_factor_effort = 0.0f;

            for (int i = 0; i < remainIndex.Count; i++)
            {
                int remain_index = remainIndex.ElementAt(i);
                arrayIndex.Add(remain_index);
                arrayIndex.Add(column_length);

                r_FactorFactor = GetMultiColumn(Data, arrayIndex.ToArray());
                compute_factor_factor = ComputeFactorFactor(r_FactorFactor);

                r_FactorEffort = GetMultiColumn(Data, arrayIndex.ToArray());
                compute_factor_effort = ComputeFactorEffort(r_FactorEffort);

                double correlation = ComputeCorrelationSum(arrayIndex.Count-1, compute_factor_effort, compute_factor_factor);

                Node node = new Node((char)(remainIndex.ElementAt(i)+65), remainIndex.ElementAt(i), correlation);
                Target.Add(node);
                arrayIndex.Remove(remain_index);
                arrayIndex.Remove(column_length);
            }
        }

        private static List<Node> GreedySearch(List<List<Object>> data)
        {           
            List<Node> best_node = new List<Node>();

            double initial_state = 0;
            double goal_state = 1;
            double current_state = 0;

            if (initial_state.Equals(goal_state))
            {
                return best_node;
            }
            else
            {
                current_state = initial_state;
                int layer = 0;
                while (!goal_state.Equals(current_state))
                {
                    //initial 
                    List<Node> child_node = new List<Node>();

                    //แสดงรอบของ Layer
                    Console.WriteLine("\nLayer : " + layer++);

                    // Create Child Node then keep child node to child_node and Show the Child
                    CreateChild(data, child_node, best_node);

                    // Show The Child_node
                    child_node.ForEach((Node node) => Console.WriteLine(node.ToObject()));

                    // get The best Correlation, *height is the best
                    Node best_correlation = GetHeight(child_node, best_node);
                    if (best_correlation != null)
                    {
                        best_node.Add(best_correlation);
                    }
                    else
                    {
                        current_state = 1;
                    }

                }

                Console.WriteLine("All Best Node : ");
                foreach(Node node in best_node)
                {
                    Console.WriteLine(node.ToObject());
                }
            }

            return best_node;
        }

        private static Node GetHeight(List<Node> child_node, List<Node> best_node)
        {
            //get heightes node
            Node CurrentCorrelation = new Node('A', 0, 0.0f);
            for (int i = 0; i < child_node.Count; i++)
            {
                if ((child_node.ElementAt(i).correlation > CurrentCorrelation.correlation))
                {
                    CurrentCorrelation = child_node[i];
                }
                else if (i.Equals(0))
                {
                    Console.Write(i);
                    CurrentCorrelation = child_node[i];
                }
            }
            Console.WriteLine("Best Node in this layer: " + CurrentCorrelation.ToObject());

            if (best_node.Any())
            {
                if (best_node.ElementAt(Math.Abs(best_node.Count - 1)).correlation < CurrentCorrelation.correlation)
                { 
                return CurrentCorrelation;
                }
            }
            else
            {
                return CurrentCorrelation;
            }

            return null;
        }

        private static double[,] GetMultiColumn(double[,] data, int[] index)
        {
            int rowLen = data.GetLength(0);
            int colLen = index.Length;
            double[,] combineData = new double[rowLen, colLen];
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    combineData[i, j] = data[i, index[j]];
                }
            }
            return combineData; //return ค่าเป็นข้อมูลหลาย Columns
        }

        private static double[,] GetMultiColumn(List<List<Object>> data, int[] index)
        {
            int rowLen = data.Count;
            int colLen = index.Length;
            double[,] combineData = new double[rowLen, colLen];
            for (int i = 0; i < rowLen; i++)
            {
                for (int j = 0; j < colLen; j++)
                {
                    combineData[i, j] = Double.Parse(data.ElementAt(i).ElementAt(index[j]).ToString());
                }
            }
            return combineData; //return ค่าเป็นข้อมูลหลาย Columns
        }

        private static bool IsDuplicate(double[] x, double[] y)
        {
            double[] ax = x;
            double[] ay = y;
            Array.Sort(ax);
            Array.Sort(ay);
            return Enumerable.SequenceEqual(ax, ay);
        }

        private static double ComputeCorrelationSum(int m, double Factor_Effort, double Factor_Factor)
        {
            return (m * Factor_Effort) / Math.Sqrt(m + (m * (m - 1) * Factor_Factor));
        }

        //สำหรับคำนวณ ค่าความสัมพันธ์ระหว่างปัจจัยในชุด s ทั้งหมด กับ Effort
        private static double ComputeFactorEffort(double[,] x)
        {
            double r_sum = 0;
            int x_length = x.GetLength(1) - 1;
            double[] Factor;
            double[] Effort = GetColumn(x, x_length);
            for (int i = 0; i < x_length; i++)
            {
                Factor = GetColumn(x, i);
                //double r = Math.Abs(Correlation(Factor, Effort));
                double r = Correlation(Factor, Effort);
                if (Double.IsNaN(r) || Double.IsInfinity(r)) r = 0;
                r_sum = r_sum + r;
            }
            return r_sum / x_length;
        }

        private static double ComputeFactorFactor(double[,] x)
        {
            double r_sum = 0;
            int count = 0;
            int x_length = x.GetLength(1) - 1;

            if (x_length == 1) return 1; // ถ้ามี 1 ปัจจัยให้ return 1 โดยที่ 1 มาจากความสัมพันธ์ระหว่างตัวมันเอง
            for (int i = 0; i < x_length - 1; i++)
            {
                for (int j = 1; j < x_length; j++)
                {
                    //double r = Math.Abs(Correlation(getColumn(x, i), getColumn(x, j)));
                    double r = Correlation(GetColumn(x, i), GetColumn(x, j));
                    if (Double.IsNaN(r) || Double.IsInfinity(r)) r = 0;
                    r_sum = r_sum + r;
                    count++;
                }
            }
            return r_sum / count;
        }

        private static double Correlation(IEnumerable<Double> xs, IEnumerable<Double> ys)
        {
            // sums of x, y, x squared etc.
            double sx = 0.0;
            double sy = 0.0;
            double sxx = 0.0;
            double syy = 0.0;
            double sxy = 0.0;

            int n = 0;

            using (var enX = xs.GetEnumerator())
            {
                using (var enY = ys.GetEnumerator())
                {
                    while (enX.MoveNext() && enY.MoveNext())
                    {
                        double x = enX.Current;
                        double y = enY.Current;

                        n += 1;
                        sx += x;
                        sy += y;
                        sxx += x * x;
                        syy += y * y;
                        sxy += x * y;
                    }
                }
            }

            // covariation
            double cov = sxy / n - sx * sy / n / n;
            // standard error of x
            double sigmaX = Math.Sqrt(sxx / n - sx * sx / n / n);
            // standard error of y
            double sigmaY = Math.Sqrt(syy / n - sy * sy / n / n);

            // correlation is just a normalized covariation
            return cov / sigmaX / sigmaY;
        }
    }
}