using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Sats.Extentions
{
    public static class Extention
    {
        public static string ToJson(this object o)
        {
            try
            {

                JavaScriptSerializer serialize = new JavaScriptSerializer();

               
               
                 
                    return serialize.Serialize(o);
                

            }
            catch (Exception)
            {
                throw;
            }

        }

        public static string ToFirstLetterCapital(this string str)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(str))
                {
                    str = str.TrimEnd().TrimStart();
                    return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
                }
                else
                {
                    return str;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string ToFirstOfEveryWord(this string str)
        {
            try
            {
                string newSentence = string.Empty;
                if (!string.IsNullOrWhiteSpace(str))
                {
                    string[] arrayOfWords = str.TrimStart().TrimEnd().Split(' ');

                    for (int i = 0; i < arrayOfWords.Length; i++)
                    {
                        newSentence += arrayOfWords[i].Substring(0, 1).ToUpper() + arrayOfWords[i].Substring(1, arrayOfWords[i].Length - 1) + " ";
                    }

                    return newSentence.TrimEnd();
                }
                else
                {
                    return str;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static T ToJsonObject<T>(this string s)
        {
            try
            {
                JavaScriptSerializer serialize = new JavaScriptSerializer();
                return serialize.Deserialize<T>(s);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static List<List<T>> SplitData<T>(this List<T> s, int splitInto)
        {

            //if (s.Count < splitInto || splitInto == 1 || splitInto == 0)
            //    throw new ArgumentException("split count is greater than list Count or split count is 0 or 1");


            List<List<T>> st = new List<List<T>>();

            double divisor = Convert.ToDouble(s.Count) / Convert.ToDouble(splitInto);


            divisor = Convert.ToInt32(Math.Round(divisor));


            List<T> newSt = new List<T>();
            for (int i = 0; i < (s.Count); i += Convert.ToInt32(divisor))
            {
                newSt = s.GetRange(i, Convert.ToInt32(divisor));
                s.RemoveRange(i, Convert.ToInt32(divisor));
                st.Add(newSt);
            }

           

            return st;
        }


    }


}


