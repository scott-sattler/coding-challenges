using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Dynamic;
using System.IO.Pipes;
using System.Xml;
using System.Net;
using System.Reflection.PortableExecutable;
using Microsoft.VisualBasic.FileIO;
using System.Runtime.CompilerServices;
using System.Diagnostics;
//using System.Net.Configuration;

namespace hackerrank
{
    // Things to read more about:
    // What is the proof (or why is this true) that the minimum swaps to sort 
    // (minimum swaps) can be accomplished with the simple algo I write.

    class Program
    {
        static void Main()
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\");

            // Place next solution here ABOVE here:

            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            // Place solution here for timed execution.

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
        }

        // ********************
        // * Helper Functions *
        // ********************
        
        public static void Print_Int_Array(int[] int_array)
        {
            Console.Write("[");
            foreach (int element in int_array)
            {
                Console.Write(" " + element);

            }
            Console.WriteLine(" ]");
        }

        public static void Print_Int_List(List<int> int_array)
        {
            Console.Write("[");
            foreach (int element in int_array)
            {
                Console.Write(" " + element);

            }
            Console.WriteLine(" ]");
        }


        public static int[][] Import_Array_From_Text_File(string file_location)
        {
            string[] text_file_into_strings = File.ReadLines(file_location).ToArray();

            int[][] text_file_converted = new int[text_file_into_strings.Length - 1][];

            for (int i = 0; i < text_file_converted.Length; i++) // not sure how else to initialize
                text_file_converted[i] = new int[3];

            for (int i = 1; i < text_file_into_strings.Length; i++)
            {
                int j_array_index = 0;
                int int_element_value = 0;
                string str_element_value = "";
                char txt = '\x0000';

                for (int j = 0; j < text_file_into_strings[i].Length; j++)
                {
                    txt = text_file_into_strings[i][j];

                    if (txt == ' ')
                    {
                        Int32.TryParse(str_element_value, out int_element_value); // TODO: review
                        text_file_converted[i - 1][j_array_index] = int_element_value;

                        j_array_index += 1;
                        int_element_value = 0;
                        str_element_value = "";
                    }
                    else if (j == text_file_into_strings[i].Length - 1)
                    {
                        str_element_value += txt;

                        Int32.TryParse(str_element_value, out int_element_value); // TODO: review
                        text_file_converted[i - 1][j_array_index] = int_element_value;

                        j_array_index += 1;
                        int_element_value = 0;
                        str_element_value = "";
                    }
                    else
                        str_element_value += txt;
                }
            }

            return text_file_converted;
        }

        // unimplemented/incomplete
        public static int[][] Import_1DArrays_From_Text_File(string file_location)
        {
            string[] text_file_into_strings = File.ReadLines(file_location).ToArray();

            int[][] text_file_converted = new int[Convert.ToInt32(text_file_into_strings[0])][];

            for (int i = 0; i < text_file_converted.Length; i++) // not sure how else to initialize
                text_file_converted[i] = new int[Convert.ToInt32(text_file_into_strings[i * 2 + 1])];

            for (int i = 0; i < text_file_into_strings.Length; i++)
            {
                //for (int j = 0; j < text_file_into_strings[(i + 1) * 2].Length; j++)
                //{
                //    //text_file_converted[i][]
                //}

            }


            return text_file_converted;
        }

        public static List<long> Parse_String(string file_location)
        {
            string[] text_file_into_strings = File.ReadLines(file_location).ToArray();

            List<long> converted_list = new List<long>();

            var temp = text_file_into_strings[1].Split(' ');
            foreach (string value in temp)
                converted_list.Add(long.Parse(value));

            return converted_list;
        }

        public static int[] Parse_String_Array(string file_location)
        {
            string[] text_file_into_strings = File.ReadLines(file_location).ToArray();

            int[] converted_list = new int[int.Parse(File.ReadLines(file_location).ToArray()[0].Split(' ')[0])];

            var temp = text_file_into_strings[1].Split(' ');
            for (int i = 0; i < temp.Length; i++)
                converted_list[i] = int.Parse(temp[i]);

            //foreach (int number in converted_list)
            //    Console.WriteLine(number);

            return converted_list;
        }

        public static List<List<int>> Parse_Input_List_List_Int(string file_location)
        {
            string[] text_file_into_strings = File.ReadLines(file_location).ToArray();
            List<List<int>> constructed_list = new List<List<int>>();

            for (int i = 0; i < text_file_into_strings.Length; i++)
            {
                if (i > 0)
                {
                    constructed_list.Add(text_file_into_strings[i].TrimEnd().Split(' ').ToList().Select(queriesTemp => Convert.ToInt32(queriesTemp)).ToList());
                }
            }

            return constructed_list;
        }

        public static int[][] Parse_Input_From_File(string file_location)
        {

            int[][] text_file_into_int_array = File
                .ReadLines(file_location)
                .Select(line => line.Split(' '))
                .Select(line => Array.ConvertAll(line, element => Convert.ToInt32(element)))
                .ToArray();

            return text_file_into_int_array;
        }

        public static object Get_Test_Case(string location, string web_or_file = "web", string output_select = "int[][]")
        {
            string text_file_raw = "";

            // import string
            switch (web_or_file)
            {
                case "web":
                    text_file_raw = new WebClient().DownloadString(location);
                    break;
                case "file":
                    text_file_raw = File.ReadAllText(location);
                    break;
                default:
                    Console.WriteLine("ERROR: web or file not specified");
                    break;
            }

            if (text_file_raw == "") { Console.WriteLine("ERROR: no text data"); return new object(); }

            // process string
            string[] text_file_lines = text_file_raw.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            switch (output_select)
            {
                case "int[0]":
                    return (object)((int[][])Get_Test_Case(location, web_or_file, "int[][]"))[0];

                case "int[1]":
                    return (object)((int[][])Get_Test_Case(location, web_or_file, "int[][]"))[1];

                case "int[][]":
                    int[][] text_file_converted_int_array = text_file_lines
                        .Select(line => line.TrimEnd('\r', '\n').Split(' '))
                        .Select(line => Array.ConvertAll(line, element => Convert.ToInt32(element)))
                        .ToArray();
                    return (object)text_file_converted_int_array;

                case "long[][]":
                    long[][] text_file_converted_long_array = text_file_lines
                        .Select(line => line.TrimEnd('\r', '\n').Split(' '))
                        .Select(line => Array.ConvertAll(line, element => Convert.ToInt64(element)))
                        .ToArray();
                    return (object)text_file_converted_long_array;

                case "string[]":
                    string[] text_file_converted_string_array = text_file_lines;
                    return (object)text_file_converted_string_array;

                default:
                    Console.WriteLine("ERROR: output not specified");
                    break;
            }

            return new object();
        }

    }

    // *******************
    // *    SOLUTIONS    *
    // *******************
    
    class Solutions
    {
        // There is a large pile of socks that must be paired by color. Given an array of integers representing the color of each sock, determine how many pairs of socks with matching colors there are.

        // Complete the sockMerchant function below.
        public static int SockMerchant(int n, int[] ar)
        {
            List<int> found_socks = new List<int>();
            int found_pairs = 0;

            foreach (int element in ar)
            {
                if (!found_socks.Contains(element))
                {
                    found_socks.Add(element);
                    found_pairs += (int)(ar.Count(n => n == element) / 2);
                }
            }

            // Return the total number of matching pairs.
            return found_pairs;
        }

        // LeetCode
        public static int[] TwoSum(int[] nums, int target)
        {
            // errors on solution not existing

            int[] indices = { 0, 1 };

            while (indices[0] < (nums.Length - 1))
            {

                // Results test.
                if (nums[indices[0]] + nums[indices[1]] == target)
                {
                    // Return.
                    return indices;
                }

                // Bounds test and reset.
                if (indices[1] == (nums.Length - 1))
                {
                    indices[0]++;
                    indices[1] = indices[0] + 1;
                }
                // Increment second index.
                else
                {
                    indices[1]++;
                }

            }

            return indices;

        }

        // Complete the countingValleys function below.
        public static int CountingValleys(int n, string s)
        {
            List<int> elevation_change_list = new List<int>();
            int elevation = 0;
            int valleys = 0;
            for (int i = 0; i < n; i++)
            {
                // translates each char to +- 1 elevation change
                if (s[i] == 'D')
                    elevation_change_list.Add(-1);
                else
                    elevation_change_list.Add(1);

                // uses translated list to test for valley
                elevation += elevation_change_list[i];
                if (elevation == 0)
                {
                    if (i > 0 && elevation_change_list[i] == 1)
                    {
                        valleys += 1;
                    }
                }

            }

            return valleys;

        }

        // Complete the jumpingOnClouds function below.
        public static int JumpingOnClouds(int[] c)
        {
            // look two spaces ahead
            // if occupied, jump 1, else jump 2

            int jumps = 0;
            int i = 0;
            while (i < c.Length - 1)
            {

                if (i < c.Length - 2 && c[i + 2] == 1)
                    i += 1;
                else
                    i += 2;

                jumps += 1;
            }

            return jumps;
        }

        // Complete the repeatedString function below.
        public static long RepeatedString(string s, long n)
        {
            // find multiple of string length, multiply by number within string
            // remainder string size, find number within partial string
            // not clear if mod works on c# long

            int string_a_count = 0;
            foreach (char c in s)
                if (c == 'a')
                    string_a_count += 1;

            long full_strings = (n / s.Length);

            long remainder_indices = n % s.Length; // must be long (s = {a}, 10e12)

            int remainder_string_count = 0;
            for (int i = 0; i < remainder_indices; i++)
                if (s[i] == 'a')
                    remainder_string_count += 1;

            return (string_a_count * full_strings) + remainder_string_count;
        }

        // Complete the hourglassSum function below.
        public static int HourglassSum(int[][] arr)
        {
            // NOTE: works with jagged matrices

            // centered on top left of grab-pattern
            // loop i to limit
            // loop j to limit

            // constraint: -9 <= array[i][j] <= 9
            // constraint: 0 <= (i, j) <= 5

            //// variables
            // sums
            // anchor
            // largest_value

            List<int> sums = new List<int>();

            int[] anchor = new int[] { 0, 0 }; // top left location in [i, j]

            for (int i = 0; i < arr.Length - 2; i++) // vertical
            {
                anchor[0] = i;

                for (int j = 0; j < arr[i].Length - 2; j++) // horizontal
                {
                    anchor[1] = j;

                    // test boundaries
                    if (arr.Length >= (anchor[0] + 3) &&                    // vertical (leftmost)
                        arr[anchor[0] + 0].Length >= (anchor[1] + 3) &&     // top horizontal
                        arr[anchor[0] + 2].Length >= (anchor[1] + 3) &&     // bottom horizontal
                        arr[anchor[0] + 1].Length >= (anchor[1] + 2))       // middle
                    {
                        // apply sum pattern
                        sums.Add(arr[anchor[0] + 0][anchor[1] + 0] +
                                    arr[anchor[0] + 0][anchor[1] + 1] +
                                    arr[anchor[0] + 0][anchor[1] + 2] +

                                    arr[anchor[0] + 1][anchor[1] + 1] +

                                    arr[anchor[0] + 2][anchor[1] + 0] +
                                    arr[anchor[0] + 2][anchor[1] + 1] +
                                    arr[anchor[0] + 2][anchor[1] + 2]);
                    }
                }
            }

            int largest_value = -int.MaxValue;  // constraint dependent
            // find largest value in list
            foreach (int sum in sums)
                if (sum > largest_value)
                    largest_value = sum;

            // "return an integer, the maximum hourglass sum in the array"
            return largest_value;
        }

        // Complete the rotLeft function below.
        public static int[] RotLeft(int[] a, int d)
        {
            // fill new array from i=0 to i=(d - 1), with (d + 1) to length
            // continue filling new array from i=d to i=length with 0 to d

            if (d == a.Length)
                return a;

            int[] rotated_array = new int[a.Length];

            for (int i = d; i < a.Length; i++)                  // start at d, loop to length
                rotated_array[i - d] = a[i];                    // for rot array, start at 0, loop to d

            for (int i = 0; i < d; i++)                         // start at 0, loop to (d - 1)
                rotated_array[i + (a.Length - d)] = a[i];       // for rot array, resume, loop to d - 1

            return rotated_array;
        }


        /// <summary>
        /// It is New Year's Day and people are in line for the Wonderland roller coaster ride. Each person wears a sticker indicating their initial position in the queue from 1 to n.
        ///Any person can bribe the person directly in front of them to swap positions, but they still wear their original sticker.One person can bribe at most two others.
        ///Determine the minimum number of bribes that took place to get to a given queue order. Print the number of bribes, or, if anyone has bribed more than two people, print Too chaotic.
        /// </summary>
        /// <param name="q">the positions of the people after all bribes</param>
        // Complete the minimumBribes function below.
        public static void MinimumBribes(int[] q)
        {
            int count = 0;

            for (int i = q.Length - 1; i >= 0; i--)
            {
                // Chaos test!
                if (q[i] - 1 - i > 2)
                {
                    Console.WriteLine("Too chaotic");
                    return;
                }

                // Count larger values to the left.
                for (int j = i - 1; j >= 0; j--)
                {
                    // Define limited scope to increase performance.
                    // Leftward inspection constrained.
                    if (q[i] - 1 - j > 2)
                        break;

                    if (q[i] < q[j])
                        count++;
                }
            }

            Console.WriteLine(count);
            return;
        }



        // Complete the minimumSwaps function below.
        public static int MinimumSwaps(int[] arr)
        {
            int count = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                // swap until arr[i] is in correct position
                while (arr[i] != i + 1)
                {
                    int swap_value = arr[arr[i] - 1];
                    arr[arr[i] - 1] = arr[i];
                    arr[i] = swap_value;

                    count += 1;
                }
            }

            return count;
        }

        // Complete the arrayManipulation function below.
        public static long ArrayManipulation(int n, int[][] queries)
        {
            float percentage = 0f;

            long max_element_value = -long.MaxValue;

            long[][] manipulated_array = new long[queries.Length][];

            for (int i = 0; i < queries.Length; i++) // not sure how else to initialize
                manipulated_array[i] = new long[n];

            // loops over entire 2d array
            for (int i = 0; i < queries.Length; i++)
            {
                // loads queries data
                int[] manipulation = queries[i];

                // loops queries data from current row to last row (down)
                for (int j = i; j < queries.Length; j++)
                {
                    // loops over queries data, into 2d array row
                    for (int k = (manipulation[0] - 1); k < manipulation[1]; k++)
                        manipulated_array[j][k] += manipulation[2];
                }

                //if (i % 500 == 0)
                //percentage = (float)(i + 1) / (float)queries.Length * 100f;
                //Console.WriteLine("Approximate Completion: %" + (percentage.ToString("0.##")) + " (" + (i + 1) + "/" + queries.Length + ")");

            }

            foreach (long[] row in manipulated_array)
                foreach (long element in row)
                    if (element > max_element_value)
                        max_element_value = element;

            return max_element_value;
        }

        public static long ArrayManipulation2(int n, int[][] queries)
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            float percentage = 0f;

            long[][] manipulated_array = new long[queries.Length][];
            long sum = 0;
            long max_element_value = -long.MaxValue;

            for (int i = 0; i < queries.Length; i++) // not sure how else to initialize
                manipulated_array[i] = new long[n];

            for (int i = 0; i < queries.Length; i++)
            {
                for (int j = i; j < queries.Length; j++)
                {
                    manipulated_array[j][queries[i][0] - 1] += queries[i][2];
                    if (queries[i][1] < manipulated_array[j].Length)
                        manipulated_array[j][queries[i][1]] += -queries[i][2];
                }

                //percentage = (float)(i + 1) / (float)queries.Length * 100f;
                //Console.WriteLine("Approximate Completion: %" + (percentage.ToString("0.##")) + " (" + (i + 1) + "/" + queries.Length + ")");
            }

            foreach (long[] row in manipulated_array)
            {
                sum = 0;
                foreach (long element in row)
                {
                    sum += element;
                    if (sum > max_element_value)
                        max_element_value = sum;
                }
            }

            Console.WriteLine("Runtime: " + ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - milliseconds) + " milliseconds.");

            return max_element_value;
        }

        public static long ArrayManipulation3(int n, int[][] queries)
        {
            // constraints do not allow negative numbers (i.e. negative values cannot be introduced)

            // debug
            Console.CursorVisible = false;
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            float percentage = 0f;

            long[] manipulated_array = new long[n + 1];
            long sum = 0;
            long max_element_value = -long.MaxValue;

            for (int i = 0; i < queries.Length; i++)
            {
                // if negative values are introduced, test prior to introduction
                if (queries[i][2] < 0)
                {
                    sum = 0;
                    foreach (long element in manipulated_array)
                    {
                        sum += element;
                        if (sum > max_element_value)
                            max_element_value = sum;
                    }
                }

                manipulated_array[queries[i][0] - 1] += queries[i][2];
                manipulated_array[queries[i][1]] += -queries[i][2];

                // debug
                percentage = (float)(i + 1) / (float)queries.Length * 100f;
                Console.Write("\r" + "Approximate Completion: " + (percentage.ToString("0.##")) + "% (" + (i + 1) + "/" + queries.Length + ")               ");
                if (i + 1 == queries.Length)
                    Console.WriteLine("");
            }

            // if no negative values were introduced, the last row will contain the maximum value
            // (if they were, the maximum value should have been recorded above)
            sum = 0;
            foreach (long element in manipulated_array)
            {
                sum += element;
                if (sum > max_element_value)
                    max_element_value = sum;
            }

            // debug
            long time_elapsed = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - milliseconds;
            Console.WriteLine("Runtime: " + String.Format("{0:#,##0}", time_elapsed) + " milliseconds.");
            Console.CursorVisible = true;

            return max_element_value;
        }

        // Complete the arrayManipulation function below.
        public static long ArrayManipulation_Final(int n, int[][] queries)
        {
            long[] manipulated_array = new long[n + 1];
            long sum = 0;
            long maximum_value = -long.MaxValue;

            for (int i = 0; i < queries.Length; i++)
            {
                manipulated_array[queries[i][0] - 1] += queries[i][2];
                manipulated_array[queries[i][1]] += -queries[i][2];
            }

            foreach (long element in manipulated_array)
            {
                sum += element;
                if (sum > maximum_value)
                    maximum_value = sum;
            }

            return maximum_value;
        }

        // Complete the checkMagazine function below.
        public static void CheckMagazine(string[] magazine, string[] note)
        {
            var dic_data = new Dictionary<string, int>();

            // create dictionary from "magazine" input
            foreach (string magazine_value in magazine)
            {
                if (!dic_data.ContainsKey(magazine_value))
                    dic_data.Add(magazine_value, 1);
                else
                    dic_data[magazine_value] += 1;
            }

            // iterate over "note" input, looking up magazine dictionary values
            foreach (string note_string in note)
            {
                if (dic_data.ContainsKey(note_string))
                    if (dic_data[note_string] > 0)
                        dic_data[note_string] -= 1;
                    else
                    {
                        Console.WriteLine("No");
                        return;
                    }
                else
                {
                    Console.WriteLine("No");
                    return;
                }
            }

            // tiger blood
            Console.WriteLine("Yes");
            return;

        }

        // Complete the twoStrings function below.
        public static string TwoStrings(string s1, string s2)
        {
            var letter_buckets = new Dictionary<char, int>();
            for (int i = 97; i < 123; i++) // 97 - 122
                letter_buckets.Add((char)i, 0);

            foreach (char letter in s1)
                letter_buckets[letter] = 1;

            foreach (char letter in s2)
                if (letter_buckets[letter] > 0)
                    return "YES";

            return "NO";
        }

        // Complete the sherlockAndAnagrams function below.
        public static int SherlockAndAnagrams(string s)
        {
            string substring_1;
            string substring_2;

            // single pairs

            // start left pair to right pair - 1

            var letter_buckets = new Dictionary<char, int>();
            for (int i = 97; i < 123; i++) // 97 - 122
                letter_buckets.Add((char)i, 0);

            // single pairs
            // double pairs
            // n + 1 pairs


            //List<Tuple<int, int>> pairs = new List<Tuple<int, int>>();
            int count = 0;

            for (int i = 0; i < s.Length - 1; i++)
            {
                // size of pair
                for (int k = 1; k < s.Length - 1; k++)
                {

                    substring_1 = s.Substring(0, k);

                    //var new_letter_bucket = letter_buckets.ToDictionary(entry => entry.Key, entry => entry.Value);

                    for (int e = 97; e < 123; e++) // 97 - 122
                        letter_buckets[(char)e] = 0;


                    foreach (char letter in substring_1)
                        letter_buckets[letter] += 1;

                    for (int j = i + k; j < s.Length - k; j++)
                    {
                        substring_2 = s.Substring(j, k);

                        // compare strings
                        foreach (char letter in substring_2)
                        {
                            if (letter_buckets[letter] > 0)
                                letter_buckets[letter] -= 1;
                            else
                                break;

                            count += 1;
                        }

                    }

                }


            }

            return count;

        }

        public static int SherlockAndAnagrams2(string s)
        {
            int count = 0;

            // iterate over bucketing letters
            Dictionary<char, int> buckets = new Dictionary<char, int>();

            for (int i = 0; i < s.Length; i++)
            {
                if (buckets.ContainsKey(s[i]))
                    buckets[s[i]] += 1;
                else
                    buckets.Add(s[i], 1);
            }
            foreach (char letter in buckets.Keys.ToArray())
                if (buckets[letter] < 2)
                    buckets.Remove(letter);

            string sub = "aaaa";
            // find consecutive
            string[] strings = { "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa" };
            foreach (string str in strings)
            {
                count = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    for (int j = i + 1; j < str.Length; j++)
                    {
                        count++;
                    }
                }

                Console.WriteLine(str.Length + " " + count);
            }



            int subcount = 0;
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == s[i + 1])
                    subcount++;
                else
                {
                    // 2 - 1
                    // 3 - 5
                    // 4 - 10
                    for (int j = 0; j < subcount; j++)
                    {

                    }


                    subcount = 0;
                }
            }

            // find pairs

            return count;
        }

        // Complete the countTriplets function below.
        public static long CountTriplets(List<long> arr, long r)
        {
            // 1 always works

            // set anchor value
            // find all first multiples
            // set new value
            // find all next multiples

            long count = 0;
            long anchor_value = 0;

            List<int> first_multiples = new List<int>();
            //List<int> second_multiples = new List<int>();

            for (int i = 0; i < arr.Count; i++)
            {
                // test anchor value? modulo == 0 || 1
                anchor_value = arr[i];
                first_multiples.Clear();

                // find first geometric progression (multiple)
                for (int j = i + 1; j < arr.Count; j++)
                {
                    if (arr[j] == anchor_value * r)
                    {
                        first_multiples.Add(j);
                    }
                }

                // from each first multiple found, find all second multiples
                foreach (int index in first_multiples)
                {
                    for (int x = index + 1; x < arr.Count; x++)
                    {
                        if (arr[x] == anchor_value * r * r)
                        {
                            count += 1;
                            Console.WriteLine(count);
                        }
                    }
                }
            }

            return count;
        }
        public static long CountTriplets2(List<long> arr, long r)
        {
            // 1 always works

            // set anchor value
            // find all first multiples
            // set new value
            // find all next multiples

            long count = 0;
            long anchor_value = 0;

            for (int i = 0; i < arr.Count; i++)
            {
                // test anchor value? modulo == 0 || 1
                anchor_value = arr[i]; // unnecessary
                //first_multiples.Clear();

                // find first geometric progression (multiple)
                for (int j = i + 1; j < arr.Count; j++)
                {
                    if (arr[j] == anchor_value * r)
                    {
                        //first_multiples.Add(j);
                        // find second geometric progression (multiple)
                        for (int k = j + 1; k < arr.Count; k++)
                        {
                            if (arr[k] == anchor_value * r * r)
                            {
                                count += 1;
                                Console.WriteLine(count);
                            }
                        }
                    }
                }

            }

            return count;
        }
        public static long CountTriplets3(List<long> arr, long r)
        {
            var r_count_dictionary = new Dictionary<long, long>();

            arr.Sort(); // decrease computational complexity (no stated constraint that values are increasing).
                        // to optimize further, while sorting, remove all non-multiples of r.

            // find *r
            // find *r^2
            // multiple *r by *r^2 (quantity of each)

            long count = 0;
            long floor_value = 0;
            long ceiling_value = long.MaxValue;

            // buckets are multiples of r from lowest
            // can move into main loop

            // find minimum r multiple
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i] % r == 0 || r % arr[i] == 0)
                {
                    floor_value = arr[i];
                    break;
                }
            }

            // find maximum r multiple
            for (int i = arr.Count - 1; i >= 0; i--)
            {
                if (arr[i] % r == 0)
                {
                    ceiling_value = arr[i];
                    break;
                }
            }

            // create buckets
            if (floor_value != ceiling_value)
            {
                for (long i = floor_value; i <= ceiling_value; i = i + r)
                {
                    r_count_dictionary.Add(i, 0);
                    //r_count_dictionary.TryAdd(i, 0);

                    //if (r == 1)
                    //    break;
                }
            }
            else
            {
                r_count_dictionary.Add(floor_value, 0);
            }



            // bucket-ize!
            for (int i = 0; i < arr.Count; i++)
            {
                if (arr[i] % r == 0) // WARNING: may not work with cases where List elements are less than r
                    r_count_dictionary[arr[i]] += 1;
            }

            // find 3-pairs of buckets, multiply their values
            int special_case = 0;
            long value1, value2, value3 = 0;
            for (long i = floor_value; i <= ceiling_value; i = (i * r)) // can further optimize
            {
                if (r == 1)
                    special_case = 1;

                if (r_count_dictionary.TryGetValue(i, out value1) &&
                    r_count_dictionary.TryGetValue(i * r, out value2) &&
                    r_count_dictionary.TryGetValue(i * r * r, out value3))
                {
                    count += value1 * (value2 - special_case) * (value3 - special_case * 2);
                }

                //r_count_dictionary.TryGetValue(i            ,out value1);
                //r_count_dictionary.TryGetValue(i * r        ,out value2);
                //r_count_dictionary.TryGetValue(i * r * r    ,out value3);

                //count += r_count_dictionary[i] * (r_count_dictionary[i * r] - special_case) * (r_count_dictionary[i * r * r] - special_case*2);

                if (r == 1)
                {
                    // n choose k
                    long result = 1;
                    int n = arr.Count;
                    int k = 3;
                    for (int x = 1; x <= k; x++)
                    {
                        result *= n - (k - x);
                        result /= x;
                    }
                    count = result;
                    break;
                }

            }

            return count;
        }

        public static long CountTriplets4(List<long> arr, long r)
        {
            var r_count_dictionary = new Dictionary<long, long>();

            arr.Sort(); // decrease computational complexity (no stated constraint that values are increasing).
                        // to optimize further, while sorting, remove all non-multiples of r.

            // iterate through unsorted array
            // check to see if bucket exists, if so, bucket
            // check to see if power of r, if so, create bucket and bucket it!

            // find *r
            // find *r^2
            // multiple *r by *r^2 (quantity of each)

            long count = 0;
            long floor_value = 0;
            long ceiling_value = long.MaxValue;

            // buckets are multiples of r from lowest
            // can move into main loop

            // find minimum r nth
            for (int i = 0; i < arr.Count; i++)
            {
                //Console.WriteLine("this finds a multiple, not a nth power");
                if (arr[i] == 1 || Math.Log(arr[i], r) % 1 == 0) //(arr[i] % r == 0 || r % arr[i] == 0) // || arr[i] == 1
                {
                    floor_value = arr[i];
                    break;
                }
            }

            // find maximum r nth
            for (int i = arr.Count - 1; i >= 0; i--)
            {
                //Console.WriteLine(Math.Log(arr[i], r));
                //Console.WriteLine(Math.Log(arr[i], r) % 1);
                //Console.WriteLine((long)Math.Truncate((double)Math.Log(arr[i], r)));

                var possible_power = (long)Math.Truncate((double)Math.Log(arr[i], r));
                //Console.WriteLine(Math.Pow(arr[i], possible_power));
                if (Math.Pow(r, possible_power) == arr[i])
                {
                    ceiling_value = arr[i];
                    break;
                }
                //if (Math.Log(arr[i], r) % 1 <= 0.00000000000001) //(arr[i] % r == 0)
                //{
                //    ceiling_value = arr[i];
                //    break;
                //}
            }

            // create and add buckets
            if (floor_value != ceiling_value)
            {
                foreach (long element in arr)
                {
                    // optimization: test for same number as last

                    // log base r of element % r == 0
                    double temp = Math.Log(element, r);
                    temp = Math.Round(temp);
                    // only include a^n
                    // yea so how the fuck do you solve the log issue
                    //for (long i = floor_value; i <= ceiling_value; i = i * r) // optimize this by using log to find close answer
                    //{
                    //    if (element == i);

                    //}

                    Math.Round(Math.Log(element) / Math.Log(r));

                    //Console.WriteLine("uh this fucking check not working"); // (temp % 1 <= 0.0000001 && element % r == 0)
                    var possible_power = (long)Math.Truncate((double)Math.Log(element, r));
                    if (Math.Pow(r, possible_power) == element || element == 1)//((Math.Log(element, r) % 1 == 0 && element % r == 0) || element == 1) // too imprecise: Math.Log(element) / Math.Log(r)
                    {
                        if (r_count_dictionary.ContainsKey(element))
                            r_count_dictionary[element] += 1;
                        else
                            r_count_dictionary.Add(element, 1);
                    }
                }
            }
            else
            {
                r_count_dictionary.Add(floor_value, 0);
                r_count_dictionary[floor_value] += arr.Count;
            }


            if (r == 1)
            {
                // n choose k
                long result = 1;
                int n = arr.Count;
                int k = 3;
                for (int x = 1; x <= k; x++)
                {
                    result *= n - (k - x);
                    result /= x;
                }
                count = result;
            }
            else
            {
                long value1, value2, value3 = 0;
                for (long i = floor_value; i <= ceiling_value; i = (i * r))
                {
                    if (r_count_dictionary.TryGetValue(i, out value1) &&
                        r_count_dictionary.TryGetValue(i * r, out value2) &&
                        r_count_dictionary.TryGetValue(i * r * r, out value3))
                    {
                        count += value1 * value2 * value3;
                    }

                }
            }

            return count;
        }

        public static long CountTriplets5(List<long> arr, long r)
        {
            // iterate through unsorted array
            // check to see if bucket exists, if so, bucket
            // check to see if power of r, if so, create bucket and bucket it!
            // evaluate; count += ((dictionary key) * (key * r) * (key * r * r))

            // yea so apparently order matters... that's bullshit. oder was not specified or stated as a constraint in problem

            long count = 0;
            var r_count_dictionary = new Dictionary<long, long>();

            long floor_value = long.MaxValue;
            long ceiling_value = -long.MaxValue;

            // debugging
            arr.Sort();

            foreach (long value in arr)
            {

                if (r_count_dictionary.ContainsKey(value))
                {
                    r_count_dictionary[value] += 1;
                }
                else
                {
                    // hacky way to get logs to work
                    double possible_power = 0;
                    if (value != 1 && r != 1)
                    {
                        var temp1 = Math.Log(value, r);
                        //var temp2 = Math.Truncate(temp1);
                        var temp2 = Math.Round(temp1);
                        possible_power = temp2;
                    }

                    //if (value == 1000)
                    //{
                    //    Console.WriteLine("foo!");
                    //}

                    //long possible_power = (long)Math.Truncate((double)Math.Log(value, r));
                    if (r == 1 || Math.Pow(r, possible_power) == value)
                    {
                        r_count_dictionary.Add(value, 1);

                        if (value > ceiling_value)
                            ceiling_value = value;
                        if (value < floor_value)
                            floor_value = value;
                    }
                }
            }

            //Dictionary<long, long>.KeyCollection power_keys = r_count_dictionary.Keys;
            long[] power_keys = r_count_dictionary.Keys.ToArray<long>();

            if (r == 1)
            {
                foreach (long key in power_keys)
                {
                    // n choose k
                    long result = 1;
                    long n = r_count_dictionary[key];
                    int k = 3;
                    for (int x = 1; x <= k; x++)
                    {
                        result *= n - (k - x);
                        result /= x;
                    }
                    count += result;
                }
            }
            else
            {
                long n_1, n_2, n_3;
                foreach (long power_key in power_keys)
                {
                    n_1 = 0; n_2 = 0; n_3 = 0;
                    if (r_count_dictionary.TryGetValue(power_key * 1 * 1, out n_1) &&
                        r_count_dictionary.TryGetValue(power_key * r * 1, out n_2) &&
                        r_count_dictionary.TryGetValue(power_key * r * r, out n_3))
                    {
                        count += n_1 * n_2 * n_3;
                    }
                }
            }

            //foreach (long key in power_keys)
            //    Console.WriteLine(key);

            //Console.WriteLine("2325652489 answer");   // 6
            //Console.WriteLine("1339347780085 answer");  // 10

            return count;



        }

        public static long CountTriplets6(List<long> arr, long r)
        {
            // layer method
            // if arr value is r raised to the nth
            // iterate across existing buckets
            //  increment full buckets last element if matching
            //  non-full buckets can take 2nd or 3rd
            //  if no 1st bucket, create one

            // solve: calculate the sum of triplet products


            long count = 0;
            long floor_value = long.MaxValue;
            long ceiling_value = -long.MaxValue;

            long[,] layer = new long[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; // first 3set indicates state, only first value is used
            List<long[,]> layered_cake = new List<long[,]>();

            foreach (long value in arr)
            {
                // hack  to get logs to work
                double possible_power = 0;
                if (value != 1 && r != 1)
                {
                    var temp1 = Math.Log(value, r);
                    //var temp2 = Math.Truncate(temp1);
                    var temp2 = Math.Round(temp1);
                    possible_power = temp2;
                }

                if (Math.Pow(r, possible_power) == value) // r == 1 || 
                {
                    if (layered_cake.Count > 0)
                    {
                        bool create_new = true;
                        foreach (long[,] element in layered_cake)
                        {
                            // legend: 0 empty, 1, 2, 3 full

                            // check existing buckets
                            for (int i = 0; i < 3; i++)
                            {
                                if (element[0, 0] == (i + 1) && element[1, i] == value)
                                {
                                    element[2, i] += 1;

                                    if (i == 0)
                                        create_new = false;
                                }
                            }

                            if (element[0, 0] == 2 && element[1, 1] * r == value)
                            {
                                element[0, 0] = 3;
                                element[1, 2] = value;
                                element[2, 2] = 1;
                            }
                            if (element[0, 0] == 1 && element[1, 0] * r == value)
                            {
                                element[0, 0] = 2;
                                element[1, 1] = value;
                                element[2, 1] = 1;
                            }
                        }

                        if (create_new)
                        {
                            layer = new long[3, 3] { { 1, 0, 0 }, { value, 0, 0 }, { 1, 0, 0 } };
                            layered_cake.Add(layer);
                        }
                    }
                    else
                        layered_cake.Add(new long[3, 3] { { 1, 0, 0 }, { value, 0, 0 }, { 1, 0, 0 } });

                    if (value > ceiling_value)
                        ceiling_value = value;
                    if (value < floor_value)
                        floor_value = value;
                }
            }

            foreach (long[,] three_pair in layered_cake)
            {
                count += three_pair[2, 0] * three_pair[2, 1] * three_pair[2, 2];
            }

            return count;
        }

        public static long CountTriplets7(List<long> arr, long r)
        {
            // bucket until next nth power is found
            // create new bucket

            List<long[]> foo_bar = new List<long[]>();


            long count = 0;
            long floor_value = long.MaxValue;
            long ceiling_value = -long.MaxValue;

            long[,] layer = new long[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } }; // first 3set indicates state, only first value is used
            List<long[,]> layered_cake = new List<long[,]>();
            List<long> gist_list = new List<long>();

            foreach (long value in arr)
            {
                // hack  to get logs to work
                double possible_power = 0;
                if (value != 1 && r != 1)
                {
                    var temp1 = Math.Log(value, r);
                    //var temp2 = Math.Truncate(temp1);
                    var temp2 = Math.Round(temp1);
                    possible_power = temp2;
                }

                if (Math.Pow(r, possible_power) == value) // r == 1 || 
                {
                    gist_list.Add(value);

                    if (value > ceiling_value)
                        ceiling_value = value;
                    if (value < floor_value)
                        floor_value = value;
                }
            }

            foreach (long element in gist_list)
                Console.Write(element + ", ");

            return count;
        }

        public static long CountTriplets8(List<long> arr, long r) // came back later
        {
            long count = 0;
            HashSet<long> buckets = new HashSet<long>();
            Dictionary<long, long> buckets_1 = new Dictionary<long, long>();
            List<long> arr_clean = new List<long>();

            double left_power_value, middle_power, right_power_value;
            int left_count, right_count;

            long lower_bound = long.MaxValue;
            long upper_bound = 0;

            // create buckets of different values
            if (r == 1)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    if (buckets_1.ContainsKey(arr.ElementAt(i)))
                    {
                        buckets_1[arr.ElementAt(i)] += 1;
                    }
                    else
                    {
                        buckets_1.Add(arr.ElementAt(i), 1);
                    }
                }
            }
            // create a hashset of powers
            //if (r == 1)
            //    buckets.Add(1);
            else
                for (long i = 1; i <= int.MaxValue; i *= r)
                    buckets.Add(i);

            // create array with non-powers removed
            for (int i = 0; i < arr.Count; i++)
            {
                var temp0 = arr.ElementAt(i);
                if (buckets.Contains(temp0))
                {
                    arr_clean.Add(temp0);

                    if (temp0 < lower_bound)
                        lower_bound = temp0;

                    if (temp0 > upper_bound)
                        upper_bound = temp0;
                }
            }

            // n choose k (SO; binomial coefficient failed)
            if (r == 1)
            {
                long subtotal = 0;
                foreach (long value in buckets_1.Keys.ToArray())
                {
                    subtotal += choose(buckets_1[value], 3);
                }

                return subtotal; //choose(arr_clean.Count, 3);
            }


            // increment over array (middle out method)
            for (int i = 1; i < arr_clean.Count - 1; i++)
            {
                left_count = right_count = 0;

                middle_power = Math.Log(arr_clean[i]) / Math.Log(r);
                left_power_value = Math.Pow(r, middle_power - 1);
                right_power_value = Math.Pow(r, middle_power + 1);

                if (right_power_value > upper_bound)
                    continue;
                if (left_power_value < lower_bound)
                    continue;

                // count decrement power left
                for (int j = 0; j < i; j++)
                    if (arr_clean[j] == left_power_value)
                        left_count++;

                // count increment power right
                if (left_count > 0)
                    for (int j = i + 1; j < arr_clean.Count; j++)
                        if (arr_clean[j] == right_power_value)
                            right_count++;

                // add to count (multiply)
                count += left_count * right_count;
            }

            return count;

            long choose(long n, long k)
            {
                if (k == 0)
                    return 1;
                return (n * choose(n - 1, k - 1)) / k;
            }

            // n choose k (binomial coefficient)
            //return factorial(arr.Count) / factorial(3) * factorial(arr.Count - 3);

            long factorial(long n)
            {
                if (n <= 1)
                    return 1;
                else
                    return n * factorial(n - 1);
            }
        }

        public static long CountTriplets9(List<long> arr, long r)
        {
            // read discussion

            long count = 0;
            HashSet<long> buckets = new HashSet<long>();
            List<long> arr_clean = new List<long>();

            double left_power_value, middle_power, right_power_value;
            int left_count, right_count;

            long lower_bound = long.MaxValue;
            long upper_bound = 0;

            // create a hashset of powers
            if (r == 1)
                buckets.Add(1);
            else
                for (long i = 1; i <= int.MaxValue; i *= r)
                    buckets.Add(i);

            // create array with non-powers removed
            for (int i = 0; i < arr.Count; i++)
            {
                var temp0 = arr.ElementAt(i);
                if (buckets.Contains(temp0))
                {
                    arr_clean.Add(temp0);

                    if (temp0 < lower_bound)
                        lower_bound = temp0;

                    if (temp0 > upper_bound)
                        upper_bound = temp0;
                }
            }

            // n choose k (SO; binomial coefficient failed)
            if (r == 1)
                return choose(arr_clean.Count, 3);

            // increment over array
            for (int i = arr_clean.Count; i >= 0; i++)
            {

            }

            return count;

            long choose(long n, long k)
            {
                if (k == 0)
                    return 1;
                return (n * choose(n - 1, k - 1)) / k;
            }
        }

        // Complete the freqQuery function below.
        public static List<int> FreqQuery(List<List<int>> queries)
        {
            List<int> output = new List<int>();
            Dictionary<int, int> buckets = new Dictionary<int, int>();

            // remove unnecessary computation
            for (int i = queries.Count - 1; i >= 0; i--)
            {
                if (queries[i][0] != 3)
                    queries.RemoveAt(i);
                else
                    break;
            }

            // iterate over operations list (queries)
            //foreach (List<int> operation in queries)
            for (int i = 0; i < queries.Count; i++)
            {
                var operation = queries[i];

                // add or increment
                if (operation.ElementAt(0) == 1)
                {
                    if (buckets.ContainsKey(operation.ElementAt(1)))
                        buckets[operation.ElementAt(1)] += 1;
                    else
                        buckets.Add(operation.ElementAt(1), 1);

                    // does not contain a definition for `TryAdd'
                    //if (!buckets.TryAdd(operation.ElementAt(1), 1))
                    //    buckets[operation.ElementAt(1)] += 1;
                }
                // remove or decrement
                else if (operation.ElementAt(0) == 2)
                {
                    if (buckets.ContainsKey(operation.ElementAt(1)))
                        if (buckets[operation.ElementAt(1)] > 0)
                            buckets[operation.ElementAt(1)] -= 1;
                }
                // evaluate (test for values equal to operation.ElementAt(1))
                else if (operation.ElementAt(0) == 3)
                {
                    // optimization
                    if (operation.ElementAt(1) > queries.Count)
                    {
                        output.Add(0);
                        continue;
                    }

                    bool flag = true;
                    foreach (KeyValuePair<int, int> bucket in buckets)
                        if (bucket.Value == operation.ElementAt(1))
                        {
                            output.Add(1);
                            flag = false;
                            break;
                        }

                    if (flag)
                        output.Add(0);

                }
            }

            Console.WriteLine(output.Count);
            return output;
        }

        /// Sorting ///

        // Complete the countSwaps function below.
        public static void CountSwaps(int[] a)
        {
            int swaps = 0;

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a.Length - 1; j++)
                {
                    // Swap adjacent elements if they are in decreasing order
                    if (a[j] > a[j + 1])
                    {
                        var temp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = temp;

                        swaps += 1;
                    }
                }
            }

            Console.WriteLine("Array is sorted in " + swaps + " swaps.");
            Console.WriteLine("First Element: " + a[0]);
            Console.WriteLine("Last Element: " + a[a.Length - 1]); // a[^1] gives error
        }

        // Complete the maximumToys function below.
        public static int MaximumToys(int[] prices, int k)
        {
            int toy_counter = 0;
            int remaining_money = k;

            Array.Sort(prices);

            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] <= remaining_money)
                {
                    remaining_money -= prices[i];
                    toy_counter += 1;
                }
                else
                    break;
            }

            return toy_counter;
        }


        // Complete the activityNotifications function below.
        public static int ActivityNotifications(int[] expenditure, int d)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            int count = 0;
            double temp0 = Convert.ToDouble(d);
            int sort_me_odd_mid = (int)Math.Floor(temp0 / 2);
            int sort_me_lower = (int)(temp0 / 2) - 1;
            int sort_me_upper = (int)(temp0 / 2);
            List<int> sorted_trailing_subarray = new List<int>();

            for (int i = 0; i < d; i++)
                sorted_trailing_subarray.Add(expenditure[i]);

            sorted_trailing_subarray.Sort();

            for (int i = 0; i < expenditure.Length - d; i++)
            {
                // modify and sort trailing array for median
                if (i > 0)
                {
                    // remove sorted element
                    sorted_trailing_subarray.Remove(expenditure[i - 1]);

                    var temp_expend = expenditure[i + d];

                    // add unsorted element
                    sorted_trailing_subarray.Add(expenditure[i + d]);

                    // sort
                    sorted_trailing_subarray.Sort();
                }

                // odd d
                if (d % 2 == 1)
                {
                    int median_expenditure = sorted_trailing_subarray[sort_me_odd_mid];

                    if (expenditure[i + d] >= median_expenditure * 2)
                        count++;
                }
                // even d
                else // (d % 2 == 0)
                {
                    decimal median_expenditure = (sorted_trailing_subarray[sort_me_lower] + sorted_trailing_subarray[sort_me_upper]) / (decimal)2;

                    if (expenditure[i + d] >= median_expenditure * 2)
                        count++;
                }
            }

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            return count;
        }

        public static int ActivityNotifications2(int[] expenditure, int d)
        {
            //var watch = new System.Diagnostics.Stopwatch();
            //watch.Start();

            int temp_count;
            int count = 0;

            double temp0 = Convert.ToDouble(d);
            int odd_median_count = (int)Math.Floor(temp0 / 2) + 1;
            int median_lower_count = (int)(temp0 / 2);
            int median_upper_count = (int)(temp0 / 2) + 1;

            decimal median_expenditure = 0;
            int[] count_sort_array = new int[201];

            // initialize window
            for (int i = 0; i < d; i++)
                count_sort_array[expenditure[i]] += 1;

            for (int i = 0; i < expenditure.Length - d; i++)
            {
                temp_count = 0;

                // update window
                if (i > 0)
                {
                    // remove element
                    count_sort_array[expenditure[i - 1]] -= 1;
                    // add element
                    count_sort_array[expenditure[i + d - 1]] += 1;
                }

                // find odd median
                if (d % 2 == 1)
                {
                    for (int j = 0; j < count_sort_array.Length; j++)
                    {
                        if (count_sort_array[j] > 0)
                            temp_count += count_sort_array[j];

                        if (temp_count >= odd_median_count)
                        {
                            median_expenditure = j;
                            break;
                        }

                    }
                }
                // find even median
                else if (d % 2 == 0)
                {
                    var temp_lower = -1; // -1 is used as a flag
                    var temp_upper = -1;

                    for (int j = 0; j < count_sort_array.Length; j++)
                    {
                        if (count_sort_array[j] > 0)
                        {
                            temp_count += count_sort_array[j];

                            if (temp_count >= median_lower_count && temp_lower == -1)
                                temp_lower = j;

                            if (temp_count >= median_upper_count && temp_upper == -1)
                            {
                                temp_upper = j;
                                median_expenditure = (temp_lower + temp_upper) / (decimal)2;
                                break;
                            }
                        }
                    }
                }

                if (expenditure[i + d] >= median_expenditure * (decimal)2)
                    count++;
            }

            //watch.Stop();
            //Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

            return count;
        }

        /// String Manipulation ///

        // Complete the makeAnagram function below.
        public static int MakeAnagram(string a, string b)
        {
            // iterate over string a
            // compare to b, remove MATCHING elements
            // count total remaining in a and b

            //string removed_a = "";
            //string removed_b = "";

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {

                    //Console.WriteLine("Compare " + i + " " + j + " :" + a[i] + " " + b[j]);

                    if (a[i] == b[j])
                    {
                        //removed_a += a[i];
                        //removed_b += b[j];

                        a = a.Remove(i, 1);
                        b = b.Remove(j, 1);

                        i += -1;
                        break;
                    }
                }
            }

            //Console.WriteLine("removed a: " + removed_a);
            //Console.WriteLine("removed b: " + removed_b);

            //Console.WriteLine("a: " + a);
            //Console.WriteLine("b: " + b);

            return a.Length + b.Length;

        }

        // Complete the alternatingCharacters function below.
        public static int AlternatingCharacters(string s)
        {
            int count = 0;

            for (int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == s[i + 1])
                    count += 1;
            }

            return count;
        }

        // Complete the isValid function below.
        // Used dictionary/map rather than string manipulation
        public static string IsValid(string s)
        {
            int[] letter_frequency_buckets = new int[26];

            Dictionary<int, int> frequency_frequency_buckets = new Dictionary<int, int>();
            List<int> bucket_keys = new List<int>();

            foreach (char letter in s)
                letter_frequency_buckets[letter - 97] += 1;

            foreach (int letter_count in letter_frequency_buckets)
                if (letter_count != 0 && frequency_frequency_buckets.ContainsKey(letter_count))
                    frequency_frequency_buckets[letter_count] += 1;
                else if (letter_count != 0)
                    frequency_frequency_buckets.Add(letter_count, 1);

            bucket_keys = frequency_frequency_buckets.Keys.ToList<int>();

            if (bucket_keys.Count > 2)
                return "NO";

            if (bucket_keys.Count > 1 && Math.Abs(bucket_keys[0] - bucket_keys[1]) > 1)
                if (frequency_frequency_buckets.ContainsKey(1))
                {
                    if (frequency_frequency_buckets[1] > 1)
                        return "NO";
                }
                else
                    return "NO";

            if (bucket_keys.Count == 2)
                if (frequency_frequency_buckets[bucket_keys[0]] > 1 && frequency_frequency_buckets[bucket_keys[1]] > 1)
                    return "NO";

            return "YES";
        }

        // Complete the substrCount function below.
        public static long SubstrCount(int n, string s)
        {
            int count = 0;

            for (int i = 0; i < s.Length; i++)
            {
                // count rule 1
                for (int j = i; j < s.Length; j++)
                {
                    if (s[i] == s[j])
                        count += 1;
                    else
                        break;
                }

                // count rule 2
                if (i > 0 &&
                    i < s.Length - 1 &&
                    s[i] != s[i + 1] &&
                    s[i - 1] == s[i + 1])
                {

                    // expand out from center letter
                    int k = 1;
                    while (i - k >= 0 && i + k < s.Length)
                    {
                        if (s[i - k] == s[i + k])
                        {
                            if (k > 1 && s[i + k - 1] != s[i + k])
                                break;
                            else
                                count += 1;
                        }
                        else
                            break;

                        k++;
                    }
                }

            }

            return count;

            // previous + (index + 1)
            static int Count_Same_String_Recursive(int Length)
            {
                if (Length < 2)
                    return Length;
                else
                    return Length + Count_Same_String_Recursive(Length - 1);

            }

            static int Count_Same_String_Iterative(int Length)
            {
                int count = 0;

                for (int i = 0; i < Length; i++)
                    for (int j = i; j < Length; j++)
                        count += 1;

                return count;
            }

            // 0 1 3 6 10 15 21 28
            foreach (string letters in new string[] { "", "a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa" })
            {
                Console.WriteLine(Count_Same_String_Recursive(letters.Length));
                Console.WriteLine(Count_Same_String_Iterative(letters.Length));
            }

        }

        // Complete the commonChild function below.
        public static int CommonChild(string s1, string s2)
        {

            // for each i to end
            // find first matching, delete all previous
            // iterate over finding matching
            // next letter, find first, delete beween i_current to i_found

            int count = 0;
            string s1_copy, s2_copy;

            for (int k = 0; k < s1.Length; k++)
            {
                s1_copy = s1;
                s2_copy = s2;

                if (k == s1.Length)
                    break;

                s1_copy.Remove(0, k);

                //for (int i = 0; i < s1.Length; i++)
                int i = 0;
                do
                {
                    for (int j = 0; j < s2_copy.Length; j++)
                    {
                        if (s1_copy[i] == s2_copy[j])
                        {
                            s2_copy = s2_copy.Remove(i, j);
                            i++;
                            break;
                        }
                        else if (j == s2_copy.Length - 1)
                        {
                            s1_copy = s1_copy.Remove(i, 1);
                            j = i - 1;

                            if (i >= s1_copy.Length)
                                break;
                        }

                    }

                    // if every char in s1 has been check, but chars in s2 may remain, swap and repeat
                    if (i == s1_copy.Length && s1_copy.Length < s2_copy.Length)
                    {
                        var temp = s1_copy;
                        s1_copy = s2_copy;
                        s2_copy = temp;

                        i = 0;
                    }

                } while (i < s1_copy.Length);

                if (s1_copy.Length > count)
                    count = s1_copy.Length;
            }

            Console.WriteLine(s2);
            Console.WriteLine(s1);


            return s1.Length;
        }

        public static int CommonChild2(string s1, string s2)
        {
            int count = 0;
            string s1_copy, s2_copy;

            s1_copy = s1;
            s2_copy = s2;

            for (int i = 0; i < s1_copy.Length; i++)
            {
                s1_copy = s1.Remove(0, i);

                for (int j = 0; j < s1_copy.Length; j++)
                {
                    while (s2_copy.Length > 0 && j < s2_copy.Length)
                    {
                        if (s1_copy[j] == s2_copy[j])
                        {
                            break;
                        }

                        s2_copy = s2_copy.Remove(j, 1);
                    }

                    if (s2_copy.Length == j)
                    {
                        if (s2_copy.Length > count)
                            count = s2_copy.Length;

                        break;
                    }
                }

                s1_copy = s1;
                s2_copy = s2;
            }

            return 0;
        }

        public static int CommonChild3(string s1, string s2)
        {
            int count = 0;
            string letters_found;
            int s2_lower;

            for (int i = 0; i < s1.Length; i++)
            {
                letters_found = "";
                s2_lower = 0;

                for (int j = i; j < s1.Length; j++)
                {

                    for (int k = s2_lower; k < s2.Length; k++)
                    {
                        var temp1 = s1[j];
                        var temp2 = s2[k];

                        if (s1[j] == s2[k])
                        {
                            s2_lower = k + 1;
                            letters_found += s1[j];
                            break;
                        }
                    }

                    if (s2_lower == s2.Length)
                        break;
                }

                if (letters_found.Length > count)
                    count = letters_found.Length;

                Console.WriteLine(letters_found);
            }

            return count;
        }

        public static int CommonChild4(string s1, string s2)
        {

            int count = 0;
            List<string[]> node_list = new List<string[]>();
            string[] next_node;

            node_list.Add(new string[3] { s1, s2, "" });

            while (node_list.Count > 0)
            {
                next_node = node_list[0];
                node_list.RemoveAt(0);

                var temp1 = next_node[0].Length;
                var temp2 = next_node[1].Length;
                var temp3 = next_node[2].Length;

                if (temp1 + temp3 > count && temp2 + temp3 > count)
                    SearchNodes(next_node);

                Console.WriteLine(node_list.Count + "\t\t" + count);
            }

            void SearchNodes(string[] node)
            {
                for (int i = 0; i < node[0].Length; i++)
                {
                    for (int j = 0; j < node[1].Length; j++)
                    {
                        if (node[0][i] == node[1][j])
                        {
                            var temp1 = node[0].Remove(0, i + 1);
                            var temp2 = node[1].Remove(0, j + 1);
                            var temp3 = node[2] + node[0][i];

                            if (temp3.Length > count)
                                count = temp3.Length;

                            if (temp1.Length > 0 &&
                                temp2.Length > 0 &&
                                Math.Min(temp1.Length, temp2.Length) + temp3.Length > count)
                                node_list.Add(new string[3] { temp1, temp2, temp3 });
                        }
                    }
                }
            }

            return count;

        }

        // greedy algorithms

        // Complete the minimumAbsoluteDifference function below.
        public static int MinimumAbsoluteDifference(int[] arr)
        {
            Array.Sort(arr);
            long minimum_abs_diff = long.MaxValue;

            for (int i = 0; i < arr.Length - 1; i++)
            {
                long temp = Convert.ToInt64(Math.Abs(arr[i] - arr[i + 1]));
                if (minimum_abs_diff > temp)
                    minimum_abs_diff = temp;
            }

            return Convert.ToInt32(minimum_abs_diff);
        }

        // Complete the luckBalance function below.
        public static int LuckBalance(int k, int[][] contests)
        {
            // maximize luck
            // k is number of losses
            // 1/0 does/not count

            int luck = 0;
            List<int> sorted_choices = new List<int>();

            for (int i = 0; i < contests.Length; i++)
            {
                // free luck
                if (contests[i][1] == 0)
                    luck += contests[i][0];
                else
                {
                    // add first encountered until limit reached
                    if (k > 0)
                    {
                        luck += contests[i][0];
                        sorted_choices.Add(contests[i][0]);
                        sorted_choices.Sort();
                        k--;
                    }
                    // look at sorted list to swap
                    else
                    {
                        if (sorted_choices.Count > 0 &&
                            contests[i][0] > sorted_choices[0])
                        {
                            luck -= (sorted_choices[0] * 2);
                            luck += contests[i][0];

                            sorted_choices.RemoveAt(0);
                            sorted_choices.Insert(0, contests[i][0]);
                            sorted_choices.Sort();
                        }
                        else
                            luck -= contests[i][0];
                    }
                }
            }

            return luck;
        }

        // Complete the getMinimumCost function below.
        public static int GetMinimumCost(int k, int[] c)
        {
            // sort
            // take largest k  {do corner cases}
            // write rest of pseudo code

            Array.Sort(c);

            int remaining_purchases = c.Length % k;

            int total_cost = 0;
            int flower_purchases = 0;

            for (int i = c.Length - (k - 1 + 1); i >= 0; i -= k)
            {
                for (int j = 0; j < k; j++)
                {
                    total_cost += c[i + j] * (1 + flower_purchases);
                }

                flower_purchases++;
            }

            for (int i = 0; i < remaining_purchases; i++)
            {
                total_cost += c[i] * (1 + flower_purchases);
            }

            return total_cost;
        }

        // Complete the maxMin function below.
        public static int MaxMin(int k, int[] arr)
        {
            Array.Sort(arr);

            int minimun_difference = int.MaxValue;

            for (int i = 0; i < arr.Length - (k - 1); i++)
            {
                var temp0 = arr[i + k - 1] - arr[i];
                if (temp0 < minimun_difference)
                    minimun_difference = temp0;
            }

            return minimun_difference;
        }

        // Complete the reverseShuffleMerge function below.             // 'advanced'
        public static string ReverseShuffleMerge(string s)
        {

            string split = "";

            int[] dictionary = new int[26];

            foreach (char letter in s)
                dictionary[letter - 'a'] += 1;

            foreach (char letter in s)                      // only works for 2
                if (dictionary[letter - 'a'] % 2 == 0)
                {
                    split += letter;
                    dictionary[letter - 'a'] -= 1;
                }

            split.Reverse();
            var s_copy = s;

            // subtract reversed split from original string
            for (int i = 0; i < s.Length / 2; i++)
                foreach (char letter in s)
                { }
            //if (s_copy[letter] )



            return "";
        }

        /// Search ///

        // Complete the whatFlavors function below.
        public static void WhatFlavors(int[] cost, int money)
        {

            int first_flavor_cost = money;
            int second_flavor_cost = 0;
            int index_of_first_flavor = -1;
            int index_of_second_flavor = -1;
            int[] unsorted_costs = cost;

            Array.Sort(cost);

            do
            {
                first_flavor_cost--;
                second_flavor_cost++;

                index_of_first_flavor = BinarySearch(first_flavor_cost, cost, 0, cost.Length);
                if (index_of_first_flavor != -1)
                {
                    index_of_second_flavor = BinarySearch(first_flavor_cost, cost, 0, cost.Length);
                    if (index_of_second_flavor != -1)
                    {
                        if (cost[index_of_first_flavor] > cost[index_of_second_flavor])
                            Console.WriteLine(index_of_first_flavor + " " + index_of_second_flavor);
                        else
                            Console.WriteLine(index_of_second_flavor + " " + index_of_first_flavor);
                    }
                }
            } while (first_flavor_cost >= second_flavor_cost); // always unique solution


            // binary search (recursive) of ascending integer array
            int BinarySearch(int find_this, int[] input_array, int left_boundary, int right_boundary)
            {
                if (right_boundary - left_boundary < 2)
                {
                    if (find_this == cost[left_boundary])
                        return left_boundary;
                    if (find_this == cost[right_boundary])
                        return right_boundary;

                    return -1;
                }

                int boundary_difference = right_boundary - left_boundary;
                int middle_index = (boundary_difference / 2) + left_boundary;

                // test middle
                if (find_this == input_array[middle_index])
                    return middle_index;
                // right bisection
                else if (find_this > input_array[middle_index])
                    left_boundary = middle_index;
                // left bisection
                else // (find_this < input_array[middle_index])
                    right_boundary = middle_index;

                return BinarySearch(find_this, input_array, left_boundary, right_boundary);
            }

        }

        public static void WhatFlavors2(int[] cost, int money)
        {
            HashSet<int> cost_table = new HashSet<int>();

            foreach (int ice_cost in cost)
                cost_table.Add(ice_cost);

            int first_flavor_cost = money;
            int second_flavor_cost = 0;
            int index_of_first_flavor = -1;
            int index_of_second_flavor = -1;

            do
            {
                first_flavor_cost--;
                second_flavor_cost++;

                if (cost_table.Contains(first_flavor_cost) &&
                    cost_table.Contains(second_flavor_cost))
                {
                    for (int i = 0; i < cost.Length; i++)
                    {
                        if (index_of_first_flavor == -1 && cost[i] == first_flavor_cost)
                        {
                            index_of_first_flavor = i;
                            continue;
                        }
                        if (index_of_second_flavor == -1 && cost[i] == second_flavor_cost)
                            index_of_second_flavor = i;

                        if (index_of_first_flavor != -1 &&
                            index_of_second_flavor != -1)
                        {
                            index_of_first_flavor++;
                            index_of_second_flavor++;

                            if (index_of_first_flavor < index_of_second_flavor)
                                Console.WriteLine((index_of_first_flavor + " " + index_of_second_flavor));
                            else
                                Console.WriteLine((index_of_second_flavor + " " + index_of_first_flavor));

                            return;
                        }
                    }
                }

            } while (first_flavor_cost >= second_flavor_cost); // always unique solution

            // binary search (recursive) of ascending integer array
            // this approach was implied by video. not an optimal approach
            int BinarySearch(int find_this, int[] input_array, int left_boundary, int right_boundary)
            {
                if (right_boundary - left_boundary < 2)
                {
                    if (find_this == cost[left_boundary])
                        return left_boundary;
                    if (find_this == cost[right_boundary])
                        return right_boundary;

                    return -1;
                }

                int boundary_difference = right_boundary - left_boundary;
                int middle_index = (boundary_difference / 2) + left_boundary;

                // test middle
                if (find_this == input_array[middle_index])
                    return middle_index;
                // right bisection
                else if (find_this > input_array[middle_index])
                    left_boundary = middle_index;
                // left bisection
                else // (find_this < input_array[middle_index])
                    right_boundary = middle_index;

                return BinarySearch(find_this, input_array, left_boundary, right_boundary);
            }

        }

        public static void WhatFlavors3(int[] cost, int money)
        {
            Dictionary<int, List<int>> cost_index_pair = new Dictionary<int, List<int>>();

            // build cost-index dictionary
            for (int i = 0; i < cost.Length; i++)
            {
                if (cost_index_pair.ContainsKey(cost[i]))
                {
                    List<int> value = cost_index_pair[cost[i]];
                    value.Add(i);
                    cost_index_pair[cost[i]] = value;
                }
                else
                    cost_index_pair.Add(cost[i], new List<int> { i });
            }

            int first_flavor_cost = money;
            int second_flavor_cost = 0;
            int index_of_first_flavor, index_of_second_flavor;

            do
            {
                first_flavor_cost--;
                second_flavor_cost++;

                if (cost_index_pair.ContainsKey(first_flavor_cost) &&
                    cost_index_pair.ContainsKey(second_flavor_cost))
                {
                    if (first_flavor_cost == second_flavor_cost)
                    {
                        if (cost_index_pair[first_flavor_cost].Count < 2)
                        {
                            Console.WriteLine("error");
                            continue;
                        }
                        else
                        {
                            index_of_first_flavor = cost_index_pair[first_flavor_cost][0];
                            index_of_second_flavor = cost_index_pair[first_flavor_cost][1];
                        }
                    }
                    else
                    {
                        index_of_first_flavor = cost_index_pair[first_flavor_cost][0];
                        index_of_second_flavor = cost_index_pair[second_flavor_cost][0];
                    }

                    index_of_first_flavor++;
                    index_of_second_flavor++;

                    if (index_of_first_flavor < index_of_second_flavor)
                        Console.WriteLine((index_of_first_flavor + " " + index_of_second_flavor));
                    else
                        Console.WriteLine((index_of_second_flavor + " " + index_of_first_flavor));

                    return;
                }
            } while (first_flavor_cost >= second_flavor_cost); // always unique solution

            // binary search (recursive) of ascending integer array
            // this approach was implied by video. not an optimal approach
            int BinarySearch(int find_this, int[] input_array, int left_boundary, int right_boundary)
            {
                if (right_boundary - left_boundary < 2) // WARNING: do not need this
                {
                    if (find_this == cost[left_boundary])
                        return left_boundary;
                    if (find_this == cost[right_boundary])
                        return right_boundary;

                    return -1;
                }

                int boundary_difference = right_boundary - left_boundary;
                int middle_index = (boundary_difference / 2) + left_boundary;

                // test middle
                if (find_this == input_array[middle_index])
                    return middle_index;
                // right bisection
                else if (find_this > input_array[middle_index])
                    left_boundary = middle_index;
                // left bisection
                else // (find_this < input_array[middle_index])
                    right_boundary = middle_index;

                return BinarySearch(find_this, input_array, left_boundary, right_boundary);
            }

        }

        // all/most of these solutions were probably working
        // added .Trim() to Array.ConvertAll(...) in provided processing code
        public static void WhatFlavors4(int[] cost, int money)
        {
            Dictionary<int, List<int>> cost_index_pair = new Dictionary<int, List<int>>();

            // build cost-index dictionary
            for (int i = 0; i < cost.Length; i++)
            {
                if (cost_index_pair.ContainsKey(cost[i]))
                {
                    List<int> value = cost_index_pair[cost[i]];
                    value.Add(i);
                    cost_index_pair[cost[i]] = value;
                }
                else
                    cost_index_pair.Add(cost[i], new List<int> { i });
            }

            int first_flavor_cost;
            int second_flavor_cost;
            int index_of_first_flavor;
            int index_of_second_flavor;

            // iterate over array to search
            for (int i = 0; i < cost.Length; i++)
            {
                first_flavor_cost = cost[i];
                second_flavor_cost = money - first_flavor_cost;

                if (cost_index_pair.ContainsKey(second_flavor_cost))
                {
                    if (first_flavor_cost != second_flavor_cost || cost_index_pair[first_flavor_cost].Count > 1)
                    {
                        index_of_first_flavor = i + 1;

                        if (first_flavor_cost == second_flavor_cost)
                            index_of_second_flavor = cost_index_pair[second_flavor_cost][1] + 1;
                        else
                            index_of_second_flavor = cost_index_pair[second_flavor_cost][0] + 1;

                        if (index_of_first_flavor < index_of_second_flavor)
                            Console.WriteLine((index_of_first_flavor + " " + index_of_second_flavor));
                        else
                            Console.WriteLine((index_of_second_flavor + " " + index_of_first_flavor));

                        return;
                    }
                }
            }

            // binary search (recursive) of ascending integer array
            // this approach was implied by video. not an optimal approach
            int BinarySearch(int find_this, int[] input_array, int left_boundary, int right_boundary)
            {
                if (right_boundary - left_boundary < 2)
                {
                    if (find_this == cost[left_boundary])
                        return left_boundary;
                    if (find_this == cost[right_boundary])
                        return right_boundary;

                    return -1;
                }

                int boundary_difference = right_boundary - left_boundary;
                int middle_index = (boundary_difference / 2) + left_boundary;

                // test middle
                if (find_this == input_array[middle_index])
                    return middle_index;
                // right bisection
                else if (find_this > input_array[middle_index])
                    left_boundary = middle_index;
                // left bisection
                else // (find_this < input_array[middle_index])
                    right_boundary = middle_index;

                return BinarySearch(find_this, input_array, left_boundary, right_boundary);
            }

        }

        // Complete the pairs function below.
        public static int Pairs(int k, int[] arr)
        {
            HashSet<int> hash = new HashSet<int>();
            foreach (int element in arr)
                hash.Add(element);

            int count_pairs = 0;

            foreach (int element in arr)
            {
                if (hash.Contains(element + k))
                    count_pairs++;
            }

            return count_pairs;
        }

        // Complete the triplets function below.
        public static long Triplets2(int[] a, int[] b, int[] c)
        {
            HashSet<string> tripplet_set = new HashSet<string>();

            foreach (int a_element in a)
            {
                foreach (int b_element in b)
                {
                    if (a_element > b_element)
                        continue;

                    foreach (int c_element in c)
                    {
                        if (b_element < c_element)
                            continue;

                        string tripplet = a_element.ToString() + " " + b_element.ToString() + " " + c_element.ToString();
                        if (!tripplet_set.Contains(tripplet))
                            tripplet_set.Add(tripplet);
                    }
                }
            }

            return tripplet_set.Count;
        }

        public static long Triplets3(int[] a, int[] b, int[] c)
        {
            long triplet_pairs = 0;

            var watch2 = new System.Diagnostics.Stopwatch();
            watch2.Start();

            // unique and for optimized search
            //int[] ss_a = new SortedSet<int>(a).ToArray();
            //int[] ss_b = new SortedSet<int>(b).ToArray();
            //int[] ss_c = new SortedSet<int>(c).ToArray();

            Array.Sort(a);
            Array.Sort(b);
            Array.Sort(c);

            int[] ss_a = a;
            int[] ss_b = b;
            int[] ss_c = c;

            watch2.Stop();
            Console.WriteLine($"Execution Time: {watch2.ElapsedMilliseconds} ms");

            long top_count, bottom_count;

            // from middle element, count top/bottom
            for (int i = 0; i < ss_b.Length; i++)
            {
                top_count = 0;
                bottom_count = 0;

                // count top
                for (int j = 0; j < ss_a.Length; j++)
                {
                    if (ss_a[j] <= ss_b[i])
                        top_count++;
                    else
                        break;
                }

                // count bottom
                for (int j = 0; j < ss_c.Length; j++)
                {
                    if (ss_c[j] <= ss_b[i])
                        bottom_count++;
                    else
                        break;
                }

                triplet_pairs += top_count * bottom_count;
            }

            return triplet_pairs;
        }

        public static long Triplets(int[] a, int[] b, int[] c)
        {
            long triplet_pairs = 0;

            // ordered and unique for binary search
            a = a.OrderBy(x => x).Distinct().ToArray();
            b = b.OrderBy(x => x).Distinct().ToArray();
            c = c.OrderBy(x => x).Distinct().ToArray();

            long top_count, bottom_count;

            // from middle element, count top/bottom
            for (int i = 0; i < b.Length; i++)
            {
                // binary search //
                // count top
                top_count = Binary_Search(a, b[i]);
                // count bottom
                bottom_count = Binary_Search(c, b[i]);

                triplet_pairs += top_count * bottom_count;
            }

            return triplet_pairs;
        }

        // returns topmost/rightmost
        static long Binary_Search(int[] search_me, int find_this)
        {
            int lower = 0;
            int upper = search_me.Length;

            while (lower < upper)
            {
                int mid = (upper + lower) / 2;

                if (search_me[mid] > find_this)
                    upper = mid;
                else // (search_me[mid] < find_this)
                    lower = mid + 1;
            }

            return upper;
        }

        // Complete the minTime function below.
        public static long MinTime2(long[] machines, long goal)
        {
            decimal productivity = 0;
            foreach (long value in machines)
                productivity += decimal.Divide(1, (decimal)value);

            decimal time = decimal.Divide((decimal)goal, productivity);

            decimal factor = 10;
            decimal hacked_time = (time * factor);
            hacked_time /= factor;
            time = Math.Ceiling(hacked_time);

            return (long)time;
        }

        public static long MinTime3(long[] machines, long goal)
        {
            decimal productivity = 0;
            decimal[] running_product = { 1, 1 };
            decimal time;

            for (int i = 0; i < machines.Length; i++)
            {
                if (i == 0)
                {
                    running_product[0] = 1;
                    running_product[1] = machines[i];
                    continue;
                }


                if (machines[i] == 1)
                    running_product[1] *= 2;
                else
                {
                    running_product[0] = (machines[i] * running_product[0]) + running_product[1];
                    running_product[1] *= machines[i];
                }

                decimal reduction = running_product[0] / running_product[1];
                if (reduction % 1 == 0)
                {
                    running_product[0] = reduction;
                    running_product[1] = 1;
                }

                if (running_product[0] / running_product[1] % 1 == 0)
                {
                    running_product[0] /= running_product[1];
                    running_product[1] /= running_product[1];
                }
                else if (running_product[1] / running_product[0] % 1 == 0)
                {
                    running_product[1] /= running_product[0];
                    running_product[0] /= running_product[0];
                }

                Console.WriteLine(running_product[0] / running_product[1]);
            }

            if (running_product[1] == 1)
                time = running_product[0] / goal;
            else
                time = Math.Ceiling(running_product[1] * goal / running_product[0]);

            return (long)time;
        }

        public static long MinTime4(long[] machines, long goal)
        {
            long items = 0;
            long day = 0;

            while (items < goal)
            {
                day++;

                foreach (long days in machines)
                    if (day % days == 0)
                        items++;
            }

            return day;
        }

        public static long MinTime5(long[] machines, long goal)
        {
            Dictionary<long, long> machine_dic = new Dictionary<long, long>();
            foreach (long machine in machines)
                if (machine_dic.ContainsKey(machine))
                    machine_dic[machine] += 1;
                else
                    machine_dic.Add(machine, 1);

            long items = 0;
            long day = 0;
            //int loop_factor = 0;
            while (items < goal)
            {
                day++;

                long[] machine_keys = machine_dic.Keys.OrderBy(x => x).ToArray();
                foreach (long days in machine_keys)
                    if (day >= days && day % days == 0)
                        items += machine_dic[days];
            }

            return day;

        }

        public static long MinTime(long[] machines, long goal)
        {
            Dictionary<long, long> machine_dic = new Dictionary<long, long>();
            foreach (long machine in machines)
                if (machine_dic.ContainsKey(machine))
                    machine_dic[machine] += 1;
                else
                    machine_dic.Add(machine, 1);

            long[] machine_keys = machine_dic.Keys.OrderBy(x => x).ToArray();

            // binary search
            long upper = goal * machine_keys.Last() / machines.Length;
            long lower = goal * machine_keys[0] / machines.Length;

            long items = 0;
            long test_day = 0;
            while (lower < upper)
            {
                long mid = (upper + lower) / 2;

                items = 0;
                test_day = mid;

                foreach (long days_to_produce in machine_keys)
                {
                    var temp0 = test_day / days_to_produce;
                    items += temp0 * machine_dic[days_to_produce];
                }

                if (items == goal)
                    upper = mid + 1;
                else if (items > goal)
                    upper = mid + 1;
                else // (items < goal)
                    lower = mid;

                if (upper - lower < 3 && items >= goal)
                    return test_day;
            }

            return -1;
        }

        /// Miscellaneous ///

        // Complete the flippingBits function below.
        public static long FlippingBits(long n)
        {
            return ~Convert.ToUInt32(n);
        }

        /// Dynamic Programing ///

        // Complete the maxSubsetSum function below.
        public static int MaxSubsetSum2(int[] arr)
        {
            // larger of first or second until negative value
            // sum segments between negative values ?

            // if adjacent non-negative values sum to less than or equal to inspected value, that inspected value is optimal
            // if one is negative, inspect positive


            // optimal when the inspected element is larger than the preceding offset elements in preceding subsection

            // if the smaller sum hits a negative, that is a suboptimal section


            // optimality tests for previous elements can occur under certian conditions

            List<int[]> optimal_segment = new List<int[]>();
            List<int> optimal_sum_segment = new List<int>();

            List<int> first_sum_segment = new List<int>();
            List<int> second_sum_segment = new List<int>();

            int first_sum = 0;
            int second_sum = 0;
            int offset = 0;

            int optimal_sum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                int inspected_element = arr[i];

                // iterating sum
                if (i % 2 == 0 && inspected_element > -1)
                {
                    first_sum += inspected_element;
                    first_sum_segment.Add(inspected_element);
                }
                else if (inspected_element > -1)
                {
                    second_sum += inspected_element;
                    second_sum_segment.Add(inspected_element);
                }

                // non-comprehensive optimality test (trigger)
                if (i > 0 && i < arr.Length - 1 && isOptimal(arr[i - 1], inspected_element, arr[i + 1]))
                {
                    optimal_segment.Add(new int[] { inspected_element, i });

                    if (first_sum >= second_sum)
                    {
                        optimal_sum_segment.Add(first_sum);
                        optimal_sum += first_sum;
                    }
                    else if (second_sum > first_sum)
                    {
                        optimal_sum_segment.Add(second_sum);
                        optimal_sum += second_sum;
                    }

                    if (first_sum == second_sum)
                        Console.WriteLine("they're equal o.o");

                    first_sum = 0;
                    second_sum = 0;

                    //Reset();

                    i++;

                }

                // negative encounter (optimality test trigger)
                if (inspected_element < 1)
                {
                    if (second_sum < first_sum)
                    {
                        optimal_sum += first_sum;
                    }

                    else if (first_sum < second_sum)
                    {
                        optimal_sum += second_sum;
                    }

                    first_sum = 0;
                    second_sum = 0;
                }

                // end of array (optimality test trigger)
                if (i == arr.Length - 1)
                {
                    if (first_sum >= second_sum)
                    {
                        optimal_sum += first_sum;
                    }
                    else if (second_sum > first_sum)
                    {
                        optimal_sum += second_sum;
                    }

                    if (first_sum == second_sum)
                        Console.WriteLine("they're equal o.o");
                }

            }

            return optimal_sum;

            static bool isOptimal(int decrement_index, int inspected, int increment_index)
            {
                if (decrement_index > -1 &&
                    increment_index > -1 &&
                    inspected >= decrement_index + increment_index)
                {
                    return true;
                }

                return false;
            }

            void Reset()
            {
                if (first_sum >= second_sum)
                {
                    optimal_sum_segment.Add(first_sum);
                    optimal_sum += first_sum;
                }
                else if (second_sum > first_sum)
                {
                    optimal_sum_segment.Add(second_sum);
                    optimal_sum += second_sum;
                }

                if (first_sum == second_sum)
                    Console.WriteLine("they're equal o.o");

                first_sum = 0;
                second_sum = 0;
            }
        }

        public static int MaxSubsetSum(int[] arr)
        {
            // optimality tests for previous elements can occur under certian conditions

            int first_sum = 0;
            int second_sum = 0;

            int optimal_sum = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                int inspected_element = arr[i];

                // iterating sum
                if (i % 2 == 0 && inspected_element > -1)
                    first_sum += inspected_element;
                else if (inspected_element > -1)
                    second_sum += inspected_element;

                // non-comprehensive optimality test (trigger)
                if (i > 0 && i < arr.Length - 1 && isOptimal(arr[i - 1], inspected_element, arr[i + 1]))
                {
                    if (i % 2 == 0)
                    {
                        first_sum -= arr[i];
                        second_sum -= arr[i - 1];
                    }
                    else
                    {
                        second_sum -= arr[i];
                        first_sum -= arr[i - 1];
                    }

                    if (first_sum >= second_sum)
                    {
                        optimal_sum += first_sum;
                    }
                    else if (second_sum > first_sum)
                    {
                        optimal_sum += second_sum;
                    }

                    first_sum = inspected_element;
                    second_sum = 0;



                    //i++;
                }

                // optimality(?) triggers:
                // negative encounter
                // end of array
                if (inspected_element < 1 ||
                    i == arr.Length - 1)
                {
                    if (first_sum >= second_sum)
                    {
                        optimal_sum += first_sum;
                    }
                    else if (second_sum > first_sum)
                    {
                        optimal_sum += second_sum;
                    }

                    first_sum = 0;
                    second_sum = 0;
                }
            }

            return optimal_sum;

            static bool isOptimal(int decrement_index, int inspected, int increment_index)
            {
                if (decrement_index > -1 &&
                    increment_index > -1 &&
                    inspected >= decrement_index + increment_index)
                {
                    return true;
                }

                return false;
            }

        }

        public static int MaxSubsetSum0(int[] arr)
        {
            int excluding = 0;
            int including = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                if (i % 2 == 0)
                {
                    including = Math.Max(excluding, including + arr[i]);
                    Console.WriteLine("including: " + including);
                }
                else
                {
                    excluding = Math.Max(including, excluding + arr[i]);
                    Console.WriteLine("excluding: " + excluding);
                }

            }

            return Math.Max(including, excluding);
        }

        /// Practice, Algorithms, Strings ///

        /// Stacks and Queues ///

        // Complete the isBalanced function below.
        public static string IsBalanced2(string s)
        {
            //  {, }, (, ), [, ]
            // +1 -1
            // 123 125, 40 41, 91 93

            //Console.WriteLine(s[0].GetType());

            char[] permissible = { '{', '(', '[' };

            Stack<char> queue = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (permissible.Contains(s[i]))
                {
                    queue.Push(s[i]);
                }
                else if (IsPair(queue.Peek(), s[i]))
                {
                    queue.Pop();
                }
                else
                    return "NO";
            }

            if (queue.Count == 0)
                return "YES";

            return "NO";

            static bool IsPair(char left_bracket, char right_bracket)
            {
                if ((int)left_bracket == 123 && (int)right_bracket == 125)
                {
                    return true;
                }
                if ((int)left_bracket == 40 && (int)right_bracket == 41)
                {
                    return true;
                }
                if ((int)left_bracket == 91 && (int)right_bracket == 93)
                {
                    return true;
                }

                return false;
            }
        }

        public static string IsBalanced3(string s)
        {
            //  {, }, (, ), [, ]
            // 123 125, 40 41, 91 93

            char[] permissible = { '{', '(', '[' };

            if (s.Length % 2 != 0)
                return "NO";

            int mid_point = s.Length / 2;

            for (int i = 0; i < mid_point; i++)
            {
                if (!IsPair(s[mid_point - (i + 1)], s[mid_point + i]))
                    return "NO";
            }

            return "YES";



            static bool IsPair(char left_bracket, char right_bracket)
            {
                if ((int)left_bracket == 123 && (int)right_bracket == 125)
                {
                    return true;
                }
                if ((int)left_bracket == 40 && (int)right_bracket == 41)
                {
                    return true;
                }
                if ((int)left_bracket == 91 && (int)right_bracket == 93)
                {
                    return true;
                }

                return false;
            }
        }

        public static string IsBalanced(string s)
        {
            //  {, }, (, ), [, ]
            // +1 -1
            // 123 125, 40 41, 91 93

            //Console.WriteLine(s[0].GetType());

            //HashSet<char> permissible = new HashSet<char>();
            //permissible.Add('{');
            //permissible.Add('(');
            //permissible.Add('[');

            char[] permissible = { '{', '(', '[' };

            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (permissible.Contains(s[i]))
                {
                    stack.Push(s[i]);
                }
                else if (stack.Count > 0 && IsPair(stack.Peek(), s[i]))
                {
                    stack.Pop();
                }
                else
                    return "NO";
            }

            if (stack.Count == 0)
                return "YES";

            return "NO";

            static bool IsPair(char left_bracket, char right_bracket)
            {
                if ((int)left_bracket == 123 && (int)right_bracket == 125)
                {
                    return true;
                }
                if ((int)left_bracket == 40 && (int)right_bracket == 41)
                {
                    return true;
                }
                if ((int)left_bracket == 91 && (int)right_bracket == 93)
                {
                    return true;
                }

                return false;
            }
        }

        // Queues: A Tale of Two Stacks
        public static void Queues(String[] args)
        {
            /* Enter your code here. Read input from STDIN. Print output to STDOUT. Your class should be named Solution */

            // Queues: A Tale of Two Stacks
            // Complete the put, pop, and peek methods in the editor below. 
            // They must perform the actions as described above.
            // 1 enqueue element x into the end of the queue
            // 2 dequeue the element at the front of the queue
            // 3 print the element at the front of the queue

            // I do not understand this:
            // "In this challenge, you must first implement a queue using two stacks."

            int[][] input = new int[10][];

            string str = "1 10";

            String[] strArr = str.Split().ToArray();

            Console.WriteLine(strArr.GetType());
            //int[] temp = Array.ConvertAll<string, int>
            //int i = 0;
            //input[i] = { temp };

            // using most primitive known data type
            int[] queue_type = { };
            int[] stack_type = { };

            int[] query = new int[2];

            // 1 enqueue element x into the end of the queue
            if (query[0] == 1)
            {
                queue_type = put(query[1], queue_type);
            }
            // 2 dequeue the element at the front of the queue
            else if (query[0] == 2)
            {
                queue_type = pop(queue_type)[1];
            }
            // 3 print the element at the front of the queue
            else if (query[0] == 3)
            {
                Console.WriteLine(peek(queue_type));
            }
            // unsupported input
            else
                throw new Exception();


            static int[] put(int put_this, int[] queue_type)
            {
                // create new array of modified size
                int[] return_queue = new int[queue_type.Length + 1];

                // construct modified array
                for (int i = 0; i < return_queue.Length; i++)
                    return_queue[i] = queue_type[i];
                return_queue[return_queue.Length - 1] = put_this; // HR does not accept ^1

                return return_queue;
            }

            static int[][] pop(int[] queue_type)
            {
                // test if operation is possible
                if (queue_type.Length == 0)
                    throw new NullReferenceException("Queue is empty.");

                // create new array of modified length
                int[] modified_queue = new int[queue_type.Length - 1];

                // copy modification to new array
                for (int i = 1; i < queue_type.Length; i++)
                    modified_queue[i - 1] = queue_type[i];

                int[][] return_data = { new int[] { queue_type[0] }, modified_queue };

                return return_data;
            }

            static int peek(int[] queue_type)
            {
                // test if operation is possible
                if (queue_type.Length == 0)
                    throw new NullReferenceException("Queue is empty.");

                return queue_type[0];
            }

        }

        public static void ProcessInput2(int[][] input)
        {
            int[] queue_type = { };

            for (int i = 1; i < input.Length; i++)
            {
                // 1 (put) enqueue element x into the end of the queue
                if (input[i][0] == 1)
                {
                    // create new array of modified size
                    int[] return_queue = new int[queue_type.Length + 1];

                    // construct modified array
                    if (queue_type.Length > 0)
                    {
                        for (int j = 0; j < queue_type.Length; j++)
                            return_queue[j] = queue_type[j];
                    }
                    return_queue[return_queue.Length - 1] = input[i][1]; // HR dna ^1

                    queue_type = return_queue;
                }
                // 2 (pop) dequeue the element at the front of the queue
                else if (input[i][0] == 2)
                {
                    // test if operation is possible
                    if (queue_type.Length > 0)
                    {
                        // create new array of modified length
                        int[] modified_queue = new int[queue_type.Length - 1];

                        // copy modification to new array
                        for (int j = 1; j < queue_type.Length; j++)
                            modified_queue[j - 1] = queue_type[j];

                        queue_type = modified_queue;
                    }
                }
                // 3 print the element at the front of the queue
                else if (input[i][0] == 3)
                {
                    // test if operation is possible
                    if (queue_type.Length > 0)
                    {
                        Console.WriteLine(queue_type[0]);
                    }
                }
                // unsupported input
                else
                    throw new Exception();
            }
        }

        public static void ProcessInput(int[][] input)
        {
            // did not understand what was being asked
            // more confusing or poorly implemented HR exercises
            // rather spend time learning something than this

            Queue<int> queue = new Queue<int>();

            for (int i = 1; i < input.Length; i++)
            {
                // 1 (put) enqueue element x into the end of the queue
                if (input[i][0] == 1)
                {
                    queue.Enqueue(input[i][1]);
                }
                // 2 (pop) dequeue the element at the front of the queue
                else if (input[i][0] == 2)
                {
                    // test if operation is possible
                    if (queue.Count > 0)
                    {
                        queue.Dequeue();
                    }
                }
                // 3 print the element at the front of the queue
                else if (input[i][0] == 3)
                {
                    // test if operation is possible
                    if (queue.Count > 0)
                    {
                        Console.WriteLine(queue.Peek());
                    }
                }
                // unsupported input
                else
                    throw new Exception();
            }
        }

        // Complete the largestRectangle function below.
        // (optimization problem ?)
        public static long LargestRectangle2(int[] h)
        {
            // minimum area is 1xn
            long minimum_area = h.Length;

            // count sequence length at given heights
            for (int i = 2; i <= h.Max(); i++)
            {
                for (int j = 0; j < h.Length; j++)
                {
                    int k = j;
                    int count = 0;
                    while (k < h.Length && h[k] >= i)
                    {
                        count++;
                        k++;
                    }

                    if (k != j)
                        j = k;

                    if (i * count > minimum_area)
                        minimum_area = i * count;
                }

                Console.WriteLine(i);
            }

            Console.WriteLine("foo");

            return minimum_area;
        }

        public static long LargestRectangle(int[] h)
        {
            // maximum area found
            long maximum_area = h.Length;

            // count sequence length in-out from inspected value
            for (int i = 0; i < h.Length; i++)
            {
                int left_index = i - 1;
                int right_index = i + 1;
                int count_left = 0;
                int count_right = 0;

                // left count
                while (left_index >= 0 && h[left_index] >= h[i])
                {
                    count_left++;
                    left_index--;
                }

                // right count
                while (right_index < h.Length && h[right_index] >= h[i])
                {
                    count_right++;
                    right_index++;
                }

                // area
                long area = h[i] * (1 + count_left + count_right);

                // if larger test
                if (area > maximum_area)
                    maximum_area = area;
            }

            return maximum_area;
        }

        // Complete the riddle function below.
        static long[] riddle(long[] arr)
        {
            // complete this function
            return null;

        }


        /// ////////// Practice > Algorithms > Warmup ////////// ///

        /*
         * Complete the 'simpleArraySum' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts INTEGER_ARRAY ar as parameter.
         */
        public static int simpleArraySum(List<int> ar)
        {

            int sum = 0;

            foreach (int array_emlement in ar)
            {
                sum += array_emlement;
            }

            return sum;

        }


        /*
         * Complete the 'formingMagicSquare' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts 2D_INTEGER_ARRAY s as parameter.
         */

        public static int formingMagicSquare(List<List<int>> s)
        {
            return 0;
        }


    }
}
