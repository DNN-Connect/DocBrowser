# Connect DocBrowser module

## Installing source version

1. Create or use an existing DNN installation on your dev machine
2. Install the latest \_Install.zip file from the releases - this will run the SQL scripts necessary
3. Go to DesktopModules/MVC/Connect
4. Delete the DocBrowser folder
5. Git clone https://github.com/DNN-Connect/DocBrowser.git here (i.e. DesktopModules/MVC/Connect)
6. That should put the DocBrowser folder back
7. Switch to that folder and run yarn to restore all packages

### Developing

I use VS Code for the front-end and VS Community for all cs stuff.

#### Front end: JS

1. Fire up VS code in the module folder
2. Start a console and type "npm run watch". This will build and watch for any file changes.

#### Front end: CSS

In a console type "gulp css" and it should build the less files and compile that to module.css

## Loading Content

1. Instantiate the module
2. Make sure you've built the React script (if you use the source version) using npm run build or npm run watch
3. If logged in as admin you should see a "Reset" button
4. Click that - nothing will happen
5. Go to Portals/\[portalid\]/Docs
6. You'll see a folder that is a number. This number is the module nr of your instantiated module
7. Git clone https://github.com/DNNCommunity/dnn-docs.git
8. Rename the dnn-docs folder to the module id (i.e. you need to replace the old moduleid folder with the repo)
9. Click the reset button on the module again - now it should be working. It may take a few minutes to load everything.
10. Reload page and start playing
