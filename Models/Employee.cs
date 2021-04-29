using System;
public class Employee
{
    public long id { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string title { get; set; }
    public string email { get; set; }
    public DateTime? created_at { get; set; }
    public DateTime? updated_at { get; set; }
    public long? user_id { get; set; }
}