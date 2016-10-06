namespace superhero
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        private string _heroName;
        public string HeroName {get {return _heroName;}}

        public void SetHeroName(){ _heroName = HeroGenerator.GetHeroName(FirstName, LastName);}
        public Person()
        {

        }
    }
}
