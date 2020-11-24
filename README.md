# Secret Santa

## Cloning Project via command line:
In the command line, navigate to the location where you want to download the project.
Then run 
```console
git clone https://github.com/mokxshi/secret-santa
```

## Importing the project in Unity
In Unity Hub, click Add, then select the secret-santa folder and import it. Select Unity Version 2019.4.14f1. Then you can open the project by double clicking on it.

## Updating the git repo
Before updating, use 
```console
git pull
```
in order to download changes from the remote repository.

After making changes to the project locally you can upload them using
```console
git add <filename>
```
or
```console
git add .
```
to add all files.

Then, commit the changes using
```console
git commit -m "commit message"
```
and push your commit to the repository using 
```console
git push
```

## Branches
You can switch branches using
```console
git checkout <branchName>
```
and create a new branch to upload your work in progress with
```console
git checkout -b <branchName>
```