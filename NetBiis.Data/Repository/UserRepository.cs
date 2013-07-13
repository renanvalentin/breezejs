using NetBiis.Data.Contracts;
using NetBiis.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace NetBiis.Data
{
    public class UserRepository : IUserRepository
    {
        private DbContext _ctx { get; set; }

        public UserRepository()
        {
            _ctx = new NetBiisDbContext();
        }

        public bool VerifyExintingEmail(string email)
        {
            var parameter = new SqlParameter("@EMAIL", SqlDbType.Text);
            parameter.Value = email;

            var count = _ctx.Database.SqlQuery<int>("exec VerifyExintingEmail @EMAIL", parameter).FirstOrDefault();

            return count > 0;
        }

        public int Save(User user)
        {
            var connectionString = _ctx.Database.Connection.ConnectionString;
            var numberOfRow = 0;

            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    var parameters = new[] {
                        new SqlParameter("@PIN", SqlDbType.Text) { 
                            Value = user.PIN 
                        },
                        new SqlParameter("@CompanyName", SqlDbType.Text){ 
                            Value = user.CompanyName 
                        },
                        new SqlParameter("@Email", SqlDbType.Text){ 
                            Value = user.Email 
                        },
                        new SqlParameter("@Password", SqlDbType.Text){ 
                            Value = user.Password 
                        },
                        new SqlParameter("@ReceivePromotions", SqlDbType.Bit){ 
                            SqlValue = user.ReceivePromotions 
                        },
                        new SqlParameter("@CallSpecialist", SqlDbType.Bit){ 
                            SqlValue = user.CallSpecialist 
                        },
                        new SqlParameter("@Number", SqlDbType.Int){ 
                            SqlValue = user.Adress.Number 
                        },
                        new SqlParameter("@StreetName", SqlDbType.Text){ 
                            Value = user.Adress.StreetName 
                        },
                        new SqlParameter("@Suite", SqlDbType.Text){ 
                            Value = user.Adress.Suite 
                        },
                        new SqlParameter("@City", SqlDbType.Text){ 
                            Value = user.Adress.City 
                        },
                        new SqlParameter("@State", SqlDbType.Text){ 
                            Value = user.Adress.State 
                        },
                        new SqlParameter("@Zipcode", SqlDbType.Text){ 
                            Value = user.Adress.Zipcode 
                        },
                        new SqlParameter("@MainContact", SqlDbType.Text){ 
                            Value = user.Contact.MainContact 
                        },
                        new SqlParameter("@Position", SqlDbType.Text){ 
                            Value = user.Contact.Position
                        },
                        new SqlParameter("@Phone", SqlDbType.Text){ 
                            Value = user.Contact.Phone 
                        }
                    };


                    var command = new SqlCommand("InsertUser", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);

                    numberOfRow= command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }
            }

            return numberOfRow;
        }
    }
}
