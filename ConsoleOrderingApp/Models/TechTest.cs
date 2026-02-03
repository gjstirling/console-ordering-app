//public class Test
//{
//    public static void Main(String[] args)
//    {
//        double[] array1 = new double[100];
//        double[,] array2 = new double[10, 10];

//        // initialises an array of 100 elements too zero (redundent) 
//        for (int i = 0; i < 100; i++)
//        {
//            array1[i] = 0;
//        }

//        // copying array1 --> array2 this is redundent as it is overwritten in next for loop 
//        for (int i = 0; i < 10; i++)
//        {
//            for (int j = 0; j < 10; j++)
//            {
//                array2[i, j] = array1[(i * 10) + j];
//            }
//        }

        
//        for (int i = 0; i < 10; i++)
//        {
//            for (int j = 0; j < 10; j++)
//            {
//                array2[i, j] = Math.Sin(0.1);
//            }
//        }

//        // Flattens the 2d array into 1d array - also redundent as each value in the 2d array is the same
//        for (int i = 0; i < 10; i++)
//        {
//            for (int j = 0; j < 10; j++)
//            {
//                array1[(i * 10) + j] = array2[i, j];
//            }
//        }

//        // this only logs output so could be changed 
//        for (int i = 0; i < 100; i++)
//        {
//            Console.WriteLine(array1[i]);
//        }
//    }


//    public static void MainSolution(String[] args)
//    {
//        double[] array1 = new double[100];

//        double value = Math.Sin(0.1);

//        for (int i = 0; i < 100; i++)
//        {
//            array1[i] = value;
//            Console.WriteLine(array1[i]);
//        }
//     }
//}