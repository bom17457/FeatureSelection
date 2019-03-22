using System;

namespace BestFirstSearch
{
    class MainClass
    {
        class Node
        {
            public int node_index = 0;
            public double correlation = 0;

            public Node(int node_index, double correlation)
            {
                this.node_index = node_index;
                this.correlation = correlation;
            }

            public char indexToChar()
            {
                return (char)(node_index + 65);
            }

            public String toObject()
            {
                return "{index:" + node_index + ", inChar:" + indexToChar() + ", correlation:" + correlation + "}";
            }
        }

        public static void Main(string[] args)
        {
            double[,] data = getTrainData();
            GreedySearch(data);
        }

        private static double[,] getTrainData()
        {
            double[,] x = {
    { 1.02,4.96,1.01,5.65,2.19,4.68,1,0.9,1,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1.1,1,1,1,0.8,1,195 },
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
    {112.28,4.96,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,0.91,0.91,1,0.8,1,1278}
    };
            return x;
        }

        private static double[] getColumn(double[,] data, int column)
        {
            int row = data.GetLength(0);
            double[] x = new double[row];

            for (int i = 0; i < row; i++)
            {
                x[i] = data[i, column];
            }
            return x;
        }

        private static List<Node> GreedySearch(double[,] data)
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
                while (!goal_state.Equals(current_state))
                {
                    //best_index is index of column                    
                    List<Node> child_state = new List<Node>();
                    for (int i = 0; i < data.GetLength(1) - 1; i++)
                    {
                        bool skip = false;
                        foreach (Node node in best_node)
                        {
                            if (node.node_index == i)
                            {
                                skip = true;
                                break;
                            }
                        }
                        if (skip)
                        {
                            break;
                        }
                        double[,] r_FactorEffort = mapArrays(getColumn(data, i), getColumn(data, data.GetLength(1) - 1));
                        double[,] r_FactorFactor = mapArrays(getColumn(data, i), getColumn(data, data.GetLength(1) - 1));

                        double compute_Factor_Effort = ComputeFactorEffort(r_FactorEffort);
                        double compute_Factor_Factor = ComputeFactorFactor(r_FactorFactor);

                        double current_correlation = ComputeCorrelationSum(data.GetLength(1), compute_Factor_Effort, compute_Factor_Factor);
                        child_state.Add(new Node(i, current_correlation));
                    }

                    //find child in best node
                    for (int i = 0; i < child_state.Count; i++)
                    {
                        for (int j = 0; j < best_node.Count; j++)
                        {
                            if (child_state[i].Equals(best_node.ElementAt(j)))
                            {
                                child_state.RemoveAt(i);
                            }
                        }
                    }

                    //find best Correlation
                    Node CurrentCorrelation = new Node(0, 0.0f);
                    for (int i = 0; i < child_state.Count; i++)
                    {
                        if ((child_state.ElementAt(i).correlation > CurrentCorrelation.correlation))
                        {
                            CurrentCorrelation = child_state[i];
                        }
                        else if (i.Equals(0))
                        {
                            CurrentCorrelation = child_state[i];
                        }
                    }
                    best_node.Add(CurrentCorrelation);
                    Console.WriteLine(CurrentCorrelation.toObject());
                }
            }

            return best_node;
        }

        private static double[,] mapArrays(double[] fac, double[] eff)
        {
            double[,] newArray = new double[fac.Length, 2];
            for (int i = 0; (i < fac.Length) & (i < fac.Length); i++)
            {
                newArray[i, 0] = fac[i];
                newArray[i, 1] = eff[i];
            }
            return newArray;
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
            if (Factor_Factor.Equals(-1))
            {
                return Factor_Effort;
            }

            double r_correlation = (m * Factor_Effort) / (Math.Sqrt(m + (m * (m - 1) * Factor_Factor)));
            return Double.IsNaN(r_correlation) || Double.IsInfinity(r_correlation) ? 0 : r_correlation;
        }

        private static double ComputeFactorEffort(double[,] x)
        {
            double r_sum = 0;
            int x_length = x.GetLength(1) - 1;
            double[] Factor;
            double[] Effort = getColumn(x, x_length);

            for (int i = 0; i < x_length; i++)
            {
                Factor = getColumn(x, i);
                Double r = Correlation(Factor, Effort);
                Double Corrate = (Double.IsNaN(r) || Double.IsInfinity(r)) ? 0 : r;
                r_sum += Corrate;
            }
            return r_sum / x_length;
        }



        private static double ComputeFactorFactor(double[,] x)
        {
            double r_sum = 0;
            int count = 0;
            int x_length = x.GetLength(1) - 1;
            if (x_length == 1)
            {
                return -1;
            }
            else
            {
                for (int i = 0; i < x_length - 2; i++)
                {
                    for (int j = 1; j < x_length - 1; j++)
                    {
                        Double r = Correlation(getColumn(x, i), getColumn(x, j));
                        Double Corrate = (Double.IsNaN(r) || Double.IsInfinity(r)) ? 0 : r;

                        r_sum += Math.Abs(Corrate);
                        count++;
                    }
                }
                return r_sum / count;
            }
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
