using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{
    // This attribute is used to mark a field as read-only in the inspector.
    // It does not have any functionality by itself, but can be used in conjunction with custom property drawers.
}