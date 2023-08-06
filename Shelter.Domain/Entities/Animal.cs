using Shelter.Domain.Entities.Enums;

namespace Shelter.Domain.Entities
{
    public class Animal : Entity
    {
        public int GenusId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public GenderEnum Gender { get; set; }
        public string Description { get; set; }
        public DateTime AcceptionDate { get; set; }

        public Genus Genus { get; set; }

        public ICollection<Adoption> Adoptions { get; set; }

        public Animal()
        {
            Adoptions = new HashSet<Adoption>();
        }

        public Animal(int id,int genusId, string name, int age, GenderEnum gender, string description, DateTime acceptionDate) : this()
        {
            Id = id;
            GenusId = genusId;
            Name = name;
            Age = age;
            Gender = gender;
            Description = description;
            AcceptionDate = acceptionDate;
        }
    }
}
