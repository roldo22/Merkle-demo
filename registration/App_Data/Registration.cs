using System;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for Registration.
/// </summary>
public class Registration
{
    #region Properties

    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public string Country {get; set; }

    #endregion


    public void AddRegistration()
    {
        
        using (db d = new db("spAddUser"))
        {
            d.addParm("@FirstName", FirstName);
            d.addParm("@LastName", LastName);
            d.addParm("@Address1", Address1);
            d.addParm("@Address2", Address2);
            d.addParm("@City", City);
            d.addParm("@State", State);
            d.addParm("@Zip", Zip);
            d.addParm("@Country", Country);            
            d.Exec();
        }        
    }

    public static DataTable Get()
    {
        DataTable dtReturn = new DataTable();
        using (db d = new db("spGetRegistrations"))
        {            
            dtReturn = d.getDataTable;
        }
        return dtReturn;
    }


}

