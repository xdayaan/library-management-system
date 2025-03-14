using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;


namespace LMS.utils
{
    public class UploadToS3
    {
        public static string UploadFile(IFormFile file)
        {
            var bucketName = Environment.GetEnvironmentVariable("S3_BUCKET_NAME");
            var awsRegion = Environment.GetEnvironmentVariable("AWS_REGION"); 
            var s3Region = RegionEndpoint.GetBySystemName(awsRegion);

            // Generate a new file name with UUID
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            using (var client = new AmazonS3Client(s3Region))
            using (var transferUtility = new TransferUtility(client))
            using (var stream = file.OpenReadStream())
            {
                var request = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    InputStream = stream,
                    Key = uniqueFileName,
                    ContentType = file.ContentType
                };

                transferUtility.Upload(request);
            }

            return $"https://{bucketName}.s3.{awsRegion}.amazonaws.com/{uniqueFileName}";
        }
    }
}