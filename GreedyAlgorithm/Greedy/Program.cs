using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestFirstSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] data = getTrainData();
            int[] bestFeatureSet = greedy(data);

            Console.WriteLine("");
            Console.WriteLine("All best factors given from greedy search alg. are composed of:");
            for (int i = 0; i < bestFeatureSet.Length; i++)
            {
                Console.WriteLine("Factor: " + bestFeatureSet[i]);
            }
            Console.ReadKey();
        }

        //อัลกอลิทึม Greey Search
        private static int[] greedy(double[,] data)
        {
            double bestScore = 0.0;//ค่าความสัมพันธ์ หรือ ค่า Heuristic ในสถานะเริ่มต้น
            double goalScore = 1.0;//ค่าความสัมพันธ์ หรือ ค่า Heuristic ในสถานะเป้าหมาย
            int factorNum = data.GetLength(1) - 1;//-1 is to ignore software effort
            bool isImprove = true;
            List<int> bestFeatureSet = new List<int>();
            List<int> remainingFeatureSet = new List<int>();
            for (int i = 0; i < factorNum; i++) remainingFeatureSet.Add(i);
            int round = 0;

            while (isImprove == true && bestScore < goalScore && bestFeatureSet.Count < factorNum)
            {
                List<double> jScore = new List<double>();
                List<int> jFeatureSet = new List<int>(bestFeatureSet);
                Console.WriteLine("####### Round : " + ++round + " #######");
                int i = 0;
                foreach (int j in remainingFeatureSet)
                {
                    jFeatureSet.Add(j);
                    jFeatureSet.Add(factorNum);
                    double[,] x = getMultiColumn(data, jFeatureSet.ToArray());

                    //compute relation score
                    double r = ComputeCorrelationSum(jFeatureSet.Count - 1, ComputeFactorEffort(x), ComputeFactorFactor(x));
                    //add the score into jScore
                    jScore.Add(r);
                    jFeatureSet = new List<int>(bestFeatureSet);
                    Console.WriteLine("Factor " + ++i + " = " + r.ToString());
                }

                int ind = maxIndex(jScore);
                double currenScore = jScore[ind];
                Console.WriteLine("The best factor in this round is factor " + (ind + 1) + " with score " + currenScore.ToString());
                Console.WriteLine("");

                if (currenScore <= bestScore)
                {
                    isImprove = false;
                }
                else
                {
                    bestScore = currenScore;
                    bestFeatureSet.Add(ind); //add max index into bestFeatureSet
                    remainingFeatureSet.Remove(ind);//remove max index from remainingFeatureSet
                }
            }
            return bestFeatureSet.ToArray();
        }



        /*สำหรับเข้าดึงข้อมูลหลาย Column
           ตัวอย่าง 
           กำหนดให้ double[,] data = {{5,7,1,4},
                                     {4,6,3,5},
                                     {2,7,9,3},
                                     {1,8,8,4}};
                   int[] requiredIndex = {0,2};
                   double[,] newData=getMultiColumn(data,requiredIndex);
                   ผลลัพท์ของ newData คือ {{5,1},
                                       {4,3},
                                       {2,9},
                                       {1,8}}
        */
        private static double[,] getMultiColumn(double[,] data, int[] index)
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

        /* สำหรับเข้าดึงข้อมู 1 Column
           ตัวอย่าง 
           กำหนดให้ double[,] data = {{5,7,1,4},
                                     {4,6,3,5},
                                     {2,7,9,3},
                                     {1,8,8,4}};
                   double[] newData=getColumn(data,2);
                   ผลลัพท์ของ newData คือ {1,
                                       3,
                                       9,
                                       8}
        */
        private static double[] getColumn(double[,] data, int column)
        {
            int row = data.GetLength(0);
            double[] x = new double[row];

            for (int i = 0; i < row; i++)
            {
                x[i] = data[i, column];
            }

            return x; //return ค่าเป็นข้อมูลทั้ง Column
        }

        //สำหรับตรวจสอบการซ้ำกันของชุดปัจจัย
        private static bool IsDuplicate(double[] x, double[] y)
        {
            double[] ax = x;
            double[] ay = y;
            Array.Sort(ax);
            Array.Sort(ay);
            return Enumerable.SequenceEqual(ax, ay);
        }

        //สำหรับคำนวณ ค่าความสัมพันธ์รวม s
        private static double ComputeCorrelationSum(int m, double Factor_Effort,
                                                    double Factor_Factor)
        {
            return (m * Factor_Effort) / Math.Sqrt(m + (m * (m - 1) * Factor_Factor));
        }

        //สำหรับคำนวณ ค่าความสัมพันธ์ระหว่างปัจจัยในชุด s ทั้งหมด กับ Effort
        private static double ComputeFactorEffort(double[,] x)
        {
            double r_sum = 0;
            int x_length = x.GetLength(1) - 1;
            double[] Factor;
            double[] Effort = getColumn(x, x_length);
            for (int i = 0; i < x_length; i++)
            {
                Factor = getColumn(x, i);
                //double r = Math.Abs(Correlation(Factor, Effort));
                double r = Correlation(Factor, Effort);
                if (Double.IsNaN(r) || Double.IsInfinity(r)) r = 0;
                r_sum = r_sum + r;

            }
            return r_sum / x_length;
        }

        //สำหรับคำนวณ ค่าความสัมพันธ์ระหว่างปัจจัยในชุด s ทั้งหมด
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
                    double r = Correlation(getColumn(x, i), getColumn(x, j));
                    if (Double.IsNaN(r) || Double.IsInfinity(r)) r = 0;
                    r_sum = r_sum + r;
                    count++;
                }
            }
            return r_sum / count;
        }


        //หา Index ใน List ที่มีค่าสูงสุด
        private static int maxIndex(List<double> x)
        {
            int indexMax
                = !x.Any() ? -1 :
                x
                .Select((value, index) => new { Value = value, Index = index })
                .Aggregate((a, b) => (a.Value > b.Value) ? a : b)
                .Index;
            return indexMax;
        }

        //สำหรับคำนวณ ค่าความสัมพันธ์ Pearson Correlation  
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

        //สำหรับเก็บ Train Data ในรูปแบบ Array 2 มิติ 
        private static double[,] getTrainData()
        {
            double[,] x = {{0.27,3.72,1.01,5.65,3.29,4.68,1,0.9,1,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1,1,1,1,0.8,1,208},
                            { 1.02,4.96,1.01,5.65,2.19,4.68,1,0.9,1,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1.1,1,1,1,0.8,1,195},
                            { 2.52,3.72,1.01,5.65,3.29,4.68,1,0.9,1,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1,1,1,1,0.8,1,162},
                            { 4.02,3.72,3.04,2.83,0,3.12,1.1,1,1,1.07,1.11,1,1,0.87,0.85,0.88,0.81,0.88,0.91,0.91,1,0.8,1,113},
                            { 4.28,4.96,1.01,5.65,3.29,4.68,1,0.9,1.17,1.24,0.91,1,1,0.87,0.85,0.88,0.81,1.1,1,1,1,0.8,1,277},
                            {4.48,4.96,1.01,5.65,1.1,4.68,1.1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,0.91,0.91,1,0.8,1,175},
                            {5,4.96,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1,0.91,0.91,1,0.8,1,120},
                            {5.11,2.48,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,0.88,0.91,0.91,1,0.8,1,189},
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

        //สำหรับเก็บ Test Data ในรูปแบบ Array 2 มิติ 
        private static double[,] getTestData()
        {
            double[,] x = {
                            { 11.53,2.48,3.04,5.65,1.1,4.68,1,0.9,0.87,1,0.91,1,1,0.87,0.85,0.88,0.81,0.88,1,1,1,0.8,1,140 },
                            {12.32,4.96,1.01,4.24,1.1,4.68,1,0.9,1,0.95,1,1,1,0.87,0.85,1,0.81,0.88,1,1,1,0.8,1,120},
                            {17.52,3.72,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,0.88,0.91,0.91,1,0.8,1,306},
                            {19.06,6.2,3.04,4.24,0,3.12,1,1,1,1.07,1.11,1,1,0.87,0.85,0.88,0.81,0.88,0.91,0.91,1,0.8,1,195},
                            {20.94,4.96,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1,0.91,0.91,1,0.8,1,228},
                            {22.2,4.96,1.01,5.65,1.1,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1,0.91,0.91,1,0.8,1,392},
                            {24.67,3.72,3.04,2.83,0,3.12,1.1,1,1,1.07,1.11,1.29,1,0.87,0.85,0.88,0.81,0.88,0.91,0.91,1,0.8,1,92},
                            {25.07,4.96,1.01,5.65,0,4.68,1,0.9,1,1,0.91,1,1,0.87,0.85,0.88,0.81,1.1,0.91,0.91,1,0.8,1,531}
                         };
            return x;
        }

    }


}
