namespace StepApp.BotExtensions.HelpCommand
{
    public class HelpCommandDescriptionAttribute : System.Attribute
    {
        public bool ShowDescription { get; set; } 
        public string Description { get; set; }

        public HelpCommandDescriptionAttribute(string description, bool showDescription=true)
        {
            this.Description = description;
            this.ShowDescription = showDescription;
        }
    }
}
