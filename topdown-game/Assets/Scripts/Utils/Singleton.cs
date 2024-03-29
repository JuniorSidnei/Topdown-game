﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Utils {
    public class Singleton<T> : MonoBehaviour  where T : Singleton<T> {
        
        private static T s_instance;

        public static T Instance => s_instance != null ? s_instance : (s_instance = FindObjectOfType<T>());
    }
}