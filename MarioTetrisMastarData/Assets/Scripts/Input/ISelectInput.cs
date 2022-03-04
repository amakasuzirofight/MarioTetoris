using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Inputer
{
    interface ISelectInput
    {
        public event Action<SelectButtonType> OnSelectButton;
        public event Action<float> MouceWhileEvent;
    }
}
