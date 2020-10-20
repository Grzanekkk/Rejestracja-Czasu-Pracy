using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DatabaseConnection
{
    public class User
    {
        public string id, name, firstName, surName, password;

        public DateTime startWorkHour;
        public DateTime finishWorkHour;


        #region Constructors


        public User(string _id, string _firstName, string _surName)
        {
            id = _id;
            name = $"{_firstName} {_surName}";
            firstName = _firstName;
            surName = _surName;

            startWorkHour = DateTime.Today.AddHours(9);
            finishWorkHour = startWorkHour.AddHours(8);
        }


        #endregion Constructors

    }
}
