﻿using Cauldron.Localization;
using System;

namespace Cauldron.Consoles
{
    /// <summary>
    /// Specifies that the property is a parameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class ParameterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterAttribute"/>
        /// </summary>
        /// <param name="description">
        /// A short description of the parameter. A description paragraph can be highlightened by adding !! at the beginning of the line.
        /// <para/>
        /// If an implementation of <see cref="ILocalizationSource"/> exist, the parser will use <paramref name="description"/> as a key for <see cref="Locale"/>
        /// </param>
        /// <param name="isRequired">Indicates if the parameter is mandatory</param>
        /// <param name="parameters">A list of parameters and aliases</param>
        public ParameterAttribute(string description, bool isRequired, params string[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
                throw new ArgumentException("parameters cannot be null or empty");

            this.Parameters = parameters;
            this.IsRequired = isRequired;
            this.Description = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterAttribute"/>
        /// </summary>
        /// <param name="description">
        /// A short description of the parameter. A description paragraph can be highlightened by adding !! at the beginning of the line.
        /// <para/>
        /// If an implementation of <see cref="ILocalizationSource"/> exist, the parser will use <paramref name="description"/> as a key for <see cref="Locale"/>
        /// </param>
        /// <param name="parameters">A list of parameters and aliases</param>
        public ParameterAttribute(string description, params string[] parameters) : this(description, false, parameters)
        {
        }

        /// <summary>
        /// Gets the description of the parameter
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets a value that indicates if the parameter mandatory or not
        /// </summary>
        public bool IsRequired { get; private set; }

        /// <summary>
        /// Gets an array of parameters and aliases
        /// </summary>
        public string[] Parameters { get; private set; }
    }
}