namespace Shelter.Domain.Entities
{
    public class Genus : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }

        public Genus()
        {
            Animals = new HashSet<Animal>();
        }

        public Genus(string name)
        {
            Name = name;
        }
    }
}
