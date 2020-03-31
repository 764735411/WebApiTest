using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiGetData
{
    [TableName("User")]
    [PrimaryKey("user_id", AutoIncrement = true)]
    public class User
    {
        [Column("user_id")]
        public int Id { get; set; }

        [Column("user_name")]
        public string UserName { get; set; }

        [Column("user_age")]
        public int UserAge { get; set; }

        [Column("user_sex")]
        public string UserSex { get; set; }

        [Column("user_adress")]
        public string UserAdress { get; set; }

        [Column("user_phone")]
        public string UserPhone { get; set; }

    }
}
