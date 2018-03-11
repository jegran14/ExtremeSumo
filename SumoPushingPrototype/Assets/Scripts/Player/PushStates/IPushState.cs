using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushState {
    void UpdateState();
    void FixedUpdateState();
}
