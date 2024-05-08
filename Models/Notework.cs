namespace BackNoteWorksTech.Models
{
    public class NoteWork
    {
        public int Id { get; set; }
        public string? Title  { get; set; }
        public string? Content  { get; set; }
        public DateTime UpdateDate { get; set; }
        public string? Status  { get; set; }
        public int CategorieId  { get; set; }
    }
}