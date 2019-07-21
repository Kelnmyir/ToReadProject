using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for Class1
/// </summary>

[DataContract]
public class InventoryRecord
{
    [DataMember]
    public int ID;

    [DataMember]
    public string Make;

    [DataMember]
    public string Color;

    [DataMember]
    public string PetName;
}