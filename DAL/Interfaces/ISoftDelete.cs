namespace DAL.Interfaces
{
    internal interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
