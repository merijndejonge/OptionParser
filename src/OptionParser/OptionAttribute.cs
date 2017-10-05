using System;

namespace OpenSoftware.OptionParsing
{
    /// <summary>
    ///     Defines an Option attribute to override parameters of an option.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class OptionAttribute : Attribute
    {
        /// <summary>
        ///     Specifies the index in the help overview where this option should appear.
        /// </summary>
        public int Index { get; set; } = 998;

        /// <summary>
        ///     Specifies the name of the option (e.g., '--output').
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Specifies the abbreviated name of the option (e.g., '-o').
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        ///     Specifies a description for the option.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Indicates whether an option is mandatory or not.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        ///     Specifies a default value for the option.
        /// </summary>
        public string DefaultValue { get; set; }
        /// <summary>
        /// For enumeration-based options (e.g., SwitchOption and CommandOption) this attribute specifies the enum value to which this option corresponds
        /// </summary>
        public string EnumValue { get; set; }
    }
}