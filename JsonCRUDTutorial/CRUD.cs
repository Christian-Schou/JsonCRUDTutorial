using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace JsonCRUDTutorial
{
    public class CRUD
    {
        public static void GetEmployeeDetails(string jsonFilePath)
        {
            var json = File.ReadAllText(jsonFilePath);
            try
            {
                var jObject = JObject.Parse(json);

                if (jObject != null)
                {
                    Console.WriteLine("ID :" + jObject["id"].ToString());
                    Console.WriteLine("Full Name :" + jObject["fullname"].ToString());

                    var address = jObject["address"];
                    Console.WriteLine("Street :" + address["street"].ToString());
                    Console.WriteLine("City :" + address["city"].ToString());
                    Console.WriteLine("Zip :" + address["zip"]);
                    JArray jobsArray = (JArray)jObject["jobs"];
                    if (jobsArray != null)
                    {
                        foreach (var job in jobsArray)
                        {
                            Console.WriteLine("Job ID :" + job["jobid"]);
                            Console.WriteLine("Company Name :" + job["company"].ToString());
                        }

                    }
                    Console.WriteLine("Phone Number :" + jObject["phone"].ToString());
                    Console.WriteLine("Role :" + jObject["current-job-role"].ToString());

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not read JSON data. More details: ", ex.Message);
                throw;
            }
        }

        public static void AddJob(string jsonFilePath)
        {
            Console.WriteLine("Enter Job ID : ");
            var jobId = Console.ReadLine();
            Console.WriteLine("\nEnter Job Name : ");
            var companyName = Console.ReadLine();

            var newJobMember = "{ 'jobid': " + jobId + ", 'company': '" + companyName + "'}";
            try
            {
                var json = File.ReadAllText(jsonFilePath);
                var jsonObj = JObject.Parse(json);
                var jobArrary = jsonObj.GetValue("jobs") as JArray;
                var newJob = JObject.Parse(newJobMember);
                jobArrary.Add(newJob);

                jsonObj["jobs"] = jobArrary;
                string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj,
                                       Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(jsonFilePath, newJsonResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Add Error : " + ex.Message.ToString());
            }
        }

        public static void UpdateJob(string jsonFilePath)
        {
            string json = File.ReadAllText(jsonFilePath);

            try
            {
                var jObject = JObject.Parse(json);
                JArray jobsArrary = (JArray)jObject["jobs"];
                Console.Write("Enter Job ID to Update Job : ");
                var jobId = Convert.ToInt32(Console.ReadLine());

                if (jobId > 0)
                {
                    Console.Write("Enter new company name : ");
                    var companyName = Convert.ToString(Console.ReadLine());

                    foreach (var company in jobsArrary.Where(obj => obj["jobid"].Value<int>() == jobId))
                    {
                        company["company"] = !string.IsNullOrEmpty(companyName) ? companyName : "";
                    }

                    jObject["jobs"] = jobsArrary;
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFilePath, output);
                }
                else
                {
                    Console.Write("Invalid Job ID, Try Again!");
                    UpdateJob(jsonFilePath);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Update Error : " + ex.Message.ToString());
            }
        }

        public static void DeleteJob(string jsonFilePath)
        {
            var json = File.ReadAllText(jsonFilePath);
            try
            {
                var jObject = JObject.Parse(json);
                JArray jobsArrary = (JArray)jObject["jobs"];
                Console.Write("Enter Job ID to delete Job : ");
                var jobId = Convert.ToInt32(Console.ReadLine());

                if (jobId > 0)
                {
                    var jobName = string.Empty;
                    var jobToBeDeleted = jobsArrary.FirstOrDefault(obj => obj["jobid"].Value<int>() == jobId);

                    jobsArrary.Remove(jobToBeDeleted);

                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFilePath, output);
                }
                else
                {
                    Console.Write("Invalid Job ID, Try Again.");
                    UpdateJob(jsonFilePath);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
