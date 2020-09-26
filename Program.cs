using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Linq;

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
            
            var lines = File.ReadAllLines(@"v1.txt");
            var count = 0;
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "") {
                    break;
                }
                count++;
            }

            var N_e = count;
            var N_w = count-1;

            double[,] e_matrix = new double[N_e, N_e];
            double[, ,] w_matrix = new double[N_e, N_w, N_w];

            // tmp vars for creating matrixs
            var data = 0.0;
            var j_v = 0;

            String[] line;
            var current_line = 0;

            for (var i = 0; i < 2*N_e-N_w; i++)
            { 
                if (i == 0) j_v = N_e; 
                else j_v = N_w;

                for (var j = 0; j < j_v; j++)
                {
                    do
                    {
                        line = lines[current_line].Trim().Split('\t');
                        current_line++;
                    } while (line[0] == "");
                    
                    if (line[0] == "") break;
                    for (var k = 0; k < j_v; k++)
                    {
                        if (line[k].Contains('/'))
                        {
                            var index = 1;
                            foreach (var item in line[k].Select((value, id) => new { id, value }))
                            {
                                if (item.value == '/') index = item.id;
                            }
                            data = double.Parse(line[k].Substring(0, index)) / double.Parse(line[k].Substring(line[k].Length - index));

                        }
                        else
                        {
                            data = double.Parse(line[k].Trim());
                        }
                        if (i == 0)
                        {

                            e_matrix[j, k] = data;
                            Console.Write("{0}\t", e_matrix[j, k]);
                        }
                        else
                        {
                            w_matrix[i-1, j, k] = data;
                            Console.Write("{0}\t", w_matrix[i-1, j, k]);
                        }
                        
                    }
                    Console.WriteLine();

                }

                Console.WriteLine();
            }

            double[] e_tilda_mtrx = new double[N_e];

            e_tilda_mtrx = getTildaArray(e_matrix, e_tilda_mtrx, "e");


            var e_tilda = e_tilda_mtrx.Sum();
            Console.WriteLine();
            Console.WriteLine("e_tilda: {0} ", e_tilda);

            double[] e_array = new double[N_e];

            Console.Write("e_array: ");

            e_array = getMatrixArray(e_array, e_tilda_mtrx, e_tilda);
            Console.WriteLine();
            Console.WriteLine();

            // w1
            double[,] w_tilda_mtrx = new double[N_e, N_w];

            var res_tmp = 1.0;
            for (var i = 0; i < N_e; i++)
            {

                Console.Write("w{0}_tilda_mtrx: ", i+1);
                
                for (int j = 0; j < w_matrix.GetLength(1); j++)
                {
                    res_tmp = 1.0;
                    for (int k = 0; k < w_matrix.GetLength(2); k++)
                    {
                        res_tmp *= w_matrix[i, j, k];
                    }
                    w_tilda_mtrx[i, j] = Math.Pow(res_tmp, 1.0 / w_matrix.GetLength(1));

                    Console.Write("{0} ", w_tilda_mtrx[i, j]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();

            double[] w_tilda = new double[N_e];
            var sum_tmp = 0.0;
            for (var i = 0; i < w_tilda.Length; i++)
            {
                sum_tmp = 0.0;         
                for (var k = 0; k < w_tilda_mtrx.GetLength(1); k++)
                {
                    sum_tmp += w_tilda_mtrx[i, k];
                }

                w_tilda[i] = sum_tmp;
                Console.WriteLine("w{0}_tilda: {1} ", i+1, w_tilda[i]);
            }

            Console.WriteLine();


            double[,] w_array = new double[N_e, N_w];




            for (var i = 0; i < w_array.GetLength(0); i++)
            {
                Console.Write("w{0}_array: ", i+1);
                for (var j = 0; j < w_array.GetLength(1); j++)
                {
                    w_array[i, j] = w_tilda_mtrx[i, j] / w_tilda[i];
                    Console.Write("{0} ", w_array[i,j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            double[] w = new double[N_w];

            for (var i = 0; i < w.Length; i++)
            {
                res_tmp = 0.0;
                for (var j = 0; j < w_array.GetLength(0); j++)
                {
                        res_tmp += e_array[j] * w_array[j, i];
                }
                w[i] = res_tmp;
                
                Console.WriteLine("w{0}: {1}", i + 1, w[i]);
            }

            Console.WriteLine();
            Console.WriteLine("Optimal alternative x{0}, w{0} = {1}", w.ToList().IndexOf(w.Max()) + 1, w.Max());



            Console.ReadKey();
        }
    }
}