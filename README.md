# About

Our application "Wasted" solves the problem of restaurants/markets/shops throwing out tons of good food every day simply because they didn't use it all. We tackled this issue by creating an application where businesses can upload food which they are going to throw in the trash. Users can then see what food items are available near them and go and buy them for a much cheaper price.

During the first iteration we created a Windows Application with .NET frontend and backend. Technologies:

- Frontend: Windows Forms, C#, .NET
- Backend: plain C#, .NET

During the second iteration we are moving our frontend to a cross-platform framework React Native. Technologies:

- Frontend: React Native, Typescript, NPM, Jest
- Backend: C#, ASP.NET

# Setup

## Frontend

In order to run the mobile application, we need to setup some tools first. The instructions are different for Mac and Windows operating systems.

1. Open [official docs](https://reactnative.dev/docs/environment-setup). Follow the "React Native CLI Quickstart" guide with "Development OS" selected as your machine's OS and "Target OS" as iOS(macOS only) or Android(macOS and Windows). Complete all the steps until "Creating a new application" section.
2. Clone this repository.
3. `cd se-wasted-app/frontend/wasted-app` to navigate to frontend source directory.
4. `npm install` to install the dependencies to your local `node_modules` directory.
   1. FOR IOS DEVELOPMENT: `cd ios && pod install && cd ..`
5. `npm run android` for Android development / `npm run ios` for iOS development to build the app and automatically open the Metro bundler.
   <br>OR
6. `npm run start` to open Metro server, which bundles the application and allows you to run the app on on your physical/emulated device. Keep in mind that you need to have the development app already installed on the device for the bundler to work. (with this command you can also run the app on your physical device)

## Backend

# Others

# Demo
