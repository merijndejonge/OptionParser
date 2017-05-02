using System.Linq;
using System.Reflection;

namespace OpenSoftware.OptionParsing
{
    /// <summary>
    ///     Abstract base class for Option parsing
    /// </summary>
    public abstract class Option
    {
        /// <summary>
        ///     Creates a new instance of the Option class.
        /// </summary>
        protected Option()
        {
            var attr = GetType().GetTypeInfo().GetCustomAttributes<OptionAttribute>(true).SingleOrDefault();
            if(attr != null)
            {
                SetValues(attr);
            }
        }

        /// <summary>
        ///     Specifies the index in the help overview where this option should appear.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        ///     Returns the Name of this option.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Returns the abreviated name of this option.
        /// </summary>
        public string ShortName { get; private set; }

        /// <summary>
        ///     Gets a value indicating if this option is mandatory.
        /// </summary>
        public bool Required { get; private set; }

        /// <summary>
        ///     Gets the default value for this option if any.
        /// </summary>
        public string DefaultValue { get; private set; }

        /// <summary>
        ///     Gets the desription of this option.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        ///     Gets the literal string value of the provided option value.
        /// </summary>
        public virtual string RawValue { get; internal set; }

        /// <summary>
        ///     Indicates whether this option is specified on the command line (or has its default value).
        /// </summary>
        public virtual bool IsDefined => RawValue != null;

        /// <summary>
        /// For enumeration-based options (e.g., SwitchOption and CommandOption) this attribute specifies the enum value to which this option corresponds
        /// </summary>
        public string Enumvalue { get; set; }

        internal void SetValues(OptionAttribute option)
        {
            if(option.DefaultValue != null)
            {
                DefaultValue = option.DefaultValue;
            }
            if(option.Description != null)
            {
                Description = option.Description;
            }
            if(option.Name != null)
            {
                Name = option.Name;
            }
            if(option.IsRequired)
            {
                Required = option.IsRequired;
            }
            if(option.ShortName != null)
            {
                ShortName = option.ShortName;
            }
            if(option.EnumValue != null)
            {
                Enumvalue = option.EnumValue;
            }
            Index = option.Index;
        }
    }

    /// <summary>
    ///     Abtract generic base class for option handling.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Option<T> : Option
    {
        public abstract T Value { get; internal set; }
    }
}