using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DBContactLibrary.Models;

namespace DBContactLibrary
{
    public class SQLRepository
    {
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBContact;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /*********************************************
         * PUBLIC CREATE
         *********************************************/
        public int CreateContact(string ssn, string firstName, string lastName)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
            new SqlParameter("@ssn", ssn),
            new SqlParameter("@firstName", firstName),
            new SqlParameter("@lastName", lastName)
            };
            return CreateRecord("CreateContact", sqlParameters);
        }

        public int CreateAddress(string street, string city, string zip)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
            new SqlParameter("@street", street),
            new SqlParameter("@city", city),
            new SqlParameter("@zip", zip)
            };
            return CreateRecord("CreateAddress", sqlParameters);
        }

        public int CreateContactInformation(string info, int? contactID)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("@info", info),
                new SqlParameter("@contactId", contactID),
            };
            return CreateRecord("CreateContactInformation", sqlParameters);
        }

        public int CreateContactToAddress(int contactID, int addressID)
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

        public Contact ReadContact(int ID)
        {
            return ReadRecord(ID, "ReadContact", CreateContObj);
        }
        public Address ReadAddress(int ID)
        {
            return ReadRecord(ID, "ReadAddress", CreateAddressObj);
        }
        public ContactInformation ReadContactInformation(int ID)
        {
            return ReadRecord(ID, "ReadContactInformation", CreateContactInformationObj);
        }
        public ContactToAddress ReadContactToAddress(int ID)
        {
            return ReadRecord(ID, "ReadContactToAddress", CreateContactToAddressObj);
        }
        public List<Contact> ReadAllContacts()
        {
            return ReadAllRecords("Contact", CreateContObj);
        }
        public List<Address> ReadAllAddresses()
        {
            return ReadAllRecords("Address", CreateAddressObj);
        }
        public List<ContactInformation> ReadAllContactInformations()
        {
            return ReadAllRecords("ContactInformation", CreateContactInformationObj);
        }
        public List<ContactToAddress> ReadAllContactsToAddresses()
        {
            return ReadAllRecords("ContactToAddress", CreateContactToAddressObj);
        }

        /*********************************************
         * PUBLIC UPDATE
         *********************************************/
        public bool UpdateContact(int id, string ssn, string firstName, string lastName)
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
        public bool UpdateAddress(int id, string street, string city, string zip)
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
        public bool UpdateContactInformation(int id, string info, int contactID)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                    new SqlParameter("@ID", id),
                    new SqlParameter("@info", info),
                    new SqlParameter("@contactId", contactID),
            };
            return UpdateRecord("UpdateContactInformation", sqlParameters);
        }
        public bool UpdateContactToAddress(int id, int contactID, int addressID)
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
        public bool DeleteContact(int id)
        {
            return DeleteRecord(id, "DeleteContact");
        }
        public bool DeleteAddress(int id)
        {
            return DeleteRecord(id, "DeleteAddress");
        }
        public bool DeleteContactInformation(int id)
        {
            return DeleteRecord(id, "DeleteContactInformation");
        }

        public bool DeleteContactToAddress(int id)
        {
            return DeleteRecord(id, "DeleteContactToAddress");
        }


        public bool DeleteAllContacts()
        {
            return DeleteAllRecords("Contact");
        }

        public bool DeleteAllAddresses()
        {
            return DeleteAllRecords("Address");
        }
        public bool DeleteAllContactInformations()
        {
            return DeleteAllRecords("ContactInformation");
        }
        public bool DeleteAllContactsToAddresses()
        {
            return DeleteAllRecords("ContactToAddress");
        }
        /*********************************************
         * PUBLIC ENTITIES
         *********************************************/
        public ContactEntity ReadContactEntity(int ID)
        {
            Contact contact = ReadContact(ID);
            ContactEntity contactEntity = new ContactEntity
            {
                ID = contact.ID,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                SSN = contact.SSN
            };

            List<ContactToAddress> contactToAddresses = ReadAllContactsToAddresses();
            contactEntity.Addresses = contactToAddresses.Where(c2a => c2a.ContactID == ID).Select(c2a => ReadAddress(c2a.AddressID)).ToList();
            contactEntity.ContactInformations = ReadAllContactInformations().Where(ci => ci.ContactID == ID).ToList();

            return contactEntity;
        }

        public List<ContactEntity> ReadAllContactEntities()
        {
            List<Contact> contacts = ReadAllContacts();
            return contacts.Select(c => ReadContactEntity(c.ID)).ToList();
        }

        public AddressEntity ReadAddressEntity(int ID)
        {
            Address address = ReadAddress(ID);
            AddressEntity addressEntity = new AddressEntity
            {
                ID = address.ID,
                Street = address.Street,
                City = address.City,
                Zip =  address.Zip
            };

            List<ContactToAddress> contactToAddresses = ReadAllContactsToAddresses();
            addressEntity.Contacts = contactToAddresses.Where(c2a => c2a.AddressID == ID).Select(c2a => ReadContact(c2a.ContactID)).ToList();

            return addressEntity;
        }

        public List<AddressEntity> ReadAllAddressEntities()
        {
            List<Address> addresses = ReadAllAddresses();
            return addresses.Select(a => ReadAddressEntity(a.ID)).ToList();
        }
        /*********************************************
         * PRIVATE CREATE
         *********************************************/
        private Contact CreateContObj(SqlDataReader reader)
        {
            return new Contact
            {
                ID = (int)reader["ID"],
                SSN = (string)reader["SSN"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"]
            };
        }

        private Address CreateAddressObj(SqlDataReader reader)
        {
            return new Address
            {
                ID = (int)reader["ID"],
                Street = (string)reader["Street"],
                City = (string)reader["City"],
                Zip = (string)reader["Zip"]
            };
        }
        private ContactInformation CreateContactInformationObj(SqlDataReader reader)
        {
            return new ContactInformation
            {
                ID = (int)reader["ID"],
                Info = (string)reader["Info"],
                ContactID = reader["ContactId"] as int?
            };
        }

        private ContactToAddress CreateContactToAddressObj(SqlDataReader reader)
        {
            return new ContactToAddress
            {
                ID = (int)reader["ID"],
                ContactID = (int)reader["ContactId"],
                AddressID = (int)reader["AddressId"]
            };
        }

        private int CreateRecord(string procedure, List<SqlParameter> sqlParameters)
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

                    SqlParameter sqlId = new SqlParameter("@ID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    sqlParameters.Add(sqlId);

                    command.Parameters.AddRange(sqlParameters.ToArray());

                    int returnValue = command.ExecuteNonQuery();
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
        private TOut ReadRecord<TOut>(int ID, string procedure, Func<SqlDataReader, TOut> createObject)
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

                    SqlDataReader reader = command.ExecuteReader();

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

        private List<TOut> ReadAllRecords<TOut>(string table, Func<SqlDataReader, TOut> createObject)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = $"select * from {table} order by id";
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();

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
        private bool UpdateRecord(string procedure, List<SqlParameter> sqlParameters)
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

                    int returnValue = command.ExecuteNonQuery();
                    connection.Close();

                    return returnValue > 0;
                }
            }
        }

        /*********************************************
          PRIVATE DELETE
         *********************************************/
        private bool DeleteRecord(int id, string procedure)
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

                    int returnValue = command.ExecuteNonQuery();
                    connection.Close();

                    return returnValue > 0;
                }
            }
        }

        private bool DeleteAllRecords(string table)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = $"delete from {table}";
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;

                    int returnValue = command.ExecuteNonQuery();
                    connection.Close();

                    return returnValue > 0;
               }
            }
        }
    }
}

