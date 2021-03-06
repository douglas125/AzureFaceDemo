# AzureFaceDemo

Face detection in C# using Azure cognitive services.

A demonstration of face detection in images and videos using Azure Face cloud services.

![Sample result](/FaceDemo/SampleIdentif.png)

# What is in this sample

- Face detection from video (webcam capture) or images;
- Histogram of age of detected faces (per gender);
- Emotion count of detected faces (per gender);

# How to use this code

- Download or clone this repository;
- Create a Face API service using the Azure platform;
- Insert your API key in frmFaceAnalytics.cs:

```
//CONFIG - Edit Api Key
const string faceAPIKey = "YOUR_API_KEY";
private readonly IFaceServiceClient faceServiceClient =
        new FaceServiceClient(faceAPIKey, "https://brazilsouth.api.cognitive.microsoft.com/face/v1.0");
```

- Set reqTimeInSeconds to assign how often requests should be made to the API;
- Compile and run the software. Start the video capture with the Play button.
- More information about features computed by the Face API: https://azure.microsoft.com/en-us/services/cognitive-services/face/
