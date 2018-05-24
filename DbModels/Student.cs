namespace SelectFromDb.DbModels
{
    public class Student
    {
        public int Id { get; set; }
        public string Navn { get; set; }

        public override string ToString()
        {
            return $"{Navn} (id={Id})";
        }
    }
}
