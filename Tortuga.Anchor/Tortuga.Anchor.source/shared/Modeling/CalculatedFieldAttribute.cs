﻿using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Tortuga.Anchor.Modeling
{
    /// <summary>
    /// Apply this attribute to indicate which properties affect this calculated property. 
    /// Classes that inherit from ModelBase will use this information to automatically throw the appropriate property changed events.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]

    public sealed class CalculatedFieldAttribute : Attribute
    {
        private readonly ReadOnlyCollection<string> m_SourceProperties;

        private readonly string m_Sources;

        /// <summary>
        /// Apply this attribute to indicate which properties affect this calculated property. 
        /// Classes that inherit from ModelBase will use this information to automatically throw the appropriate property changed events.
        /// </summary>
        /// <param name="sources">Comma separated list of properties that, when changed, invalidate this property. Whitespace will be trimmed.</param>

        public CalculatedFieldAttribute(string sources)
        {
            if (string.IsNullOrEmpty(sources))
                throw new ArgumentException($"{nameof(sources)} is null or empty.", nameof(sources));

            m_Sources = sources;
            m_SourceProperties = new ReadOnlyCollection<string>((from s in sources.Split(',') select s.Trim()).ToArray());
        }

        /// <summary>
        /// List of properties that, when changed, invalidate this property
        /// </summary>
        public ReadOnlyCollection<string> SourceProperties
        {
            get { return m_SourceProperties; }
        }

        /// <summary>
        /// Comma separated list of properties that, when changed, invalidate this property
        /// </summary>
        public string Sources
        {
            get { return m_Sources; }
        }

    }
}
