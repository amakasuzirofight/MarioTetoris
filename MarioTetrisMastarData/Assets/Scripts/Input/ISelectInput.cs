using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Inputer
{
    interface ISelectInput
    {
        public event Action<SelectBottonType> OnSelectButton;
        public event Action<float> MouceWhileEvent;
    }
}
