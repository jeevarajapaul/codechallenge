using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codility
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int resultValue;


            Console.WriteLine("Give the range in the format of x:y");
            string rangeNum = Console.ReadLine();
            int.TryParse(rangeNum.Split(":")[0], out int startNo);
            int.TryParse(rangeNum.Split(":")[1], out int endNo);

            for (int idx = startNo; idx <= endNo; idx++)
            {
                string multipeOfThreeValue = string.Empty;
                string finalValue = string.Empty;
                if (idx % 3 == 0)
                {
                    finalValue = "Fizz";
                }
                if (idx % 5 == 0)
                {
                    finalValue = finalValue + "Buzz";
                }
                Console.WriteLine(finalValue == string.Empty ? idx.ToString() : finalValue);
            }

            Console.WriteLine("Enter the input please.");
            int[] resultArr = new int[] { };
            //string inputValue = Console.ReadLine();
            //Console.WriteLine(GetMaxBinaryGap(529));
            int[] ipArr = { 3, 1, 2, 4, 3 };
            //resultValue = GetMinimumTapeEquilibrium(ipArr);
            ipArr = new[] { 1, 3, 6, 4, 1, 2, 5, 8, 10, 12, 7, -3 };
            ipArr = new[] { -1, -3 };
            ipArr = new[] { 9, 3, 9, 3, 9, 7, 9, 12, 5, 5, 12, 7, 4 };
            ipArr = new[] { 4, 2, 3, 1, 5, 8, 7 };
            ipArr = new[] { 3, 4, 4, 6, 1, 4, 4 };
            ipArr = new[] { -7, -3, 1, 2, -2, 5, 6, 1, 4 };
            //ipArr = new int[] { };
            ipArr = new[] { 1, 3, 6, 4, 1, 2 };
            ipArr = new[] { 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2 };
            //resultValue = GetNoOfPeaks(ipArr);
            ipArr = new[] { 126, 1956, 343322, 23, 45, 32123 };
            ipArr = new[] { -3, -8, 123, 54 };
            //ipArr = new[] { -3, -8, -87 };
            ipArr = new[] { 1, 2, 3 };
            //ipArr = new[] {1};
            //ipArr = new int[] { -122, -5, 1, 2, 3, 4, 5, 6, 7 }; // 8
            //ipArr = new int[] { 1, 3, 6, 4, 1, 2 }; // 5
            //resultValue = GetLowestMissingNo(ipArr);
            ipArr = new[] { -1, -3 };
            ipArr = new[] { 1, 2, 3 };
            //ipArr = new int[] { };
            //ipArr = new[] { 6 };
            //ipArr = new[] { 4, 8, 76, 8, 76, 8, 8, -1, 8, 8, 8, 2, 8, 76, 76, 8 };
            //resultValue = GetNoOfJumps(10, 85, 30);
            ipArr = new[] { 1, 1, 1, 1, 1, 1, 3, 3, 3, 4, 5, 5, 5, 5 };
            resultValue = GetMaxNails(ipArr, 8);
            ipArr = new[] { 1, 3, 6, 4, 1, 2, 5, 8, 10, 12, 7, -3 };
            ipArr = new[] { -1, -3 };
            ipArr = new[] { 9, 3, 9, 3, 9, 7, 9, 12, 5, 5, 12, 7, 4 };
            ipArr = new[] { 4, 2, 3, 1, 5, 8, 7 };
            ipArr = new[] { 3, 4, 4, 6, 1, 4, 4 };
            ipArr = new[] { -7, -3, 1, 2, -2, 5, 6, 1, 4 };
            //ipArr = new int[] { };
            ipArr = new[] { 1, 3, 6, 4, 1, 2 };
            ipArr = new[] { 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2 };
            resultValue = GetNoOfPeaks(ipArr);
            ipArr = new[] { 126, 1956, 343322, 23, 45, 32123 };
            ipArr = new[] { -3, -8, 123, 54 };
            //ipArr = new[] { -3, -8, -87 };
            ipArr = new[] { 1, 2, 3 };
            //ipArr = new[] {1};
            //ipArr = new int[] { -122, -5, 1, 2, 3, 4, 5, 6, 7 }; // 8
            //ipArr = new int[] { 1, 3, 6, 4, 1, 2 }; // 5
            resultValue = GetLowestMissingNo(ipArr);
            ipArr = new[] { -1, -3 };
            ipArr = new[] { 1, 2, 3 };
            //ipArr = new int[] { };
            //ipArr = new[] { 6 };
            //ipArr = new[] { 4, 8, 76, 8, 76, 8, 8, -1, 8, 8, 8, 2, 8, 76, 76, 8 };
            resultValue = FindDomnatorUsingSort(ipArr);
            Console.ReadLine();
        }

        private static int GetMaxNails(int[] arrInput, int K)
        {
            //int maxElment
            int n = arrInput.Length;
            int best = 0;
            int count = 1;
            int prevBest = 0;
            for (int i = 0; i < n - 1; i++)
            {
                if (arrInput[i] == arrInput[i + 1])
                    count = count + 1;
                else
                {
                    prevBest = best;
                    if (count > best)
                    {
                        best = count;
                    }
                    //prevBest = best;
                    count = 1;
                }

            }

            if (count > best)
            {
                best = count;
            }
            //if (K)
            //int result = best + 1 + K;
            //int result = best + 1 + K;
            int result = prevBest + Math.Abs(best - K);
            return result;
        }

        /*
         * https://app.codility.com/programmers/lessons/10-prime_and_composite_numbers/flags/
        */
        private static int GetNoOfPeaks(int[] arrInput)
        {
            List<int> peakPsition = new List<int>();
            int NoOfPeak = 0;
            if (arrInput.Length == 1)
            {
                NoOfPeak = 1;
            }
            for (int idx = 1; idx < arrInput.Length; idx++)
            {
                //if it is the first element
                if (idx == 1)
                {
                    if (arrInput[idx] > arrInput[idx + 1])
                    {
                        peakPsition.Add(idx);
                        NoOfPeak++;
                    }
                }
                //if it is the last element
                else if (idx == (arrInput.Length - 1))
                {
                    if (arrInput[idx] > arrInput[idx - 1])
                    {
                        peakPsition.Add(idx);
                        NoOfPeak++;
                    }
                }
                //if it is in between
                else
                {
                    if (arrInput[idx] > arrInput[idx - 1] && arrInput[idx] > arrInput[idx + 1])
                    {
                        NoOfPeak++;
                        peakPsition.Add(idx);
                    }
                }
            }
            int[] peakArr = peakPsition.ToArray();
            //int maxFlags = 0;
            int flagsCount = 0;
            return flagsCount;
        }


        private static void GetLeader(int[] arrInput)
        {
            int arrLen = arrInput.Length;
            int expectedCount = (arrLen / 2);


        }

        private void FindBinary(int noToManipulate)
        {
            string binaryRepresentation = Convert.ToString(noToManipulate, 2);
            char[] binaryArray = binaryRepresentation.ToCharArray();
        }

        private string LetterChanges(string textToManipulate)
        {

            // code goes here  
            /* Note: In C# the return type of a function and the 
               parameter types being passed are defined, so this return 
               call must match the return type of the function.
               You are free to modify the return type. */
            char[] vowelArrary = new char[] { 'a', 'e', 'i', 'o', 'u' };
            char[] arrCharacters = textToManipulate.ToCharArray();
            StringBuilder resultantText = new StringBuilder();
            int currentAscValue = 0;
            //A - 65 Z - 90 a - 97 z - 122
            foreach (char eachChar in arrCharacters)
            {
                currentAscValue = (byte)eachChar;
                if (currentAscValue >= 65 && currentAscValue <= 90 ||
                    currentAscValue >= 97 && currentAscValue <= 122)
                {
                    currentAscValue++;
                    if (currentAscValue > 90 && currentAscValue < 97)
                    {
                        currentAscValue = 65;
                    }
                    else if (currentAscValue > 122)
                    {
                        currentAscValue = 97;
                    }
                    char nextChar = (char)currentAscValue;
                    if (Array.IndexOf(vowelArrary, nextChar) > -1)
                    {
                        nextChar = char.ToUpper(nextChar);
                    }
                    resultantText.Append(nextChar);
                }
                else
                {
                    resultantText.Append(eachChar);
                }
            }
            return resultantText.ToString();
        }

        private string FirstReverse(string textToReverse)
        {
            char[] arrCharacters = textToReverse.ToCharArray();
            string reversedText = string.Empty;
            foreach (char eachChar in arrCharacters)
            {
                reversedText = eachChar + reversedText;
            }
            return reversedText;
        }


        //https://app.codility.com/programmers/lessons/92-tasks_from_indeed_prime_2016_college_coders_challenge/tennis_tournament/
        private static int GetNoOfParallelMatches(int playersCount, int noOfCourts)
        {
            int noOfMatches = 0;
            int totCapablePlayers = noOfCourts * 2;
            if (playersCount > 1 && noOfCourts >= 1)
            {
                if ((playersCount % 2) == 1)
                {
                    playersCount--;
                }
                for (int idx = 2; idx <= (noOfCourts * 2); idx = idx + 2)
                {

                }
            }
            return noOfMatches;
        }


        /*
         * An array A consisting of N integers is given. The dominator of array A is the value that occurs in more than half of the elements of A.

			For example, consider array A such that

			 A[0] = 3    A[1] = 4    A[2] =  3
			 A[3] = 2    A[4] = 3    A[5] = -1
			 A[6] = 3    A[7] = 3
			The dominator of A is 3 because it occurs in 5 out of 8 elements of A (namely in those with indices 0, 2, 4, 6 and 7) and 5 is more than a half of 8.
         */

        private static int FindDomnatorUsingSort(int[] inputArr)
        {
            Array.Sort(inputArr);
            int dominatorIdx = -1;
            int expectedLen = inputArr.Length / 2;
            int noOfOccurances = 0;
            for (int idx = 0; idx < inputArr.Length - 1; idx++)
            {
                if (inputArr[idx] == inputArr[idx + 1])
                {
                    if (dominatorIdx == -1)
                    {
                        dominatorIdx = idx;
                    }
                    noOfOccurances++;
                    if (noOfOccurances > expectedLen)
                    {
                        //dominatorIdx = idx + 1;
                        break;
                    }
                }
                else
                {
                    dominatorIdx = -1;
                }
            }
            return dominatorIdx;
        }

        private static int FindDominator(int[] inputArr)
        {
            int dominatorIdx = 0;
            int noOfInstances = 0;
            for (int idx = 0; idx < inputArr.Length; idx++)
            {
                if (noOfInstances == 0)
                {
                    dominatorIdx = idx;
                }
                if (inputArr[idx] == inputArr[dominatorIdx])
                {
                    dominatorIdx++;
                }
                else
                {
                    dominatorIdx--;
                }
            }
            noOfInstances = 0;
            for (int idx = 0; idx < inputArr.Length; idx++)
            {
                if (inputArr[idx] == inputArr[dominatorIdx])
                {
                    noOfInstances++;
                    break;
                }
            }
            if (noOfInstances < inputArr.Length / 2)
            {
                dominatorIdx = -1;
            }
            return dominatorIdx;
        }

        private static int FindMoreOccInArrDominator(int[] inputArr)
        {
            int resultIndex = -1;
            if (inputArr.Length > 1)
            {
                var resSet = from arrTable in inputArr
                             group arrTable by arrTable into resultSet
                             select new { ArrValue = resultSet.Key, Occurance = resultSet.Count() };
                int pinPoint = inputArr.Length / 2;
                resSet = resSet.Select(x => x).Where(x => x.Occurance > pinPoint);
                if (resSet.Count() > 0)
                {
                    int domValue = resSet.OrderByDescending(g => g.Occurance).FirstOrDefault().ArrValue;
                    resultIndex = Array.IndexOf(inputArr, domValue);
                }
            }
            return resultIndex;
        }

        //https://app.codility.com/programmers/lessons/12-euclidean_algorithm/chocolates_by_numbers/
        private static int GetNoOfPicksByReminder(int totalNo, int inputGap)
        {
            List<int> noOfPicks = new List<int>();
            if (totalNo > 0 && inputGap > 0)
            {
                //int retVal = 1;
                int nextPos = 0;
                noOfPicks.Add(nextPos);
                for (; ; )
                {
                    nextPos = (nextPos + inputGap) % totalNo;
                    if (noOfPicks.Contains(nextPos))
                    {
                        break;
                    }
                    else
                    {
                        noOfPicks.Add(nextPos);
                    }
                }
            }
            return noOfPicks.Count;
        }

        //https://app.codility.com/programmers/lessons/2-arrays/cyclic_rotation/
        private static int[] RotateArray(int[] arrInput, int noOfTimes)
        {
            int[] counterArr = arrInput;
            if (arrInput != null && arrInput.Length > 0)
            {
                counterArr = Enumerable.Repeat(0, arrInput.Length).ToArray();
                if (noOfTimes > arrInput.Length)
                {
                    noOfTimes = noOfTimes % arrInput.Length;
                }
                if (arrInput.Length > 1 && noOfTimes > 0 && noOfTimes <= arrInput.Length)
                {
                    int newIdx = 0;
                    for (int idx = 0; idx < arrInput.Length; idx++)
                    {
                        newIdx = idx + noOfTimes;
                        if (newIdx > arrInput.Length - 1)
                        {
                            newIdx = newIdx - arrInput.Length;
                        }
                        counterArr[newIdx] = arrInput[idx];
                    }
                }
                else
                {
                    counterArr = arrInput;
                }
            }
            return counterArr;
        }

        private static int GetNoOfFactors(int inputNo)
        {
            int factorsCount = 0;
            for (int idx = 1; idx <= inputNo; idx++)
            {
                if (inputNo % idx == 0)
                {
                    factorsCount++;
                }
            }
            return factorsCount;
        }

        private static int GetMaxTripletValue(int[] arrInput)
        {
            List<int> newList = new List<int>();
            newList.AddRange(arrInput.ToList().Select(n => (n < 0) ? -n : n));
            newList.Sort();
            int maxVal = 1;

            foreach (int eachInt in newList.GetRange(newList.Count - 3, 3))
            {
                maxVal = maxVal * eachInt;
            }
            return maxVal;
        }

        //https://app.codility.com/programmers/lessons/4-counting_elements/max_counters/
        private static int[] GetResultArray(int counterLen, int[] inputArr, int arrLen)
        {
            //int[] resultArr;
            int[] counterArr = Enumerable.Repeat(0, counterLen).ToArray();
            for (int idx = 0; idx < inputArr.Length; idx++)
            {
                counterArr = ManipulateCounter(counterArr, inputArr[idx], counterLen);
            }
            return counterArr;
        }

        private static int[] ManipulateCounter(int[] inputArr, int arrVal, int counterLen)
        {
            //int[] manipulatedArr;
            if (arrVal < counterLen)
            {
                inputArr[arrVal - 1]++;
            }
            else if (arrVal == counterLen + 1)
            {
                int currentMaxVal = inputArr.Max();
                inputArr = Enumerable.Repeat(currentMaxVal, counterLen).ToArray();
            }
            return inputArr;
        }

        private static int GetMissingElement(int[] inputArr)
        {

            int missingNo = 0;
            Array.Sort(inputArr);
            for (int idx = 0; idx < (inputArr.Length - 1); idx++)
            {
                if (inputArr[idx] + 1 != inputArr[idx + 1])
                {
                    missingNo = inputArr[idx] + 1;
                    break;
                }
            }
            return missingNo;
        }

        private static int GetOddValue(int[] inputArr)
        {
            int oddNo = 0;
            Array.Sort(inputArr);
            for (int idx = 0; idx < (inputArr.Length - 1); idx = idx + 2)
            {

                if (inputArr[idx] != inputArr[idx + 1])
                {
                    oddNo = inputArr[idx];
                    break;
                }
            }
            return oddNo;
        }
        /*
        Write a function:
            class Solution { public int solution(int[] A); }
            that, given an array A of N integers, returns the smallest positive integer(greater than 0) that does not occur in A.
            For example, given A = [1, 3, 6, 4, 1, 2], the function should return 5.
            Given A = [1, 2, 3], the function should return 4.
            Given A = [−1, −3], the function should return 1.
            */

        private static int GetLowestMissingNo(int[] inputArr)
        {
            int missingNo = 1;

            Array.Sort(inputArr);
            for (int idx = 0; idx < inputArr.Length; idx++)
            {

                if (inputArr[idx] == missingNo)
                {

                    missingNo++;
                }
            }

            return missingNo;
        }
        private static int GetLowestNo(int[] inputArr)
        {
            //1000000
            int missingNo = 0;
            bool arrHasPositive = false;
            if (inputArr.Length > 0)
            {
                Array.Sort(inputArr);
                for (int idx = 0; idx < (inputArr.Length - 1); idx++)
                {
                    if (inputArr[idx] > 0)
                    {
                        if (!arrHasPositive)
                        {
                            arrHasPositive = true;
                        }
                        if ((inputArr[idx + 1] - inputArr[idx]) > 1)
                        {
                            missingNo = inputArr[idx] + 1;
                        }
                    }
                }
                if (arrHasPositive)
                {
                    if (missingNo == 0)
                    {
                        missingNo = inputArr.Max() + 1;
                    }
                }
                else
                {
                    missingNo = 1;
                }
            }
            return missingNo;
        }

        private static int GetMinimumTapeEquilibrium(int[] arrayInput)
        {
            int minDiff = 0;
            List<int> listInput = new List<int>(arrayInput);
            listInput.Sum();
            List<int> listFirst;
            List<int> listSecond;
            List<int> listDiff = new List<int>();
            int listBoundary = listInput.Count - 1;
            int startIdx = 0;
            for (int idxArr = 0; idxArr < listBoundary; idxArr++)
            {
                listFirst = listInput.GetRange(0, idxArr + 1);
                startIdx = idxArr + 1;
                listSecond = listInput.GetRange(startIdx, listInput.Count - startIdx);
                listDiff.Add(listFirst.Sum() - listSecond.Sum());

            }
            minDiff = listDiff.Min();
            return minDiff;
        }

        private static int GetSum(int[] inputArr)
        {
            int sumOfArr = 0;
            for (int idxArr = 0; idxArr <= inputArr.Length; idxArr++)
            {
                sumOfArr += inputArr[idxArr];
            }
            return sumOfArr;
        }

        private static int GetNoOfJumps(int startPos, int endPos, int jumpDistance)
        {
            int noOfJumps = 1;
            int distanceTotal = endPos - startPos;
            if (endPos > startPos && distanceTotal > jumpDistance)
            {
                noOfJumps = Math.DivRem(distanceTotal, jumpDistance, out int remainderValue);
                if (remainderValue > 0)
                {
                    noOfJumps++;
                }
            }
            return noOfJumps;
        }

        private static int GetMaxBinaryGap(int noToManipulate)
        {
            //100010010000001
            string binaryRepresentation = Convert.ToString(noToManipulate, 2);
            binaryRepresentation = binaryRepresentation.TrimEnd('0');
            char[] delimiterForSplit = new char[] { '1' };
            string[] binaryArray = binaryRepresentation.Split(delimiterForSplit, StringSplitOptions.None);
            int maxBinaryGap = 0;
            foreach (string eachPortion in binaryArray)
            {
                if (eachPortion.Length > maxBinaryGap)
                {
                    maxBinaryGap = eachPortion.Length;
                }
            }
            return maxBinaryGap;
        }
    }
}
