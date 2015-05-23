using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TaipeiHachathon
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] templesList = System.IO.File.ReadAllLines(@"C:\Users\user\Desktop\TaipeiHackaton\TaipeiHackaton\religion_lite.csv", Encoding.Default);
            string[] buildingList = System.IO.File.ReadAllLines(@"C:\Users\user\Desktop\TaipeiHackaton\TaipeiHackaton\TestData_ALL_adjust.csv", Encoding.Default);

            List<string> Result = new List<string>();
            Result = MergeDistance(templesList, buildingList,100);

            System.IO.File.WriteAllLines(@"C:\Users\user\Desktop\TaipeiHackaton\TaipeiHackaton\TestData_Rent_adjust.csv", Result, Encoding.UTF8);

            #region
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\user\Desktop\TaipeiHackaton\TaipeiHackaton\TestData_Rent.csv", Encoding.Default);
            //List<string> AdjustResultList = new List<string>();
            //Regex rAddress = new Regex("\\d{1,4}~\\d{1,4}");
            //int count = 0;
            //foreach (string line in lines)
            //{
            //    if (count != 0)
            //    {
            //        string[] templist = line.Split(new char[] { ',' });
            //        string addressRange = rAddress.Match(templist[1]).ToString();
            //        string[] tempAddressRanges = addressRange.Split(new char[] { '~' });
            //        Random rand = new Random();
            //        System.Threading.Thread.Sleep(30);
            //        int randomAddress = rand.Next(Convert.ToInt32(tempAddressRanges[0]), Convert.ToInt32(tempAddressRanges[1]));
            //        string addressAdjust = rAddress.Replace(templist[1], randomAddress.ToString());
            //        string adjustResult = templist[0] + "," + addressAdjust + "," + templist[2] + "," + templist[3];
            //        AdjustResultList.Add(adjustResult);
            //    }
            //    else 
            //    {
            //        string[] templist = line.Split(new char[] { ',' });
            //        AdjustResultList.Add(templist[0] + "," + templist[1] + "," + templist[2] + "," + templist[3]);
            //    }
            //    Console.WriteLine(count);
            //    count++;
            //}

            //System.IO.File.WriteAllLines(@"C:\Users\user\Desktop\TaipeiHackaton\TaipeiHackaton\TestData_Rent_adjust.csv", AdjustResultList, Encoding.UTF8);
            #endregion
        }

        public static double distance(double[] templeGPS_X_Y, double[] buildingGPS_X_Y) 
        {
            double meter_Z = Math.Sqrt(Math.Pow((templeGPS_X_Y[0] - buildingGPS_X_Y[0]), 2) + Math.Pow((templeGPS_X_Y[1] - buildingGPS_X_Y[1]), 2));

            return meter_Z;
        }

        public static List<string> MergeDistance(string[] templeRowList, string[] buildingRowList, double distanceMeter) 
        {
            List<string> tmpResult = new List<string>();

            foreach (string templeRow in templeRowList) 
            {
                string[] templeRowSplie = templeRow.Split(new char[] { ',' });
                double[] tmpTempleXY = new double[] { Convert.ToDouble(templeRowSplie[4]), Convert.ToDouble(templeRowSplie[5]) };

                for (int i = 0; i<buildingRowList.Count();i++) 
                {
                    string[] buildRowSplit = buildingRowList[i].Split(new char[] { ',' });
                    double[] tmpBuildXY = new double[] { Convert.ToDouble(buildRowSplit[4]), Convert.ToDouble(buildRowSplit[5]) };

                    double distanceTempleAndBuilding = distance(tmpTempleXY, tmpBuildXY);
                    if (distanceMeter > distanceTempleAndBuilding) 
                    {
                        if (!tmpResult.Contains(buildingRowList[i]))
                        {
                            tmpResult.Add(buildingRowList[i]);
                        }
                    }
                }
            }

            return tmpResult;
        }
    }
}
