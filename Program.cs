using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ConsoleApp1
{
    class Program
    {

        static double[] getTildaArray(double[,] matrix, double[] tilda_matrix, String name)
        {
            Console.Write("{0}_tilda_mtrx:", name);
            var res_tmp = 1.0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                res_tmp = 1.0;
                for (int l = 0; l < matrix.GetLength(1); l++)
                {
                    res_tmp *= matrix[i, l];
                }
                tilda_matrix[i] = Math.Pow(res_tmp, 1.0 / matrix.GetLength(0));

                Console.Write("{0}", tilda_matrix[i]);
            }
            return tilda_matrix;
        }

        static double[] getMatrixArray(double[] matrix_array, double[] tilda_matrix, double tilda)
        {
            for (var i = 0; i < tilda_matrix.Length; i++)
            {
                matrix_array[i] = tilda_matrix[i] / tilda;
                Console.Write("{0} ", matrix_array[i]);
            }
            return matrix_array;
        }

        static void Main(string[] args)
        {
            String input = File.ReadAllText(@"test.txt");


            //int i = 0, j = 0;
            double[,] e_matrix = new double[5, 5];
            double[,] w1_matrix = new double[4, 4];
            double[,] w2_matrix = new double[4, 4];
            double[,] w3_matrix = new double[4, 4];
            double[,] w4_matrix = new double[4, 4];
            double[,] w5_matrix = new double[4, 4];


            var lines = File.ReadAllLines(@"test.txt");
            
            // tmp vars for creating matrixs
            var counter = 0;
            var j = 0;
            var num_lines = 0;
            var plus_c = 0;
            var res = 0.0;

            for (var c = 0; c < 6; c++)
            {
                
                if (c == 0)
                {
                    num_lines = 5;
                }
                else
                {
                    plus_c = num_lines+1;
                    num_lines += 5;
                }

                for (var i = plus_c; i < num_lines; i++)
                {
                    j = 0;
                    foreach (var col in lines[i].Trim().Split('\t'))
                    {
                        if (col.Contains('/'))
                        {
                            var index = 1;
                            foreach (var item in col.Select((value, id) => new { id, value }))
                            {
                                if (item.value == '/') index = item.id;
                            }
                            res = double.Parse(col.Substring(0, index)) / double.Parse(col.Substring(col.Length - index));

                        }
                        else
                        {
                            res = double.Parse(col.Trim());
                        }
                        
                        switch (c) 
                        {
                            case 0:
                                e_matrix[i, j] = res;
                                Console.Write("{0}\t", e_matrix[i, j]);
                                break;
                            case 1:
                                w1_matrix[i - (plus_c), j] = res;
                                Console.Write("{0}\t", w1_matrix[i - (plus_c), j]);
                                break;
                            case 2:
                                w2_matrix[i - (plus_c), j] = res;
                                Console.Write("{0}\t", w2_matrix[i - (plus_c), j]);
                                break;
                            case 3:
                                w3_matrix[i - (plus_c), j] = res;
                                Console.Write("{0}\t", w3_matrix[i - (plus_c), j]);
                                break;
                            case 4:
                                w4_matrix[i - (plus_c), j] = res;
                                Console.Write("{0}\t", w4_matrix[i - (plus_c), j]);
                                break;
                            case 5:
                                w5_matrix[i - (plus_c), j] = res;
                                Console.Write("{0}\t", w5_matrix[i - (plus_c), j]);
                                break;
                        }

                        //Console.Write("{0}\t", e[i, j]);
                        j++;
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            // e
            double[] e_tilda_mtrx = new double[5];

            e_tilda_mtrx = getTildaArray(e_matrix, e_tilda_mtrx, "e");
            

            var e_tilda = e_tilda_mtrx.Sum();
            Console.WriteLine();
            Console.WriteLine("e_tilda: {0} ", e_tilda);

            double[] e_array = new double[5];

            Console.Write("e_array: ");

            e_array = getMatrixArray(e_array, e_tilda_mtrx, e_tilda);
            Console.WriteLine();
            Console.WriteLine();

            // w1
            double[] w1_tilda_mtrx = new double[4];

            w1_tilda_mtrx = getTildaArray(w1_matrix, w1_tilda_mtrx, "w1");


            var w1_tilda = w1_tilda_mtrx.Sum();
            Console.WriteLine();
            Console.WriteLine("w1_tilda: {0} ", w1_tilda);

            double[] w1_array = new double[4];

            Console.Write("w1_array: ");

            w1_array = getMatrixArray(w1_array, w1_tilda_mtrx, w1_tilda);
            Console.WriteLine();
            Console.WriteLine();

            // w2
            double[] w2_tilda_mtrx = new double[4];

            w2_tilda_mtrx = getTildaArray(w2_matrix, w2_tilda_mtrx, "w2");


            var w2_tilda = w2_tilda_mtrx.Sum();
            Console.WriteLine();
            Console.WriteLine("w2_tilda: {0} ", w2_tilda);

            double[] w2_array = new double[4];

            Console.Write("w2_array: ");

            w2_array = getMatrixArray(w2_array, w2_tilda_mtrx, w2_tilda);
            Console.WriteLine();
            Console.WriteLine();


            // w3
            double[] w3_tilda_mtrx = new double[4];

            w3_tilda_mtrx = getTildaArray(w3_matrix, w3_tilda_mtrx, "w3");


            var w3_tilda = w3_tilda_mtrx.Sum();
            Console.WriteLine();
            Console.WriteLine("w3_tilda: {0} ", w3_tilda);

            double[] w3_array = new double[4];

            Console.Write("w3_array: ");

            w3_array = getMatrixArray(w3_array, w3_tilda_mtrx, w3_tilda);
            Console.WriteLine();
            Console.WriteLine();


            // w4
            double[] w4_tilda_mtrx = new double[4];

            w4_tilda_mtrx = getTildaArray(w4_matrix, w4_tilda_mtrx, "w4");


            var w4_tilda = w4_tilda_mtrx.Sum();
            Console.WriteLine();
            Console.WriteLine("w4_tilda: {0} ", w4_tilda);

            double[] w4_array = new double[4];

            Console.Write("w4_array: ");

            w4_array = getMatrixArray(w4_array, w4_tilda_mtrx, w4_tilda);
            Console.WriteLine();
            Console.WriteLine();


            // w5
            double[] w5_tilda_mtrx = new double[4];

            w5_tilda_mtrx = getTildaArray(w5_matrix, w5_tilda_mtrx, "w5");


            var w5_tilda = w5_tilda_mtrx.Sum();
            Console.WriteLine();
            Console.WriteLine("w5_tilda: {0} ", w5_tilda);

            double[] w5_array = new double[4];

            Console.Write("w5_array: ");

            w5_array = getMatrixArray(w5_array, w5_tilda_mtrx, w5_tilda);
            Console.WriteLine();
            Console.WriteLine();


            double[] w = new double[4];

            for (var i=0; i< w.Length; i++) 
            {
                w[i] = e_array[0] * w1_array[i] + e_array[1] * w2_array[i] + e_array[2] * w3_array[i] + e_array[3] * w4_array[i] + e_array[4] * w5_array[i];  
                Console.WriteLine("w{0}: {1}", i+1, w[i]);
            }


            //for (double[] e_row in e_matrix)
            //{
            //    foreach (double e_col in e_row)
            //    {
            //        e_tilda_mtrx
            //    }

            //}
  




            //foreach (var row in input.Split('\n').Select((value1, id1) => new { id1, value1 }))
            //{
            //    Console.WriteLine("{0}", row);
            //    if (row.value1 == "\r" && row.id1 == 5)
            //    {
                    
            //    }
            //    Console.WriteLine("{0} {1}", row.value1, row.id1);
            //    j = 0;
            //    foreach (var col in row.value1.Trim().Split('\t'))
            //    {
            //        if (col.Contains('/')) 
            //        { 
            //            var index = 1;
            //            foreach (var item in col.Select((value, id) => new { id, value }))
            //            {
            //                if (item.value == '/') index = item.id;
            //            }
            //            e[i, j] = double.Parse(col.Substring(0, index)) / double.Parse(col.Substring(col.Length - index));
            //        }
            //        else
            //        {
            //            e[i, j] = double.Parse(col.Trim());
            //        }

            //        j++;
            //    }
            //    i++;
            //}
            
            Console.ReadKey();
        }
    }
}