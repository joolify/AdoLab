using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DBContactLibrary.Models;

namespace DBContactLibrary
{
    public static class SQLRepository
    {
        private static string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBContact;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /*********************************************
         * PUBLIC CREATE
         *********************************************/
        public static int CreateContact(string ssn, string firstName, string lastName)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
            new SqlParameter("@ssn", ssn),
            new SqlParameter("@firstName", firstName),
            new SqlParameter("@lastName", lastName)
            };
            return CreateRecord("CreateContact", sqlParameters);
        }

        public static int CreateAddress(string street, string city, string zip)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
            new SqlParameter("@street", street),
            new SqlParameter("@city", city),
            new SqlParameter("@zip", zip)
            };
            return CreateRecord("CreateAddress", sqlParameters);
        }

        public static int CreateContactInformation(string info, int? contactID)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("@info", info),
                new SqlParameter("@contactId", contactID),
            };
            return CreateRecord("CreateContactInformation", sqlParameters);
        }

        public static int CreateContactToAddress(int contactID, int addressID)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("@contactId", contactID),
                new SqlParameter("@addressId", addressID),
            };
            return CreateRecord("CreateContactToAddress", sqlParameters);

        }
        /*********************************************
         * PUBLIC READ
         *********************************************/

        public static Contact ReadContact(int ID)
        {
            return ReadRecord(ID, "ReadContact", CreateContObj);
        }
        public static Address ReadAddress(int ID)
        {
            return ReadRecord(ID, "ReadAddress", CreateAddressObj);
        }
        public static ContactInformation ReadContactInformation(int ID)
        {
            return ReadRecord(ID, "ReadContactInformation", CreateContactInformationObj);
        }
        public static ContactToAddress ReadContactToAddress(int ID)
        {
            return ReadRecord(ID, "ReadContactToAddress", CreateContactToAddressObj);
        }
        public static List<Contact> ReadAllContacts()
        {
            return ReadAllRecords("Contact", CreateContObj);
        }
        public static List<Address> ReadAllAddresses()
        {
            return ReadAllRecords("Address", CreateAddressObj);
        }
        public static List<ContactInformation> ReadAllContactInformations()
        {
            return ReadAllRecords("ContactInformation", CreateContactInformationObj);
        }
        public static List<ContactToAddress> ReadAllContactsToAddresses()
        {
            return ReadAllRecords("ContactToAddress", CreateContactToAddressObj);
        }

        /*********************************************
         * PUBLIC UPDATE
         *********************************************/
        public static bool UpdateContact(int id, string ssn, string firstName, string lastName)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@ssn", ssn),
                    new SqlParameter("@firstName", firstName),
                    new SqlParameter("@lastName", lastName)
            };
            return UpdateRecord("UpdateContact", sqlParameters);
        }
        public static bool UpdateAddress(int id, string street, string city, string zip)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@street", street),
                    new SqlParameter("@city", city),
                    new SqlParameter("@zip", zip)
            };
            return UpdateRecord("UpdateAddress", sqlParameters);
        }
        public static bool UpdateContactInformation(int id, string info, int contactID)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@info", info),
                    new SqlParameter("@contactId", contactID),
            };
            return UpdateRecord("UpdateContactInformation", sqlParameters);
        }
        public static bool UpdateContactToAddress(int id, int contactID, int addressID)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@contactId", contactID),
                    new SqlParameter("@addressId", addressID)
            };
            return UpdateRecord("UpdateContactToAddress", sqlParameters);
        }

        /*********************************************
         * PUBLIC DELETE
         *********************************************/
        public static bool DeleteContact(int id)
        {
            return DeleteRecord(id, "DeleteContact");
        }
        public static bool DeleteAddress(int id)
        {
            return DeleteRecord(id, "DeleteAddress");
        }
        public static bool DeleteContactInformation(int id)
        {
            return DeleteRecord(id, "DeleteContactInformation");
        }

        public static bool DeleteContactToAddress(int id)
        {
            return DeleteRecord(id, "DeleteContactToAddress");
        }


        public static bool DeleteAllContacts()
        {
            return DeleteAllRecords("Contact");
        }

        public static bool DeleteAllAddresses()
        {
            return DeleteAllRecords("Address");
        }
        public static bool DeleteAllContactInformations()
        {
            return DeleteAllRecords("ContactInformation");
        }
        public static bool DeleteAllContactsToAddresses()
        {
            return DeleteAllRecords("ContactToAddress");
        }
        /*********************************************
         * PUBLIC ENTITIES
         *********************************************/
        public static ContactEntity ReadContactEntity(int ID)
        {
            Contact contact = ReadContact(ID);
            ContactEntity contactEntity = new ContactEntity
            {
                ID = contact.ID,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                SSN = contact.SSN,
                Addresses = ReadAllContactsToAddresses()?
                    .Where(c2a => c2a.ContactID == ID)
                    .Select(c2a => ReadAddress(c2a.AddressID))
                    .ToList(),
                ContactInformations = ReadAllContactInformations()?
                    .Where(ci => ci.ContactID == ID).ToList()
            };

            return contactEntity;
        }

        public static List<ContactEntity> ReadAllContactEntities()
        {
            return ReadAllContacts()?.Select(c => ReadContactEntity(c.ID)).ToList();
        }

        public static AddressEntity ReadAddressEntity(int ID)
        {
            Address address = ReadAddress(ID);
            AddressEntity addressEntity = new AddressEntity
            {
                ID = address.ID,
                Street = address.Street,
                City = address.City,
                Zip = address.Zip,
                Contacts = ReadAllContactsToAddresses()
                    .Where(c2a => c2a.AddressID == ID)
                    .Select(c2a => ReadContact(c2a.ContactID))
                    .ToList()
            };

            return addressEntity;
        }

        public static List<AddressEntity> ReadAllAddressEntities()
        {
            List<Address> addresses = ReadAllAddresses();
            return addresses?.Select(a => ReadAddressEntity(a.ID)).ToList();
        }
        /*********************************************
         * PRIVATE CREATE
         *********************************************/
        private static Contact CreateContObj(SqlDataReader reader)
        {
            return new Contact
            {
                ID = (int)reader["ID"],
                SSN = (string)reader["SSN"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"]
            };
        }

        private static Address CreateAddressObj(SqlDataReader reader)
        {
            return new Address
            {
                ID = (int)reader["ID"],
                Street = (string)reader["Street"],
                City = (string)reader["City"],
                Zip = (string)reader["Zip"]
            };
        }
        private static ContactInformation CreateContactInformationObj(SqlDataReader reader)
        {
            return new ContactInformation
            {
                ID = (int)reader["ID"],
                Info = (string)reader["Info"],
                ContactID = reader["ContactId"] as int?
            };
        }

        private static ContactToAddress CreateContactToAddressObj(SqlDataReader reader)
        {
            return new ContactToAddress
            {
                ID = (int)reader["ID"],
                ContactID = (int)reader["ContactId"],
                AddressID = (int)reader["AddressId"]
            };
        }

        private static int CreateRecord(string procedure, List<SqlParameter> sqlParameters)
        {
            sqlParameters.Select(p => p.Value ?? (p.Value = DBNull.Value));

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = procedure;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlId = new SqlParameter("@ID", SqlDbType.Int) { Direction = ParameterDirection.Output };

                    sqlParameters.Add(sqlId);

                    command.Parameters.AddRange(sqlParameters.ToArray());

                    int returnValue;
                    try
                    {
                        returnValue = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                        return 0;
                    }
                    connection.Close();

                    if (returnValue > 0)
                        return int.Parse(sqlId.Value.ToString());

                    return 0;
                }
            }
        }

        /*********************************************
         * PRIVATE READ
         *********************************************/
        private static TOut ReadRecord<TOut>(int ID, string procedure, Func<SqlDataReader, TOut> createObject)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = procedure;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlId = new SqlParameter("@ID", ID);

                    command.Parameters.Add(sqlId);

                    SqlDataReader reader;
                    try
                    {
                        reader = command.ExecuteReader();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                        return default(TOut);
                    }

                    if (reader.Read())
                    {
                        var obj = createObject(reader);
                        connection.Close();
                        return obj;
                    }

                    connection.Close();
                    return default(TOut);
                }
            }
        }

        private static List<TOut> ReadAllRecords<TOut>(string table, Func<SqlDataReader, TOut> createObject)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = $"select * from {table} order by id";
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    SqlDataReader reader;
                    try
                    {
                        reader = command.ExecuteReader();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                        return null;
                    }

                    if (reader.HasRows)
                    {
                        List<TOut> list = new List<TOut>();
                        while (reader.Read())
                        {
                            var obj = createObject(reader);
                            list.Add(obj);
                        }

                        connection.Close();
                        return list;
                    }
                    connection.Close();
                    return null;
                }
            }
        }

        /*********************************************
         * PRIVATE UPDATE
         *********************************************/
        private static bool UpdateRecord(string procedure, List<SqlParameter> sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = procedure;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddRange(sqlParameters.ToArray());

                    int returnValue;
                    try
                    {
                        returnValue = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                        return false;
                    }
                    connection.Close();

                    return returnValue > 0;
                }
            }
        }

        /*********************************************
          PRIVATE DELETE
         *********************************************/
        private static bool DeleteRecord(int id, string procedure)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = procedure;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection = connection;

                    SqlParameter sqlId = new SqlParameter("@id", id);

                    command.Parameters.Add(sqlId);

                    int returnValue;
                    try
                    {
                        returnValue = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                        return false;
                    }
                    connection.Close();

                    return returnValue > 0;
                }
            }
        }

        private static bool DeleteAllRecords(string table)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = $"delete from {table}";
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    int returnValue;
                    try
                    {
                        returnValue = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                        return false;
                    }
                    connection.Close();

                    return returnValue > 0;
                }
            }
        }
    }
}

