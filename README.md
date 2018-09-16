# AzureFaceDemo

A demonstration of face recognition in images and videos using Azure Face Recognition cloud services.

![Sample result](/FaceDemo/SampleIdentif.png)

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
- More information about features computed by the Face APIÇ https://azure.microsoft.com/en-us/services/cognitive-services/face/
