﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Interactivity
{
    public class InteractivityItems : FrameworkElement
    {
        private List<Behavior> _behaviors;
        private List<TriggerBase> _triggers;

        /// <summary>
        /// Storage for triggers
        /// </summary>
        new public List<TriggerBase> Triggers
        {
            get
            {
                if (_triggers == null)
                    _triggers = new List<TriggerBase>();
                return _triggers;
            }
        }

        /// <summary>
        /// Storage for Behaviors
        /// </summary>
        public List<Behavior> Behaviors
        {
            get
            {
                if (_behaviors == null)
                    _behaviors = new List<Behavior>();
                return _behaviors;
            }
        }

        #region Template attached property

        public static InteractivityTemplate GetTemplate(DependencyObject obj)
        {
            return (InteractivityTemplate)obj.GetValue(TemplateProperty);
        }

        public static void SetTemplate(DependencyObject obj, InteractivityTemplate value)
        {
            obj.SetValue(TemplateProperty, value);
        }

        public static readonly DependencyProperty TemplateProperty =
            DependencyProperty.RegisterAttached("Template",
            typeof(InteractivityTemplate),
            typeof(InteractivityItems),
            new PropertyMetadata(default(InteractivityTemplate), OnTemplateChanged));

        private static void OnTemplateChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            InteractivityTemplate dt = (InteractivityTemplate)e.NewValue;
#if(!SILVERLIGHT)
            dt.Seal();
#endif
            InteractivityItems ih = (InteractivityItems)dt.LoadContent();
            BehaviorCollection bc = Interaction.GetBehaviors(d);
            TriggerCollection tc = Interaction.GetTriggers(d);

            foreach (Behavior behavior in ih.Behaviors)
                bc.Add(behavior);

            foreach (TriggerBase trigger in ih.Triggers)
                tc.Add(trigger);


        }

        #endregion
    }
}
