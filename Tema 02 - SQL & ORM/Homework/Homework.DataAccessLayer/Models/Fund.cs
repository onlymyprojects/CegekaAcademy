using Homework.Common.Enums;

namespace Homework.DataAccessLayer.Models
{
    public class Fund : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Target { get; set; }
        public int? OwnerId { get; set; }

        public Person Owner { get; set; }
    }
}
