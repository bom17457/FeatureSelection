using System;
using System.Collections.Generic;
using System.Linq;

namespace BestFirstSearch
{
    class MainClass
    {
        class Node
        {
            public int column_index = 0;
            public char column_name = '#';
            public double correlation = 0;
            public Node parent_node;
            public bool isGenerate = false;

            public Node(Node parent_node,char node_name, int node_index, double correlation)
            {
                this.column_name = node_name;
                this.column_index = node_index;
                this.correlation = correlation;
                this.parent_node = parent_node;
            }

            public Node(Node parent_node)
            {
                this.parent_node = parent_node;
            }

            public String ToObject()
            {
                //if(parent_node.parent_node != null)
                //{
                    String str = "";
                    Node prototype_parent_node = this.parent_node;
                    int round = 0;
                    while (prototype_parent_node!=null)
                    {
                        str += ((round>0)?"->":"")+prototype_parent_node.column_name;
                        prototype_parent_node = prototype_parent_node.parent_node;
                        round++;
                    }
                    return "{index:" + column_index + ", parentNode:{"+str+"},ColumnName:" + column_name + ", correlation:" + correlation + "}";
            }

            public void hasGenerate()
            {
                this.isGenerate = true;
            }
        }

        public static void Main(string[] args)
        {

            List<List<Object>> data = getTrainDataList();
            int[] index = { 0, 1, 2, 3, 4, 5, 23 };
            data = GetMultiColumn(data, index, "");
            BestFirstSearch(data);
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

        private static void CreateChildBFS(List<List<Object>> Data, List<Node> Target, Node parent_node, List<Node> parent_list)
        {
            List<int> arrayIndex = new List<int>();
            List<int> remainIndex = new List<int>();

            int col_len = Data.ElementAt(0).Count - 1;

            if (parent_node.parent_node != null)
            {
                for (int i = parent_node.column_index + 1; i < col_len; i++) remainIndex.Add(i);
                Node prototype_parent_node = parent_node;
                while (prototype_parent_node.parent_node != null)
                {
                    arrayIndex.Add(prototype_parent_node.column_index);
                    remainIndex.Remove(prototype_parent_node.column_index);
                    prototype_parent_node = prototype_parent_node.parent_node;
                }
            }
            else
            {
                for (int i = parent_node.column_index; i < col_len; i++) remainIndex.Add(i);
            }

            double[,] r_FactorFactor;
            double compute_factor_factor = 0.0f;

            double[,] r_FactorEffort;
            double compute_factor_effort = 0.0f;

            for (int i=0; i<remainIndex.Count; i++)
            {
                int column_index = remainIndex.ElementAt(i);
                arrayIndex.Add(column_index);
                arrayIndex.Add(col_len); //col_len is effort column

                r_FactorFactor = GetMultiColumn(Data, arrayIndex.ToArray());
                compute_factor_factor = ComputeFactorFactor(r_FactorFactor);

                r_FactorEffort = GetMultiColumn(Data, arrayIndex.ToArray());
                compute_factor_effort = ComputeFactorEffort(r_FactorEffort);

                double correlation = ComputeCorrelationSum(arrayIndex.Count - 1, compute_factor_effort, compute_factor_factor);

                Node child = new Node(parent_node, (char)(remainIndex.ElementAt(i) + 65), remainIndex.ElementAt(i), correlation);
                Target.Add(child);

                arrayIndex.Remove(column_index);
                arrayIndex.Remove(col_len);
            }
        }

        private static List<Node> BestFirstSearch(List<List<Object>> data)
        {
            List<Node> child_node = new List<Node>();
            List<Node> parent_node = new List<Node>();

            double initial_state = 0;
            double goal_state = 1;
            double current_state = 0;
            int column_length = data.ElementAt(0).Count;
            int layer = 0;

            parent_node.Add(new Node(null));

            while (!goal_state.Equals(initial_state))
            {
                Console.WriteLine("Layer : "+layer++);
                if (current_state.Equals(goal_state))
                {
                    return parent_node;
                }
                else
                {
                    child_node = new List<Node>();
                    foreach (Node p_node in parent_node)
                    {
                        if (!p_node.isGenerate) {
                            CreateChildBFS(data, child_node, p_node, parent_node);
                            p_node.hasGenerate();
                        }
                    }

                    foreach (Node c_node in child_node)
                    {
                        Console.WriteLine(c_node.ToObject());
                        parent_node.Add(c_node);
                    }

                    if(child_node.Count == 1)
                    {
                        initial_state = 1;
                    }
                }
            }

            GetHeight(parent_node);

            return child_node;
        }

        private static void GetHeight(List<Node> parent)
        {
            //get heightes node
            Node CurrentCorrelation = new Node(null,'A', 0, 0.0f);
            for (int i = 0; i < parent.Count; i++)
            {
                if ((parent.ElementAt(i).correlation > CurrentCorrelation.correlation))
                {
                    CurrentCorrelation = parent[i];
                }
                else if (i.Equals(0))
                {
                    CurrentCorrelation = parent[i];
                }
            }
            Console.WriteLine("\nThe best factor is: " + CurrentCorrelation.ToObject());
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

        private static List<List<Object>> GetMultiColumn(List<List<Object>> data, int[] index, String func)
        {
            int rowLen = data.Count;
            int colLen = index.Length;
            List<List<Object>> combineList = new List<List<object>>();
            for (int i = 0; i < rowLen; i++)
            {
                List<Object> dataList = new List<object>();
                for (int j = 0; j < colLen; j++)
                {
                    dataList.Add(Double.Parse(data.ElementAt(i).ElementAt(index[j]).ToString()));
                }
                combineList.Add(dataList);
            }
            return combineList; //return ค่าเป็นข้อมูลหลาย Columns
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