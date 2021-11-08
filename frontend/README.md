# Frontend

## Stack of technologies
- React Native 
- Typescript
- NPM
- Jest

## Set up
In order to run the mobile application, we need to setup some tools first. The instructions are different for Mac and Windows operating systems.

1. Open [official docs](https://reactnative.dev/docs/environment-setup). Follow the "React Native CLI Quickstart" guide with "Development OS" selected as your machine's OS and "Target OS" as iOS(macOS only) or Android(macOS and Windows). Complete all the steps until "Creating a new application" section.
2. Clone this repository.
3. To navigate to frontend source directory:
```bash
cd se-wasted-app/frontend/wasted-app
``` 

4. To install the dependencies to your local `node_modules` directory.
```bash
npm install
``` 
   1. FOR IOS DEVELOPMENT: 
   ```bash
   cd ios && pod install && cd ..
   ```

# Run our application in dev mode
1. For Android development:
```bash
npm run android
```
2. For IOS development:
```bash
npm run ios
```
3. Using expo. Use this command to open Metro server, which bundles the application and allows you to run the app on on your physical/emulated device. Keep in mind that you need to have the development app already installed on the device for the bundler to work. (with this command you can also run the app on your physical device) :
```bash
npm run start
``` 