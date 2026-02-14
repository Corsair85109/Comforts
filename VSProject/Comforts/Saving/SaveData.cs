using Comforts.Utility;
using Nautilus.Json;
using Nautilus.Json.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Saving
{
    [FileName("ComfortsSaveData")]
    public class SaveData : SaveDataCache
    {
        
    }


    public partial class SaveHandler
    {
        public static void OnSaveStart(object sender, JsonFileEventArgs args)
        {
            
            
        }

        public static void OnLoadFinish(object sender, JsonFileEventArgs args)
        {
            
        }
    }
}
