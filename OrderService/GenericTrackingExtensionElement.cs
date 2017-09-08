using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel.Configuration;
using System.Configuration;
using System.Activities.Tracking;

namespace OrderService
{
    public class GenericTrackingExtensionElement : BehaviorExtensionElement
    {
        [ConfigurationProperty("profileName", DefaultValue = null, IsKey = false, IsRequired = false)]
        public string ProfileName
        {
            get { return (string) this["profileName"]; }
            set { this["profileName"] = value; }
        }

        [ConfigurationProperty("participantTypeName", DefaultValue = null, IsKey = false, IsRequired = false)]
        public string ParticipantTypeName
        {
            get { return (string)this["participantTypeName"]; }
            set { this["participantTypeName"] = value; }
        }

        public override Type BehaviorType
        {
            get { return typeof(GenericTrackingBehavior); }
        }

        protected override object CreateBehavior()
        {
            throw new NotImplementedException();
        }
    }
}