var firebase = require("firebase");
var admin = require("firebase-admin");

var config = {
  apiKey: "",
  authDomain: "cardboardcameraoculusviewer.firebaseapp.com",
  storageBucket: "cardboardcameraoculusviewer.appspot.com",
  projectId: "cardboardcameraoculusviewer"
};

firebase.initializeApp(config);

function execute(cmd) {
  const exec = require("child_process").exec;
  return new Promise((resolve, reject) => {
    exec(cmd, (error, stdout, stderr) => {
      if (error) {
        console.warn(error);
      }
      resolve(stdout ? stdout : stderr);
    });
  });
}

async function breakCore() {
  await execute("vrjpeg_example.exe");
}

async function uploadBreaks() {
  await execute("python plol.py");
}

async function convertmp4() {
  await execute("rm c:/Users/Ben/Desktop/breakCore/core_audio.mp3");
  await execute(
    "ffmpeg -i C:/Users/Ben/Desktop/breakCore/core_audio.mp4 C:/Users/Ben/Desktop/breakCore/core_audio.mp3"
  );
}

// Get a reference to the database service
var yepRef = firebase
  .firestore()
  .collection("sure")
  .doc("yep");

yepRef.onSnapshot(async doc => {
  if (doc.data().convertPending) {
    console.log("starting conversion");
    await breakCore();
    await convertmp4();
    console.log("Convert over. Uploading...");
    uploadBreaks();
    console.log("Upload over");

    yepRef
      .update({
        convertPending: false
      })
      .then(() => {
        console.log("done with convert and upload");
      });
  } else {
    console.log("convert turned off");
  }
});
