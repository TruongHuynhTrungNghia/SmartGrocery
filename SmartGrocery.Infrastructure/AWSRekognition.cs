using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartGrocery.Infrastructure
{
    public class AWSRekognition
    {
        public EmotionalData DetectImage(byte[] image)
        {
            var awsImage = new Image();

            try
            {
                //using (FileStream stream = new FileStream(image, FileMode.Open, FileAccess.Read))
                //{
                //    byte[] data = new byte[stream.Length];
                //    stream.Read(data, 0, (int)stream.Length);
                //    awsImage.Bytes = new MemoryStream(data);
                //}
                awsImage.Bytes = new MemoryStream(image);
            }
            catch (Exception ex)
            {
            }

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(
                "", "", RegionEndpoint.APSoutheast1
                );
            DetectLabelsRequest detectlabelsRequest = new DetectLabelsRequest()
            {
                Image = awsImage,
                MaxLabels = 10,
                MinConfidence = 77F
            };

            var faceAttr = new List<string>();
            //faceAttr.Add("Emotions");
            faceAttr.Add("ALL");

            DetectFacesRequest detectFacesRequest = new DetectFacesRequest
            {
                Attributes = faceAttr,
                Image = awsImage
            };

            try
            {
                //DetectLabelsResponse detectLabelsResponse =
                //rekognitionClient.DetectLabels(detectlabelsRequest);
                //foreach (Label label in detectLabelsResponse.Labels)
                //    Console.WriteLine("{0}: {1}", label.Name, label.Confidence);

                DetectFacesResponse detectFacesResponse = rekognitionClient.DetectFaces(detectFacesRequest);
                if (detectFacesResponse.FaceDetails != null)
                {
                    var customerFace = detectFacesResponse.FaceDetails.OrderByDescending(x => x.Confidence).FirstOrDefault();
                    var customerEmotionData = customerFace.Emotions.OrderByDescending(x => x.Confidence).FirstOrDefault();

                    return new EmotionalData
                    {
                        Emotion = customerEmotionData.Type,
                        Probability = Convert.ToDecimal(customerEmotionData.Confidence) 
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return new EmotionalData();
        }
    }
}
