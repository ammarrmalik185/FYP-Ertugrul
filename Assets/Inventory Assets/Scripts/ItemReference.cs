using System;
using System.IO;
using Inventory_Assets.Items.Interfaces_and_Enums;
using Inventory_Assets.Items.Swords._0_defaultSword;
using UnityEngine;

namespace Inventory_Assets.Scripts{
    public class ItemReference : MonoBehaviour
    {
        public IItem reference{ get; set; }
    }
}
