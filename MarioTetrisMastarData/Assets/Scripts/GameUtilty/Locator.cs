using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Utility
{
    public class Locator<T>
    {
        private static Dictionary<int, T> dic = new Dictionary<int, T>(1);

        public static void Bind(T Sarbis, int id = 0)
        {
            dic[id] = Sarbis;
        }

        public static void UnBind(int id = 0)
        {
            dic[id] = default;
        }

        public static void UnBindAll()
        {
            dic.Clear();
        }

        public static T GetT(int id)
        {
            if (!dic.ContainsKey(id)) return default;
            return dic[id];
        }
    }

}