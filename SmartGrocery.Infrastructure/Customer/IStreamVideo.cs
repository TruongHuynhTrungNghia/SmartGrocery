using Amazon;
using Amazon.KinesisVideo;
using Amazon.Runtime;

namespace SmartGrocery.Infrastructure.Customer
{
    public class IStreamVideo : AmazonKinesisVideoClient
    {
        public IStreamVideo(AWSCredentials credentials, RegionEndpoint region)
            : base(credentials, region)
        {
        }

        private AWSCredentials credentials;

        private RegionEndpoint region;
    }
}