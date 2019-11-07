from google.cloud import storage
import os
from pathlib import Path

os.environ["GOOGLE_APPLICATION_CREDENTIALS"]="C:/Users/Ben/Documents/Github/hackisu19/nodething/CardboardCameraOculusViewer-65da6418c5d5.json"
client = storage.Client()
bucket = client.get_bucket('cardboardcameraoculusviewer.appspot.com')
# posting to firebase storage
imageBlob = bucket.blob("/")
imagePath = "C:/Users/Ben/Desktop/breakCore/core_right.jpg"
imageBlob = bucket.blob("core_right.jpg")
imageBlob.upload_from_filename(imagePath)
imagePath = "C:/Users/Ben/Desktop/breakCore/core_audio.mp3"
imageBlob = bucket.blob("core_audio.mp3")
imageBlob.upload_from_filename(imagePath)