/*
* Name: SimpleRandomWalkSO
* Author: Jaewoo Choi
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_ ",menuName = "PCG/SimpleRandomWalkData")]

public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
