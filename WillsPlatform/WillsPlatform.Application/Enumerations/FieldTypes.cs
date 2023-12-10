namespace WillsPlatform.Application.Enumerations
{
    public sealed class FieldTypes
    {
        public static readonly FieldTypes Textbox = new("Textbox", 1);
        public static readonly FieldTypes Dropdown = new("Dropdown", 2);
        public static readonly FieldTypes RadioButton = new("RadioButton", 3);
        public static readonly FieldTypes Checkbox = new("Checkbox", 4);
        public static readonly FieldTypes MultilineTextbox = new("MultilineTextbox", 5);
        public static readonly FieldTypes DateTime = new("DateTime", 6);

        public string Name { get; }
        public int Value { get; }

        private FieldTypes(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public static IEnumerable<FieldTypes> Values
        {
            get
            {
                yield return Textbox;
                yield return Dropdown;
                yield return RadioButton;
                yield return Checkbox;
                yield return MultilineTextbox;
                yield return DateTime;
            }
        }
    }
}
