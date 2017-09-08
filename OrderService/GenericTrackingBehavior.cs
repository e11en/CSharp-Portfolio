using System;
using System.Activities.Tracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Activities.Tracking.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Web;
using System.Web.Configuration;

namespace OrderService
{
    public class GenericTrackingBehavior : IServiceBehavior
    {
        private string trackingProfileName { get; set; }
        public string ParticipantTypeName { get; set; }

        public GenericTrackingBehavior(string profileName, string participantTypeName)
        {
            try
            {
                ParticipantTypeName = participantTypeName;
                trackingProfileName = profileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            var workflowServiceHost = serviceHostBase as WorkflowServiceHost;

            try
            {
                if (workflowServiceHost != null)
                {
                    var workflowDisplayName = workflowServiceHost.Activity.DisplayName;
                    var trackingProfile = GetTrackingProfileFromConfig(trackingProfileName, workflowDisplayName);
                    var participant = Activator.CreateInstance(Type.GetType(ParticipantTypeName));
                    var tp = participant as TrackingParticipant;
                    tp.TrackingProfile = trackingProfile;
                    workflowServiceHost.WorkflowExtensions.Add(tp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        TrackingProfile GetTrackingProfileFromConfig(string profileName, string displayName)
        {
            TrackingProfile trackingProfile = null;
            var trackingSection = (TrackingSection) WebConfigurationManager.GetSection("system.serviceModel/tracking");
            if (trackingSection == null)
            {
                return null;
            }

            var match = from p in new List<TrackingProfile>(trackingSection.TrackingProfiles)
                        where p.Name == profileName
                        select p;

            if (!match.Any())
            {
                throw new ConfigurationErrorsException(string.Format("Could not find a profile with name {0}",
                    profileName));
            }

            trackingProfile = match.First() ?? new TrackingProfile
            {
                ActivityDefinitionId = displayName
            };

            return trackingProfile;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public virtual void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {  
        }


    }
}