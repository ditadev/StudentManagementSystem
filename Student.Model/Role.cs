namespace Student.Model;

public class Role
{
    public Enums.Role Id { get; set; }
    public List<User> Users { get; set; }
}